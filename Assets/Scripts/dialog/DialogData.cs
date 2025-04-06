using UnityEngine;

namespace dialog
{
    [CreateAssetMenu(fileName = "DialogData", menuName = "Data/DialogData")]
    public class DialogData : ScriptableObject
    {
        public DialogType DialogType;
        [TextArea]
        public string Text;
        public AudioClip Voice;
    }
}