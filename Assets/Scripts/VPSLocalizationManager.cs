using UnityEngine;
using NianticSpatial.NSDK.AR.VPS2;

public class VPSLocalizationManager : MonoBehaviour
{
    [Header("Paste Scaniverse Anchor Payload")]
    [TextArea(5,10)]
    public string anchorPayload;

    [Header("Reference ARVps2Manager")]
    public ARVps2Manager vpsManager;

    [Header("World Space SOP Canvas")]
    public GameObject sopCanvas;

    [Header("Distance Settings")]
    public float showDistance = 2.0f;

    public float hideDistance = 3.0f;

    private ARVps2Anchor trackedAnchor;

    private bool localized = false;

    void Start()
    {
        Debug.Log("========== VPS SYSTEM START ==========");

        if(vpsManager == null)
        {
            Debug.LogError("ARVps2Manager Missing");
            return;
        }

        /*
            Hide SOP Initially
        */
        sopCanvas.SetActive(false);

        /*
            Start VPS Anchor Tracking
        */
        bool success =
            vpsManager.TryTrackAnchor
            (
                anchorPayload,
                out trackedAnchor
            );

        if(success)
        {
            Debug.Log("Anchor Tracking Initialized");
        }
        else
        {
            Debug.LogError("Failed To Start Anchor Tracking");
        }
    }

    void Update()
    {
        /*
            Wait until anchor available
        */
        if(trackedAnchor == null)
            return;

        /*
            VPS Anchor Position
        */
        Vector3 anchorPos =
            trackedAnchor.transform.position;

        /*
            VPS Anchor Rotation
        */
        Quaternion anchorRot =
            trackedAnchor.transform.rotation;

        /*
            Current Camera Position
        */
        Vector3 cameraPos =
            Camera.main.transform.position;

        /*
            Distance Between User and Anchor
        */
        float distance =
            Vector3.Distance
            (
                cameraPos,
                anchorPos
            );

        /*
            Runtime Logs
        */
        Debug.Log
        (
            "Camera Position: " +
            cameraPos
        );

        Debug.Log
        (
            "Anchor Position: " +
            anchorPos
        );

        Debug.Log
        (
            "Distance To Anchor: " +
            distance
        );

        /*
            Validate REAL VPS Localization
        */
        bool validPosition =
            anchorPos.magnitude > 0.1f;

        bool validRotation =
            anchorRot != Quaternion.identity;

        /*
            First Localization
        */
        if(validPosition && validRotation && !localized)
        {
            localized = true;

            Debug.Log
            (
                "REAL VPS LOCALIZATION ACHIEVED"
            );

            /*
                Spawn SOP Panel
            */
            Transform cam =
                Camera.main.transform;

            /*
                Spawn slightly above
                and in front of user
            */
            Vector3 spawnPos =
                cam.position +
                cam.forward * 1f +
                Vector3.up * 1f;

            /*
                Apply Position
            */
            sopCanvas.transform.position =
                spawnPos;

            /*
                Face User
            */
            Vector3 lookDir =
                sopCanvas.transform.position -
                cam.position;

            sopCanvas.transform.rotation =
                Quaternion.LookRotation(lookDir);

            Debug.Log
            (
                "SOP Canvas Positioned"
            );
        }

        /*
            Show SOP When User Near Anchor
        */
        if(distance < showDistance)
        {
            if(!sopCanvas.activeSelf)
            {
                sopCanvas.SetActive(true);

                Debug.Log("SOP Activated");
            }
        }

        /*
            Hide SOP When User Far Away
        */
        if(distance > hideDistance)
        {
            if(sopCanvas.activeSelf)
            {
                sopCanvas.SetActive(false);

                Debug.Log("SOP Hidden");
            }
        }
    }
}