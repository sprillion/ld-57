using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace ui
{
    public class Settings : MonoBehaviour
    {
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _sfxSlider;

        [SerializeField] private AudioMixer _audioMixer;

        private void Start()
        {
            _musicSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
            _sfxSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
        }
        
        public void Toggle()
        {
            if (!Boot.HaveControl && !gameObject.activeSelf) return;
            
            gameObject.SetActive(!gameObject.activeSelf);
            
            Cursor.lockState = gameObject.activeSelf ? CursorLockMode.None : CursorLockMode.Locked;
            
            Boot.HaveControl = !gameObject.activeSelf;
        }
        
        private void OnMusicVolumeChanged(float value)
        {
            _audioMixer.SetFloat("MusicVolume", Mathf.Log10(value) * 20);
        }
        
        private void OnSFXVolumeChanged(float value)
        {
            _audioMixer.SetFloat("SFXVolume", Mathf.Log10(value) * 20);
        }
    }
}