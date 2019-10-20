using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    [SerializeField] private Slider myHungerSlider;
    [SerializeField] private Slider myThirstSlider;

    private Camera myCamera;
    private Animal myAnimal;
    private Transform myCanvasTransform;

    private void Awake()
    {
        myCamera = FindObjectOfType<Camera>();
        myAnimal = GetComponent<Animal>();
        myCanvasTransform = GetComponentInChildren<Canvas>().transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        myHungerSlider.value = myAnimal.hunger;
        myThirstSlider.value = myAnimal.thirst;

        if (Vector3.Distance(myCamera.transform.position, myCanvasTransform.position) < 10.0f)
        {
            myCanvasTransform.gameObject.SetActive(true);
            myCanvasTransform.rotation = Quaternion.LookRotation(myCamera.transform.forward);
        }
        else
        {
            myCanvasTransform.gameObject.SetActive(false);
        }
    }
}
