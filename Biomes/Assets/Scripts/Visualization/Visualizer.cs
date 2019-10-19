using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Visualization
{
    public class Visualizer : MonoBehaviour
    {
        [SerializeField] private ScrollRect myScrollView;
        [SerializeField] private GameObject myScrollContent;
        [SerializeField] private LivingEntityTracker myLivingEntityTrackerPrefab;

        private List<LivingEntityTracker> myLivingEntityTrackers;
        private int trackerCount;

        private void Awake()
        {
            myLivingEntityTrackers = new List<LivingEntityTracker>();
            myScrollView.verticalNormalizedPosition = 1.0f;
            trackerCount = 0;
        }

        // Start is called before the first frame update
        void Start()
        {
           
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SpawnTrackerDynamically(Transform aTransform, string aName)
        {
            LivingEntityTracker livingEntityTracker = Instantiate(myLivingEntityTrackerPrefab);
            livingEntityTracker.transform.SetParent(myScrollContent.transform, false);
            livingEntityTracker.SetupTracker(aName, trackerCount, aTransform);
            myLivingEntityTrackers.Add(livingEntityTracker);
            livingEntityTracker.gameObject.SetActive(false);
            trackerCount++;
        }

        public void ToggleFilters(string aBiodiversity)
        {
            foreach (var livingEntityTracker in myLivingEntityTrackers)
            {
                if (livingEntityTracker.GetSpecimen() == aBiodiversity)
                {
                    livingEntityTracker.gameObject.SetActive(true);
                    livingEntityTracker.UpdateButton();
                }
                else if (livingEntityTracker.gameObject.activeInHierarchy)
                {
                    livingEntityTracker.gameObject.SetActive(false);
                }
            }
        }
    }
}