using System;
using System.Threading.Tasks;
using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Firestore;
using UnityEngine;
public class AccountManager : Singleton<AccountManager>
{
    private AccountRepository _repository;

    public AccountDTO CurrentAccount = new AccountDTO("123@123123.com","어쩔껀데");

    private async void Start()
    {
        await FirebaseConnect.Instance.Initialization;
        
        _repository = new AccountRepository( FirebaseConnect.Instance.Db, FirebaseConnect.Instance.Auth);
        
    }

    public async Task Login(string email, string password)
    {
        try
        {
            await _repository.LoginAsync(email, password);
            Debug.Log($"[AccountManager] Login 성공 : {email}");

            // FirebaseUser에서 추가 정보 가져와서 Account 객체 초기화 (선택 사항)
            var user = FirebaseConnect.Instance.Auth.CurrentUser;
        }
        catch (Exception ex)
        {
            Debug.LogError($"[AccountManager] Login 실패 : {ex.Message}");
            throw; // 호출자가 처리할 수 있도록 예외 재던짐
        }
    }
    public async Task Register(string email, string nickname, string password)
    {
        try
        {
            CurrentAccount = await _repository.RegisterAsync(email, nickname, password);

            Debug.Log($"[AccountManager] Register 완료: {email}");
        }
        catch (Exception ex)
        {
            Debug.LogError($"[AccountManager] Register 실패: {ex.Message}");
            throw;
        }
    }
}
