using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using CarGame.Scripts.Core;

namespace CarGame.Scripts.UI
{
    public class LevelCompletedPanel : MonoBehaviour
    {
        [SerializeField] private Button NextLevelButton;
        private void Start()
        {
            NextLevelButton.onClick.AddListener(() => LoadNextLevel());
        }
        /// <summary>
        /// Loads Next Level if there exist a next level, loads same level otherwise.
        /// </summary>
        private void LoadNextLevel()
        {
            int currentsceneindex = SceneManager.GetActiveScene().buildIndex;
            if (currentsceneindex < SceneManager.sceneCountInBuildSettings - 1)
            {
                currentsceneindex++;
                SceneManager.LoadSceneAsync(currentsceneindex);

            }
            else
            {
                LevelManager.THIS.RestartLevel();
            }

        }
    }

}
