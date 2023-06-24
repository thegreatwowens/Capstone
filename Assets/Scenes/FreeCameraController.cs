using UnityEngine;

public class FreeCameraController : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float rotationSpeed = 3f;
    public float mouseSensitivity = 2f;
    public float smoothing = 1f;

    private Vector2 smoothMouse;
    private Vector2 currentMouseDelta;
    private Vector2 targetMouseDelta;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            Cursor.visible = !Cursor.visible;

        }

        if(Input.GetKeyDown(KeyCode.PageUp)) { 
                rotationSpeed += rotationSpeed +5;
            
        }
        if(Input.GetKeyDown(KeyCode.PageDown)) { 
                rotationSpeed -= rotationSpeed -5;
            
        }
        if(Input.GetKeyDown(KeyCode.Home)) { 
                movementSpeed += movementSpeed +5;
            
        }
        if(Input.GetKeyDown(KeyCode.End)){
          movementSpeed -= movementSpeed -5;
        }
        // Handle camera movement
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        Vector3 movement = transform.forward * verticalMovement + transform.right * horizontalMovement;
        movement.Normalize();

        // Calculate the new position based on the input
        Vector3 newPosition = transform.position + movement * movementSpeed * Time.deltaTime;

        // Update the camera position
        transform.position = newPosition;

        // Handle camera rotation based on mouse input
        currentMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        // Apply mouse sensitivity and smoothing
        currentMouseDelta *= mouseSensitivity * smoothing;
        targetMouseDelta += currentMouseDelta;

        // Smooth the mouse movement
        smoothMouse = Vector2.Lerp(smoothMouse, targetMouseDelta, 1f / smoothing);
        targetMouseDelta = Vector2.zero;

        // Apply rotation to the camera
        transform.localRotation *= Quaternion.Euler(-smoothMouse.y, smoothMouse.x, 0f) * Quaternion.Euler(0f, 0f, 0f);
    }
}

 