using System.Threading.Tasks;
using Firebase;
using Firebase.Auth;
using Firebase.Firestore;
using UnityEngine;

public class FirebaseConnect : Singleton<FirebaseConnect>
{
	public Task Initialization => _initializationTask;
	private Task _initializationTask;
	
	public FirebaseAuth Auth { get; private set; }
	public FirebaseFirestore Db { get; private set; }

	protected override void Awake()
	{
		base.Awake();
		_initializationTask = Init(); 
	}

	// 파이어베이스 내 프로젝트 연결
	private async Task Init()
	{
		DependencyStatus dependencyStatus = await FirebaseApp.CheckAndFixDependenciesAsync();

		if (dependencyStatus == DependencyStatus.Available)
		{
			Debug.Log("파이어베이스 연결에 성공했습니다");
			
			Auth = FirebaseAuth.DefaultInstance;
			Db = FirebaseFirestore.DefaultInstance;
		}
		else
		{
			Debug.LogError($"파이어베이스 연결 실패했습니다 {dependencyStatus}");
		}

	}
}
