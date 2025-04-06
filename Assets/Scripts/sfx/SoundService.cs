using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace sfx
{
    public class SoundService
    {
        private readonly ObjectPool _audioPool;
        private readonly Dictionary<SoundType, SoundData> _sounds;

        public static SoundService Instance;
        
        public SoundService()
        {
            Instance = this;

            _sounds = Resources.LoadAll<SoundData>("Sounds/").ToDictionary(s => s.SoundType, s => s);
            _audioPool = new ObjectPool(Resources.Load<AudioObject>("Prefabs/AudioObject"), 5);
            
            PlaySound(SoundType.Music);
        }

        public void PlaySound(SoundType soundType)
        {
            _audioPool.GetObject<AudioObject>().Play(_sounds[soundType]);
        }
    }
}