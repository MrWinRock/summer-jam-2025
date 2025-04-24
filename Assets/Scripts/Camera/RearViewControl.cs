using UnityEngine;

public class RearViewController : MonoBehaviour
{
    public Camera rearCamera;

    void Start()
    {
        rearCamera.enabled = true;
        rearCamera.clearFlags = CameraClearFlags.Depth;

        InvokeRepeating("RenderRearView", 0f, 0.5f);
    }

    void RenderRearView()
    {
        rearCamera.Render();
    }
}