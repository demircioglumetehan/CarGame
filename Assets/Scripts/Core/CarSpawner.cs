using CarGame.Scripts.CarComponents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarGame.Scripts.Core
{
    public class CarSpawner : MonoBehaviour
    {

        [SerializeField] private GameObject carPrefab;
        [SerializeField] private Transform carParentObject;
        public List<Car> allcars;
        public List<Transform> SpawnPoints;
        public List<Transform> FinishPoints;
        private int currentcarindex = 0;
        [Header("start and finish point gameobjects")]
        [SerializeField] private GameObject startpointobject;
        [SerializeField] private GameObject finishpointobject;

        private void OnEnable()
        {
            LevelManager.OnCarCompleteState += SpawnNextCar;

        }
        private void OnDisable()
        {

            LevelManager.OnCarCompleteState -= SpawnNextCar;
        }

        private void SpawnNextCar()
        {
            //Check win condition
            if (currentcarindex + 1 == allcars.Count)
            {
                StartCoroutine(WaitForWinCor());
                
            }
            else
            {
                EnableCar(currentcarindex + 1);
            }
        }

        IEnumerator WaitForWinCor()
        {
            yield return new WaitForSeconds(.1f);
            LevelManager.THIS.GameStatus = GameState.Win;
        }

        private void Start()
        {
            GenerateAllCars(8);
            DisableAllCars();
            EnableCar(0);
        }
        /// <summary>
        /// Generates all cars for the level 
        /// </summary>
        /// <param name="caramount">Total number of car for the level </param>
        private void GenerateAllCars(int caramount)
        {
            for (int i = 0; i < caramount; i++)
            {
                var car = Instantiate(carPrefab, carParentObject);
                var CarComponent = car.GetComponent<Car>();
                if (CarComponent != null)
                    allcars.Add(car.GetComponent<Car>());
                else
                    Debug.LogError("This car prefab does not contain car Script");
            }
        }
        /// <summary>
        /// initializes all cars for level beginning.
        /// </summary>
        private void DisableAllCars()
        {
            allcars.ForEach(i => i.gameObject.SetActive(false));
        }
        /// <summary>
        /// Enables the car when its turn has come.
        /// </summary>
        /// <param name="index">current car number</param>
        private void EnableCar(int index)
        {
            SetStartandFinishPointLocations(index);
            allcars[index].gameObject.SetActive(true);

            allcars[index].OnSpawn(SpawnPoints[index]);
            Car.currentCar = allcars[index];
            currentcarindex = index;
        }
        /// <summary>
        /// Sets the positions of start and finish point indicators 
        /// </summary>
        /// <param name="index"> current car number  </param>
        private void SetStartandFinishPointLocations(int index)
        {
            startpointobject.transform.position = SpawnPoints[index].position;
            finishpointobject.transform.position = FinishPoints[index].position;
        }
    }
}

