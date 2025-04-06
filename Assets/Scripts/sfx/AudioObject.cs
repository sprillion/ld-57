using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace sfx
{
    public class AudioObject : PooledObject
    {
        [SerializeField] private AudioSource _audioSource;


        public void Play(SoundData soundData)
        {
            _audioSource.clip = soundData.AudioClip;
            _audioSource.volume = soundData.Volume;
            _audioSource.pitch = Random.Range(soundData.Pitch.x, soundData.Pitch.y);
            
            gameObject.SetActive(true);
            _audioSource.Play();
            
            DelayRelease().Forget();
        }

        private async UniTaskVoid DelayRelease()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_audioSource.clip.length));
            Release();
        }
    }
}