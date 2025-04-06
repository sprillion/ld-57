using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using level;
using ui;
using UnityEngine;

namespace dialog
{
    public class DialogService : MonoBehaviour
    {
        public static DialogService Instance;

        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private TypewriterEffect _text;

        private Dictionary<DialogType, DialogData> _dialogMap;

        private bool _played;

        public void Initialize()
        {
            Instance = this;

            _dialogMap = Resources.LoadAll<DialogData>("Dialogs").ToDictionary(d => d.DialogType, d => d);

            LevelService.Instance.OnLevelComplete += () => PlayDialog(DialogType.GoodDay);
            LevelService.Instance.OnLevelStart += OnLevelStart;
            LevelService.Instance.OnFinish += () => PlayDialog(DialogType.Finish);
        }

        public void PlayDialog(DialogType dialogType)
        {
            if (_played) return;
            _played = true;
            _text.ShowText(_dialogMap[dialogType].Text);
            _audioSource.clip = _dialogMap[dialogType].Voice;
            _audioSource.gameObject.SetActive(true);
            _audioSource.Play();
            DisableVoiceWithDelay(_audioSource.clip.length).Forget();
        }

        private async UniTaskVoid DisableVoiceWithDelay(float delay)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(delay));
            _audioSource.Stop();
            _audioSource.gameObject.SetActive(false);
            _played = false;
        }

        private void OnLevelStart()
        {
            DialogType dialogType = LevelService.Instance.CurrentLevelNumber switch
            {
                0 => DialogType.Level1,
                1 => DialogType.Level2,
                2 => DialogType.Level3,
                3 => DialogType.Level4,
                _ => DialogType.Level1
            };
            
            PlayDialog(dialogType);
        }
    }
}