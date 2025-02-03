using NUnit.Framework;
using UnityEngine;

public class S_EquipmentHandeler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] GameObject PrimaryHand;
    [SerializeField] GameObject SecondaryHand;
    void Start()
    {
        
    }

    public void SwichPrimaryHand(GameObject item)
    {
        SwitchHand(item, PrimaryHand);
    }

    public void SwichSecondaryHand(GameObject item)
    {
        SwitchHand(item, SecondaryHand);
    }

    private void SwitchHand(GameObject item, GameObject hand)
    {
        

        for (int i = 0; i < hand.transform.childCount; i++)
        {
            GameObject child = hand.transform.GetChild(i).gameObject;
            child.GetComponent<Rigidbody>().isKinematic = false;
            Debug.Log(child.name + " rigidbody kinematic turned on");

            child.transform.position = hand.transform.position;
        }
        
        hand.transform.DetachChildren();
        item.transform.position = hand.transform.position;
        item.transform.rotation = hand.transform.rotation;
        item.transform.SetParent(hand.transform);
        
        item.GetComponent<Rigidbody>().isKinematic = true;
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}


