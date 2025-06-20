using UnityEngine;
public class UI_PostRead : MonoBehaviour
{
	private PostDTO _postDto;
	public UI_Post UI_Post;
	public void Refresh(PostDTO postDto)
	{
		_postDto = postDto;
		UI_Post.Refresh(_postDto);
	}
}
