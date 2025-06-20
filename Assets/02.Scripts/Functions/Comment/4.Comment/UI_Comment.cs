using TMPro;
using UnityEngine;

public class UI_Comment : MonoBehaviour
{
	public TextMeshProUGUI NameTextUI;
	public TextMeshProUGUI ContentTextUI;
	
	public TextMeshProUGUI LikeCountTextUI;
	
	private CommentDTO _commentDto;
	public void Refresh(CommentDTO commentDTO)
	{
		_commentDto = commentDTO;
		NameTextUI.text = commentDTO.Nickname;
		ContentTextUI.text = commentDTO.Content;
		
	}
}
