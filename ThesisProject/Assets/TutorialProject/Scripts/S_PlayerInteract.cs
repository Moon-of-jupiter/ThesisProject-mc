using UnityEngine;
using UnityEngine.InputSystem;

public interface IInteractable
{
    void Interact(GameObject ineractor);

}

public class S_PlayerInteract : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] Camera PlayerCamera;
    

    void Start()
    {
        
    }

    protected void OnInteract(InputValue input)
    {
        Debug.Log("Interaction Started");
        if (Physics.Raycast(PlayerCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 3))
        {
            if(hit.collider != null)
            {
                hit.collider.GetComponent<IInteractable>().Interact(gameObject);
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void Interact(object target, int distance)
    {

    }
}
