using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Login : MonoBehaviour
{
    [Header("로그인")]
    public GameObject LoginPanel;
    public TMP_InputField EmailInputField;
    public TMP_InputField PasswordInputField;

    [Header("회원가입")]
    public GameObject RegisterPanel;
    public TMP_InputField RegisterEmailInputField;
    public TMP_InputField RegisterNicknameInputField;
    public TMP_InputField RegisterPasswordInputField;


    public async void OnLoginButtonClicked()
    {
        try
        {
            await AccountManager.Instance.Login(EmailInputField.text, PasswordInputField.text);
            Debug.Log("로그인 완료!");
            gameObject.GetComponentInParent<UI_Canvas>().LoginAndOpenPost();
            // 다음 화면 이동 등
        }
        catch (Exception ex)
        {
            Debug.LogError("로그인 실패: " + ex.Message);
            // 에러 메시지 출력 등
        }
    }
    public void OnRegisterPanelButtonClicked()
    {
        LoginPanel.SetActive(false);
        RegisterPanel.SetActive(true);
    }
    public void OnLoginPanelButtonClicked()
    {
        LoginPanel.SetActive(true);
        RegisterPanel.SetActive(false);
    }
    public async void OnRegisterButtonClicked()
    {
        try
        {
            await AccountManager.Instance.Register(RegisterEmailInputField.text,
                RegisterNicknameInputField.text,
                RegisterPasswordInputField.text);
            Debug.Log("회원가입 완료!");
            // 다음 화면 이동 등
            LoginPanel.SetActive(true);
            EmailInputField.text = RegisterEmailInputField.text;
            RegisterPanel.SetActive(false);
        }
        catch (Exception ex)
        {
            Debug.LogError("회원가입 실패: " + ex.Message);
            // 에러 메시지 출력 등
        }
    }
}
