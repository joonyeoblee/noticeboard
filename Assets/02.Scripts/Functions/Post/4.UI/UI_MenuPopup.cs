using UnityEngine;

public class UI_MenuPopup : MonoBehaviour
{
    private PostDTO _postDto;

    public void Refresh(PostDTO postDto)
    {
        _postDto = postDto;
    }
    public void OnClickModifyButton()
    {
        UI_Canvas.Instance.ModifyObject(_postDto);
        gameObject.SetActive(false);
    }
    public async void OnClickDeleteButton()
    {
        await PostManager.Instance.TryRemovePost(_postDto);
        gameObject.SetActive(false);
    }

    public void OnClickBackButton()
    {
        gameObject.SetActive(false);
    }
}
