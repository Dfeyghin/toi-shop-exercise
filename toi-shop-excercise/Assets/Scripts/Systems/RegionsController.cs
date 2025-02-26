using System.Collections;
using System.Collections.Generic;
using Entities;
using UnityEngine;

namespace Systems
{
    public class RegionsController : MonoBehaviour
    {
        [SerializeField] private float highlightEveryXSeconds = 5f;
        [SerializeField] private List<Region> regions;

        void Start()
        {
            StartCoroutine(HighlightRegions());
        }

        IEnumerator HighlightRegions()
        {
            while (true)
            {
                yield return new WaitForSeconds(highlightEveryXSeconds);
                HighlightRandomRegion();
            }
        }

        void HighlightRandomRegion()
        {
            List<Region> availableRegions = regions.FindAll(r => !r.isOccupied);
            if (availableRegions.Count <= 0) return;
            int randomIndex = Random.Range(0, availableRegions.Count);
            availableRegions[randomIndex].Highlight();
        }
    }
}