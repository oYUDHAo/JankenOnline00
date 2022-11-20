using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class NetCard : MonoBehaviourPun
{
    public static List<NetCard> NetPlayers = new List<NetCard>(2);
    private Player cardPlayer;
    private Card[] cards;

    public void Set(Player player)
    {
        cardPlayer = player;
        cards = player.GetComponentsInChildren<Card>();
        foreach (var card in cards)
        {
            var button = card.GetComponent<Button>();
            button.onClick.AddListener(()=> RemoteClickButton(card.AttackValue));
        }
    }

    private void OnDestroy()
    {
        foreach (var card in cards)
        {
            var button = card.GetComponent<Button>();
            button.onClick.RemoveListener(()=>RemoteClickButton(card.AttackValue));
        }
    }

    private void RemoteClickButton(Attack value)
    {
        if (photonView.IsMine)
            photonView.RPC("RemoteClickButtonRPC", RpcTarget.Others, (int) value);
    }
    [PunRPC]
    private void RemoteClickButtonRPC(int value)
    {
        foreach (var card in cards)
        {
            var button = card.GetComponent<Button>();
            button.onClick.Invoke();
            break;
        }
    }

    private void OnEnable()
    {
        NetPlayers.Add(this);
    }

    private void OnDisable()
    {
        NetPlayers.Remove(this);
    }
}
