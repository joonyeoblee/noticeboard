using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Chat : MonoBehaviour
{
    [Header("프리팹")]
    public UI_ChatMessage MinePrefab;
    public UI_ChatMessage OtherPrefab;
    public UI_ChatMessage Systemrefab;
    
    public TMP_InputField InputField;
    
    private List<UI_ChatMessage> _chatMessages = new();
    
    
    public void OnClickSendButton()
    {
        string text = InputField.text;
        if (string.IsNullOrEmpty(text)) return;
        
        
        Debug.Log("Send Message");
        ChatManager.Instance.SendPublicChatMessage(text);
        InputField.text = "";
        InputField.ActivateInputField();
    }
    void Start()
    {
        ChatManager.Instance.OnDataChanged += Refresh;
        InputField.lineType = TMP_InputField.LineType.MultiLineNewline;
    }

    void Update()
    {
        // 포커스가 있을 때만
        if (InputField.isFocused)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                {
                    // 줄바꿈
                    InputField.text += "\n";
                    InputField.caretPosition = InputField.text.Length;
                }
                else
                {
                    // 전송
                    OnClickSendButton();
                }

                // 입력 처리 방지
                EventSystem.current.SetSelectedGameObject(InputField.gameObject, null);
            }
        }
    }
    public void Refresh()
    {
        var chatList = ChatManager.Instance.ChatList;

        // ui를 다 지운다
        foreach (var ui in _chatMessages)
        {
            Destroy(ui.gameObject);
        }
        _chatMessages.Clear();
        // 챗 타입에 따라서 다른 프리팹을 생성해서 스크롤뷰(content에 넣어줘야 한다.)
        foreach (var chat in chatList)
        {
            UI_ChatMessage message = null;
            switch (chat.Type)
            {
                case EChatType.Mine:
                    message = Instantiate(MinePrefab);
                    break;
                case EChatType.Other:
                    message = Instantiate(OtherPrefab);
                    break;
                case EChatType.System:
                    message = Instantiate(Systemrefab);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            _chatMessages.Add(message);
        }
    }
}
