using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Comment : MonoBehaviour
{
    public TextMeshProUGUI NameTextUI;
    public TextMeshProUGUI CommentTimeTextUI;
    public TextMeshProUGUI ContentTextUI;
    public TextMeshProUGUI LikeCountTextUI;
    public TMP_InputField ModifyInputField; 
	public Button DropdownButton;
	
    // 댓글 좋아요 기능
    public Button LikeButton;
    public GameObject[] LikeImage; // 0: 비활성화, 1: 활성화
	
    private CommentDTO _commentDto;
    private PostDTO _postDto;
    private bool _isLiked;
    private LikeDTO _myLike;

    AccountDTO account => AccountManager.Instance.CurrentAccount;

    private void Start()
    {
        ModifyInputField.onEndEdit.AddListener(OnModifyCommentEndEdit);
    }
    public PostDTO GetPostDTO()
    {
        return _postDto;
    }
    public void Refresh(CommentDTO commentDTO, PostDTO postDto)
    {
        _commentDto = commentDTO;
        _postDto = postDto;
		
        // UI 텍스트 업데이트
        if (NameTextUI != null) NameTextUI.text = commentDTO.Nickname;
        if (ContentTextUI != null) ContentTextUI.text = commentDTO.Content;
        if (LikeCountTextUI != null) LikeCountTextUI.text = commentDTO.LikeCount.ToString();
		if(CommentTimeTextUI != null) CommentTimeTextUI.text = DisplayTimestamp.GetPrettyTimeAgo(commentDTO.CommentTime);
        // 현재 계정 체크
        if (account == null)
        {
            Debug.LogError("Current account is null");
            return;
        }
        var latestPost = PostManager.Instance.GetPostById(postDto.PostID);
        if (latestPost != null)
        {
            _postDto = latestPost;
        }
        else
        {
            _postDto = postDto;
        }
        if (_commentDto.Likes != null && _commentDto.Likes.Any(like => like.Email == account.Email))
        {
            _isLiked = true;
            _myLike = _commentDto.Likes.FirstOrDefault(like => like.Email == account.Email);
            LikeImage[0].SetActive(false);
            LikeImage[1].SetActive(true);
        }
        else
        {
            _isLiked = false;
            _myLike = null;
            LikeImage[0].SetActive(true);
            LikeImage[1].SetActive(false);
        }
        
        bool isMyPost = commentDTO.Email == AccountManager.Instance.CurrentAccount.Email;
        DropdownButton.gameObject.SetActive(isMyPost);
    }
    public void OnClickMenuButton()
    {
        GameObject Popup = UI_Canvas.Instance.Popup;
        Popup.SetActive(true);
        Popup.transform.position = DropdownButton.transform.position;
        Popup.GetComponent<UI_MenuPopup>().IsComment = true;
        Popup.GetComponent<UI_MenuPopup>().Refresh(_commentDto, this);
    }

    public void ModifyComment(CommentDTO commentDto)
    {
        _commentDto = commentDto;
        ModifyInputField.text = ContentTextUI.text;
        ContentTextUI.gameObject.SetActive(false);
        ModifyInputField.gameObject.SetActive(true);
    }

    public async void OnModifyCommentEndEdit(string text)
    {
        bool success = await CommentManager.Instance.TryUpdateComment(_postDto, _commentDto, text);

        if (success)
        {
            Debug.Log("댓글 수정 성공");
            ModifyInputField.text = "";
            ContentTextUI.text = text;
            ContentTextUI.gameObject.SetActive(true);
            ModifyInputField.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("댓글 수정 실패");
        }
    }
    private async void Like()
    {
        // 버튼 비활성화로 중복 클릭 방지
        LikeButton.interactable = false;
        
        Like like = new Like(account.Email, account.Nickname);
        if (await CommentManager.Instance.TryAddCommentLike(_postDto, _commentDto, like))
        {
            // UI 즉시 업데이트
            _isLiked = true;
            _myLike = new LikeDTO(account.Email, account.Nickname);
            UpdateLikeUI();
        }
        
        LikeButton.interactable = true;
    }

    private async void UnLike()
    {
        // 버튼 비활성화로 중복 클릭 방지
        LikeButton.interactable = false;
        
        if (await CommentManager.Instance.TryRemoveCommentLike(_postDto, _commentDto, _myLike))
        {
            // UI 즉시 업데이트
            _isLiked = false;
            _myLike = null;
            UpdateLikeUI();
        }
        
        LikeButton.interactable = true;
    }

    private void UpdateLikeUI()
    {
        if (_isLiked)
        {
            LikeImage[0].SetActive(false);
            LikeImage[1].SetActive(true);
        }
        else
        {
            LikeImage[0].SetActive(true);
            LikeImage[1].SetActive(false);
        }
    }
	
    public void ToggleCommentLike()
    {
        // 필수 데이터 검증
        if (_commentDto == null || _postDto == null || account == null)
        {
            Debug.LogError("Required data is null for comment like toggle");
            return;
        }
        
        if (_isLiked)
        {
            UnLike();
        }
        else
        {
            Like();
        }
    }
}