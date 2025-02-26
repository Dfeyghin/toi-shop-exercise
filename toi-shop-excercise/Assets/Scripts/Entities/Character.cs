using Systems;
using System.Collections;
using UnityEngine;

namespace Entities
{
    public class Character : MonoBehaviour
    {

        public Region CurrentRegion { get; set; }

        private bool _isMoving = false;
        public void AssignRegion(Region region)
        {
            CurrentRegion = region;
            CurrentRegion.IsOccupied = true;
            CurrentRegion.OccupyingCharacter = this;
        }

        public void Select()
        {
            Debug.Log("Character selected.");
        }

        public void MoveToRegion(Region newRegion)
        {
            if (_isMoving || newRegion.IsOccupied || !newRegion.IsAdjacentTo(CurrentRegion)) return;

            StartCoroutine(MoveCharacter(newRegion));
        }

        private IEnumerator MoveCharacter(Region targetRegion)
        {
            _isMoving = true;

            Vector3 startPosition = transform.position;
            Vector3 endPosition = targetRegion.transform.position;
            float duration = GameLoopManager.Instance.constants.Timing.characterMovementDuration;
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.position = endPosition;

            // Update region occupancy
            CurrentRegion.IsOccupied = false;
            CurrentRegion.OccupyingCharacter = null;
            CurrentRegion = targetRegion;
            CurrentRegion.IsOccupied = true;
            CurrentRegion.OccupyingCharacter = this;

            if (targetRegion.isHighlighted)
            {
                GameLoopManager.Instance.AddScore();
                targetRegion.RemoveHighlight(); 
            }

            _isMoving = false;
        }
    }
}