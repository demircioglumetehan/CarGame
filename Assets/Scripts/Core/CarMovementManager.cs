using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CarGame.Scripts.CarComponents;

namespace CarGame.Scripts.Core
{
    public class CarMovementManager : MonoBehaviour
    {
        [SerializeField, Range(0f, 10f)] private float _rotationspeed;
        [SerializeField, Range(0f, 100f)] private float _movingspeed;

        //the coroutine which turns the current car
        Coroutine turningcoroutine;

        #region ActionListeners
        private void OnEnable()
        {
            InputManager.OnPressScreen += OnPressScreen;
            InputManager.OnReleaseScreen += ResetTurning;
            LevelManager.OnCarCompleteState += ResetTurning;
            LevelManager.OnCarFailedState += ResetTurning;

        }
        private void OnDisable()
        {
            InputManager.OnPressScreen -= OnPressScreen;
            InputManager.OnReleaseScreen -= ResetTurning;
            LevelManager.OnCarCompleteState -= ResetTurning;
            LevelManager.OnCarFailedState -= ResetTurning;
        }

        private void ResetTurning()
        {
            if (turningcoroutine != null)
                StopCoroutine(turningcoroutine);
        }
        private void OnPressScreen(bool pressedleft)
        {
            if (turningcoroutine != null)
                StopCoroutine(turningcoroutine);
            turningcoroutine = StartCoroutine(TurnTheCarCor(pressedleft));

        }
        #endregion

        private void FixedUpdate()
        {
            MoveForward();
        }

        /// <summary>
        /// Turns the current car according to its input
        /// </summary>
        /// <param name="pressedleft">This should be true if the car should turn left, it turns right otherwise.</param>
        private IEnumerator TurnTheCarCor(bool pressedleft)
        {
            while (true)
            {
                yield return null;
                Car.currentCar.transform.RotateAround(Car.currentCar.transform.position, Vector3.forward, (pressedleft ? 1 : -1) * _rotationspeed);
            }
        }


        /// <summary>
        /// Moves the current car towards its rotation
        /// </summary>
        private void MoveForward()
        {
            if (LevelManager.THIS.GameStatus != GameState.Play)
                return;

            Car.currentCar.transform.position += Car.currentCar.transform.up * Time.deltaTime * _movingspeed;
            Car.currentCar.Gameplayrecord.SavePoint();
        }
        /// <summary>
        /// This function sets the variables of the level from Custom editor window
        /// </summary>
        /// <param name="carvelocity"> Velocity of the cars</param>
        /// <param name="carrotationspeed"> Turn speed of the cars</param>
        public void SetRotationSpeedAndMovingSpeedFromInspector(float carvelocity, float carrotationspeed)
        {
            _movingspeed = carvelocity;
            _rotationspeed = carrotationspeed;
        }
    }
}

