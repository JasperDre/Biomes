using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    [SerializeField] private Slider myHungerSlider;
    [SerializeField] private Slider myThirstSlider;

    private Animal myAnimal;

    private void Awake()
    {
        myAnimal = GetComponent<Animal>();
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
    }
}
