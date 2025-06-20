using System;
using System.Threading.Tasks;
using Firebase.Firestore;
using UnityEngine;
public class CommentRepository
{
	private readonly FirebaseFirestore _db;

	public CommentRepository(FirebaseFirestore db)
	{
		_db = db;
	}
	public async Task<bool> AddComment(PostDTO postDto, CommentDTO CommentDto)
	{
		try
		{
			// postId 문서의 하위 컬렉션 'Comment'에 CommentId를 문서 ID로 사용
			DocumentReference CommentRef = _db.Collection("Post").Document(postDto.PostID)
				.Collection("Comment").Document();

			await CommentRef.SetAsync(CommentDto);
			return true;
		}
		catch (Exception e)
		{
			Debug.LogError($"Failed to add Comment: {e.Message}");
			return false;
		}
	}
	public async Task RemoveComment(PostDTO postDto, CommentDTO CommentDto)
	{
		try
		{
			// 'Post' 컬렉션에서 해당 postId의 문서의 서브컬렉션 'Comment' 가져오기
			DocumentReference CommentRef = _db.Collection("Post")
				.Document(postDto.PostID)
				.Collection("Comment").Document(CommentDto.CommentID);

			await CommentRef.DeleteAsync();
		}
		catch (Exception e)
		{
			Debug.LogError($"Failed to remove Comment: {e.Message}");
			throw;
		}
	}
}
