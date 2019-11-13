using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities;
using TMPro;
using UnityEngine.Networking;
using System;
using Proyecto26;

public class WebRequests : MonoBehaviour
{
    [SerializeField] private GameObject _passangerUI, _contentContainer;

    private testContainer tc;

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
        StartCoroutine(GetClients(PlaceClientsOnUI));
    }

    public void CreateBooking()
    {
        StartCoroutine(PostRequest());
    }

    public void PlaceClientsOnUI(test client)
    {
        Debug.Log("CLIENT NAME: " + client.clientName);
        foreach(test c in tc.tests)
        {
            GameObject go = Instantiate(_passangerUI, _contentContainer.transform);
            go.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = c.clientName;
        }
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
        var jsonString = "{{\"name\":\""+ _name +"\", \"coordinates\":{\"latitude\": " + +_latitude +", \"longitude\": " + _longitude+ "}}]}";
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


    ////////////////

    IEnumerator GetClients(Action<test> onSuccess)
    {
       using(UnityWebRequest req = UnityWebRequest.Get("http://localhost:3001/api/ride-offers"))
        {
            yield return req.SendWebRequest();
            while (!req.isDone)
                yield return null;
            byte[] result = req.downloadHandler.data;
            string jsonString = System.Text.Encoding.Default.GetString(result);
            Debug.Log("JSONSTRING: " + jsonString);

            var j = JsonUtility.FromJson<test>(jsonString);
            onSuccess(j);
   
        }
    }

}

[Serializable]
public class test
{
    public string id { get; set; }
    public string clientName { get; set; }
    public string distance { get; set; }
}

[Serializable]
public class testContainer
{
    public test[] tests;
}
