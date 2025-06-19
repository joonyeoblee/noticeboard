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
	public string LikeID { get; set; }

	public LikeDTO()
	{
		
	}	

	public LikeDTO(string email, string nickname, string targetID, string likeID, int targetType)
	{
		Email = email;
		Nickname = nickname;
		TargetID = targetID;
		LikeID = likeID;
		TargetType = targetType;
	}
}
