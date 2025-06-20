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
	public IReadOnlyList<Like> Likes => _likes;
	private readonly List<Comment> _comments = new List<Comment>();
	public IReadOnlyList<Comment> Comments => _comments; // Comments도 동일하게 적용
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
			_comments.Add(commentDto.ToDomain()); // Comments 필드 대신 _comments 사용
		}
	}
	public PostDTO ToDto()
	{
		List<LikeDTO> likeDTOs = new List<LikeDTO>();
		List<CommentDTO> commentDtos = new List<CommentDTO>();

		foreach (Like like in _likes) // _likes 필드를 사용
		{
			LikeDTO likeDTO = like.ToDto();
			likeDTOs.Add(likeDTO);
		}

		foreach (Comment comment in _comments) // _comments 필드를 사용
		{
			CommentDTO commentDTO = comment.ToDto();
			commentDtos.Add(commentDTO);
		}

		// PostDTO 생성자에 LikeCount를 전달해야 할 경우:
		// return new PostDTO(Email, Nickname, PostID, Content, _likes.Count, ViewCount, PostTime, likeDTOs, commentDtos);
		// PostDTO에 LikeCount 속성이 있다면 이렇게 LikeCount를 계산해서 전달합니다.
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
	public void RemoveLike(Like likeToRemove)
	{
		if (likeToRemove == null)
		{
			return;
		}

		Like foundLike = _likes.Find(l => l.Email == likeToRemove.Email);

		if (foundLike != null)
		{
			_likes.Remove(foundLike);
		}

	}
}
