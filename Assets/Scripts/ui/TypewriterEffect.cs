using System.Collections;
using TMPro;
using UnityEngine;

namespace ui
{
    public class TypewriterEffect : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private float _delayBetweenChars = 0.1f;

        private string _fullText;

        private Coroutine _coroutine;
        
        public void ShowText(string text)
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }
            _fullText = text;
            _text.text = "";
            gameObject.SetActive(true);
            _coroutine =  StartCoroutine(ShowTextCoroutine());
        }

        private IEnumerator ShowTextCoroutine()
        {
            for (int i = 0; i <= _fullText.Length; i++)
            {
                _text.text = _fullText.Substring(0, i);
                yield return new WaitForSeconds(_delayBetweenChars);
            }
            yield return new WaitForSeconds(2f);
            gameObject.SetActive(false);
        }
    }
}