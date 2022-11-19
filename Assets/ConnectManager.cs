using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.SceneManagement;

public class ConnectManager : MonoBehaviourPunCallbacks
{
   [SerializeField] TMP_InputField usernameInput;
   [SerializeField] TMP_Text feedback;


   public void ClickConnect()
   {
        feedback.text = "";

        if (usernameInput.text.Length < 3)
        {
            feedback.text = "Username minimum 3 characters";
                return;
        }

        PhotonNetwork.NickName = usernameInput.text;

        PhotonNetwork.ConnectUsingSettings();
        feedback.text = "Connecting...";
   }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        feedback.text = "Connected to Master";
        SceneManager.LoadScene("Loby");
    }
}
