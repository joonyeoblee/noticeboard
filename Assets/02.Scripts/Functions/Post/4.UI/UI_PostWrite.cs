using Firebase.Auth;
using Firebase.Firestore;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_PostWrite : MonoBehaviour
{
	public TMP_InputField ContentInputField;

	public async void WritePost()
	{
		AccountDTO accountDto = AccountManager.Instance.CurrentAccount;
		string Time = Timestamp.GetCurrentTimestamp().ToString();
		Post post = new Post(accountDto.Email, accountDto.Nickname,$"{accountDto.Email}_{Time}",ContentInputField.text);
		await PostManager.Instance.TryAddPost(post.ToDto());
	}
	
}
