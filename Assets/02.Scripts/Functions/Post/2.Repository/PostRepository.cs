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
	public async Task<List<Post>> LoadPosts()
	{
		List<Post> posts = new List<Post>();

		try
		{
			// rankings 컬렉션의 모든 문서를 가져오는 쿼리
			Query allRankingsQuery = _db.Collection("Posts");

			// await으로 결과를 비동기로 받아옴
			QuerySnapshot allRankingsSnapshot = await allRankingsQuery.GetSnapshotAsync();

			foreach (DocumentSnapshot documentSnapshot in allRankingsSnapshot.Documents)
			{
				if (documentSnapshot.Exists)
				{
					// 문서 데이터를 커스텀 객체 Post로 변환
					Post post = documentSnapshot.ConvertTo<Post>();
					posts.Add(post);
				}
				else
				{
					Debug.LogWarning($"Document {documentSnapshot.Id} does not exist!");
				}
				// Debug.Log($"Document data for {documentSnapshot.Id} document:");
				//
				// Dictionary<string, object> postData = documentSnapshot.ToDictionary();
				// Post post = new Post(documentSnapshot.Id, postData);

				return posts;
			}
		}
		catch (Exception e)
		{
			Debug.LogError($"데이터 로딩 중 오류 발생: {e.Message}");
		}
		return null;
	}

	public async Task SavePosts(List<Post> posts)
	{

	}

}
