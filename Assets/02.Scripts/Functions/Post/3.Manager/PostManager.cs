using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
public class PostManager : Singleton<PostManager>
{
	private PostRepository _repository;
	private List<PostDTO> _posts;
	public List<PostDTO> Posts => _posts;
	public Action OnDataChanged;

	public GameObject Comments;
	private async void Start()
	{
		await FirebaseConnect.Instance.Initialization;

		_repository = new PostRepository(FirebaseConnect.Instance.Db);
		
		// _posts = await _repository.LoadPosts();
		// Debug.Log(_posts[0].Likes[0].Email);
	}

	public async Task TryAddPost(Post post)
	{
		try
		{
			await _repository.AddPost(post.ToDto());
			OnDataChanged?.Invoke();
		}
		catch (Exception e)
		{
			Debug.LogError($"Upload failed: {e.Message}");
			throw;
		}
	}
	
	public async Task<bool> TryAddLike(Like like)
	{
		try
		{
			await _repository.Addlike(like.ToDto());
			OnDataChanged?.Invoke();
			return true;
		}
		catch (Exception e)
		{
			Debug.LogError($"Upload failed: {e.Message}");
			return false;
		}
	}

	public async Task OpenComments()
	{
		await _repository.GetPosts(0, 3);
	}
}
