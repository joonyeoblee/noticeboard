using System;
using UnityEngine;
public class AccountManager : Singleton<AccountManager>
{
    private Account _myAccount;

    // public AccountDTO CurrentAccount => _myAccount.ToDTO();
    public AccountDTO CurrentAccount;
    private const string SALT = "903872727";

    private readonly AccountRepository _repository = new AccountRepository();

    // public Result TryRegister(string email, string nickname, string password)
    // {
    //     AccountSaveData accountDto = _repository.Find(email);
    //     if (accountDto != null)
    //     {
    //         return new Result(false, "이미 가입한 이메일입니다");
    //     }
    //
    //     // TODO: 매니저로 빼야함
    //
    //     string encrypted = CryptoUtil.EncryptAES(password, SALT);
    //     Account account = new Account(email, nickname, encrypted);
    //
    //     // 레포 저장
    //     _repository.Save(account.ToDTO());
    //
    //     return new Result(true);
    // }
    // public bool TryLogin(string email, string password)
    // {
    //     string encrypted = CryptoUtil.EncryptAES(password, SALT);
    //
    //     AccountSaveData accountDto = _repository.Find(email);
    //     if (accountDto == null)
    //     {
    //         Debug.Log($"[AccountManager] Login failed: Email not found for {email}");
    //         return false;
    //     }
    //     if (accountDto.Password == encrypted)
    //     {
    //         _myAccount = new Account(accountDto.Email, accountDto.Nickname, accountDto.Password);
    //         CurrentAccount = _myAccount.ToDTO();
    //
    //         Debug.Log($"[AccountManager] Login successful for {email}. Loading attendance data.");
    //
    //
    //         return true;
    //     }
    //     Debug.Log($"[AccountManager] Login failed: Incorrect password for {email}");
    //     return false;
    // }
}
