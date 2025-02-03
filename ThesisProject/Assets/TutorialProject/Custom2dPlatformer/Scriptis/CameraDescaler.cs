using UnityEngine;
using UnityEngine.Rendering;

public class CameraDescaler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] Camera inputCamera;
    [SerializeField] float scale;
    private RenderTexture renderTexture;

    private int hight;
    private int width;
    void Start()
    {
        width = (int)(Display.displays[0].renderingWidth * scale);
        hight = (int)(Display.displays[0].renderingHeight * scale);

        //Display.displays[0].SetParams(width, hight, 0, 0);
        //Display.displays[0].SetRenderingResolution(10, 10);

        Display.displays[inputCamera.targetDisplay].SetRenderingResolution(10, 10);
        //renderTexture.height = (int)(inputCamera.pixelHeight * 0.5f);
        //renderTexture.width = (int)(inputCamera.pixelWidth * 0.5f);

        //inputCamera.targetTexture = renderTexture;


    }

    // Update is called once per frame
    void Update()
    {
        //Vector2Int v = new Vector2Int(inputCamera.pixelWidth, inputCamera.pixelHeight);
        
    }
}
