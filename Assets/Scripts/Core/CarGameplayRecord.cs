using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CarGame.Scripts.Core;

namespace CarGame.Scripts.CarComponents
{
    [System.Serializable]
    public class CarPoint
    {
        public Vector3 CarPosition { get; private set; }
        public Quaternion CarRotation { get; private set; }
        public CarPoint(Vector3 position, Quaternion rotation)
        {
            CarPosition = position;
            CarRotation = rotation;
        }
    }
    public class CarGameplayRecord : MonoBehaviour
    {
        public List<CarPoint> points;
        Coroutine ReplayCoroutine;
        private Car _thisCar;

        #region ActionListeners
        private void OnEnable()
        {
            LevelManager.OnPlayState += OnPlayState;
            LevelManager.OnCarCompleteState += StopReplay;
            LevelManager.OnCarFailedState += StopReplay;
        }
        private void OnDisable()
        {
            LevelManager.OnPlayState -= OnPlayState;
            LevelManager.OnCarCompleteState -= StopReplay;
            LevelManager.OnCarFailedState -= StopReplay;

        }

        private void OnPlayState()
        {
            if (Car.currentCar == _thisCar)
                return;
            StartPreviousPlaySimulation();
        }
        private void StopReplay()
        {
            if (ReplayCoroutine != null)
                StopCoroutine(ReplayCoroutine);

        }
        #endregion

        private void Start()
        {
            _thisCar = GetComponent<Car>();
        }
        /// <summary>
        /// Saves the current position and rotation of the current car
        /// </summary>
        public void SavePoint()
        {
            CarPoint point = new CarPoint(transform.position, transform.rotation);
            points.Add(point);
        }
        /// <summary>
        /// Starts the previously completed car simulation
        /// </summary>
        private void StartPreviousPlaySimulation()
        {

            ReplayCoroutine = StartCoroutine(PreviousPlaySimulationCor());
        }

        private IEnumerator PreviousPlaySimulationCor()
        {
            int step = 0;
            while (step < points.Count)
            {
                SetCarLocation(points[step]);
                step++;
                yield return new WaitForFixedUpdate();
            }

        }
        private void SetCarLocation(CarPoint carPoint)
        {
            transform.position = carPoint.CarPosition;
            transform.rotation = carPoint.CarRotation;
        }
        /// <summary>
        /// Restarts record after collision occurs.
        /// </summary>
        public void RestartRecord()
        {
            points.Clear();
        }
    }

}
