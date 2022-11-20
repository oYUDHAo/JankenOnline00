using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using UnityEngine.UI;

public class LobyManager : MonoBehaviourPunCallbacks
{
   [SerializeField] TMP_InputField newRoomInputField;
   [SerializeField] TMP_Text feedback;
   [SerializeField] Button StartButton;
   [SerializeField] TMP_Text roomNameText;
   [SerializeField] GameObject roomPanel;
   [SerializeField] GameObject RoomListObj;
   [SerializeField] GameObject PlayerListObj;
   [SerializeField] RoomItem roomItemPref;
   [SerializeField] PlayerItem PlayerItemPref;

   List<RoomItem> roomItemList = new List<RoomItem>();
   List<PlayerItem> playerItemList = new List<PlayerItem>();

   private void Start()
   {
        PhotonNetwork.JoinLobby();
        roomPanel.SetActive(false);
   }

   public void ClickCreateRoom()
   {
        feedback.text = "";

        if (newRoomInputField.text.Length<3)
        {
            feedback.text =  "Room Name Minimum 3 Chacarters";
            return;
        }
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;
        PhotonNetwork.CreateRoom(newRoomInputField.text);
   }

   public void ClickStartGame(string levelName)
   {
        if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.LoadLevel(levelName);
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

    UpdatePlayerList();
   }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        UpdatePlayerList();
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        UpdatePlayerList();
    }

    public override void OnMasterClientSwitched(Photon.Realtime.Player newMasterClient)
    {
        SetStartGameButton();
    }

    private void SetStartGameButton()
    {
        StartButton.gameObject.SetActive(PhotonNetwork.IsMasterClient);
        StartButton.interactable = PhotonNetwork.CurrentRoom.PlayerCount > 1;
    }

    private void UpdatePlayerList()
    {
        foreach (var item in playerItemList)
        {
            Destroy(item.gameObject);
        }
        playerItemList.Clear();

        foreach (var (id, player) in PhotonNetwork.CurrentRoom.Players)
        {
            PlayerItem newPlayerItem = Instantiate(PlayerItemPref, PlayerListObj.transform);
            newPlayerItem.Set(player);
            playerItemList.Add(newPlayerItem);

            if(player == PhotonNetwork.LocalPlayer)
                newPlayerItem.transform.SetAsFirstSibling();
        }
        SetStartGameButton();
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
