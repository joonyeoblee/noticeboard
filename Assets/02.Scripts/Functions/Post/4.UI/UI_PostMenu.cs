using System;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;
public class UI_PostMenu : MonoBehaviour
{
	private PostDTO _post;
	private readonly ETargetType _targetType = ETargetType.Post;
	
	public Button LikeButton;
	AccountDTO account => AccountManager.Instance.CurrentAccount;

	public GameObject[] LikeImage;
	
	private bool _isLiked = false;
	public void Refresh(PostDTO post)
	{
		_post = post;
		if (LikeImage.Count() == 0)
		{
			return;
		}
		if (_post.Likes.Any(like => like.Email == account.Email))
		{
			
			LikeImage[0].SetActive(false);
			LikeImage[1].SetActive(true);
			_isLiked = true;
		}
		else
		{
			LikeImage[0].SetActive(true);
			LikeImage[1].SetActive(false);
		}
	}
	public async void Like()
	{
		string LikeId = Guid.NewGuid().ToString();
		
		Like like = new Like(account.Email, account.Nickname, _post.PostID, LikeId, (int)_targetType);
		if (await PostManager.Instance.TryAddLike(like, _post.PostID))
		{
			// 좋아요 버튼 색칠
			Debug.Log("Like added");
 			LikeImage[0].SetActive(false);
			LikeImage[1].SetActive(true);
		}

		// 실패 알람
		Debug.Log("asdasd");
	}
	
}
