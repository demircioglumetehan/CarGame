using UnityEngine;
using CarGame.Scripts.Core;

namespace CarGame.Scripts.CarComponents
{
    public class CarAppearance : MonoBehaviour
    {
        [Header("Car Sprites for currently driving and previous drived")]
        public Sprite CurrentCarSprite;
        public Sprite PreviousCarSprite;
        private SpriteRenderer _spriterenderer;
        private Car _thiscar;

        private void Start()
        {
            _spriterenderer = GetComponent<SpriteRenderer>();
            _thiscar = GetComponent<Car>();
        }
        private void OnEnable()
        {
            LevelManager.OnCarCompleteState += OnCarCompleteState;

        }
        private void OnDisable()
        {
            LevelManager.OnCarCompleteState -= OnCarCompleteState;
        }

        private void OnCarCompleteState()
        {
            _spriterenderer.sprite = (Car.currentCar == _thiscar) ? CurrentCarSprite : PreviousCarSprite;
        }

    }

}
