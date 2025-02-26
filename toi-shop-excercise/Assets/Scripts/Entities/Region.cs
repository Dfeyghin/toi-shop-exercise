using UnityEngine;
using UnityEngine.Serialization;

namespace Entities
{
    public class Region : MonoBehaviour
    {
        public bool isHighlighted = false;
        public bool isOccupied = false;
        private SpriteRenderer _spriteRenderer;
        // private Color _defaultColor;
        // private static Color _highlightColor = Color.yellow;
        [SerializeField] private Sprite highlightSprite;
        [SerializeField] private Sprite normalSprite;

        void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            // _defaultColor = _spriteRenderer.color;
        }

        public void Highlight()
        {
            if (isOccupied || isHighlighted) return;
            isHighlighted = true;
            // _spriteRenderer.color = _highlightColor;
            _spriteRenderer.sprite = highlightSprite;
            Invoke(nameof(RemoveHighlight), 5f);
        }

        private void RemoveHighlight()
        {
            isHighlighted = false;
            _spriteRenderer.sprite = normalSprite;
        }
    }
}