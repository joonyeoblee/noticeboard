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

        // 명세
        PostContentSpecification postSpecification = new PostContentSpecification();
        if (!postSpecification.IsSatisfiedBy(newContent))
        {
            throw new Exception(postSpecification.ErrorMessage);
        }
        
        // 내용만 바꾼 새로운 Post 생성
        Post modifiedPost = new Post(_post.Email
            , _post.Nickname, _post.PostID
            , newContent, _post.ViewCount, _post.PostTime);

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
