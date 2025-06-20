using System;
using System.Collections.Generic;
using Firebase.Firestore;
public class Post
{
	public string Email { get; }
	public string Nickname { get; }
	public string PostID { get; }
	public string Content { get; }
	public int ViewCount { get; }
	public Timestamp PostTime { get; }
	private readonly List<Like> _likes = new List<Like>();
	public IReadOnlyList<Like> Likes { get; } = new List<Like>();
	private readonly List<Comment> _comments = new List<Comment>();
	public IReadOnlyList<Comment> Comments => _comments;
	public Post() { }

	public Post(string email, string nickname, string postID, string content, int like = 0, int viewCount = 0)
		: this(email, nickname, postID, content, like, viewCount, Timestamp.GetCurrentTimestamp())
	{
	}

	public Post(string email, string nickname, string postID, string content, int like, int viewCount, Timestamp postTime)
	{
		AccountEmailSpecification accountEmailSpecification = new AccountEmailSpecification();
		if (!accountEmailSpecification.IsSatisfiedBy(email))
		{
			throw new Exception(accountEmailSpecification.ErrorMessage);
		}
		
		AccountNicknameSpecification accountNicknameSpecification = new AccountNicknameSpecification();
		if (!accountNicknameSpecification.IsSatisfiedBy(nickname))
		{
			throw new Exception(accountNicknameSpecification.ErrorMessage);
		}

		if (string.IsNullOrEmpty(postID))
		{
			throw new Exception("PostID는 비어있을 수 없습니다");
		}

		if (string.IsNullOrEmpty(content))
		{
			throw new Exception("content은 비어있을 수 없습니다");
		}

		if (like < 0)
		{
			throw new Exception("like수는 0보다 작을 수 없습니다");
		}

		if (viewCount < 0)
		{
			throw new Exception("viewCount는 0보다 작을 수 없습니다");
		}
		Email = email;
		Nickname = nickname;
		PostID = postID;
		Content = content;
		ViewCount = viewCount;
		PostTime = postTime;
	}
	public Post(PostDTO postDto)
	{
		if (postDto == null)
		{
			throw new ArgumentNullException(nameof(postDto));
		}
		Email = postDto.Email;
		Nickname = postDto.Nickname;
		PostID = postDto.PostID;
		Content = postDto.Content;
		ViewCount = postDto.ViewCount;
		PostTime = postDto.PostTime;
		foreach (LikeDTO likeDto in postDto.Likes)
		{
			_likes.Add(likeDto.ToDomain());
		}
		foreach (CommentDTO commentDto in postDto.Comments)
		{
			_comments.Add(commentDto.ToDomain());
		}
	}
	public PostDTO ToDto()
	{
		List<LikeDTO> likeDTOs = new List<LikeDTO>();
		List<CommentDTO> commentDtos = new List<CommentDTO>();

		foreach (Like like in _likes)
		{
			LikeDTO likeDTO = like.ToDto();
			likeDTOs.Add(likeDTO);
		}

		foreach (Comment comment in _comments)
		{
			CommentDTO commentDTO = comment.ToDto();
			commentDtos.Add(commentDTO);
		}

		return new PostDTO(Email, Nickname, PostID, Content, ViewCount, PostTime, likeDTOs, commentDtos);
	}

	public void AddLike(Like like)
	{
		_likes.Add(like);
	}

	public void AddComment(Comment comment)
	{
		_comments.Add(comment);
	}

}
