using System;
using System.Threading.Tasks;
public class CommentManager : Singleton<CommentManager>
{
	private CommentRepository _repository;
	public event Action<PostDTO> OnDataChanged;

	private async void Start()
	{
		await FirebaseConnect.Instance.Initialization;

		_repository = new CommentRepository(FirebaseConnect.Instance.Db);

	}


	public async Task<bool> TryWriteComment(PostDTO postDto, Comment comment)
	{
		Post post = new Post(postDto);
		post.AddComment(comment);
		postDto = post.ToDto();
		await _repository.AddComment(postDto, comment.ToDto());
		OnDataChanged?.Invoke(postDto);
		return true;
	}
}
