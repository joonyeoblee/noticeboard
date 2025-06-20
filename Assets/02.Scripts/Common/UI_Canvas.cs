
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
		public void ModifyObject(string id)
		{
			Modify.SetActive(true);
			Modify.GetComponent<UI_PostModify>().Init(id);
		}
		public async void Close()
		{
			Write.SetActive(false);
			Modify.SetActive(false);
			await PostManager.Instance.OpenComments();
		}

	
	}

