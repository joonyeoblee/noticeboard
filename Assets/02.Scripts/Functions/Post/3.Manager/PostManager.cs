using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
public class PostManager : Singleton<PostManager>
{
	private PostRepository _repository;
	public List<PostDTO> PostDtos { get; set; }
	public event Action OnDataChanged;
	public int Limit { get; } = 10;
	
	private async void Start()
	{
		await FirebaseConnect.Instance.Initialization;
		_repository = new PostRepository(FirebaseConnect.Instance.Db);
	}

	public async Task TryAddPost(PostDTO postDto)
	{
		await _repository.AddPost(postDto);
		OnDataChanged?.Invoke();
	}

	public async Task<bool> TryAddLike(PostDTO postDto, Like like)
	{
		Post post = new Post(postDto);
		post.AddLike(like);

		int index = PostDtos.FindIndex(p => p.PostID == post.PostID);
		if (index != -1)
		{
			PostDtos[index] = post.ToDto();
		}

		OnDataChanged?.Invoke();

		return await _repository.AddLike(postDto, like.ToDto());
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
		Post post = new Post(postDto);
		post.RemoveLike(likeDto.ToDomain());
		int index = PostDtos.FindIndex(p => p.PostID == post.PostID);
		if (index != -1)
		{
			PostDtos[index] = post.ToDto();
			Debug.LogError(PostDtos[index].LikeCount); // Log the updated count
		}
		
		OnDataChanged?.Invoke();
		return await _repository.RemoveLike(postDto, likeDto);
	}

	public PostDTO GetPostById(string postId)
	{
		return PostDtos?.Find(p => p.PostID == postId);
	}
}
