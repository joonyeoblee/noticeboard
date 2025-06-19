using System;
using System.Collections.Generic;
using Firebase;
using Firebase.Extensions;
using Firebase.Auth;
using Firebase.Firestore;
using UnityEngine;

public class FirebaseTest : MonoBehaviour
{
    private FirebaseApp _app;
    
    public static string EMAIL = $"rlarudgh998@skkukdp.re.kr";
    public static string PASSWORD = "123456";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        Init();
    }
    // 파이어베이스 내 프로젝트에 연결
    private void Init()
    {
        // ContinueWith는 멀티나 메인 쓰레드에서도 가능하지만 MonoBehavior를 상속받는 경우는 아래 함수를 사용하자
        // Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available) 
            {
                Debug.Log("파이어베이스 연결에 성공했습니다.");
                _app = FirebaseApp.DefaultInstance;
                
                Login();
            } 
            else 
            {
                Debug.LogError($"파이어베이스 연결 실패했습니다. {dependencyStatus}");
            }
        });
    }
    private void Register()
    {
        FirebaseConnect.Instance.Auth.CreateUserWithEmailAndPasswordAsync(EMAIL, PASSWORD).ContinueWithOnMainThread(task => {
            if (task.IsCanceled || task.IsFaulted) {
                Debug.LogError($"회원가입에 실패했습니다.{task.Exception.Message}");
                return;
            }
            
            Firebase.Auth.AuthResult result = task.Result;
            Debug.LogFormat("회원가입에 성공했습니다. {0} ({1})",
                result.User.DisplayName, result.User.UserId);
            return;
        });
    }
    private void Login()
    {
        FirebaseConnect.Instance.Auth.SignInWithEmailAndPasswordAsync(EMAIL, PASSWORD).ContinueWithOnMainThread(task => {
            if (task.IsCanceled || task.IsFaulted) {
                Debug.LogError("로그인이 실패했습니다.");
                return;
            }

            Firebase.Auth.AuthResult result = task.Result;
            Debug.LogFormat("로그인이 성공했습니다.: {0} ({1})", result.User.DisplayName, result.User.UserId);
            NicknameChange();
            //AddRanking();
            GetRankings();
            
        });
    }

    private void NicknameChange()
    {
        var user = FirebaseConnect.Instance.Auth.CurrentUser;
        if (user == null) return;

        var profile = new UserProfile
        {
            DisplayName = "rlarudgh998"
        };
            
        user.UpdateUserProfileAsync(profile).ContinueWithOnMainThread(task => {
            if (task.IsCanceled || task.IsFaulted) {
                Debug.LogError($"닉네임 변경에 실패했습니다{task.Exception.Message}");
                return;
            }
            Debug.Log("닉네임 변경에 성공했습니다.");
        });
    }

    private void GetProfile()
    {
        var user = FirebaseConnect.Instance.Auth.CurrentUser;
        if (user == null) return;
        
        string nickname = user.DisplayName;
        string email = user.Email;

        Account account = new Account(email, nickname, PASSWORD);
    }
    
    private void AddRanking()
    {
            // RankData rankData = new RankData( EMAIL,"허허허", 2300);
            //
            // Dictionary<string, object> rankDict = new Dictionary<string, object>
            // {
            //     { "Email",rankData.Email },
            //     { "Nickname",rankData.Nickname},
            //     { "Score",rankData.Score }
            // };
            // FirebaseConnect.Instance.Db.Collection("rankings").AddAsync(rankDict).ContinueWithOnMainThread(task =>
            // {
            //     if (task.IsCanceled || task.IsFaulted)
            //     {
            //         Debug.LogError($"데이터 추가에 실패했습니다.. {task.Exception.Message}");
            //     }
            // });
            // FirebaseConnect.Instance.Db.Collection("rankings").Document(rankData.Email).SetAsync(rankDict).ContinueWithOnMainThread(task =>
            // {
            //     Debug.Log(String.Format("데이터가 성공적으로 저장되었습니다 ID Nickname {0}.", task.Id));
            // });
        
    }
    private void GetMyRanking()
    {
        DocumentReference docRef = FirebaseConnect.Instance.Db.Collection("rankings").Document(EMAIL);
        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            var snapshot = task.Result;
            if (snapshot.Exists) {
                Debug.Log(String.Format("Document data for {0} document:", snapshot.Id));
                Dictionary<string, object> city = snapshot.ToDictionary();
                foreach (KeyValuePair<string, object> pair in city) 
                {
                    Debug.Log(String.Format("{0}: {1}", pair.Key, pair.Value));
                }
            }
            else 
            {
                Debug.Log(String.Format("Document {0} does not exist!", snapshot.Id));
            }
        });
    }

    private void GetRankings()
    {
        // 쿼리(질의)란 컬렉션으로부터 데이터를 가져올 때 어떻게 가져와라고 쓰는 명령문 
        Query capitalQuery = FirebaseConnect.Instance.Db.Collection("rankings").OrderByDescending("Score");
        capitalQuery.GetSnapshotAsync().ContinueWithOnMainThread(task => 
        {
            QuerySnapshot capitalQuerySnapshot = task.Result;
            foreach (DocumentSnapshot documentSnapshot in capitalQuerySnapshot.Documents) 
            {
                Debug.Log(String.Format("Document data for {0} document:", documentSnapshot.Id));
                Dictionary<string, object> rankings = documentSnapshot.ToDictionary();
                
                foreach (KeyValuePair<string, object> pair in rankings) 
                {
                    Debug.Log(String.Format("{0}: {1}", pair.Key, pair.Value));
                }

                // Newline to separate entries
                Debug.Log("");
            };
        });
    }
}
