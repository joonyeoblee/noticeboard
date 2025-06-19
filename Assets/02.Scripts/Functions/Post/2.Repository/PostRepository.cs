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
	public async Task<List<PostDTO>> LoadPosts()
	{
		List<PostDTO> postDtos = new List<PostDTO>();
		try
		{
			// 포스트 컬렉션을 불러옴
			Query allPostsQuery = _db.Collection("Post");

			QuerySnapshot allPostsSnapshot = await allPostsQuery.GetSnapshotAsync();

			foreach (DocumentSnapshot documentSnapshot in allPostsSnapshot.Documents)
			{
				if (documentSnapshot.Exists)
				{
					// 문서 데이터를 커스텀 객체 Post로 변환
					PostDTO postDto = documentSnapshot.ConvertTo<PostDTO>();
					postDtos.Add(postDto);
				}
				else
				{
					Debug.LogWarning($"Document {documentSnapshot.Id} does not exist!");
				}
			}



			return postDtos;

		}
		catch (Exception e)
		{
			Debug.LogError($"데이터 로딩 중 오류 발생: {e.Message}");
		}
		return null;
	}

	public async Task AddPost(PostDTO postDto)
	{
		DocumentReference docRef = _db.Collection("Post").Document(postDto.PostID);
		await docRef.SetAsync(postDto);
	}
	public async Task<List<PostDTO>> GetPosts(int start, int limit)
	{

		return null;
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

	}
	public async Task DeletePost(string postId)
	{

	}


	public async Task Addlike(LikeDTO likeDto)
	{
		DocumentReference docRef = _db.Collection("Like").Document(likeDto.LikeID);
		await docRef.SetAsync(likeDto);
	}
}
