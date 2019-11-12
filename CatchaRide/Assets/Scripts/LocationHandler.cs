using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LocationHandler : MonoBehaviour
{
    private PermissionHandler _permissionHandler;
    double longitude, latitude;


    private void Start()
    {
        _permissionHandler = GetComponent<PermissionHandler>();
        _permissionHandler.PromptLocationPermissionRequest();

        //Debug
        longitude = 12.462724;
        latitude = 55.638539;
    }

    IEnumerator GetLocation()
    {
        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
        {
            _permissionHandler.PromptLocationPermissionRequest();
            yield break;
        }     


        // Start service before querying location
        Input.location.Start();

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            print("Timed out");
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            yield break;
        }
        else
        {
            // Access granted and location value could be retrieved
            latitude = Input.location.lastData.latitude;
            longitude = Input.location.lastData.longitude;

        }

        // Stop service if there is no need to query location updates continuously
        Input.location.Stop();
    }

    public void SendPosition()
    {
        Application.OpenURL("https://www.google.com/maps/dir/?api=1&" + latitude + ","+ longitude);

    }
}
