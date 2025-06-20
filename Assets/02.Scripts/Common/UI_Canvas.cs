
	using UnityEngine;
	public class UI_Canvas : MonoBehaviour
	{
		public GameObject Write;
		public GameObject Posts;
		public GameObject Modify;
		

		public void WriteObject()
		{
			Write.SetActive(true);
		}
		public void ModifyObject(PostDTO postDto)
		{
			Modify.SetActive(true);
			Modify.GetComponent<UI_PostModify>().Refresh(postDto);
		}
		public async void Close()
		{
			Write.SetActive(false);
			Modify.SetActive(false);
			await PostManager.Instance.OpenComments();
		}

	
	}

