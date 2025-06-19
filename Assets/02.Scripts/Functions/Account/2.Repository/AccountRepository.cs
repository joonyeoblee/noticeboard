using System;
using UnityEngine;
public class AccountRepository 
{
    private const string SAVE_KEY = nameof(AccountRepository);

    public void Save(AccountDTO accountDto)
    {
        AccountSaveData data = new AccountSaveData(accountDto.Email, accountDto.Nickname, accountDto.Password);
        string json = JsonUtility.ToJson(data);

        PlayerPrefs.SetString(SAVE_KEY + data.Email, json);
    }

    public AccountSaveData Find(string email)
    {
        if (!PlayerPrefs.HasKey(SAVE_KEY + email))
        {
            return null;
        }

        return JsonUtility.FromJson<AccountSaveData>(PlayerPrefs.GetString(SAVE_KEY + email));
    }

    public AccountDTO Load(string email)
    {
        if (!PlayerPrefs.HasKey(SAVE_KEY + email))
        {
            return null;
        }
        return JsonUtility.FromJson<AccountDTO>(PlayerPrefs.GetString(SAVE_KEY + email));
    }
}

[Serializable]
public class AccountSaveData
{
    public string Email;
    public string Nickname;
    public string Password;

    public AccountSaveData(string email, string nickname, string password)
    {
        Email = email;
        Nickname = nickname;
        Password = password;
    }
}
