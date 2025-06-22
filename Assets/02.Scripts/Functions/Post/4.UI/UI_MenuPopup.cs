using System;
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
        _postDto = uiComment.GetPostDTO();
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
        try
        {
            if (IsComment == false)
            {
                if (_postDto?.PostID != null)
                {
                    await PostManager.Instance.TryRemovePost(_postDto);
                }
                else
                {
                    Debug.LogError("PostDTO is null or missing PostID");
                }
            }
            else
            {
                // 댓글 삭제의 경우 UI_Comment에서 PostDTO를 직접 가져오기
                PostDTO currentPostDto = _ui_Comment.GetPostDTO();
                if (currentPostDto?.PostID != null && _commentDto?.CommentID != null)
                {
                    await CommentManager.Instance.TryRemoveComment(currentPostDto, _commentDto);
                }
                else
                {
                    Debug.LogError("PostDTO or CommentDTO is null or missing required IDs");
                }
            }

            gameObject.SetActive(false);
        }
        catch (Exception e)
        {
            Debug.LogError($"Error in OnClickDeleteButton: {e.Message}");
            gameObject.SetActive(false);
        }
    }

    public void OnClickBackButton()
    {
        gameObject.SetActive(false);
    }
}
