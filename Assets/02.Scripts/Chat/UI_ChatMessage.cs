using TMPro;
using UnityEngine;

public class UI_ChatMessage : MonoBehaviour
{
    public EChatType ChatType;

    public TextMeshProUGUI NicknameText;
    public TextMeshProUGUI MessageText;
    public TextMeshProUGUI DateTimeText;

    public void Set(Chat chat)
    {
        NicknameText.text = chat.Nickname;
        MessageText.text = chat.Message;
        DateTimeText.text = "미구현";
    }
}
