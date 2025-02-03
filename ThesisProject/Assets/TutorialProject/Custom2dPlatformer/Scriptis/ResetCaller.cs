using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public interface IReseter
{
    string name {  get; }
    void ResetObject();
}

public class ResetCaller : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private UnityEvent EVENT_Reset = new UnityEvent();

    void Start()
    {

        Screen.SetResolution((int)(Screen.width * 0.1f), (int)(Screen.height * 0.1f), true);

    }

    public void SubsciribeToReset(IReseter reseter)
    {
        Debug.Log(reseter.name + " subscribed to resetter");

        if (EVENT_Reset == null)
        {
            Debug.Log("event is null");
        }
        EVENT_Reset.AddListener(reseter.ResetObject);
    }

    public void UnsubscribeToReset(IReseter reseter)
    {
        try
        {
            EVENT_Reset.RemoveListener(reseter.ResetObject);
        }
        catch
        {
            Debug.LogError("Failed To Remove Listener: " + reseter.name);
        }
        
    }

    private void OnReset()
    {
        Debug.Log("objects reset");
        InvokeReset();
    }

    public void InvokeReset()
    {
        EVENT_Reset?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
