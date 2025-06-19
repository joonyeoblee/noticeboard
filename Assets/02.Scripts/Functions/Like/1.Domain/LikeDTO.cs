using Firebase.Firestore;
[FirestoreData]
public class LikeDTO
{
	[FirestoreProperty]
	public string Email { get; set; }
	
	[FirestoreProperty]
	public string Nickname{ get; set; }
	
	[FirestoreProperty]
	public string TargetID{ get; set; }
	
	[FirestoreProperty]
	public int TargetType{ get; set; }
	
	[FirestoreProperty]
	public bool IsLiked{ get; set; }
}
