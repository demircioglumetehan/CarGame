using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CarGame.Scripts.Core;
namespace CarGame.Scripts.UI
{
    public class PreGamePanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _leveltext;
        [SerializeField] private Button restartButton;
        private void OnEnable()
        {
            LevelManager.OnPrePlayState += UpdateLevelText;
            LevelManager.OnOpenGame += UpdateLevelText;
        }
        private void OnDisable()
        {
            LevelManager.OnPrePlayState -= UpdateLevelText;
            LevelManager.OnOpenGame -= UpdateLevelText;

        }
        private void Start()
        {
            restartButton.onClick.AddListener(() => PlayAgain());
        }
        public void UpdateLevelText()
        {
            _leveltext.text = "Level " + PlayerPrefs.GetInt("currentlevel").ToString();
        }
        void PlayAgain()
        {
            LevelManager.THIS.RestartLevel();
        }
        public void StartGame()
        {
            LevelManager.THIS.StartGame();
        }
    }

}
