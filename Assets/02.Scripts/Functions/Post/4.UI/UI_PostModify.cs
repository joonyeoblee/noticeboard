using Firebase.Firestore;
using TMPro;
using UnityEngine;

public class UI_PostModify : MonoBehaviour
{
    public TMP_InputField ModifyContentInputField;

    private string _currentPostID;
    private PostDTO _currentPost;

    public void Init(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            Debug.LogWarning("수정할 PostID가 설정되지 않았습니다.");
            return;
        }
        
        _currentPostID = id;
        _currentPost = PostManager.Instance.Posts.Find(t => t.PostID == _currentPostID);
        
        if (_currentPost == null)
        {
            Debug.LogWarning($"PostID {_currentPostID}에 해당하는 게시글을 찾을 수 없습니다.");
            return;
        }
        ModifyContentInputField.text = _currentPost.Content;

    }
    public async void UpdatePost()
    {

       Debug.Log($"CurrentPostDTOID : {_currentPost.PostID}");
        string newContent = ModifyContentInputField.text;

        if (string.IsNullOrWhiteSpace(newContent))
        {
            Debug.LogWarning("수정할 내용이 비어 있습니다.");
            return;
        }

        // 내용만 바꾼 새로운 Post 생성
        Post modifiedPost = new Post(_currentPost.Email
        , _currentPost.Nickname, _currentPost.PostID
        , newContent, _currentPost.LikeCount
        , _currentPost.ViewCount, _currentPost.PostTime);
        
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
