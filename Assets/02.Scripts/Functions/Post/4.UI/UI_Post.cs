using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UI_Post : MonoBehaviour
{
	// 좋아요 버튼, 댓글쓰기 버튼
	public List<UI_PostMenu> UI_PostMenus = new List<UI_PostMenu>();
	
	[Header("유저 정보 영역")]
	public TextMeshProUGUI PosterNameTextUI;
	public TextMeshProUGUI PosterTimestampTextUI;
	public Button DropdownButton;
	
	[Header("작성 영역")]
	public TextMeshProUGUI ContentTextUI;
	public TextMeshProUGUI LikeAndCommentTextUI;
	
	public GameObject CommentPanel;
	
	private PostDTO _post;

	public void Refresh(PostDTO postDto)
	{
		_post = postDto;
		foreach (UI_PostMenu ui_PostMenu in UI_PostMenus)
		{
			ui_PostMenu.Refresh(_post);
		}

		PosterNameTextUI.text = _post.Nickname;
		PosterTimestampTextUI.text = DisplayTimestamp.GetPrettyTimeAgo(_post.PostTime);
		ContentTextUI.text = _post.Content;
		if (LikeAndCommentTextUI != null)
		{
			Debug.Log($"{_post.Likes.Count} Count");
			LikeAndCommentTextUI.text = $"좋아요 {_post.Likes.Count}, 댓글 {_post.Comments.Count}";
		}


		bool isMyPost = _post.Email == AccountManager.Instance.CurrentAccount.Email;
		DropdownButton.gameObject.SetActive(isMyPost);
		
	}

	public void OnClickMenuButton()
	{
		GameObject Popup = UI_Canvas.Instance.Popup;
		Popup.SetActive(true);
		Popup.transform.position = DropdownButton.gameObject.transform.position;
		Popup.GetComponent<UI_MenuPopup>().Refresh(_post);
	}

	public void OnClickCommentButton()
	{
		CommentPanel.SetActive(true);
		CommentPanel.GetComponent<UI_PostRead>().Refresh(_post);
	}
}
