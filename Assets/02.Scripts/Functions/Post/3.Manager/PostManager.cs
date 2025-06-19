public class PostManager : Singleton<PostManager>
{
	private PostRepository _repository;

	private async void Start()
	{
		await FirebaseConnect.Instance.Initialization;

		PostRepository repo = new PostRepository(FirebaseConnect.Instance.Db);
		await repo.LoadPosts();
	}
}
