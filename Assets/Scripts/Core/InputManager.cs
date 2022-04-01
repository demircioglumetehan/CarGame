using System;
using UnityEngine;

namespace CarGame.Scripts.Core
{
    public class InputManager : MonoBehaviour
    {
        public static event Action<bool> OnPressScreen;
        public static event Action OnReleaseScreen;
        public void OnPressingLeftButton()
        {
            OnPressScreen?.Invoke(true);
        }
        public void OnPressingRightButton()
        {
            OnPressScreen?.Invoke(false);
        }
        public void OnReleaseButton()
        {
            OnReleaseScreen?.Invoke();
        }

    }

}
