using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Visualization
{
    public class LivingEntityTracker : MonoBehaviour
    {
        [SerializeField] private GameObject myButtonPrefab;
        private string myName;
        private string mySpecimen;
        private int myIndex;
        private Transform myTrackedLivingEntity;
        private GameObject myButton;
        private CameraController myCameraController;

        private void Awake()
        {
            myCameraController = Camera.main.GetComponent<CameraController>();
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
            transform.Find("Text_name").gameObject.GetComponent<Text>().text = myName;
        }

        public void UpdateButton()
        {
            myButton = Instantiate(myButtonPrefab);
            myButton.GetComponent<RectTransform>().SetParent(transform, false);
            myButton.GetComponent<Button>().onClick.AddListener(() => FocusTrackedLivingEntity());
            myButton.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
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