using UnityEngine;

public class VPSDebugLogger : MonoBehaviour
{
    void Start()
    {
        Debug.Log("NSDK VPS Debug Started");
    }

    void Update()
    {
        if(Camera.main != null)
        {
            Debug.Log("Camera Position: " + Camera.main.transform.position);
        }
    }
}