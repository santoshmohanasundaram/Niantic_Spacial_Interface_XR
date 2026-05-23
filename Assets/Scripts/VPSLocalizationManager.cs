using UnityEngine;

public class VPSLocalizationManager : MonoBehaviour
{
    public GameObject localizedCube;

    void Start()
    {
        localizedCube.SetActive(false);

        Invoke(nameof(SimulateLocalization), 5f);
    }

    void SimulateLocalization()
    {
        Debug.Log("VPS Localization Success");

        localizedCube.SetActive(true);

        localizedCube.transform.position = new Vector3(0, 0, 2);
    }
}