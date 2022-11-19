using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class RoomItem : MonoBehaviour
{
    [SerializeField] TMP_Text roomNameText;
    LobyManager manager;

    public void Set(LobyManager manager, string roomName)
    {
        this.manager = manager;
        roomNameText.text = roomName;
    }

    public void ClickRoomName()
    {
        manager.JoinRoom(roomNameText.text);
    }
    
}
