using System;
using System.Threading.Tasks;
using UnityEngine;
public class UI_PostMenu : MonoBehaviour
{
	private PostDTO _post;
	private readonly ETargetType _targetType = ETargetType.Post;
	public void Refresh(PostDTO post)
	{
		_post = post;
	}
	public async void Like()
	{
		AccountDTO account = AccountManager.Instance.CurrentAccount;
		string LikeId = Guid.NewGuid().ToString();

		Like like = new Like(account.Email, account.Nickname, _post.PostID, LikeId, (int)_targetType);
		if (await PostManager.Instance.TryAddLike(like))
		{
			// 좋아요 버튼 색칠
		}

		// 실패 알람
	}
	
}
