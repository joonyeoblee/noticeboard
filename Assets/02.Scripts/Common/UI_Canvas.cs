
	using UnityEngine;
	public class UI_Canvas : MonoBehaviour
	{
		public GameObject Write;
		public GameObject Posts;
		

		public void WriteObject()
		{
			Write.SetActive(true);
			
		}
		public async void CloseWrite()
		{
			Write.SetActive(false);
			await PostManager.Instance.OpenComments();
		}
	}

