using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class UI_PostRead : MonoBehaviour
{
    private PostDTO _postDto;
    public UI_Post UI_Post;
    public TMP_InputField CommentContentInputField;

    public GameObject CommentPrefab;
    public GameObject CommentContainer;

    public List<UI_Comment> UI_Comments = new List<UI_Comment>();

    private void Start()
    {
        CommentManager.Instance.OnDataChanged += Refresh;
        PostManager.Instance.OnDataChanged += RefreshFromPostManager; // 추가
    }

    // PostManager 이벤트용 새로운 메서드
    private void RefreshFromPostManager()
    {
        if (_postDto != null)
        {
            // PostManager에서 최신 PostDTO 가져오기
            var updatedPost = PostManager.Instance.GetPostById(_postDto.PostID);
            if (updatedPost != null)
            {
                Refresh(updatedPost);
            }
        }
    }

    public void Refresh(PostDTO postDto)
    {
        if (postDto == null) return;

        // 최신 데이터를 PostManager에서 가져오기
        var latestPost = PostManager.Instance.GetPostById(postDto.PostID);
        if (latestPost != null)
        {
            _postDto = latestPost;
        }
        else
        {
            _postDto = postDto;
        }

        UI_Post.Refresh(_postDto);

        int count = _postDto.CommentCount;
        Debug.Log(_postDto.Likes.Count + "/" + _postDto.Comments.Count);

        EnsureCommentSlots(count);

        for (int i = 0; i < UI_Comments.Count; i++)
        {
            if (i < count)
            {
                UI_Comments[i].gameObject.SetActive(true);
                UI_Comments[i].Refresh(_postDto.Comments[i], _postDto);
            }
            else
            {
                UI_Comments[i].gameObject.SetActive(false);
            }
        }
    }

    private void EnsureCommentSlots(int requiredCount)
    {
        while (UI_Comments.Count < requiredCount)
        {
            GameObject go = Instantiate(CommentPrefab, CommentContainer.transform);
            UI_Comment uiComment = go.GetComponent<UI_Comment>();
            UI_Comments.Add(uiComment);
        }
    }

    public async void WriteComment()
    {
        AccountDTO accountDto = AccountManager.Instance.CurrentAccount;

        CommentContentSpecification commentContentSpecification = new CommentContentSpecification();
        string content = CommentContentInputField.text;
        if (!commentContentSpecification.IsSatisfiedBy(content))
        {
            throw new Exception(commentContentSpecification.ErrorMessage);
        }

        AccountEmailSpecification accountEmailSpecification = new AccountEmailSpecification();
        if (!accountEmailSpecification.IsSatisfiedBy(accountDto.Email))
        {
            throw new Exception(accountEmailSpecification.ErrorMessage);
        }

        AccountNicknameSpecification accountNicknameSpecification = new AccountNicknameSpecification();
        if (!accountNicknameSpecification.IsSatisfiedBy(accountDto.Nickname))
        {
            throw new Exception(accountNicknameSpecification.ErrorMessage);
        }

        Comment comment = new Comment(accountDto.Email, accountDto.Nickname, content);

        if (await CommentManager.Instance.TryWriteComment(_postDto, comment))
        {
            // 코멘트 리스트 갱신되었을 테니 Refresh
            Refresh(_postDto);
            CommentContentInputField.text = "";
        }
    }
}