using System.Collections.Generic;
using Firebase.Firestore;
public class Post
{
	public readonly Account Account;
	public readonly string PostID;
	public readonly string Content;
	public readonly int Like;
	public readonly int ViewCount;
	public readonly Timestamp PostTime;

	// 댓글 리스트 
	public List<Comment> Comments = new List<Comment>();
	// 어카운트
	public Post(string content, int like, int viewCount, Timestamp postTime)
	{
		Content = content;
		Like = like;
		ViewCount = viewCount;
		PostTime = postTime;
	}

	public Post(string id, Dictionary<string, object> data)
	{
		PostID = id;
		if (data.TryGetValue("content", out object content)) Content = content.ToString();
		if (data.TryGetValue("Like", out object like)) Like = int.Parse(like.ToString());
		if (data.TryGetValue("ViewCount", out object viewCount)) ViewCount = int.Parse(viewCount.ToString());
		if (data.TryGetValue("PostTime", out object postTime) && postTime is Timestamp timestamp)
		{
			PostTime = timestamp;
		}

	}
}
