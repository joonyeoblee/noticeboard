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

    public async Task<AccountDTO> RegisterAsync(string email, string nickname, string password)
    {
        try
        {
            // FirebaseAuth: 인증 계정 생성
            AuthResult result = await _auth.CreateUserWithEmailAndPasswordAsync(email, password);
            FirebaseUser user = result.User;
            Debug.Log($"[AccountRepository] FirebaseAuth 회원가입 성공: {user.UserId}");

            // DisplayName 업데이트 (선택)
            UserProfile profile = new UserProfile { DisplayName = nickname };
            await user.UpdateUserProfileAsync(profile);

            // Firestore: 비밀번호 없이 이메일과 닉네임만 저장
            var accountDto = new AccountDTO(email, nickname);

            await _db.Collection("Account").Document(email).SetAsync(accountDto);
            Debug.Log("[AccountRepository] Firestore 사용자 정보 저장 성공");
            return accountDto;
        }
        catch (Exception ex)
        {
            Debug.LogError($"[AccountRepository] 회원가입 실패: {ex.Message}");
            throw;
        }
        
    }
    public async Task LoginAsync(string email, string password)
    {
        AuthResult result = await _auth.SignInWithEmailAndPasswordAsync(email, password);
        Debug.Log($"로그인 성공 : {result.User.DisplayName}");
        
    }
}
