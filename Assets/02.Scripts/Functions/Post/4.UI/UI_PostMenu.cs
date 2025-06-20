using System.Linq;
using UnityEngine;
public class UI_PostMenu : MonoBehaviour
{
	private PostDTO _post;
	AccountDTO account => AccountManager.Instance.CurrentAccount;

	public GameObject[] LikeImage;
	
	private bool _isLiked;
	private	LikeDTO _myLike;
	
	public void Refresh(PostDTO post)
	{
		_post = post;
		Debug.Log(post.Content);
		if (_post.Likes.Any(like => like.Email == account.Email))
		{
			LikeImage[0].SetActive(false);
			LikeImage[1].SetActive(true);
			_isLiked = true;
			_myLike = _post.Likes.FirstOrDefault(like => like.Email == account.Email);
			Debug.Log(_isLiked);
		}
		else
		{
			_isLiked = false;
			_myLike = null;
			LikeImage[0].SetActive(true);
			LikeImage[1].SetActive(false);
			Debug.Log(_isLiked);
		}
	
	}
	private async void Like()
	{
		Like like = new Like(account.Email, account.Nickname);

		if (await PostManager.Instance.TryAddLike(_post, like))
		{
			Debug.Log("Like added");
			LikeImage[0].SetActive(false);
			LikeImage[1].SetActive(true);


			_myLike = new LikeDTO(account.Email, account.Nickname);
		}
		else
		{
			Debug.LogError("Failed to add like");
		}
	}

	private async void UnLike()
	{
		if (await PostManager.Instance.TryRemoveLike(_post, _myLike))
		{
			Debug.Log("Like removed");
			LikeImage[0].SetActive(true);
			LikeImage[1].SetActive(false);

			_myLike = null;
		}
		else
		{
			Debug.LogError("Failed to remove like");
		}
	}



	// 버튼 클릭 시, isLike에 따라 Like 또는 UnLike 함수를 호출
	public void ToggleLike()
	{
		if (_isLiked)
		{
			UnLike();  // 현재 좋아요 상태이면 UnLike
		}
		else
		{
			Like();    // 현재 비활성화된 상태이면 Like
		}
	}
}
