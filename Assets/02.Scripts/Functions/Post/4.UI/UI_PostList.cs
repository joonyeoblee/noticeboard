using System.Collections.Generic;
using UnityEngine;
public class UI_PostList : MonoBehaviour
{
	private List<UI_Post> _ui_Posts;
	public GameObject UI_PostPrefab;
	public int limit = 3;
	public void Start()
	{
		PostManager.Instance.OnDataChanged += Refresh;
	}

	public void Refresh()
	{
		for (int i = 0; i < limit; i++)
		{
			
		}
	}
}
