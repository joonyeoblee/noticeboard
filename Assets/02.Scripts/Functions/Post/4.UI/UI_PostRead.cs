using TMPro;
using UnityEngine;
public class UI_PostRead : MonoBehaviour
{
	private PostDTO _postDto;
	public UI_Post UI_Post;
	public TMP_InputField CommentContent;
	public void Refresh(PostDTO postDto)
	{
		_postDto = postDto;
		UI_Post.Refresh(_postDto);
	}

	public async void WriteComment()
	{

	}
}
