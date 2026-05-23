using UnityEngine;

public class VPSRuntimeDebugger : MonoBehaviour
{
    void Update()
    {
        if (Camera.main != null)
        {
            Vector3 pos = Camera.main.transform.position;

            Debug.Log("Camera VPS Position: " + pos);
        }
    }
}