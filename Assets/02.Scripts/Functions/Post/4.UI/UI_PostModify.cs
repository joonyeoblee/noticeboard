using Firebase.Firestore;
using TMPro;
using UnityEngine;

public class UI_PostModify : MonoBehaviour
{
    public TMP_InputField ModifyContentInputField;

    public string CurrentPostID;

    public async void UpdatePost()
    {
        if (string.IsNullOrEmpty(CurrentPostID))
        {
            Debug.LogWarning("수정할 PostID가 설정되지 않았습니다.");
            return;
        }

        PostDTO currentPost = PostManager.Instance.Posts.Find(t => t.PostID == CurrentPostID);

        if (currentPost == null)
        {
            Debug.LogWarning($"PostID {CurrentPostID}에 해당하는 게시글을 찾을 수 없습니다.");
            return;
        }

        string newContent = ModifyContentInputField.text;

        if (string.IsNullOrWhiteSpace(newContent))
        {
            Debug.LogWarning("수정할 내용이 비어 있습니다.");
            return;
        }

        // 내용만 바꾼 새로운 Post 생성
        Post modifiedPost = new Post(currentPost.Email
        , currentPost.Nickname, currentPost.Content
        , newContent, currentPost.Like
        , currentPost.ViewCount, currentPost.PostTime);

        bool success = await PostManager.Instance.TryUpdatePost(modifiedPost);

        if (success)
        {
            Debug.Log("게시글 수정 성공");
        }
        else
        {
            Debug.LogError("게시글 수정 실패");
        }
    }
}
