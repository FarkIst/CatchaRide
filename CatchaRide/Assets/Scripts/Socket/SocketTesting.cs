using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using System;

public class SocketTesting : MonoBehaviour
{
    private SocketIOComponent _socket;

    // Start is called before the first frame update
    void Start()
    {
        _socket = FindObjectOfType<SocketIOComponent>();
        _socket.On("testme", Test);
    }

    private void Test(SocketIOEvent obj)
    {
        Debug.Log("TESTING");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("w"))
        {
            _socket.Emit("testme");
        }
    }


}
