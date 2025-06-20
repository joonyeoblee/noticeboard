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
	public int LikeCount { get; private set; }
	[FirestoreProperty]
	public int ViewCount { get; private set; }
	[FirestoreProperty]
	public Timestamp PostTime { get; private set;}
	public List<LikeDTO> Likes { get; private set;} = new List<LikeDTO>(); // 좋아요 리스트 추가
	[FirestoreProperty]
	public List<CommentDTO> Comments { get; private set;} = new List<CommentDTO>(); // 좋아요 리스트 추가

	// 좋아요 추가하는 메소드
	public void AddLike(LikeDTO like)
	{
		Likes.Add(like);
		LikeCount = Likes.Count;
	}

	public void AddComment(CommentDTO comment)
	{
		Comments.Add(comment);
	}
	public PostDTO()
	{

	}
	public PostDTO(string email, string nickname, string postID, string content, int likeCount, int viewCount, Timestamp postTime)
	{
		Email = email;
		Nickname = nickname;
		PostID = postID;
		Content = content;
		LikeCount = likeCount;
		ViewCount = viewCount;
		PostTime = postTime;
		
	}

	public void RemoveLike(LikeDTO likeDto)
	{
		Likes.Remove(likeDto);
		LikeCount = Likes.Count;
	}
}
