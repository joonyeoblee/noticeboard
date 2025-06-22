using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PostList : MonoBehaviour
{
	public List<UI_Post> _ui_Posts;
	public GameObject UI_PostPrefab;
	public GameObject UI_PostContainer;


	private List<PostDTO> _postDtos;
	
	public void Start()
	{
		PostManager.Instance.OnDataChanged += Refresh;
	}

	public void Refresh()
	{
		_postDtos = PostManager.Instance.PostDtos;
		
		int postCount = _postDtos?.Count ?? 0;
		int limit = PostManager.Instance.Limit;
		int count = Mathf.Min(postCount, limit);

		EnsurePostSlots(limit); // 슬롯 부족하면 생성

		for (int i = 0; i < limit; i++)
		{
			if (i < count)
			{
				_ui_Posts[i].gameObject.SetActive(true);
				_ui_Posts[i].Refresh(_postDtos[i]);
			}
			else
			{
				_ui_Posts[i].gameObject.SetActive(false);
			}
		}

	}
	private void EnsurePostSlots(int requiredCount)
	{
		while (_ui_Posts.Count < requiredCount)
		{
			GameObject go = Instantiate(UI_PostPrefab, UI_PostContainer.transform);
			UI_Post postUI = go.GetComponent<UI_Post>();
			_ui_Posts.Add(postUI);
		}
	}
}
