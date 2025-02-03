using UnityEngine;
using UnityEngine.InputSystem;

public class S_MouseCameraTurn : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] float Sensetivaty;
    [SerializeField] float Smotheness;
    [SerializeField] float HeadLaggbehind;
    [SerializeField] float HeadTiltScale;
    [SerializeField] float Tiltyness;
    [SerializeField] Transform PitchTransform;
    

    private float rotationX;
    private float rotationY;
    private float rotationZ;

    private Vector2 mouseMovementTarget;
    private Vector2 mouseMovement;

    private Vector2 scaledMouseMovement;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

    }

    private void OnLook(InputValue input)
    {
        mouseMovementTarget = input.Get<Vector2>();
    }


    // Update is called once per frame
    void Update()
    {
        mouseMovement = Vector2.Lerp(mouseMovement, mouseMovementTarget, Smotheness);

        scaledMouseMovement = mouseMovement * Time.deltaTime * Sensetivaty;

        rotationX += scaledMouseMovement.x;
        rotationY += scaledMouseMovement.y;

        rotationZ *= 1 - Tiltyness;
        rotationZ += scaledMouseMovement.x * Tiltyness * HeadTiltScale;

        transform.rotation = Quaternion.Euler(0, rotationX, 0);

       

        PitchTransform.rotation = Quaternion.Euler(rotationY, rotationX, rotationZ);

       
    }
}
