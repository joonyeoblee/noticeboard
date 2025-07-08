using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Chat;
using Unity.VisualScripting;
using UnityEngine;

public class ChatManager : Singleton<ChatManager>, IChatClientListener // 채팅 이벤트를 수신 받겠어요...
{
     private ChatClient _client;
     private const string DEFAULT_GLOBAL_CHANNEL = "global"; 
     private const string DEFAULT_NOTICE_CHANNEL = "notice";
     private const string NICKNAME = "Kyoungho"; 

     public event Action OnDataChanged;
     
     private List<Chat> _chatList;
     public List<Chat> ChatList => _chatList;
     
     // 인스턴스가 하나임을 보장
     // 전역접근을 보장
     protected override void Awake()
     {
          base.Awake();
     }

     protected override void Start()
     {
          base.Start();
          
          // IChatClientListener 구현 객체를 this로 넘겨줘서 초기화된다.
          _client = new ChatClient(this);
          
          // 디버그 로그 레벨
          _client.DebugOut = DebugLevel.ALL;
          
          // 서버 지역 설정(US, ES, ASIA)
          _client.ChatRegion = "ASIA";
          
          // 유저 ID
          var auth = new AuthenticationValues(NICKNAME);
          
          // 채팅 연결
          _client.Connect("95fc88d6-ff70-46bf-a61e-efc8219d85a0", "1.0.0", auth);
     }

     //채팅 이벤트 (총 11개)
     // 1. (1) 서버 로그
     // 2  (3) 서버 접속/해제/상태변화 (카카오톡 접속/해제)
     // 3. (2) 채널 접속/해제 (카카오톡 채팅방(1:1, 오픈채팅) 접속/해제)
     // 4. (2) 메시지 수신 (1:1. 오픈채팅)
     // 5. (2) 다른 사람 방 입장/퇴장 (카카오톡 단톡방 입장/퇴장)
     // 6. (1) 친구 이벤트(친구 상태 변화)
     
     // 포톤 챗 내부 로그 발생시 호출되는 함수(필터링 레벨 이상)
     public void DebugReturn(DebugLevel level, string message)
     {
          switch (level)
          {
               case DebugLevel.ERROR: Debug.LogError(message); break;
               case DebugLevel.WARNING: Debug.LogWarning(message); break;
               default: Debug.Log(message); break;
          }
     }
     private void Update()
     {
          //ChatClient는 MonoBehaviour가 아니므로, 매 프레임마다 서비스를 호출해줘야
          // 네트워크 메시지가 처리되고, 콜백 메서드들이 실행된다.
          _client?.Service();

          if (Input.GetKeyDown(KeyCode.Alpha1))
          {
               SendPublicChatMessage("안녕하세요");
          }
          if (Input.GetKeyDown(KeyCode.Alpha2))
          {
               SendPublicChatMessage("반갑습니다.");
          }
     }
     // 1. 서버 접속 / 해제
     public void OnDisconnected()
     {
          // 포톤 접속 해제가 생각보다 많이 호출된다.(와이파이 끊김, 아무 활동이 없는 경우 등등)
          // 따라서 재접속하는 코드를 짜줘야 함
          Debug.Log("포톤 챗 접속 종료");
          
     }
     public void OnConnected()
     {
          Debug.Log("포톤 챗 접속 완료");

          //_client.Subscribe("global"); // 채널 1개 구독
          _client.Subscribe(new string[] { DEFAULT_GLOBAL_CHANNEL, DEFAULT_NOTICE_CHANNEL }); // 채널 여러개 구독
     }
     public void OnChatStateChange(ChatState state)
     {
          Debug.Log($"포톤챗 상태 : {state}");
     }
     // 2. 채널 구독/해제
     public void OnSubscribed(string[] channels, bool[] results)
     {
          // channels : '이번에' 구독 요청한 채널들, 새로운 채널을 구독하면 갱신해야함
          // results : 구독 성공 여부

          for (int i = 0; i < channels.Length; i++)
          {
               Debug.Log($"[포톤챗] 구독 {channels[i]} (결과 : {results[i]})");
          }
          
          // UI 채널 리스트 갱신
          OnDataChanged?.Invoke();
          // 채널 리스트 갱신
          // -> 내가 구독 중인 채널 모든 목록을 알고 싶다면:
          foreach (var channel in _client.PublicChannels)
          {
               Debug.Log($"현재 구독중인 채널 : {channel.Key}");
          }
     }
     public void OnUnsubscribed(string[] channels)
     {
          foreach (var channel in channels)
          {
               Debug.Log($"[PhotonChat] Unsubscribed ▶ {channel}");
          }
          // UI 채널 리스트 갱신
          OnDataChanged?.Invoke();
     }
     
     public void OnGetMessages(string channelName, string[] senders, object[] messages)
     {
          for (int i = 0; i < messages.Length; i++)
          {
               Debug.Log($"[{channelName}] {senders[i]}: {messages[i]}");

               if (senders[i] == NICKNAME)
               {
                    _chatList.Add(new Chat(EChatType.Mine, senders[i], messages[i].ToString()));
               }
               else
               {
                    _chatList.Add(new Chat(EChatType.Other, senders[i], messages[i].ToString()));
               }
          }
          OnDataChanged?.Invoke();
     }
     
     //메세지 송수신
     public void SendPublicChatMessage(string message)
     {
          if (_client == null || _client.CanChat)
          {
               return;
          }
          _client.PublishMessage(DEFAULT_NOTICE_CHANNEL, message);
     }
     
     public void OnPrivateMessage(string sender, object message, string channelName)
     {
          Debug.Log($"[Whisper] {sender} ▶ {message}");
     }
     
     public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
     {
          throw new System.NotImplementedException();
     }
     public void OnUserSubscribed(string channel, string user)
     {
          throw new System.NotImplementedException();
     }
     public void OnUserUnsubscribed(string channel, string user)
     {
          throw new System.NotImplementedException();
     }
}

