using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ResetOnReset : MonoBehaviour, IReseter
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] Transform startPosition;
    [SerializeField] ResetCaller resetter;

    
    void Start()
    {
        transform.position = startPosition.position;
        transform.rotation = startPosition.rotation;

        resetter.SubsciribeToReset(this);
        Debug.Log("subscribed to resetter: " + resetter.name);
    }

    public void ResetObject()
    {
        transform.position = startPosition.position;
        transform.rotation = startPosition.rotation;

        GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
}
