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
			// "PostTime" 기준으로 내림차순 정렬하여 최신 게시글부터 가져옴
			Query query = _db.Collection("Post")
				.OrderByDescending("PostTime")
				.Limit(start + limit);

			QuerySnapshot snapshot = await query.GetSnapshotAsync();

			// start부터 limit 개수만큼 리스트에 추가
			for (int i = start; i < Math.Min(start + limit, snapshot.Count); i++)
			{
				DocumentSnapshot doc = snapshot[i];
				PostDTO postDto = doc.ConvertTo<PostDTO>();
				postDtos.Add(postDto);
				Debug.Log(postDto.PostID);
			}
		}
		catch (System.Exception e)
		{
			UnityEngine.Debug.LogError($"Error fetching posts: {e.Message}");
		}

		return postDtos;
	}


	public async Task<PostDTO> GetPost(string postId)
	{
		PostDTO postDto = null;
		try
		{
			DocumentReference docRef = _db.Collection("Post").Document(postId);
			DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

			if (snapshot.Exists)
			{
				postDto = snapshot.ConvertTo<PostDTO>();
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



	public async Task Addlike(LikeDTO likeDto)
	{
		DocumentReference docRef = _db.Collection("Like").Document(likeDto.LikeID);
		await docRef.SetAsync(likeDto);
	}
}
