
	using System;
	using System.Collections.Generic;
	using UnityEngine;
	public class UI_Canvas : Singleton<UI_Canvas>
	{
		public GameObject Write;
		public GameObject Posts;
		public GameObject Modify;
		public GameObject Login;
		public GameObject Details;

		public List<GameObject> Panels;

		public GameObject Popup;

		private void AllPanelActiveFalse()
		{
			foreach (var obj in Panels)
			{
				obj.SetActive(false);
			}
		}
		public void WriteObject()
		{
			Write.SetActive(true);
		}
		public void ModifyObject(PostDTO postDto)
		{
			Modify.SetActive(true);
			Modify.GetComponent<UI_PostModify>().Refresh(postDto);
		}
		public void LoginAndOpenPost()
		{
			Login.SetActive(false);
			Posts.SetActive(true);
			PostManager.Instance.OpenComments();
		}
		public void DetailsToPost()
		{
			AllPanelActiveFalse();
			Posts.SetActive(true);
			PostManager.Instance.OpenComments();
		}
		public void Logout()
		{
			Login.SetActive(true);
			Posts.SetActive(false);
		}
		public async void Close()
		{
			Write.SetActive(false);
			Modify.SetActive(false);
			await PostManager.Instance.OpenComments();
		}

	
	}

