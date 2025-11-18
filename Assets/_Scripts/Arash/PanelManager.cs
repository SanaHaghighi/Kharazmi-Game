using System;
using System.Collections.Generic;
using NUnit.Framework;
using RTLTMPro;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanelManager : SingletonBehaviour<PanelManager>
{
    [SerializeField] GameObject StartPanel;
    [SerializeField] Button StartButton;
    [SerializeField] GameObject LoginPanel;
    [SerializeField] GameObject SignUpPanel;
    [SerializeField] GameObject LoginAndSignUpPanel;
    [SerializeField] Button LoginButton;
    [SerializeField] RTLTextMeshPro LoginUsernameText;
    [SerializeField] RTLTextMeshPro LoginPasswordText;
    [SerializeField] TMP_InputField LoginPasswordField;
    [SerializeField] Button SignUpButton;
    [SerializeField] RTLTextMeshPro SignUpUsernameText;
    [SerializeField] RTLTextMeshPro SignUpPasswordText;
    [SerializeField] RTLTextMeshPro SignUpEmailText;
    [SerializeField] TMP_InputField SignUpPasswordField;
    [SerializeField] TMP_InputField SignUpUsernameField;
    [SerializeField] Button LoginBackButton;
    [SerializeField] Button SignUpBackButton;
    [SerializeField] Button LoginConfirmButton;
    [SerializeField] Button SignUpConfirmButton;
    [SerializeField] Button ShowPasswordButtonLogin;
    [SerializeField] Button ShowPasswordButtonSignup;
    [SerializeField] RTLTextMeshPro Label;
    [SerializeField] TMP_Dropdown dropdown;
    [SerializeField] Image usernameCheckImage;
    [SerializeField] Image passwordCheckImage;


    private string classnametext;
    void Start()
    {
        StartPanel.gameObject.SetActive(true);
        LoginPanel.gameObject.SetActive(false);
        SignUpPanel.gameObject.SetActive(false);
        LoginAndSignUpPanel.gameObject.SetActive(false);
        StartButton.onClick.AddListener(StartButtonClicked);
        LoginButton.onClick.AddListener(LoginButtonClicked);
        SignUpButton.onClick.AddListener(SignUpButtonClicked);
        LoginBackButton.onClick.AddListener(LoginBackButtonClicked);
        SignUpBackButton.onClick.AddListener(SignUpBackButtonClicked);
        ShowPasswordButtonLogin.onClick.AddListener(ShowPasswordLogin);
        ShowPasswordButtonSignup.onClick.AddListener(ShowPasswordSignup);
        SignUpConfirmButton.onClick.AddListener(ConfirmSignUp);
        LoginConfirmButton.onClick.AddListener(ConfirmLogin);
        SignUpPasswordField.onValueChanged.AddListener(CheckPassword);
        CheckLogin();
    }

    private void CheckPassword(string arg0)
    {
        if (SignUpPasswordField.text.Length >= 6)
            passwordCheckImage.gameObject.SetActive(true);
        else
            passwordCheckImage.gameObject.SetActive(false);
    }

    private async void CheckLogin()
    {
        var username = PlayerPrefs.GetString("username", string.Empty);
        var password = PlayerPrefs.GetString("password", string.Empty);
        if (username.Equals(string.Empty) || password.Equals(string.Empty)) return;
        var result = await APIManager.Instance.Login(() => {
            SceneManager.LoadScene("Menu");
        }, (error) =>
        {
            Debug.LogError("Failed To Use Previous Login");
        }, username, password);
    }

    private async void ConfirmSignUp()
    {
        var username = SignUpUsernameText.text;
        var password = SignUpPasswordField.text;
        var email = SignUpEmailText.text;
        var className = classnametext;
        if (username.Equals(string.Empty) || password.Equals(string.Empty)) return;
        SignUpConfirmButton.interactable = false;
        var result = await APIManager.Instance.SignUp(()=> {
            PlayerPrefs.SetString("username", username);
            PlayerPrefs.SetString("password", password);
            SceneManager.LoadScene("Menu");
        }, (error) =>
        {
            SignUpConfirmButton.interactable = true;
            Debug.LogError("Failed To Login");
            if (error.Contains("Username already exists!"))
            {
                usernameCheckImage.gameObject.SetActive(true);
            }
        },username, password, email, className);
    }

    private async void ConfirmLogin()
    {
        var username = LoginUsernameText.text;
        var password = LoginPasswordField.text;
        if (username.Equals(string.Empty) || password.Equals(string.Empty)) return;
        var result = await APIManager.Instance.Login(() => {
            SceneManager.LoadScene("Menu");
        }, (error) =>
        {
            Debug.LogError("Failed To Login");
        },username, password);
    }

    private void ShowPasswordLogin()
    {
        if(LoginPasswordField.contentType==TMP_InputField.ContentType.Password)
            LoginPasswordField.contentType=TMP_InputField.ContentType.Standard;
        else
            LoginPasswordField.contentType=TMP_InputField.ContentType.Password;
        LoginPasswordField.ForceLabelUpdate();
    }
    private void ShowPasswordSignup(){
        if(SignUpPasswordField.contentType==TMP_InputField.ContentType.Password)
            SignUpPasswordField.contentType=TMP_InputField.ContentType.Standard;
        else
            SignUpPasswordField.contentType=TMP_InputField.ContentType.Password;
        SignUpPasswordField.ForceLabelUpdate();
    }

    private void SignUpBackButtonClicked()
    {
        SignUpUsernameText.text = String.Empty;
        SignUpPasswordText.text = String.Empty;
        SignUpEmailText.text = String.Empty;
        SignUpPasswordField.contentType=TMP_InputField.ContentType.Password;
        SignUpPasswordField.ForceLabelUpdate();
        SignUpPanel.SetActive(false);
        LoginAndSignUpPanel.SetActive(true);
    }

    private void LoginBackButtonClicked()
    {
        LoginUsernameText.text = String.Empty;
        LoginPasswordText.text = String.Empty;
        LoginPanel.SetActive(false);
        LoginPasswordField.contentType=TMP_InputField.ContentType.Password;
        LoginPasswordField.ForceLabelUpdate();
        LoginAndSignUpPanel.SetActive(true);
    }

    private void SignUpButtonClicked()
    {
        LoginAndSignUpPanel.SetActive(false);
        SignUpPasswordField.contentType=TMP_InputField.ContentType.Password;
        SignUpPasswordField.ForceLabelUpdate();
        SignUpPanel.SetActive(true);
        SetLabel("نام کلاس");
    }
    public void DropDownValueChanged(string value)
    {
        classnametext = value;
        SetLabel(classnametext);
        dropdown.Hide();
        
    }
    void SetLabel(string label)
    {
        
        if (Label != null)
        {
            Label.text = label; 
        }
    }
    private void LoginButtonClicked()
    {
        LoginAndSignUpPanel.SetActive(false);
        LoginPasswordField.contentType=TMP_InputField.ContentType.Password;
        LoginPasswordField.ForceLabelUpdate();
        LoginPanel.SetActive(true);
    }

    private void StartButtonClicked()
    {
        StartPanel.gameObject.SetActive(false);
        LoginAndSignUpPanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
