using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

// @이근혁
// 방 생성, 입장, 퇴장 관련 Script
public class RoomControll : MonoBehaviour
{
    [Header("Room Name")]
    public InputField creatRoomName;                    // 생성할 방 이름
    public InputField enterRoomName;                    // 입장할 방 이름

    // 3vs3 방 생성 함수
    public void Create3vs3Room()
    {
        string roomName = creatRoomName.text;
        if (roomName == "") return;
        RoomOptions option = new RoomOptions();
        option.MaxPlayers = 6;               // 최대 입장 인원
        option.IsOpen = true;                           // 방 모두 참여가능 여부
        option.IsVisible = true;                        // 방 공개

        PhotonNetwork.CreateRoom(roomName, option);
    }

    // 1vs1 방 생성 함수
    public void Create1vs1Room()
    {
        string roomName = creatRoomName.text;
        if (roomName == "") return;
        RoomOptions option = new RoomOptions();
        option.MaxPlayers = 2;                          // 최대 입장 인원
        option.IsOpen = true;                           // 방 모두 참여가능 여부
        option.IsVisible = true;                        // 방 공개

        PhotonNetwork.CreateRoom(roomName, option);
    }

    // 방 랜덤 입장
    public void JoinRandomRoom() 
    {
        PhotonNetwork.JoinRandomRoom();
    }

    // 방 이름으로 입장
    public void JoinRoom()
    {
        string roomName = enterRoomName.text;
        if (roomName != "") PhotonNetwork.JoinRoom(roomName);
        else return;
    }

    // 현재 방 나가기
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
}
