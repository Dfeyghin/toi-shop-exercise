using UnityEngine;
using UnityEngine.Serialization;

namespace Entities
{
    public class Region : MonoBehaviour
    {
        public bool isHighlighted = false;
        public bool IsOccupied { get; set; } = false;
        private SpriteRenderer _spriteRenderer;
        
        [SerializeField] private Sprite highlightSprite;
        [SerializeField] private Sprite normalSprite;

        void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            
        }

        public void Highlight()
        {
            if (IsOccupied || isHighlighted) return;
            isHighlighted = true;
            _spriteRenderer.sprite = highlightSprite;
        }

        public void RemoveHighlight()
        {
            isHighlighted = false;
            _spriteRenderer.sprite = normalSprite;
        }
    }
}