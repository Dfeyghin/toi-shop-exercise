using Systems;
using UnityEngine;

namespace Entities
{
    public class Region : MonoBehaviour
    {
        public bool isHighlighted = false;
        public bool IsOccupied { get; set; } = false;
        public Character OccupyingCharacter { get; set; }

        private SpriteRenderer _spriteRenderer;
        
        [SerializeField] private Sprite highlightSprite;
        [SerializeField] private Sprite normalSprite;
        private float _adjacencyThreshold;
        void Start()
        {
            float regionWidth = transform.localScale.x;
            float regionHeight = transform.localScale.y;

            // The maximum distance for adjacency is the diagonal of a single region
            _adjacencyThreshold = Mathf.Sqrt(regionWidth * regionWidth + regionHeight * regionHeight);
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Highlight()
        {
            if (IsOccupied || isHighlighted) return;
            isHighlighted = true;
            _spriteRenderer.sprite = highlightSprite;
            Invoke(nameof(RemoveHighlight), GameLoopManager.Instance.constants.Timing.highlightDuration);
        }

        public void RemoveHighlight()
        {
            isHighlighted = false;
            _spriteRenderer.sprite = normalSprite;
        }

        void OnMouseDown()
        {
            GameLoopManager.Instance.OnRegionClicked(this);
        }

        public bool IsAdjacentTo(Region otherRegion)
        {
            return Vector2.Distance(transform.position, otherRegion.transform.position) < _adjacencyThreshold;
        }
    }
}