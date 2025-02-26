using UnityEngine;

namespace Entities
{
    public class Region : MonoBehaviour
    {
        public bool isHighlighted = false;
        public bool isOccupied = false;
        private SpriteRenderer _spriteRenderer;
        private Color _defaultColor;
        private static Color _highlightColor = Color.yellow;

        void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _defaultColor = _spriteRenderer.color;
        }

        public void Highlight()
        {
            if (!isOccupied)
            {
                isHighlighted = true;
                _spriteRenderer.color = _highlightColor;
                Invoke(nameof(RemoveHighlight), 5f);
            }
        }

        private void RemoveHighlight()
        {
            isHighlighted = false;
            _spriteRenderer.color = _defaultColor;
        }
    }
}