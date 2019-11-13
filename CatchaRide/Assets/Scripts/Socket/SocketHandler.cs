using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using Entities;
using System;
using TMPro;

public class SocketHandler : MonoBehaviour
{
    [SerializeField] private GameObject _passangerUI, _contentContainer;

    private SocketIOComponent socket;
    private ClientContainer _clientContainer;

    // Start is called before the first frame update
    void Start()
    {
        socket = FindObjectOfType<SocketIOComponent>();

        socket.On("open", TestOpen);
        socket.Emit("availableclients");
        socket.On("returnavailableclients", GetAllClients);
    }

    private void GetAllClients(SocketIOEvent e)
    {
        Debug.Log(e.data);
        _clientContainer = JsonUtility.FromJson<ClientContainer>(e.data.ToString());

        if(_contentContainer.transform.childCount > 0)
        {
            for(int i = 0; i< _contentContainer.transform.childCount; i++)
            {
                Destroy(_contentContainer.transform.GetChild(i).gameObject);
            }
        }

        foreach(client c in _clientContainer.clients)
        {
            GameObject go = Instantiate(_passangerUI, _contentContainer.transform);
            go.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = c.clientName;
        }
    }

    public void RefreshClientList()
    {
        socket.Emit("availableclients");
    }

    public void TestOpen(SocketIOEvent e)
    {
        Debug.Log("[SocketIO] Open received: " + e.name + " " + e.data);
    }
}
