using Firebase.Firestore;
[FirestoreData]
public class LikeDTO
{
	[FirestoreDocumentId]
	public string LikeID { get; set; }
	[FirestoreProperty]
	public string Email { get; set; }
	
	[FirestoreProperty]
	public string Nickname{ get; set; }

	public LikeDTO()
	{

	}

	public LikeDTO(string email, string nickname)
	{
		Email = email;
		Nickname = nickname;
	}

	public Like ToDomain()
	{
		return new Like(Email, Nickname);
	}
}
