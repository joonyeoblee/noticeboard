using System.Collections.Generic;
using UnityEngine;
public class UI_Post : MonoBehaviour
{
	public List<UI_PostMenu> UI_PostMenus = new List<UI_PostMenu>();
	private Post Post;
	private void Start()
	{
	}
	public void Refresh(Post post)
	{
		Post = post;
		foreach (UI_PostMenu ui_PostMenu in UI_PostMenus)
		{

		}
	}
}
