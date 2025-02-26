using System.Collections;
using System.Collections.Generic;
using Entities;
using UnityEngine;
using UnityEngine.Serialization;

namespace Systems
{
    public class RegionsController : MonoBehaviour
    {
        //singleton pattern:
        public static RegionsController Instance { get; private set; }
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject); // Ensure only one instance exists
            }
        }
        
        [SerializeField] private List<Region> regions;
        public List<Region> Regions => regions;
        
        private Region _lastRegionHighlighted;

        
        void Start()
        {
            StartCoroutine(HighlightRegions());
        }

        IEnumerator HighlightRegions()
        {
            //wait for a while before first highlight:
            yield return new WaitForSeconds(GameLoopManager.Instance.constants.Timing.timeWithNoHighlightWhenGameStarts);
            while (!GameLoopManager.Instance.isGameOver)
            {
                var newRegion = HighlightRandomRegion();
                // _lastRegionHighlighted?.RemoveHighlight();
                _lastRegionHighlighted = newRegion;
                yield return new WaitForSeconds(GameLoopManager.Instance.constants.Timing.timeBetweenHighlights);
            }
        }

        Region HighlightRandomRegion()
        {
            List<Region> availableRegions = regions.FindAll(r => !r.IsOccupied && !r.isHighlighted);
            if (availableRegions.Count <= 0) return null;
            int randomIndex = Random.Range(0, availableRegions.Count);
            Region selectedRegion = availableRegions[randomIndex]; 
            selectedRegion.Highlight();
            return selectedRegion;
        }
    }
}