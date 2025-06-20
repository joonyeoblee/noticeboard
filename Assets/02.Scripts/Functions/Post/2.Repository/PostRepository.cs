using System;
using System.Collections.Generic;
using System.Linq;
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
			Debug.Log("snapshot" + snapshot.Count);

			// start부터 limit 개수만큼 리스트에 추가
			for (int i = start; i < Math.Min(start + limit, snapshot.Count); i++)
			{
				DocumentSnapshot doc = snapshot[i];
				PostDTO postDto = doc.ConvertTo<PostDTO>();

				// 좋아요 서브컬렉션에서 좋아요 수 가져오기
				var likeQuerySnapshot = await _db.Collection("Post")
					.Document(doc.Id)
					.Collection("Like")
					.GetSnapshotAsync();

				// 좋아요 데이터를 PostDTO에 추가
				foreach (var likeDoc in likeQuerySnapshot.Documents)
				{
					LikeDTO likeDto = likeDoc.ConvertTo<LikeDTO>();
					postDto.AddLike(likeDto);  // AddLike 함수 사용
				}
				postDtos.Add(postDto);
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

				// 좋아요 서브컬렉션에서 좋아요 수 가져오기
				var likeQuerySnapshot = await _db.Collection("Post")
					.Document(postId)
					.Collection("Like")
					.GetSnapshotAsync();
				// 좋아요 데이터를 PostDTO에 추가
				foreach (var likeDoc in likeQuerySnapshot.Documents)
				{
					LikeDTO likeDto = likeDoc.ConvertTo<LikeDTO>();
					postDto.AddLike(likeDto);  // AddLike 함수 사용
				}
	
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


	public async Task AddLike(LikeDTO likeDto, string postId)
	{
		try
		{
			// postId 문서의 하위 컬렉션 'Like'에 likeId를 문서 ID로 사용
			string likeId = likeDto.LikeID ?? Guid.NewGuid().ToString(); // null 방지
			DocumentReference likeRef = _db.Collection("Post").Document(postId)
				.Collection("Like").Document(likeId);

			await likeRef.SetAsync(likeDto);
		}
		catch (Exception e)
		{
			Debug.LogError($"Failed to add like: {e.Message}");
			throw;
		}
	}
	public async Task RemoveLike(string postId, LikeDTO likeDto)
	{
		try
		{
			// 'Post' 컬렉션에서 해당 postId의 문서의 서브컬렉션 'Like' 가져오기
			CollectionReference likeCollectionRef = _db.Collection("Post")
				.Document(postId)
				.Collection("Like");

			// LikeDTO에 해당하는 문서를 찾기 위한 쿼리
			Query query = likeCollectionRef.WhereEqualTo("LikeID", likeDto.LikeID); // LikeID로 문서를 찾음
			QuerySnapshot querySnapshot = await query.GetSnapshotAsync();

			// 해당 LikeID를 가진 문서가 있으면 삭제
			DocumentSnapshot doc = querySnapshot.Documents.FirstOrDefault(); // 첫 번째 문서 찾기

			// 해당 문서 삭제
			if (doc != null)
			{
				await doc.Reference.DeleteAsync();
			}
			Debug.Log("asdasd");
		}
		catch (Exception e)
		{
			Debug.LogError($"Failed to remove like: {e.Message}");
			throw;
		}
	}


}
