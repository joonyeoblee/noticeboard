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
	
	public LikeDTO likeDto;
	
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

	public async Task<bool> TryAddLike(LikeDTO likeDto, PostDTO postDto)
	{
		try
		{
			// 직접 추가로 UI 빠르게 갱신 및 초기화 비용 절약
			postDto.AddLikeDto(likeDto);
			await _repository.AddLike(likeDto, postDto);
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

	public async Task<bool> TryRemoveLike(LikeDTO likeDto, PostDTO postDto)
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
