using UnityEngine;

public class UI_MenuPopup : MonoBehaviour
{
    private PostDTO _postDto;
    private CommentDTO _commentDto;
    private UI_Comment _ui_Comment;

    public bool IsComment = false;
    public void Refresh(PostDTO postDto)
    {
        _postDto = postDto;
    }
    public void Refresh(CommentDTO commentDto, UI_Comment uiComment)
    {
        _commentDto = commentDto;
        _ui_Comment = uiComment;
    }
    public void OnClickModifyButton()
    {
        if (IsComment == false)
        {
            UI_Canvas.Instance.ModifyObject(_postDto);    
        }
        else
        {
            _ui_Comment.ModifyComment(_commentDto);
        }
        gameObject.SetActive(false);
    }
    public async void OnClickDeleteButton()
    {
        if (IsComment == false)
        {
            await PostManager.Instance.TryRemovePost(_postDto);            
        }
        else
        {
            await CommentManager.Instance.TryRemoveComment(_postDto, _commentDto);
        }
        
        gameObject.SetActive(false);
    }

    public void OnClickBackButton()
    {
        gameObject.SetActive(false);
    }
}
