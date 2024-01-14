using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using Unity.Services.CloudSave;
using Unity.Services.Core;
using Unity.VisualScripting;
using UnityEngine;
using Unity.Services.Authentication;
using UnityEngine.UI;


public class FormManager : MonoBehaviour 
{
    [Header("Toggles")]
    public Toggle _individual;
    public Toggle _organization;
    public Toggle _vr;
    public Toggle _ar;
    public Toggle _bl;
    public Toggle _ai;
    public Toggle _fs;


    [Header("Input Fields")]
    public TMP_InputField _name;
    public TMP_InputField _emailId;
    public TMP_InputField _phoneNumber;
    public TMP_InputField _dob;
    public TMP_InputField _add1;
    public TMP_InputField _add2;
    public TMP_InputField _city;
    public TMP_InputField _country;
    public TMP_InputField _state;
    public TMP_InputField _zipcode;
    public TMP_InputField _occupation;
    public TMP_InputField _employer;
    
    [Header("Buttons")]
    public Button _additionalDetails;
    public Button _areaofintrest;
    public Button _save;

    [Header("Text")]
    public TextMeshProUGUI _areaofintrestText;

    [Header("Dropdown")]
    public TMP_Dropdown _gender;


    [Header ("Lists")]
    public List<TMP_InputField> _requiredfields;
    public List<GameObject> _formObjects;
    public List<GameObject> _additionalFormObjects;
    public List<Toggle> _availableareaofintreset;

    [Header("Scripts")]
    public LoaderManager _loaderManager;

    [Header("Gameobjects")]
    public GameObject _areaofint;
    
    
    string[] selectedareaofintrest = new string[5];

    // Start is called before the first frame update




    async void Awake()
    {
        try
        {
            await UnityServices.InitializeAsync();

            
            
            SSTools.ShowMessage("cloudsave service initialized", SSTools.Position.bottom, SSTools.Time.twoSecond);
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }


    }


    void Start()
    {
        _save.interactable = false;
       /* Debug.Log(_individual.GetComponentInChildren<Text>().text);*/
    }




    // Update is called once per frame
    void Update()
    {
        foreach (var item in _requiredfields)
        {
           if(item.text.Length > 0)
            {
                _save.interactable = true;
                
            }
           else
            {
                //show popup
                _save.interactable= false;
                /*SSTools.ShowMessage("", SSTools.Position.bottom, SSTools.Time.twoSecond);*/
            }

           if(_individual.isOn)
            {
                foreach (var obj in _formObjects)
                {
                    obj.SetActive(true);
                  
                }
            }
           else
            {
                foreach (var obj in _formObjects)
                {
                    obj.SetActive(false);
                }
            }

        }
    }


    public void Addfrom()
    {
        foreach (var item in _additionalFormObjects)
        {
            item.SetActive(true);
        }
    }

    public void Areaofintreset()
    {
        _areaofint.SetActive(true);
        
    }

    

    public void ChooseAreaofIntrest() 
    {
        foreach (var item in _availableareaofintreset)
        {
            /*for(int i = 0; i <= _availableareaofintreset.Count; i++ )*/
            if (item.isOn)
            {
                /* Debug.Log(item.GetComponentInChildren<Text>().text.ToString());*/
                selectedareaofintrest[_availableareaofintreset.IndexOf(item)] = item.GetComponentInChildren<Text>().text.ToString();
            }
        }
        SaveAreaofIntrest();

       
    }

    public void SaveAreaofIntrest()
    {
        string[] filteredArray = System.Array.FindAll(selectedareaofintrest, element => element != null);

        string result = string.Join(", ", filteredArray);
        _areaofintrestText.text = result;
        _areaofint.SetActive(false);
        Debug.Log(result);




    }

    public async void SaveData()
    {
        Debug.Log(_individual.GetComponentInChildren<Text>().text);
        Debug.Log(_name.text);
        Debug.Log(_emailId.text);
        Debug.Log(_dob.text.ToString());
        Debug.Log(_gender.captionText.text);
        Debug.Log(_phoneNumber.text.ToString());
        try
        {
            var playerData = new Dictionary<string, object>{
          {"Playertype", _individual.GetComponentInChildren<Text>().text},
          {"Name", _name.text },
          {"Email-Id",_emailId.text},{"Gender",_gender.captionText.text},{"Date of Birth", _dob.text.ToString() }, {"Contact Number",_phoneNumber.text.ToString() }
        };
            await CloudSaveService.Instance.Data.Player.SaveAsync(playerData);
        }
       /* catch (Exception ex)
        {
            Debug.Log(ex);
        }*/
        catch (CloudSaveValidationException ex)
        {
            Debug.Log(ex);
        }

        
    }

}
