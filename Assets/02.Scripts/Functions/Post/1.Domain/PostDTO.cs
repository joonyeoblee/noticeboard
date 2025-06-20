using System.Collections.Generic;
using Firebase.Firestore;
[FirestoreData]
public class PostDTO
{
	[FirestoreDocumentId]
	public string PostID { get; set; }
	[FirestoreProperty]
	public string Email { get; private set; }
	[FirestoreProperty]
	public string Nickname { get; private set; }
	[FirestoreProperty]
	public string Content { get; private set; }
	[FirestoreProperty]
	public int ViewCount { get; private set; }
	[FirestoreProperty]
	public Timestamp PostTime { get; private set;}
	
	
	public List<LikeDTO> Likes { get; private set;} = new List<LikeDTO>(); // 좋아요 리스트 추가
	public int LikeCount => Likes.Count;
	public List<CommentDTO> Comments { get; private set;} = new List<CommentDTO>(); // 좋아요 리스트 추가
	public int CommentCount => Comments.Count;

	// 좋아요 추가하는 메소드
	public PostDTO()
	{

	}
	public PostDTO(string email, string nickname, string postID, string content, int viewCount, Timestamp postTime, List<LikeDTO> likes, List<CommentDTO> comments)
	{
		Email = email;
		Nickname = nickname;
		PostID = postID;
		Content = content;
		ViewCount = viewCount;
		PostTime = postTime;
		Likes = likes;
		Comments = comments;
	}

	public PostDTO(PostDTO postDto, List<LikeDTO> likes, List<CommentDTO> comments)
	{
		Email = postDto.Email;
		Nickname = postDto.Nickname;
		PostID = postDto.PostID;
		Content = postDto.Content;
		ViewCount = postDto.ViewCount;
		PostTime = postDto.PostTime;
		Likes = likes;
		Comments = comments;
	}

	public void RemoveLikeDto(LikeDTO likeDto)
	{
		Likes.Remove(likeDto);
	}
}
