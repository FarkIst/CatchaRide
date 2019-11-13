using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class UserLoginHandler : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _usernameText;
    [SerializeField] TextMeshProUGUI _passwordText;
    [SerializeField] TMP_Dropdown _choice;
    [SerializeField] TextMeshProUGUI _errorText;
    [SerializeField] GameObject _errorPage;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Handle Login
    public void AttemptLogin()
    {
        if (PlayerPrefs.HasKey(_usernameText.text))
        {
            if (_passwordText.text == PlayerPrefs.GetString(_usernameText.text))
            {
                //Successful login
                ChangeScene();
            }
            else
            {
                //error
                CatchError("User/Password not found!");
                return;
            }

        }
        else
        {
            //user not found
            CatchError("User/Password not found!");
            return;
        }
    }

    public void ChangeScene()
    {
        if (_choice.value != 0)
        {
            
            int _sceneNumber = _choice.value;
            SceneManager.LoadScene(_sceneNumber);
        }

        else
            CatchError("Select Passenger/Driver");
    }


    // Handle Register
    public void RegisterAccount()
    {
        if(_usernameText.text.Length > 3 && _passwordText.text.Length > 3)
        {
            PlayerPrefs.SetString(_usernameText.text, _passwordText.text);
        }
        else
        {
            CatchError("Enter user name and password with more than 3 characters");
        }
    }


    public void OnBackButtonPressed()
    {
        _errorPage.SetActive(false);
    }

    private void CatchError(string er)
    {
        _errorPage.SetActive(true);
        _errorText.text = er;
    }
}
