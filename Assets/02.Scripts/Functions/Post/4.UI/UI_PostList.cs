using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_PostList : MonoBehaviour
{
	public List<UI_Post> _ui_Posts;
	public GameObject UI_PostPrefab;
	public GameObject UI_PostContainer;
	public int limit = 3;
	
	List<PostDTO> postDtos => PostManager.Instance.Posts;
	
	public void Start()
	{
		PostManager.Instance.OnDataChanged += Refresh;
	}

	public void Refresh()
	{
		int postCount = postDtos?.Count ?? 0;
		int count = Mathf.Min(postCount, limit);

		for (int i = 0; i < limit; i++)
		{
			if (i < count)
			{
				_ui_Posts[i].gameObject.SetActive(true);
				_ui_Posts[i].Refresh(postDtos[i]);
			}
			else
			{
				_ui_Posts[i].gameObject.SetActive(false);
			}
		}

		LayoutRebuilder.ForceRebuildLayoutImmediate(UI_PostContainer.GetComponent<RectTransform>());
	}

}
