using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

// @김주완
// 로그인 관련 함수 관리
public class Login : MonoBehaviour
{
    public InputField input; // ID text Field

    // 입력받은 ID를 Player Nickname으로 설정
    public void InputId()
    {
        string text = input.text;
        // 입력받은 ID가 공백일 시 실행 X 
        if (text == "")
            return;      
        PhotonNetwork.LocalPlayer.NickName = text;
        // Lobby 입장
        PhotonNetwork.JoinLobby();
        // LobbyScene으로 장면 전환
        SceneManager.LoadScene("3LobbyScene"); 
    }
}
