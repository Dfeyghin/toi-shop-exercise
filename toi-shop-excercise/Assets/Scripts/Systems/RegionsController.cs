using System.Collections;
using System.Collections.Generic;
using Entities;
using UnityEngine;

namespace Systems
{
    public class RegionsController : MonoBehaviour
    {
        //singleton pattern:
        public static RegionsController Instance { get; private set; }


        [SerializeField] private float highlightEveryXSeconds = 5f;
        [SerializeField] private List<Region> regions;
        public List<Region> Regions => regions;

        private Region _lastRegionHighlighted;
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
        void Start()
        {
            StartCoroutine(HighlightRegions());
        }

        IEnumerator HighlightRegions()
        {
            while (true)
            {
                //highlight another region before removing the old highlight for the purpose of not selecting twice the same region
                var newRegion = HighlightRandomRegion();
                _lastRegionHighlighted?.RemoveHighlight();
                _lastRegionHighlighted = newRegion;
                yield return new WaitForSeconds(highlightEveryXSeconds);
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