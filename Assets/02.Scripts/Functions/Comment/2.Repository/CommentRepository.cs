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
			// postId 문서의 하위 컬렉션 'Comment'에 새 문서 생성
			DocumentReference CommentRef = _db.Collection("Post").Document(postDto.PostID)
				.Collection("Comment").Document();

			// CommentID 설정 (Firebase에서 자동 생성된 ID 사용)
			CommentDto.CommentID = CommentRef.Id;
			
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

	// 댓글 좋아요 추가
	public async Task<bool> AddCommentLike(PostDTO postDto, CommentDTO commentDto, LikeDTO likeDto)
	{
		try
		{
			// null 체크 추가
			if (postDto?.PostID == null || commentDto?.CommentID == null || likeDto?.Email == null)
			{
				Debug.LogError("Required IDs are null - PostID, CommentID, or Email");
				return false;
			}

			DocumentReference likeRef = _db.Collection("Post").Document(postDto.PostID)
				.Collection("Comment").Document(commentDto.CommentID)
				.Collection("Like").Document(likeDto.Email);

			await likeRef.SetAsync(likeDto);
			return true;
		}
		catch (Exception e)
		{
			Debug.LogError($"Failed to add comment like: {e.Message}");
			return false;
		}
	}

	// 댓글 좋아요 제거
	public async Task<bool> RemoveCommentLike(PostDTO postDto, CommentDTO commentDto, LikeDTO likeDto)
	{
		try
		{
			// null 체크 추가
			if (postDto?.PostID == null || commentDto?.CommentID == null || likeDto?.Email == null)
			{
				Debug.LogError("Required IDs are null - PostID, CommentID, or Email");
				return false;
			}

			DocumentReference likeRef = _db.Collection("Post").Document(postDto.PostID)
				.Collection("Comment").Document(commentDto.CommentID)
				.Collection("Like").Document(likeDto.Email);

			await likeRef.DeleteAsync();
			return true;
		}
		catch (Exception e)
		{
			Debug.LogError($"Failed to remove comment like: {e.Message}");
			return false;
		}
	}
}