using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Visualization
{
    public class LivingEntityTracker : MonoBehaviour
    {
        private string myName;
        private string mySpecimen;
        private int myIndex;
        private Transform myTrackedLivingEntity;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetupTracker(string aSpecimen, int anIndex, Transform aTransform)
        {
            mySpecimen = aSpecimen;
            myIndex = anIndex;
            myTrackedLivingEntity = aTransform;
            myName = aSpecimen + anIndex;
            transform.Find("Text_name").gameObject.GetComponent<Text>().text = myName;
        }

        public string GetSpecimen()
        {
            return mySpecimen;
        }
    }
}