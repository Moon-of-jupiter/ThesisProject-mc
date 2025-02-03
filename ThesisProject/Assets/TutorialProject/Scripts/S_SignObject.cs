using UnityEngine;

public class S_ : MonoBehaviour, IInteractable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void Interact(GameObject interactor)
    {
        Debug.Log("Item Interacted With: " + name);

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
