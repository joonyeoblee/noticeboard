using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using _02.Scripts.Functions.Post._1.Domain;
using UnityEngine;
public class AccountRepository 
{

    // public async Task AddAccount(AccountDTO post)
    // {
    //     
    // }
    // public async Task<List<AccountDTO>> GetAccounts(int start, int limit)
    // {
    //     
    // }
    // public async Task<AccountDTO> GetPost(string postId)
    // {
    //     
    // }
    // public async Task UpdatePost(AccountDTO post)
    // {
    //     
    // }
    // public async Task DeletePost(string postId)
    // {
    //     
    // }
    // private const string SAVE_KEY = nameof(AccountRepository);

    // public void Save(AccountDTO accountDto)
    // {
    //     AccountSaveData data = new AccountSaveData(accountDto.Email, accountDto.Nickname, accountDto.Password);
    //     string json = JsonUtility.ToJson(data);
    //
    //     PlayerPrefs.SetString(SAVE_KEY + data.Email, json);
    // }

    // public AccountSaveData Find(string email)
    // {
    //     if (!PlayerPrefs.HasKey(SAVE_KEY + email))
    //     {
    //         return null;
    //     }
    //
    //     return JsonUtility.FromJson<AccountSaveData>(PlayerPrefs.GetString(SAVE_KEY + email));
    // }

    // public AccountDTO Load(string email)
    // {
    //     if (!PlayerPrefs.HasKey(SAVE_KEY + email))
    //     {
    //         return null;
    //     }
    //     return JsonUtility.FromJson<AccountDTO>(PlayerPrefs.GetString(SAVE_KEY + email));
    // }
}
