using UnityEngine;
using System;
using UnityEngine.SceneManagement;

namespace CarGame.Scripts.Core
{
    public enum GameState
    {
        Preplay,
        CarComplete,
        CarFailed,
        Play,
        PreLose,
        Lose,
        Win

    }
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager THIS;
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
                //LoadCurrentLevel();
            }
            //LoadCurrentLevel();
            GameStatus = GameState.Preplay;
        }
        #region LevelManagerGameStateActions
        public static Action OnPrePlayState;
        public static Action OnPlayState;
        public static Action OnCarCompleteState;
        public static Action OnCarFailedState;
        public static Action OnLoseState;
        public static Action OnWinState;
        public static Action OnOpenGame;
        public static Action<int> OnLevelChanged;
        #endregion
        private GameState gameStatus;
        public GameState GameStatus
        {
            get { return gameStatus; }
            set
            {
                if (value == gameStatus)
                    return;
                gameStatus = value;
                switch (value)
                {
                    case GameState.Preplay:
                        OnPrePlayState?.Invoke();
                        break;
                    case GameState.Play:
                        OnPlayState?.Invoke();
                        break;
                    case GameState.CarComplete:
                        OnCarCompleteState?.Invoke();
                        break;
                    case GameState.CarFailed:
                        OnCarFailedState?.Invoke();
                        break;
                    case GameState.Lose:
                        OnLoseState?.Invoke();
                        break;
                    case GameState.Win:
                        OnWinState?.Invoke();
                        break;
                }

            }
        }
        public int currentlevel { get; private set; }
        private void OnEnable()
        {
            UnityEngine.SceneManagement.SceneManager.activeSceneChanged += ActiveSceneChanged;
        }
        private void Start()
        {
            //Calls once at the beginnig of the opening of the game.
            LoadCurrentLevel();
        }
        private void ActiveSceneChanged(Scene arg0, Scene arg1)
        {
            currentlevel = arg1.buildIndex + 1;
            if (currentlevel == 1)//this is necessary since at the launch always scene 1 is loaded.
                return;
            PlayerPrefs.SetInt("currentlevel", currentlevel);
            OnLevelChanged?.Invoke(currentlevel);
        }
        private void OnDisable()
        {
            UnityEngine.SceneManagement.SceneManager.activeSceneChanged -= ActiveSceneChanged;
        }

        /// <summary>
        /// Initializes the game for first launch
        /// </summary>
        private void InitializeGame()
        {
            PlayerPrefs.SetInt("Initialized", 1);
            currentlevel = 1;
            PlayerPrefs.SetInt("currentlevel", currentlevel);
            SceneManager.LoadSceneAsync(currentlevel - 1, LoadSceneMode.Single);
           
        }

        /// <summary>
        /// Loads current level which has been reached before.
        /// </summary>
        private void LoadCurrentLevel()
        {
            if (!PlayerPrefs.HasKey("Initialized"))
            {
                InitializeGame();
            }
            else
            {
                currentlevel = PlayerPrefs.GetInt("currentlevel");
                if (SceneManager.GetActiveScene() != SceneManager.GetSceneByBuildIndex(currentlevel - 1))
                    SceneManager.LoadSceneAsync(currentlevel - 1, LoadSceneMode.Single);
            }
            OnOpenGame?.Invoke();
        }

        /// <summary>
        /// Restarts level for UI button
        /// </summary>
        public void RestartLevel()
        {
            SceneManager.LoadSceneAsync(currentlevel - 1, LoadSceneMode.Single);
        }

        /// <summary>
        /// Starts time for each submission.
        /// </summary>
        public void StartGame()
        {
            GameStatus = GameState.Play;
        }
    }

}
