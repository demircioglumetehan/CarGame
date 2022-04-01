using UnityEngine;
using CarGame.Scripts.Core;
namespace CarGame.Scripts.UI
{
    public class MenuReferences : MonoBehaviour
    {
        public static MenuReferences THIS;
        private void Awake()
        {
            if (THIS != null)
            {
                Destroy(this.gameObject);

                return;
            }
            else
            {
                THIS = this;
                DontDestroyOnLoad(this.gameObject);
            }
            SetPanelsForNewLevel();

        }
        [SerializeField] private GameObject LevelCompletedPanel;
        [SerializeField] private GameObject PreGamePanel;
        [SerializeField] private GameObject InGamePanel;
        private void OnEnable()
        {
            LevelManager.OnWinState += OnWinState;
            LevelManager.OnLevelChanged += SetPanelsForNewLevel;
            LevelManager.OnPlayState += OnPlayState;
            LevelManager.OnCarCompleteState += RestartPanels;
            LevelManager.OnCarFailedState += RestartPanels;
        }
        private void OnDisable()
        {
            LevelManager.OnWinState -= OnWinState;
            LevelManager.OnLevelChanged -= SetPanelsForNewLevel;
            LevelManager.OnPlayState -= OnPlayState;
            LevelManager.OnCarCompleteState -= RestartPanels;
            LevelManager.OnCarFailedState -= RestartPanels;
        }

        private void SetPanelsForNewLevel(int level = 1)
        {
            if (LevelCompletedPanel.activeSelf)
            {
                LevelCompletedPanel.SetActive(false);
            }
            if (!PreGamePanel.activeSelf)
            {
                PreGamePanel.SetActive(true);
            }
            if (InGamePanel.activeSelf)
            {
                InGamePanel.SetActive(false);
            }
            LevelManager.THIS.GameStatus = GameState.Preplay;
        }

        private void OnPlayState()
        {
            PreGamePanel.SetActive(false);
            InGamePanel.SetActive(true);
        }
        private void RestartPanels()
        {
            PreGamePanel.SetActive(true);
            InGamePanel.SetActive(false);
        }
        private void OnWinState()
        {
            
            LevelCompletedPanel.SetActive(true);
            
            InGamePanel.SetActive(false);


            PreGamePanel.SetActive(false);
        }

    }

}
