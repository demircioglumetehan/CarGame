using UnityEngine;
using CarGame.Scripts.Core;
using CarGame.Scripts.CarComponents;
namespace CarGame.Scripts.Effects
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Animator))]
    public class CrashEffect : MonoBehaviour
    {
        private SpriteRenderer _spriterenderer;
        private Animator _animator;
        void Start()
        {
            _spriterenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
            CrashAnimDisable();
        }

        private void OnEnable()
        {
            LevelManager.OnCarFailedState += CrashAnimEnable;
        }
        private void OnDisable()
        {
            LevelManager.OnCarFailedState -= CrashAnimEnable;
        }
        private void CrashAnimEnable()
        {
            transform.position = Car.currentCar.transform.position;
            _spriterenderer.enabled = true;
            _animator.enabled = true;
        }
        public void CrashAnimDisable()
        {
            _spriterenderer.enabled = false;
            _animator.enabled = false;
        }



    }

}
