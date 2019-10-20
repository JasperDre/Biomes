using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Visualization
{
    public class LivingEntityTracker : MonoBehaviour
    {
        [SerializeField] private Text myText;
        private string myName;
        private string mySpecimen;
        private int myIndex;
        private Transform myTrackedLivingEntity;
        private Button myButton;
        private CameraController myCameraController;

        private void Awake()
        {
            myCameraController = FindObjectOfType<CameraController>();
            myButton = GetComponentInChildren<Button>();
        }
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
            myText.text = myName;
        }

        public void UpdateButton()
        {
            myButton.onClick.AddListener(() => FocusTrackedLivingEntity());
        }

        private void FocusTrackedLivingEntity()
        {
            myCameraController.StartFollowing(myTrackedLivingEntity);
        }

        public string GetSpecimen()
        {
            return mySpecimen;
        }
    }
}