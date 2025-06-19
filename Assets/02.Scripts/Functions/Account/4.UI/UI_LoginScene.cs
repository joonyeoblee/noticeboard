using System;
using TMPro;
using System.Security.Cryptography;
using Firebase;
using Firebase.Auth;
using Firebase.Firestore;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[Serializable]
public class UI_InputFields
{
    public TextMeshProUGUI ResultText; // 결과 텍스트
    public TMP_InputField IDInputField;
    public TMP_InputField NicknameInputField;
    public TMP_InputField PasswordInputField;
    public TMP_InputField PasswordConfirmInputField;
    public Button ConfirmButton;
}
public class UI_LoginScene : MonoBehaviour
{
    [Header("패널")]
    public GameObject LoginPanel;
    public GameObject RegisterPanel;

    [Header("로그인")]   public UI_InputFields LoginInputFields;
    [Header("회원가입")] public UI_InputFields RegisterInputFields;
    
    private FirebaseApp _app;
    private FirebaseAuth _auth;
    private FirebaseFirestore _db;

    
    private void Start()
    {
        LoginPanel.SetActive(true);
        RegisterPanel.SetActive(false);
        // LoginCheck();
        
        LoginInputFields.ResultText.text = string.Empty;
        RegisterInputFields.ResultText.text = string.Empty;
        Init();
    }

    public void OnClickRegisterButton()
    {
        LoginPanel.SetActive(false);
        RegisterPanel.SetActive(true);
    }
    public void OnClickGoToLoginButton()
    {
        LoginPanel.SetActive(true);
        RegisterPanel.SetActive(false);
    }
    private void Init()
    {
        
    }
    // 회원가입
    public void Register()
    {
        // 1. 아이디 입력을 확인한다.
        string email = RegisterInputFields.IDInputField.text;
        var emailSpecification = new AccountEmailSpecification();
        if(!emailSpecification.IsSatisfiedBy(email))
        {
            throw new Exception(emailSpecification.ErrorMessage);
            // RegisterInputFields.ResultText.text = "아이디를 입력해주세요.";
        }
        // 2. 닉네임 도메인 규칙을 확인한다.
        string nickname = RegisterInputFields.NicknameInputField.text;
        var nicknameSpecification = new AccountNicknameSpecification();
        if (!nicknameSpecification.IsSatisfiedBy(nickname))
        {
            RegisterInputFields.ResultText.text = nicknameSpecification.ErrorMessage;
            return;
        }
        // 3. 1차 비밀번호 입력을 확인한다.
        string password = RegisterInputFields.PasswordInputField.text;
        
        var passwordSpecification = new AccountPasswordSpecification();
        if (!passwordSpecification.IsSatisfiedBy(password))
        {
            RegisterInputFields.ResultText.text =passwordSpecification.ErrorMessage;
            return;
        }
        // 4. 2차 비밀번호 입력을 확인하고, 1차 비밀번호 입력과 같은지 확인한다.

        string passwordConfirm = RegisterInputFields.PasswordConfirmInputField.text;
        if (!passwordSpecification.IsSatisfiedBy(passwordConfirm))
        {
            RegisterInputFields.ResultText.text = passwordSpecification.ErrorMessage;
            return;
        }

        if (password != passwordConfirm)
        {
            RegisterInputFields.ResultText.text = "비밀번호가 일치하지 않습니다.";
            return;
        }
        // Result result = AccountManager.Instance.TryRegister(email, nickname, password);
        // if (result.IsSuccess)
        // {
        //     OnClickGoToLoginButton();
        //     RegisterInputFields.ResultText.text = "회원가입이 완료되었습니다."; 
        // }
        // else
        // {
        //     RegisterInputFields.ResultText.text = result.Message; 
        // }


        // 5. 로그인 창으로 돌아간다. (이 때 아이디는 자동 입력 되어있다.)'
        LoginInputFields.IDInputField.text = email;
        
    }

    
    public void Login()
    {
        // 1. 아이디 입력을 확인한다.
        string email = LoginInputFields.IDInputField.text;
        var emailSpecification = new AccountEmailSpecification();
        if (!emailSpecification.IsSatisfiedBy(email))
        {
            
            LoginInputFields.ResultText.text = emailSpecification.ErrorMessage;
            return;
        }
        // 2. 비밀번호 입력을 확인한다.
        string password = LoginInputFields.PasswordInputField.text;
        var passwordSpecification = new AccountPasswordSpecification();
        if (!passwordSpecification.IsSatisfiedBy(password))
        {
            LoginInputFields.ResultText.text = passwordSpecification.ErrorMessage;
            return;
        }
        
        // 3. PlayerPrefs.Get을 이용해서 아이디와 비밀번호가 맞는지 확인한다.
        // if (AccountManager.Instance.TryLogin(email, password))
        // {
        //     LoginInputFields.ResultText.text = "아이디와 비밀번호를 확인해주세요";
        // }
        // else
        // {
        //     SceneManager.LoadScene(1);   
        // }
        // string hashedPassword = PlayerPrefs.GetString(PREFIX + id);
        // if (hashedPassword != Encryption(password + SALT))
        // {
        //     LoginInputFields.ResultText.text = "아이디와 비밀번호를 확인해주세요";
        //     return;
        // }
        // // 4. 맞다면 로그인
        // else
        // {
        //     LoginInputFields.ResultText.text = "로그인 성공";
        //     // 로그인 성공 시 처리
        //     // LoadingScene으로 이동
    }
    
    // 아이디와 비밀번호 InputField 값이 바뀌었을 경우에만.
    public void LoginCheck()
    {
        string id = LoginInputFields.IDInputField.text;
        string password = LoginInputFields.PasswordInputField.text;

        LoginInputFields.ConfirmButton.enabled = !string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(password);
    }
}
