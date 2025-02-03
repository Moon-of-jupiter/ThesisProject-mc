using UnityEngine;

public class S_Equipable : MonoBehaviour, IInteractable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void Interact(GameObject interactor)
    {
        interactor.GetComponent<S_EquipmentHandeler>().SwichPrimaryHand(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
