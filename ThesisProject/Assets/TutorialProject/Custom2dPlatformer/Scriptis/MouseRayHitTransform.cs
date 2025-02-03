using UnityEngine;
using UnityEngine.InputSystem;

public class MouseRayHitTransform : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created    
    [SerializeField] Camera gameCamera;
    [SerializeField] float offsetZlayer;
    [SerializeField] float lerpValue;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Physics.Raycast(gameCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
        {
            Vector3 hitPoint = hit.point;
            hitPoint.z = offsetZlayer;

            transform.position = Vector3.Lerp(transform.position, hitPoint, lerpValue);
            
        }
    }
}
