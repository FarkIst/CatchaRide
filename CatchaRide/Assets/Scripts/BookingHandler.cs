using Proyecto26;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Entities;
using System.IO;

public class BookingHandler : MonoBehaviour
{
    private double _latitude, _longitude;
    [SerializeField] private LocationHandler _locationHandler;

    RequestHelper currentRequest;
    string basePath = "";


    private void LogMessage(string title, string message)
    {
#if UNITY_EDITOR
        EditorUtility.DisplayDialog(title, message, "Ok");
#else
		Debug.Log(message);
#endif
    }


    // Start is called before the first frame update
    void Start()
    {
        if (!_locationHandler)
            _locationHandler = FindObjectOfType<LocationHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        _latitude = _locationHandler.Latitude;
        _longitude = _locationHandler.Longitude;
    }


    //Post method
    public void CreateBooking()
    {
       
        currentRequest = new RequestHelper
        {
            Uri = basePath + "/posts",

            Body = new Client
            {
                Name = "",
                Status = "",
                Coordinates = new Coordinates
                {
                   Latitude = _latitude,
                   Longitude = _longitude
                }
            },
            EnableDebug = true
        };
        RestClient.Post<ClientContainer>(currentRequest)
        .Then(res => {

            // And later we can clear the default query string params for all requests
            RestClient.ClearDefaultParams();

            this.LogMessage("Success", JsonUtility.ToJson(res, true));
        })
        .Catch(err => this.LogMessage("Error", err.Message));
    }


    //Get Method
    public List<Client> GetAllClients()
    {
        List<Client> _clients = new List<Client>();
        RestClient.Get<ClientContainer>(basePath).Then(res =>
        {
            foreach(Client c in res.Clients)
            {
                _clients.Add(c);
            }
        });
        return _clients;
    }

}


////Debug
//public void PrintClient()
//{

//    List<Client> clients = new List<Client>();

//    Client c1 = new Client
//    {
//        Name = "Vladimir",
//        Status = "Accepted",
//        Coordinates = new Coordinates
//        {
//            Latitude = 55.1023,
//            Longitude = 12.2111
//        }
//    };

//    Client c2 = new Client
//    {
//        Name = "Stefano",
//        Status = "Declined",
//        Coordinates = new Coordinates
//        {
//            Latitude = 55.1023,
//            Longitude = 12.2111
//        }
//    };

//    Client c3 = new Client
//    {
//        Name = "Arcadio",
//        Status = "Declined",
//        Coordinates = new Coordinates
//        {
//            Latitude = 55.1023,
//            Longitude = 12.2111
//        }
//    };
//    Client c4 = new Client
//    {
//        Name = "Seeeeeean",
//        Status = "Accepted",
//        Coordinates = new Coordinates
//        {
//            Latitude = 55.1023,
//            Longitude = 12.2111
//        }
//    };

//    clients.Add(c1);
//    clients.Add(c2);
//    clients.Add(c3);
//    clients.Add(c4);

//    ClientContainer clientContainer = new ClientContainer() { Clients = clients };

//    string savePath = "./Assets/clients";

//    using (StreamWriter stream = new StreamWriter(savePath + ".json"))
//    {
//        string json = JsonUtility.ToJson(clientContainer);
//        stream.Write(json);
//    }
//}

//public void PrintDriver()
//{

//    List<Driver> drivers = new List<Driver>();

//    Driver d1 = new Driver
//    {
//        Name = "Saaeeen",
//        Distance = "2km",
//        Status = "Accepted",
//        Coordinates = new Coordinates
//        {
//            Latitude = 55.12211,
//            Longitude = 13.112
//        }
//    };

//    Driver d2 = new Driver
//    {
//        Name = "Salami",
//        Distance = "20km",
//        Status = "Declined",
//        Coordinates = new Coordinates
//        {
//            Latitude = 55.12211,
//            Longitude = 13.112
//        }
//    };

//    Driver d3 = new Driver
//    {
//        Name = "Kebab",
//        Distance = "1km",
//        Status = "Processing",
//        Coordinates = new Coordinates
//        {
//            Latitude = 55.12211,
//            Longitude = 13.112
//        }
//    };


//    drivers.Add(d1);
//    drivers.Add(d2);
//    drivers.Add(d3);

//    DriverContainer driverContainer = new DriverContainer() { Drivers = drivers };

//    string savePath = "./Assets/drivers";

//    using (StreamWriter stream = new StreamWriter(savePath + ".json"))
//    {
//        string json = JsonUtility.ToJson(driverContainer);
//        stream.Write(json);
//    }
//}