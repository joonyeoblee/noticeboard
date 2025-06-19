using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_PostList : MonoBehaviour
{
	public List<UI_Post> _ui_Posts;
	public GameObject UI_PostPrefab;
	public GameObject UI_PostContainer;
	public int limit = 3;
	public void Start()
	{
		PostManager.Instance.OnDataChanged += Refresh;
	}

	public void Refresh()
	{
		// 기존 UI 요소 제거
		foreach (var uiPost in _ui_Posts)
		{
			if (uiPost != null)
				Destroy(uiPost.gameObject);
		}
		_ui_Posts.Clear(); // 리스트도 초기화

		// 새로 생성
		List<PostDTO> postDtos = PostManager.Instance.Posts;
		int Count = postDtos.Count < limit ? postDtos.Count : limit;

		for (int i = 0; i < Count; i++)
		{
			GameObject ui_Post = Instantiate(UI_PostPrefab, UI_PostContainer.transform);
			var uiPostComponent = ui_Post.GetComponent<UI_Post>();
			_ui_Posts.Add(uiPostComponent);
			uiPostComponent.Refresh(postDtos[i]);
		}

		// 레이아웃 강제 갱신
		LayoutRebuilder.ForceRebuildLayoutImmediate(UI_PostContainer.GetComponent<RectTransform>());
	}

}
