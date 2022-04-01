using UnityEngine;
namespace CarGame.Scripts.UI
{
    /// <summary>
    /// This is a auxiliary class to make the leveldesigners work easy when creating levels
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpawnPointEditor : MonoBehaviour
    {
        SpriteRenderer _spriterenderer;
        void Start()
        {
            _spriterenderer = GetComponent<SpriteRenderer>();
            if (_spriterenderer != null)
            {
                _spriterenderer.enabled = false;
            }
        }
    }

}
