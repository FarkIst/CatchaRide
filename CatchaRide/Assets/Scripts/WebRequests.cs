using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities;
using UnityEngine.Networking;

public class WebRequests : MonoBehaviour
{
    private string _name = "SMTH";
    private double _latitude, _longitude;

    // Use this for initialization
    void Start()
    {
        _latitude = 41.1111;
        _longitude = 12.4141;
    }

    public void GetBookings()
    {
        StartCoroutine(getWWW());
    }

    public void CreateBooking()
    {
        StartCoroutine(PostRequest());
    }

    IEnumerator getWWW()
    {
        // First define the url, this should be a valid url
        string url = "http://localhost:3001/api/ride-offers";

        WWW www = new WWW(url);
        while (!www.isDone)
            yield return null;

        if (string.IsNullOrEmpty(www.error))
        {
            Debug.Log(www.text);
        }
        else
            Debug.Log(www.error);

    }

    IEnumerator PostRequest()
    {
        string s = string.Format("name:{0},coordinates:latitude:{1}, longitude:{2}", _name, _latitude.ToString(), _longitude.ToString());
        Debug.Log(s);
        var jsonString = "{\"name\":\""+ _name +"\", \"coordinates\":{\"latitude\": " + +_latitude +", \"longitude\": " + _longitude+ "}}";
        byte[] byteData = System.Text.Encoding.ASCII.GetBytes(jsonString.ToCharArray());

        UnityWebRequest unityWebRequest = new UnityWebRequest("http://localhost:3001/api/booking", "POST");
        unityWebRequest.uploadHandler = new UploadHandlerRaw(byteData);
        unityWebRequest.SetRequestHeader("Content-Type", "application/json");
        yield return unityWebRequest.Send();

        if (unityWebRequest.isNetworkError || unityWebRequest.isHttpError)
        {
            Debug.Log(unityWebRequest.error);
        }
        else
        {
            Debug.Log("Form upload complete! Status Code: " + unityWebRequest.responseCode);
        }
    }
}
