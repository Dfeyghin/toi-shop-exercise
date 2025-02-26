using UnityEngine;

[CreateAssetMenu(fileName = "GameConstants", menuName = "Constants/Game Constants")]
public class Constants : ScriptableObject
{
    [System.Serializable]
    public class TimingConstants
    {
        [Range(30f, 180f)] public float gameDuration = 60f;
        [Range(1f, 3f)] public float highlightDuration = 1.75f;
        [Range(0.3f, 1f)] public float characterMovementDuration = 0.5f;
        [Range(2f, 5f)] public float timeBetweenHighlights = 3f;
        [Range(0f, 5f)] public float timeWithNoHighlightWhenGameStarts = 2f;
    }

    public TimingConstants Timing;
}