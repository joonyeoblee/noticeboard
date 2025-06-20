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
	
	private PostDTO _post;
	private void Start()
	{
	}
	public void Refresh(PostDTO postDto)
	{
		_post = postDto;
		foreach (UI_PostMenu ui_PostMenu in UI_PostMenus)
		{
			ui_PostMenu.Refresh(_post);
		}

		PosterNameTextUI.text = _post.Nickname;
		PosterTimestampTextUI.text = _post.PostTime.ToString();
		ContentTextUI.text = _post.Content;
		LikeAndCommentTextUI.text = $"좋아요 {_post.Likes.Count}, 댓글 {_post.Comments.Count}";
	}

	public void OnClickModifyButton()
	{
		UI_Canvas canvas = gameObject.GetComponentInParent<UI_Canvas>();

		if (canvas == null) return;

		canvas.ModifyObject(_post.PostID);
	}
}
