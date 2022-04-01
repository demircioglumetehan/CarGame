using UnityEngine;
using CarGame.Scripts.Core;

namespace CarGame.Scripts.CarComponents
{
    public class Car : MonoBehaviour
    {
        public static Car currentCar;
        private CarPoint initialpoint;
        public CarGameplayRecord Gameplayrecord { get; private set; }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("finishpoint"))
            {
                if (currentCar == this)
                {
                    LevelManager.THIS.GameStatus = GameState.CarComplete;
                }

            }
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("obstacle") || collision.gameObject.CompareTag("Player"))
            {
                if (currentCar == this)
                {
                    Gameplayrecord.RestartRecord();
                    LevelManager.THIS.GameStatus = GameState.CarFailed;
                }

            }

        }
        #region ActionListeners
        private void OnEnable()
        {
            LevelManager.OnCarCompleteState += ResetToinitialPosition;
            LevelManager.OnCarFailedState += ResetToinitialPosition;
        }
        private void OnDisable()
        {
            LevelManager.OnCarCompleteState -= ResetToinitialPosition;
            LevelManager.OnCarFailedState -= ResetToinitialPosition;
        }
        #endregion
        private void Start()
        {
            Gameplayrecord = GetComponent<CarGameplayRecord>();
        }

        public void OnSpawn(Transform spawnigpoint)
        {
            transform.position = spawnigpoint.position;
            transform.rotation = spawnigpoint.rotation;
            initialpoint = new CarPoint(transform.position, transform.rotation);
        }
        /// <summary>
        /// Resets the car position to its initial position
        /// </summary>
        void ResetToinitialPosition()
        {
            if (initialpoint != null)
            {
                transform.position = initialpoint.CarPosition;
                transform.rotation = initialpoint.CarRotation;
            }
        }


    }

}
