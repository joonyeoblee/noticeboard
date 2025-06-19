using System.Collections.Generic;
using UnityEngine;
public class UI_PostList : MonoBehaviour
{
	public List<UI_Post> UI_Posts;

	public void Start()
	{
		PostManager.Instance.OnDataChanged += Refresh;
	}

	public void Refresh()
	{
		for (int i = 0; i < UI_Posts.Count; i++)
		{
		}
	}
}
