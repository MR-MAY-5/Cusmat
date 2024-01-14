using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    public TMP_InputField username;
    public TMP_InputField password;
    public Button login;
    public Button register;
    public GameObject popup;
    public TextMeshProUGUI popupText;

    public LoaderManager loaderManager;


   /* SSTools.ShowMessage("Service Initialized",SSTools.Position.bottom,SSTools.Time.twoSecond);*/

    /*popup.gameObject.SetActive(true);
            popupText.text = "Username must be more " +
                "than or equal to 5 characters";*/

    async void Awake()
    {
        try
        {
            await UnityServices.InitializeAsync();
            SSTools.ShowMessage("Service Initialized",SSTools.Position.bottom,SSTools.Time.twoSecond);
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
      if (popup.activeInHierarchy)
        {
            login.interactable = false;
            register.interactable = false;
        }
        else
        {
            login.interactable = true;
            register.interactable = true;
        }
       

    }

    // Update is called once per frame

    public async void loginn()
    {
        string usernam = username.text;
        string pass = password.text;
        Debug.Log(usernam);
        Debug.Log(pass);
        if(username.textComponent.text.Length > 5)
        {
            await SignInWithUsernamePasswordAsync(usernam, pass);
        }
        else
        {
            popup.gameObject.SetActive(true);
            popupText.text = "Username must be more " +
                "than or equal to 5 characters"; 

        }

      
    }

    public async void registeruser()
    {
        string regusernam = username.text;
        string regpass = password.text;
        Debug.Log(regusernam);
        Debug.Log(regpass);

        await SignUpWithUsernamePassword(regusernam, regpass);

    }



    public void popupexit()
    {
       popup.gameObject.SetActive(false);
    }


     async Task SignUpWithUsernamePassword(string username, string password)
    {
        try
        {
            await AuthenticationService.Instance.SignUpWithUsernamePasswordAsync(username, password);
            Debug.Log("SignUp is successful.");
        }
        catch (AuthenticationException ex)
        {
            // Compare error code to AuthenticationErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
            popup.gameObject.SetActive(true);
            popupText.text = ex.Message;
        }
        catch (RequestFailedException ex)
        {
            // Compare error code to CommonErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
            popup.gameObject.SetActive(true);
            popupText.text = ex.Message;
        }
    }




    async Task SignInWithUsernamePasswordAsync(string username, string password)
    {
        try
        {
            await AuthenticationService.Instance.SignInWithUsernamePasswordAsync(username, password);
            Debug.Log("SignIn is successful.");
            SSTools.ShowMessage("SignIn is successful.", SSTools.Position.bottom, SSTools.Time.twoSecond);
            loaderManager.sceneloader(1);
        }
        catch (AuthenticationException ex)
        {
            // Compare error code to AuthenticationErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
            popup.gameObject.SetActive(true);

            popupText.text = ex.Message;

        }
        
        catch (RequestFailedException ex)
        {
            // Compare error code to CommonErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
            popup.gameObject.SetActive(true);
            popupText.text = ex.Message;
        }
    }
}
