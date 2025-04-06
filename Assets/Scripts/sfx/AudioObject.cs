using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace sfx
{
    public class AudioObject : PooledObject
    {
        [SerializeField] private AudioSource _audioSource;


        public void Play(SoundData soundData)
        {
            _audioSource.clip = soundData.AudioClip;
            _audioSource.volume = soundData.Volume;
            _audioSource.loop = soundData.Loop;
            
            gameObject.SetActive(true);
            _audioSource.Play();
            
            if (soundData.Loop) return;
            DelayRelease().Forget();
        }

        private async UniTaskVoid DelayRelease()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_audioSource.clip.length));
            Release();
        }
    }
}