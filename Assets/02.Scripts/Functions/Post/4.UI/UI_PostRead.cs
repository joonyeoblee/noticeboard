using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class UI_PostRead : MonoBehaviour
{
	private PostDTO _postDto;
	public UI_Post UI_Post;
	public TMP_InputField CommentContentInputField;

	public GameObject CommentPrefab;
	public GameObject CommentContainer;

	public List<UI_Comment> UI_Comments = new List<UI_Comment>();

	private void Start()
	{
		CommentManager.Instance.OnDataChanged += Refresh;
	}

	public void Refresh(PostDTO postDto)
	{
		if (postDto == null) return;

		_postDto = postDto;
		UI_Post.Refresh(_postDto);

		int count = postDto.CommentCount;

		EnsureCommentSlots(count); // 필요한 만큼 보장

		for (int i = 0; i < UI_Comments.Count; i++)
		{
			if (i < count)
			{
				UI_Comments[i].gameObject.SetActive(true);
				UI_Comments[i].Refresh(postDto.Comments[i]);
			}
			else
			{
				UI_Comments[i].gameObject.SetActive(false);
			}
		}
	}

	private void EnsureCommentSlots(int requiredCount)
	{
		while (UI_Comments.Count < requiredCount)
		{
			GameObject go = Instantiate(CommentPrefab, CommentContainer.transform);
			UI_Comment uiComment = go.GetComponent<UI_Comment>();
			UI_Comments.Add(uiComment);
		}
	}

	public async void WriteComment()
	{
		var accountDto = AccountManager.Instance.CurrentAccount;
		var comment = new Comment(accountDto.Email, accountDto.Nickname, CommentContentInputField.text);

		if (await CommentManager.Instance.TryWriteComment(_postDto, comment))
		{
			// 코멘트 리스트 갱신되었을 테니 Refresh
			Refresh(_postDto);
			CommentContentInputField.text = "";
		}
	}
}
