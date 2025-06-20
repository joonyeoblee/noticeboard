using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
public class PostManager : Singleton<PostManager>
{
	private PostRepository _repository;
	private List<PostDTO> _posts;
	public List<PostDTO> Posts => _posts;
	public event Action OnDataChanged;
	
	public int limit;
	
	public GameObject Comments;
	private async void Start()
	{
		await FirebaseConnect.Instance.Initialization;

		_repository = new PostRepository(FirebaseConnect.Instance.Db);
		AccountDTO _accountDto = AccountManager.Instance.CurrentAccount;
	}

	public async Task TryAddPost(PostDTO postDto)
	{
		try
		{
			await _repository.AddPost(postDto);
			OnDataChanged?.Invoke();
		}
		catch (Exception e)
		{
			Debug.LogError($"Upload failed: {e.Message}");
			throw;
		}
	}

	public async Task<bool> TryAddLike(PostDTO postDto, Like like)
	{
		try
		{
			// 직접 추가로 UI 빠르게 갱신 및 초기화 비용 절약
			Post post = new Post(postDto);
			post.AddLike(like);
			// 기존 리스트에서 동일한 ID의 요소 찾아서 교체
			int index = _posts.FindIndex(p => p.PostID == post.PostID);
			if (index != -1)
			{
				_posts[index] = post.ToDto();
			}

			await _repository.AddLike(postDto, like.ToDto());
			OnDataChanged?.Invoke();
			return true;
		}
		catch (Exception e)
		{
			Debug.LogError($"Upload failed: {e.Message}");
			return false;
		}
	}
	public async Task<bool> TryUpdatePost(Post post)
	{
		try
		{
			await _repository.UpdatePost(post.ToDto());
			OnDataChanged?.Invoke();
			return true;
		}
		catch (Exception e)
		{
			Debug.LogError($"Modify failed: {e.Message}");
			return false;
		}
	}

	public async Task OpenComments()
	{
		_posts = await _repository.GetPosts(0, limit);
		OnDataChanged?.Invoke();
	}

	public async Task<bool> TryRemoveLike(PostDTO postDto, LikeDTO likeDto)
	{
		try
		{
			postDto.RemoveLikeDto(likeDto);
			_repository.RemoveLike(postDto, likeDto);
			
			OnDataChanged?.Invoke();
			return true;
		}
		catch (Exception e)
		{
			Debug.LogError($"Upload failed: {e.Message}");
			return false;
		}
	}
}
