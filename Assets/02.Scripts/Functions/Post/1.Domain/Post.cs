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
	public List<LikeDTO> Likes { get; } = new List<LikeDTO>(); // 좋아요 리스트 추가

	public List<CommentDTO> Comments { get; } = new List<CommentDTO>(); // 좋아요 리스트 추가
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

		PostContentSpecification contentSpecification = new PostContentSpecification();
		if (!contentSpecification.IsSatisfiedBy(content))
		{
			throw new Exception(contentSpecification.ErrorMessage);
		}
		
		PostViewCountSpecification viewCountSpecification = new PostViewCountSpecification();
		if (!viewCountSpecification.IsSatisfiedBy(viewCount))
		{
			throw new Exception(viewCountSpecification.ErrorMessage);
		}
		PostLikeCountSpecification likeSpecification = new PostLikeCountSpecification();
		if (!likeSpecification.IsSatisfiedBy(like))
		{
			throw new Exception(likeSpecification.ErrorMessage);
		}

		if (string.IsNullOrEmpty(postID))
		{
			throw new Exception("PostID는 비어있을 수 없습니다");
		}

		
		

		Email = email;
		Nickname = nickname;
		PostID = postID;
		Content = content;
		ViewCount = viewCount;
		PostTime = postTime;
	}
	public PostDTO ToDto()
	{
		return new PostDTO(Email, Nickname, PostID, Content, ViewCount, PostTime);
	}

}
