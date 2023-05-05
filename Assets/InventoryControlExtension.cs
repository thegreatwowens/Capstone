using UnityEngine.Events;
using UnityEngine;

namespace OwnCode
{
    public class InventoryControlExtension : MonoBehaviour
    {
        [SerializeField]
        UnityEvent OnEnable;
        [SerializeField]
        UnityEvent OnDisable;

        public void DisableInputs()
        {
            if (OnDisable != null)
            {
                OnDisable.Invoke();

            }
        }
        public void EnableInputs()
        {
            if (OnEnable != null)
            {
                OnEnable.Invoke();

            }
        }


    }

}
