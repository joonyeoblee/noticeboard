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
	
		if (_post.Likes.Any(like => like.Email == account.Email))
		{
			LikeImage[0].SetActive(false);
			LikeImage[1].SetActive(true);
			_isLiked = true;
			_myLike = _post.Likes.FirstOrDefault(like => like.Email == account.Email);
		}
		else
		{
			_isLiked = false;
			_myLike = null;
			LikeImage[0].SetActive(true);
			LikeImage[1].SetActive(false);
		}
	
	}
	private async void Like()
	{
		// LikeDTO 생성
		Like like = new Like(account.Email, account.Nickname);
		
		// 좋아요 추가 (PostManager를 통해)
		if (await PostManager.Instance.TryAddLike(_post, like))
		{
			// UI 반영: 좋아요 이미지 변경
			Debug.Log("Like added");
			LikeImage[0].SetActive(false);  // 비활성화된 UnLike 이미지
			LikeImage[1].SetActive(true);   // 활성화된 Like 이미지
		}
		else
		{
			// 실패 처리
			Debug.LogError("Failed to add like");
		}
	}

	private async void UnLike()
	{
		// UnLike 처리를 위한 로직
		if (await PostManager.Instance.TryRemoveLike(_post, _myLike))
		{
			// UI 반영: 좋아요 이미지 변경
			Debug.Log("Like removed");
			LikeImage[0].SetActive(true);   // 활성화된 UnLike 이미지
			LikeImage[1].SetActive(false);  // 비활성화된 Like 이미지
		}
		else
		{
			// 실패 처리
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
