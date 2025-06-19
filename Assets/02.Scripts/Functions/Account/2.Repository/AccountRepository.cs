using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Firestore;
using UnityEngine;
public class AccountRepository 
{
    private readonly FirebaseFirestore _db;
    private readonly FirebaseAuth _auth;

    public AccountRepository(FirebaseFirestore db, FirebaseAuth auth)
    {
        _db = db;
        _auth = auth;
    }

    public async Task AddAsync(AccountDTO accountDTO)
    {
        
    }
    public async Task RegisterAsync(string email, string password)
    {
        FirebaseConnect.Instance.Auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task => {
            if (task.IsCanceled || task.IsFaulted) {
                Debug.LogError($"회원가입에 실패했습니다.{task.Exception.Message}");
                return;
            }
            
            AuthResult result = task.Result;
            Debug.LogFormat("회원가입에 성공했습니다. {0} ({1})",
                result.User.DisplayName, result.User.UserId);
            return;
        });
    }
    public async Task LoginAsync(string email, string password)
    {
        AuthResult result = await _auth.SignInWithEmailAndPasswordAsync(email, password);
        Debug.Log($"로그인 성공 : {result.User.DisplayName}");
        
    }
}
