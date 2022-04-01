using UnityEngine;

namespace CarGame.Scripts.GameArea
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ArrangeBackgroundSize : MonoBehaviour
    {
        private void Start()
        {
            ResizeSpriteToScreen();
        }
        /// <summary>
        /// Resize the background area according to different screen sizes.
        /// </summary>
        void ResizeSpriteToScreen()
        {
            var sr = GetComponent<SpriteRenderer>();
            if (sr == null) return;

            transform.localScale = Vector3.one;

            var width = sr.sprite.bounds.size.x;
            var height = sr.sprite.bounds.size.y;

            float worldScreenHeight = Camera.main.orthographicSize * 2.0f;
            float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

            transform.localScale = new Vector3(worldScreenWidth / width, worldScreenHeight / height, transform.localScale.z);

        }
    }

}
