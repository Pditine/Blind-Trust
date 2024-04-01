using UnityEngine;

namespace Hmxs.Scripts
{
    public class Test : MonoBehaviour
    {
        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha0))
            {
                SoundManager.Instance.PlaySound(0);
            }
            if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha1))
            {
                SoundManager.Instance.PlaySound(1);
            }
            if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha2))
            {
                SoundManager.Instance.PlaySound(2);
            }
            if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha3))
            {
                SoundManager.Instance.PlaySound(3);
            }
            if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha4))
            {
                SoundManager.Instance.PlaySound(4);
            }
            if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha5))
            {
                SoundManager.Instance.PlaySound(5);
            }
            if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha6))
            {
                SoundManager.Instance.PlaySound(6);
            }
            if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha7))
            {
                SoundManager.Instance.PlaySound(7);
            }
            if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha8))
            {
                SoundManager.Instance.PlaySound(8);
            }
            if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha9))
            {
                SoundManager.Instance.PlaySound(9);
            }
        }
    }
}