using UnityEngine;
using NianticSpatial.NSDK.AR.VPS2;

public class VPSLocalizationManager : MonoBehaviour
{
    [Header("Paste Scaniverse Anchor Payload")]
    [TextArea(5, 10)]
    public string anchorPayload;

    [Header("Reference ARVps2Manager")]
    public ARVps2Manager vpsManager;

    [Header("Debug Localization Cube")]
    public GameObject localizedCube;

    private ARVps2Anchor trackedAnchor;

    void Start()
    {
        Debug.Log("Starting VPS Localization");

        if (vpsManager == null)
        {
            Debug.LogError("ARVps2Manager Missing");
            return;
        }

        bool success =
            vpsManager.TryTrackAnchor
            (
                anchorPayload,
                out trackedAnchor
            );

        if (success)
        {
            Debug.Log("Anchor Tracking Started");
        }
        else
        {
            Debug.LogError("Failed To Start Anchor Tracking");
        }
    }

    void Update()
    {
        if (trackedAnchor != null)
        {
            localizedCube.transform.position =
                trackedAnchor.transform.position;

            localizedCube.transform.rotation =
                trackedAnchor.transform.rotation;

            Debug.Log
            (
                "Localized Position: " +
                trackedAnchor.transform.position
            );
        }
    }
}