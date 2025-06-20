using System;
using TMPro;
using UnityEngine;
public class UI_PostModify : MonoBehaviour
{
    public TMP_InputField ModifyContentInputField;

    private string _currentPostID;
    private PostDTO _post;

    public void Refresh(PostDTO postDto)
    {
        _post = postDto;

        if (_post == null)
        {
            throw new Exception("Post가 없음");
        }
        ModifyContentInputField.text = _post.Content;

    }
    public async void UpdatePost()
    {
        string newContent = ModifyContentInputField.text;

        if (string.IsNullOrWhiteSpace(newContent))
        {
            throw new Exception("수정할 내용이 비어 있습니다.");
        }

        // 내용만 바꾼 새로운 Post 생성
        Post modifiedPost = new Post(_post.Email
            , _post.Nickname, _post.PostID
            , newContent, _post.LikeCount
            , _post.ViewCount, _post.PostTime);
        
        Debug.Log($"PostID = {modifiedPost.PostID}");

        bool success = await PostManager.Instance.TryUpdatePost(modifiedPost);

        if (success)
        {
            Debug.Log("게시글 수정 성공");
            gameObject.GetComponentInParent<UI_Canvas>().Close();
        }
        else
        {
            Debug.LogError("게시글 수정 실패");
        }
    }
}
