using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class LobyManager : MonoBehaviourPunCallbacks
{
   [SerializeField] TMP_InputField newRoomInputField;
   [SerializeField] TMP_Text feedback;
   [SerializeField] TMP_Text roomNameText;
   [SerializeField] GameObject roomPanel;
   [SerializeField] GameObject RoomListObj;
   [SerializeField] RoomItem roomItemPref;

   List<RoomItem> roomItemList = new List<RoomItem>();

   private void Start()
   {
        PhotonNetwork.JoinLobby();
   }

   public void ClickCreateRoom()
   {
        feedback.text = "";

        if (newRoomInputField.text.Length<3)
        {
            feedback.text =  "Room Name Minimum 3 Chacarters";
            return;
        }
        PhotonNetwork.CreateRoom(newRoomInputField.text);
   }

   public void JoinRoom(string roomName)
   {
        PhotonNetwork.JoinRoom(roomName);
   }

   public override void OnCreatedRoom()
   {
    feedback.text = "Created Room : " + PhotonNetwork.CurrentRoom.Name;
   }

   public override void OnJoinedRoom()
   {
    feedback.text = "Joined Room : " + PhotonNetwork.CurrentRoom.Name;
    roomNameText.text = PhotonNetwork.CurrentRoom.Name;
    roomPanel.SetActive(true);
   }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (var item in this.roomItemList)
        {
            Destroy(item.gameObject);
        }
        this.roomItemList.Clear();
        
        foreach (var roomInfo in roomList)
        {
            RoomItem newRoomItem = Instantiate(roomItemPref, RoomListObj.transform);
            newRoomItem.Set(this, roomInfo.Name);
            this.roomItemList.Add(newRoomItem);
        }
    }

}
