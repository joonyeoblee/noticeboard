﻿using Firebase.Firestore;
[FirestoreData]
public class AccountDTO
{
    [FirestoreProperty]
    public string Email { get; }
    
    [FirestoreProperty]
    public string Nickname { get; }


    public AccountDTO()
    {
        
    }
    public AccountDTO(string email, string nickname)
    {
        Email = email;
        Nickname = nickname;
    }

    // Account 도메인 객체로부터 DTO 생성
    public static AccountDTO FromDomain(Account account)
    {
        return new AccountDTO(account.Email, account.Nickname);
    }

    // Account 도메인 객체로 변환 (필요시)
    public Account ToDomain()
    {
        return new Account(Email, Nickname);
    }
}
