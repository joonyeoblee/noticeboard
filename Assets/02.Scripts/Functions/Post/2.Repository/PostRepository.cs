using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Firestore;
using UnityEngine;
public class PostRepository
{
	private readonly FirebaseFirestore _db;

	public PostRepository(FirebaseFirestore db)
	{
		_db = db;
	}

	public async Task AddPost(PostDTO postDto)
	{
		DocumentReference docRef = _db.Collection("Post").Document(postDto.PostID);
		await docRef.SetAsync(postDto);
	}
	public async Task<List<PostDTO>> GetPosts(int start, int limit)
	{
		List<PostDTO> postDtos = new List<PostDTO>();
		try
		{
			Query query = _db.Collection("Post")
				.OrderByDescending("PostTime")
				.Limit(start + limit);

			QuerySnapshot snapshot = await query.GetSnapshotAsync();
			Debug.Log("snapshot" + snapshot.Count);

			for (int i = start; i < Math.Min(start + limit, snapshot.Count); i++)
			{
				DocumentSnapshot doc = snapshot[i];
				PostDTO postDto = doc.ConvertTo<PostDTO>();

				List<LikeDTO> likeDtos = new List<LikeDTO>();
				List<CommentDTO> commentDtos = new List<CommentDTO>();

				var likeQuerySnapshot = await _db.Collection("Post")
					.Document(postDto.PostID)
					.Collection("Like")
					.GetSnapshotAsync();

				foreach (var likeDoc in likeQuerySnapshot.Documents)
				{
					LikeDTO likeDto = likeDoc.ConvertTo<LikeDTO>();
					likeDtos.Add(likeDto);
				}

				var commentQuerySnapshot = await _db.Collection("Post")
					.Document(postDto.PostID)
					.Collection("Comment")
					.GetSnapshotAsync();

				foreach (DocumentSnapshot commentDoc in commentQuerySnapshot.Documents)
				{
					CommentDTO commentDto = commentDoc.ConvertTo<CommentDTO>();
					commentDtos.Add(commentDto);
				}

				// DTO에 넣기
				postDto = new PostDTO(postDto, likeDtos, commentDtos);
				postDtos.Add(postDto);
			}

		}
		catch (Exception e)
		{
			Debug.LogError($"Error fetching posts: {e.Message}");
		}

		return postDtos;
	}

	public async Task<PostDTO> GetPost(string postId)
	{
		PostDTO postDto = null;
		List<LikeDTO> likeDtos = new List<LikeDTO>();
		List<CommentDTO> commentDtos = new List<CommentDTO>();
		try
		{
			DocumentReference docRef = _db.Collection("Post").Document(postId);
			DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

			if (snapshot.Exists)
			{
				postDto = snapshot.ConvertTo<PostDTO>();

				var likeQuerySnapshot = await _db.Collection("Post")
					.Document(postId)
					.Collection("Like")
					.GetSnapshotAsync();
				foreach (var likeDoc in likeQuerySnapshot.Documents)
				{
					LikeDTO likeDto = likeDoc.ConvertTo<LikeDTO>();
					likeDtos.Add(likeDto);
				}

				QuerySnapshot CommentQuerySnapshot = await _db.Collection("Post")
					.Document(postId)
					.Collection("Comment")
					.GetSnapshotAsync();
				foreach (DocumentSnapshot CommentDoc in CommentQuerySnapshot.Documents)
				{
					CommentDTO CommentDto = CommentDoc.ConvertTo<CommentDTO>();
					commentDtos.Add(CommentDto);
				}

				postDto = new PostDTO(postDto, likeDtos, commentDtos);
				
			}
			else
			{
				Debug.LogWarning($"Document {snapshot.Id} does not exist!");
			}

			return postDto;

		}
		catch (Exception e)
		{
			Debug.LogError($"데이터 로딩 중 오류 발생: {e.Message}");
		}
		return null;
	}

	public async Task UpdatePost(PostDTO post)
	{
		try
		{
			DocumentReference docRef = _db.Collection("Post").Document(post.PostID);

			Dictionary<string, object> updates = new Dictionary<string, object>
			{
				{ "PostTime", post.PostTime },
				{ "Nickname", post.Nickname },
				{ "Content", post.Content }
			};

			await docRef.UpdateAsync(updates);
			Debug.Log($"[PostRepository] 게시글 {post.PostID} 갱신 완료");
		}
		catch (Exception e)
		{
			Debug.LogError($"[PostRepository] 게시글 갱신 실패: {e.Message}");
			throw;
		}
	}


	public async Task DeletePost(string postId)
	{
		try
		{
			DocumentReference docRef = _db.Collection("Post").Document(postId);
			await docRef.DeleteAsync();
			// 댓글 좋아요 별도 처리 필요
			Debug.Log($"[PostRepository] 게시글 {postId} 삭제 완료");
		}
		catch (Exception e)
		{
			Debug.LogError($"[PostRepository] 게시글 삭제 실패: {e.Message}");
			throw;
		}
	}


	public async Task AddLike(PostDTO postDto, LikeDTO likeDto)
	{
		try
		{
			DocumentReference likeRef = _db.Collection("Post").Document(postDto.PostID)
				.Collection("Like").Document();

			await likeRef.SetAsync(likeDto);
		}
		catch (Exception e)
		{
			Debug.LogError($"Failed to add like: {e.Message}");
			throw;
		}
	}
	public async Task RemoveLike(PostDTO postDto, LikeDTO likeDto)
	{
		try
		{
			// 'Post' 컬렉션에서 해당 postId의 문서의 서브컬렉션 'Like' 가져오기
			DocumentReference likeRef = _db.Collection("Post")
				.Document(postDto.PostID)
				.Collection("Like").Document(likeDto.LikeID);

			await likeRef.DeleteAsync();
		}
		catch (Exception e)
		{
			
		}
	}
}
