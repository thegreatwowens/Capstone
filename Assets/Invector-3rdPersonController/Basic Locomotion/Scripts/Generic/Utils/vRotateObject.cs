using UnityEngine;
using System.Collections;
namespace Invector
{
    public class vRotateObject : MonoBehaviour
    {
        public Vector3 rotationSpeed;
        public bool quartenion;
        public float speed;

        // Update is called once per frame
        void Update()
        {
            if (quartenion) {
                if (gameObject.TryGetComponent(out Renderer renderer))
                {
                    gameObject.transform.RotateAround(renderer.bounds.center, rotationSpeed, speed *Time.deltaTime);
                }

            } else
            {
                transform.Rotate(rotationSpeed * Time.deltaTime, Space.Self);
            }
            
        }
    }
}