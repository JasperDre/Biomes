using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class Simulation : MonoBehaviour
{
    public enum State
    {
        None = 0,
        Menu = 1,
        Loading = 2,
        Simulating = 3,
        Paused = 4,
    }

    [Header("Panels")]
    [SerializeField] private GameObject myMenuPanel;
    [SerializeField] private GameObject myLoadPanel;
    [SerializeField] private GameObject myPausePanel;
    [SerializeField] private GameObject myScreenPanel;

    [Header("Specifics")]
    [SerializeField] private Slider myLoadingBar;


    [Header("Data")]
    [SerializeField] private State myState;

    private Environment myEnvironment;

    private static Simulation instance = null;
    private static readonly object padlock = new Object();

    public static Simulation Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new Simulation();
                }
                return instance;
            }
        }
    }

    private void Awake()
    {
        myEnvironment = FindObjectOfType<Environment>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SwitchState();
    }

    // Update is called once per frame
    void Update()
    {
        switch (myState)
        {
            case State.None:
                break;
            case State.Menu:
                UpdateMenuState();
                break;
            case State.Loading:
                UpdateLoadingState();
                break;
            case State.Simulating:
                UpdateSimulatingState();
                break;
            case State.Paused:
                UpdatePausedState();
                break;
            default:
                Debug.LogWarning("State is undefined");
                break;
        }
    }

    private void UpdateMenuState()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            SwitchState();
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void UpdateLoadingState()
    {
        if (myLoadingBar.value >= 1.0f)
        {
            myLoadingBar.value = 1.0f;
            SwitchState();
        }
    }

    private void UpdateSimulatingState()
    {
        if (Input.GetKeyUp(KeyCode.Escape) || Input.GetKeyUp(KeyCode.Space))
        {
            SwitchState();
        }
    }

    private void UpdatePausedState()
    {
        if (Input.GetKeyUp(KeyCode.Escape) || Input.GetKeyUp(KeyCode.Space))
        {
            SwitchState();
        }
    }

    private void SwitchToLoadingState()
    {
        myState = State.Loading;
        myLoadPanel.SetActive(true);
        myMenuPanel.SetActive(false);
        myPausePanel.SetActive(false);
        myScreenPanel.SetActive(false);

        myLoadingBar.value = 0.0f;

        myEnvironment.Grow();
    }

    private void SwitchToSimulatingState()
    {
        myState = State.Simulating;
        myScreenPanel.SetActive(true);
        myLoadPanel.SetActive(false);
        myPausePanel.SetActive(false);
        myMenuPanel.SetActive(false);

        Time.timeScale = 1.0f;
    }

    private void SwitchToPausedState()
    {
        myState = State.Paused;
        myPausePanel.SetActive(true);
        myMenuPanel.SetActive(false);
        myScreenPanel.SetActive(false);
        myLoadPanel.SetActive(false);

        Time.timeScale = 0.0f;
    }

    private void SwitchToMenuState()
    {
        myState = State.Menu;
        myMenuPanel.SetActive(true);
        myLoadPanel.SetActive(false);
        myPausePanel.SetActive(false);
        myScreenPanel.SetActive(false);
    }

    private void SwitchState()
    {
        switch (myState)
        {
            case State.None:
                SwitchToMenuState();
                break;
            case State.Menu:
                SwitchToLoadingState();
                break;
            case State.Loading:
                SwitchToSimulatingState();
                break;
            case State.Simulating:
                SwitchToPausedState();
                break;
            case State.Paused:
                SwitchToSimulatingState();
                break;
            default:
                Debug.LogWarning("State is undefined");
                break;
        }
    }

    public void Exit()
    {
        Application.Quit();
    }

    public bool isPaused()
    {
        return myState == State.Paused;
    }

    public bool isSimulating()
    {
        return myState == State.Simulating;
    }
}
