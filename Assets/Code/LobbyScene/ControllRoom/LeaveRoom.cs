using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class LeaveRoom : MonoBehaviour
{
    public void leaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("3LobbyScene");
    }
}
