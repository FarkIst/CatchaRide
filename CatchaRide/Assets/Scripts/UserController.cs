using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserController : MonoBehaviour
{
    public string userName;
    public string userType;

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("usercontroller");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
