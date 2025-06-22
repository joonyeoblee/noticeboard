using System;
using System.Threading.Tasks;
using UnityEngine;
public class CommentManager : Singleton<CommentManager>
{
    private CommentRepository _repository;
    public event Action<PostDTO> OnDataChanged;
    private async void Start()
    {
        await FirebaseConnect.Instance.Initialization;

        _repository = new CommentRepository(FirebaseConnect.Instance.Db);

    }


    public async Task<bool> TryWriteComment(PostDTO postDto, Comment comment)
    {
        Post post = new Post(postDto);
        post.AddComment(comment);
        postDto = post.ToDto();
        await _repository.AddComment(postDto, comment.ToDto());
        PostDTO newLoadPostDto = await PostManager.Instance.TryGetPost(postDto);
        OnDataChanged?.Invoke(newLoadPostDto);
        return true;
    }

    // 댓글 좋아요 추가
    public async Task<bool> TryAddCommentLike(PostDTO postDto, CommentDTO commentDto, Like like)
    {
        bool success = await _repository.AddCommentLike(postDto, commentDto, like.ToDto());
        if (success)
        {
            // 로컬에서만 업데이트 - DB 재조회 제거
            UpdateCommentLikeLocally(postDto, commentDto, like.ToDto(), true);
            OnDataChanged?.Invoke(postDto);
        }
        return success;
    }

    // 댓글 좋아요 제거
    public async Task<bool> TryRemoveCommentLike(PostDTO postDto, CommentDTO commentDto, LikeDTO likeDto)
    {
        bool success = await _repository.RemoveCommentLike(postDto, commentDto, likeDto);
        if (success)
        {
            // 로컬에서만 업데이트 - DB 재조회 제거
            UpdateCommentLikeLocally(postDto, commentDto, likeDto, false);
            OnDataChanged?.Invoke(postDto);
        }
        return success;
    }

    // 로컬에서 댓글 좋아요 상태 업데이트
    private void UpdateCommentLikeLocally(PostDTO postDto, CommentDTO commentDto, LikeDTO likeDto, bool isAdd)
    {
        // PostManager의 PostDtos에서 해당 포스트 찾기
        var postInManager = PostManager.Instance.GetPostById(postDto.PostID);
        if (postInManager != null)
        {
            // 해당 댓글 찾기
            var targetComment = postInManager.Comments.Find(c => c.CommentID == commentDto.CommentID);
            if (targetComment != null)
            {
                if (isAdd)
                {
                    // 좋아요 추가 (중복 체크)
                    if (!targetComment.Likes.Exists(l => l.Email == likeDto.Email))
                    {
                        targetComment.Likes.Add(likeDto);
                    }
                }
                else
                {
                    // 좋아요 제거
                    targetComment.Likes.RemoveAll(l => l.Email == likeDto.Email);
                }
            }
            
            // PostManager의 이벤트도 발생시켜 UI 업데이트
            PostManager.Instance.OnDataChanged?.Invoke();
        }
    }

    public void InvokeAction(PostDTO postDto)
    {
        Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        OnDataChanged?.Invoke(postDto);
    }
}