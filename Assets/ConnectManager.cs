using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class ConnectManager : MonoBehaviourPunCallbacks
{
   [SerializeField] TMP_InputField usernameInput;
   [SerializeField] TMP_Text feedbackText;


   public void ClickConnect()
   {
        feedbackText.text = "";

        if (usernameInput.text.Length < 3)
        {
            feedbackText.text = "Username minimum 3 characters";
                return;
        }

        PhotonNetwork.ConnectUsingSettings();
        feedbackText.text = "Connecting...";
   }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        feedbackText.text = "Connected to Master";
    }
}
