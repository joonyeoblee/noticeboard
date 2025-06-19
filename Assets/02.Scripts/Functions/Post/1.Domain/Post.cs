using System.Collections.Generic;
using Firebase.Firestore;
[FirestoreData]
public class Post
{
	[FirestoreProperty]
	public string Email { get; set; }
	[FirestoreProperty]
	public string Nickname { get; set; }
	[FirestoreProperty]
	public string PostID { get; set; }
	[FirestoreProperty]
	public string Content { get; set; }
	[FirestoreProperty]
	public int Like { get; set; }
	[FirestoreProperty]
	public int ViewCount { get; set; }
	[FirestoreProperty]
	public Timestamp PostTime { get; set; }

	// 댓글 리스트 
	public List<Comment> Comments = new List<Comment>();
	// 어카운트
	

}
