using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
public class PostManager : Singleton<PostManager>
{
	private PostRepository _repository;
	public List<PostDTO> PostDtos { get; private set; }
	public event Action OnDataChanged;
	public int Limit { get; } = 10;
	
	private async void Start()
	{
		await FirebaseConnect.Instance.Initialization;

		_repository = new PostRepository(FirebaseConnect.Instance.Db);
		
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

		Post post = new Post(postDto);
		post.AddLike(like);
		// 기존 리스트에서 동일한 ID의 요소 찾아서 교체
		int index = PostDtos.FindIndex(p => p.PostID == post.PostID);
		if (index != -1)
		{
			PostDtos[index] = post.ToDto();
			Debug.LogError(PostDtos[index].LikeCount);
		}

		await _repository.AddLike(postDto, like.ToDto());
		OnDataChanged?.Invoke();
		return true;
	
	}
	public async Task<bool> TryUpdatePost(Post post)
	{
		await _repository.UpdatePost(post.ToDto());
		OnDataChanged?.Invoke();
		return true;
	}

	public async Task OpenComments()
	{
		PostDtos = await _repository.GetPosts(0, Limit);
		
		OnDataChanged?.Invoke();
	}

	public async Task<bool> TryRemoveLike(PostDTO postDto, LikeDTO likeDto)
	{
		postDto.RemoveLikeDto(likeDto);
		_repository.RemoveLike(postDto, likeDto);

		OnDataChanged?.Invoke();
		return true;
	}
}
