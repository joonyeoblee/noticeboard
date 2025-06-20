using Firebase.Firestore;
public class Comment
{
	public string Email;
	public string Nickname;
	public string PostID;
	public string CommentID;
	public string Content;
	public Timestamp CommentTime;

	public Comment(string email, string nickname, string postID, string commentId, string content)
	{
		Email = email;
		Nickname = nickname;
		PostID = postID;
		CommentID = commentId;
		Content = content;
	}
}
