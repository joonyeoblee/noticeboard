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
	public Timestamp PostTime { get; private set; }
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
