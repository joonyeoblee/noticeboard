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
	
    // 댓글 좋아요 기능
    public Button LikeButton;
    public GameObject[] LikeImage; // 0: 비활성화, 1: 활성화
	
    private CommentDTO _commentDto;
    private PostDTO _postDto;
    private bool _isLiked;
    private LikeDTO _myLike;

    AccountDTO account => AccountManager.Instance.CurrentAccount;
	
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