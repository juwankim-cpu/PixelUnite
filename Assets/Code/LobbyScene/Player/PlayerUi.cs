using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerUi : MonoBehaviourPunCallbacks
{
    public Text playerName;               // Text PlayerName Prefab
    GameObject playerNameUi;              // 생성된 playerName
    PhotonView pv;                        // PhotonView
    public GameObject playerHp;           // 

    private void Awake() 
    {
        pv = GetComponent<PhotonView>();
        
    }

    private void Update() 
    {
        if (playerNameUi.gameObject.activeSelf) return;

        if (PhotonNetwork.InRoom) 
            playerNameUi.gameObject.SetActive(true);
    }

    [PunRPC]
    void InstantiateNameUi()
    {
        
    }
}
