using UnityEngine;

namespace sfx
{
    [CreateAssetMenu(fileName = "SoundData", menuName = "Data/SoundData")]
    public class SoundData : ScriptableObject
    {
        public SoundType SoundType;
        public AudioClip AudioClip;

        [Range(0, 1)]
        public float Volume = 1;

        public Vector2 Pitch;
    }
}