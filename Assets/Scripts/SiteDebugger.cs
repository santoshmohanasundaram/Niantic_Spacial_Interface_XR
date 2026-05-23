using UnityEngine;
using NianticSpatial.NSDK.AR.Sites;

public class SiteDebugger : MonoBehaviour
{
    [SerializeField]
    private SitesClientManager sitesClientManager;

    async void Start()
    {
        var userResult =
            await sitesClientManager.GetSelfUserInfoAsync();

        Debug.Log("Sites API Started");

        if(userResult.Status ==
            SitesRequestStatus.Success)
        {
            Debug.Log("User authenticated");
        }
        else
        {
            Debug.Log("Sites API failed");
        }
    }
}