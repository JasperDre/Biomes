using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Simulation : MonoBehaviour
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
        if (myLoadingBar.value < 1.0f)
        {
            myLoadingBar.value += Time.deltaTime;
        }
        else
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
            if (myState == State.Paused)
            {
                Time.timeScale = 1.0f;
            }
            else if (myState == State.Simulating)
            {
                Time.timeScale = 0;
            }

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
    }

    private void SwitchState()
    {
        switch (myState)
        {
            case State.None:
                myState = State.Menu;
                myMenuPanel.SetActive(true);
                myLoadPanel.SetActive(false);
                myPausePanel.SetActive(false);
                myScreenPanel.SetActive(false);
                break;
            case State.Menu:
                SwitchToLoadingState();
                break;
            case State.Loading:
                myState = State.Simulating;
                myScreenPanel.SetActive(true);
                myLoadPanel.SetActive(false);
                myPausePanel.SetActive(false);
                myMenuPanel.SetActive(false);
                break;
            case State.Simulating:
                myState = State.Paused;
                myPausePanel.SetActive(true);
                myMenuPanel.SetActive(false);
                myScreenPanel.SetActive(false);
                myLoadPanel.SetActive(false);
                break;
            case State.Paused:
                myState = State.Simulating;
                myScreenPanel.SetActive(true);
                myMenuPanel.SetActive(false);
                myLoadPanel.SetActive(false);
                myPausePanel.SetActive(false);
                break;
            default:
                Debug.LogWarning("State is undefined");
                break;
        }
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
