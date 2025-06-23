using System;
using Firebase.Firestore;
using TMPro;
using UnityEngine;
public class UI_PostWrite : MonoBehaviour
{
	public TMP_InputField ContentInputField;

	public async void WritePost()
	{
		AccountDTO accountDto = AccountManager.Instance.CurrentAccount;
		string Time = Timestamp.GetCurrentTimestamp().ToString();

		// 명세
		PostContentSpecification postSpecification = new PostContentSpecification();
		if (!postSpecification.IsSatisfiedBy(ContentInputField.text))
		{
			throw new Exception(postSpecification.ErrorMessage);
		}
		AccountEmailSpecification accountEmailSpecification = new AccountEmailSpecification();
		if (!accountEmailSpecification.IsSatisfiedBy(accountDto.Email))
		{
			throw new Exception(accountEmailSpecification.ErrorMessage);
		}

		AccountNicknameSpecification accountNicknameSpecification = new AccountNicknameSpecification();
		if (!accountNicknameSpecification.IsSatisfiedBy(accountDto.Nickname))
		{
			throw new Exception(accountNicknameSpecification.ErrorMessage);
		}

		Post post = new Post(accountDto.Email, accountDto.Nickname,$"{accountDto.Email}_{Time}",ContentInputField.text);
		await PostManager.Instance.TryAddPost(post.ToDto());
		
		gameObject.SetActive(false);
		await PostManager.Instance.OpenComments();
	}
	
}
