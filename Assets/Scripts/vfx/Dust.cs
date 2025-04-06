using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using upgrades;

namespace vfx
{
    public class Dust : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;

        private CancellationTokenSource _cancellationTokenSource;

        public void Play(Vector3 position)
        {
            gameObject.SetActive(true);
            _particleSystem.Stop();
            transform.position = position + Vector3.down * UpgradeService.Instance.GetValue(UpgradeType.Radius);
            _particleSystem.Play();
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();
            DelayRelease().Forget();
        }

        private async UniTaskVoid DelayRelease()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_particleSystem.main.duration), cancellationToken: _cancellationTokenSource.Token);
            gameObject.SetActive(false);
            //Release();
        }
    }
}