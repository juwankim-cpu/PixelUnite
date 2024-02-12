using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

// @이근혁
// LoginScene에서 Photon관련 함수를 관리
public class LoginPhotonManager : MonoBehaviourPunCallbacks
{
    
    public GameObject Login;                    // LoginCanvas
    private readonly string version = "1.0";    // Game version

    private void Awake() 
    {
        // 같은 룸의 유저들에게 자동으로 씬을 로딩
        PhotonNetwork.AutomaticallySyncScene = true;
        // 같은 버전의 유저끼리 접속 허용
        PhotonNetwork.GameVersion = version;
        // 서버에 연결되지 않았다면 서버 접속
        if (!PhotonNetwork.IsConnected)
            PhotonNetwork.ConnectUsingSettings();
    }

    // 포톤 서버에 접속 후 호출되는 콜백 함수
    public override void OnConnectedToMaster()
    {
        Login.SetActive(true);
    }
}
