using System.Collections.Generic;
using Firebase.Firestore;
public class Post
{
	public string Email { get; }
	public string Nickname { get; }
	public string PostID { get; }
	public string Content { get; }
	public int Like { get; }
	public int ViewCount { get; }
	public Timestamp PostTime { get; }
	public Post() { }


	// 댓글 리스트 
	public List<Comment> Comments = new List<Comment>();
	// 어카운트

	public Post(string email, string nickname, string postID, string content, int like = 0, int viewCount = 0)
		: this(email, nickname, postID, content, like, viewCount, Timestamp.GetCurrentTimestamp())
	{
	}

	public Post(string email, string nickname, string postID, string content, int like, int viewCount, Timestamp postTime)
	{
		Email = email;
		Nickname = nickname;
		PostID = postID;
		Content = content;
		Like = like;
		ViewCount = viewCount;
		PostTime = postTime;
	}
	public PostDTO ToDto()
	{
		return new PostDTO(Email, Nickname, PostID, Content, Like, ViewCount, PostTime);
	}
}
