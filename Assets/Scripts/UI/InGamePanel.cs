using UnityEngine;
using TMPro;

namespace CarGame.Scripts.UI
{
    public class InGamePanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _leveltext;
        private void OnEnable()
        {
            
            UpdateLevelText();
        }
        public void UpdateLevelText()
        {
            _leveltext.text = "Level " + PlayerPrefs.GetInt("currentlevel").ToString();
        }

    }

}
