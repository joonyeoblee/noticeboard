using System.Collections.Generic;
using Firebase.Firestore;
[FirestoreData]
public class PostDTO
{
	[FirestoreProperty]
	public string Email { get; private set; }
	[FirestoreProperty]
	public string Nickname { get; private set; }
	[FirestoreProperty]
	public string PostID { get; private set; }
	[FirestoreProperty]
	public string Content { get; private set; }
	[FirestoreProperty]
	public int Like { get; private set; }
	[FirestoreProperty]
	public int ViewCount { get; private set; }
	[FirestoreProperty]
	public Timestamp PostTime { get; }
	[FirestoreProperty]
	public List<LikeDTO> Likes { get; } = new List<LikeDTO>(); // 좋아요 리스트 추가
	[FirestoreProperty]
	public List<CommentDTO> Comments { get; } = new List<CommentDTO>(); // 좋아요 리스트 추가

	// 좋아요 추가하는 메소드
	public void AddLike(LikeDTO like)
	{
		Likes.Add(like);
	}

	public void AddComment(CommentDTO comment)
	{
		Comments.Add(comment);
	}
	public PostDTO()
	{

	}
	public PostDTO(string email, string nickname, string postID, string content, int like, int viewCount, Timestamp postTime)
	{
		Email = email;
		Nickname = nickname;
		PostID = postID;
		Content = content;
		Like = like;
		ViewCount = viewCount;
		PostTime = postTime;
		
	}
	
}
