using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

// @이근혁
// Lobby에서의 Photon 설정 Script
public class LobbyPhotonManager : MonoBehaviourPunCallbacks, IPunObservable
{
    PhotonView pv;
    public GameObject beforeRoom;           // 방 입장 전 UI
    public GameObject afterRoom;            // 방 입장 후 UI
    public Text stateText;                  // 현재 상태 텍스트
    public GameObject gameUi;               // GameUi

    Text roomNameNum;
    Text PlayerNum;

    private void Awake() 
    {
        pv = GetComponent<PhotonView>();
        roomNameNum = afterRoom.transform.Find("GameInfo").Find("RoomNameNum").GetComponent<Text>();
        PlayerNum = afterRoom.transform.Find("GameInfo").Find("PlayerNum").GetComponent<Text>();
    }

    // 로비에 입장했을 때 호출되는 콜백 함수
    public override void OnJoinedLobby()
    {
        stateText.text = $"어서오세요 \"{PhotonNetwork.NickName}님!\"";
        beforeRoom.SetActive(true);
        afterRoom.SetActive(false);
        AudioManager.instance.PlayBgm(true);
    }

    // 랜덤한 룸 입장이 실패했을 경우 호출되는 콜백 함수
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        stateText.text = $"Failed {returnCode}:{message}";
    }

    // 룸 지정 입장이 실패했을 경우 호출되는 콜백 함수
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        stateText.text = $"Failed {returnCode}:{message}";
    }

    // 룸 생성이 완료된 후 호출되는 콜백 함수
    public override void OnCreatedRoom()
    {
        Debug.Log("Created Room");
        Debug.Log($"Room Name = {PhotonNetwork.CurrentRoom.Name}");          
    }

    // 룸 입장 후 호출되는 콜백 함수
    public override void OnJoinedRoom()
    {
        // 플레이어 소환
        PhotonNetwork.Instantiate("Player", transform.position, transform.rotation);
        // 플레이어 캐릭터 찾기
        GameManager.instance.FindCharacter();
        // 플레이어에게 팀 부여
        TeamMaker();
        // spawn 위치 조정
        Spawn();
        // Player Name 활성화
        GameManager.instance.InstantPlayerUI();

        // Room 정보 표시(Room 이름)
        roomNameNum.text =  $"현재 방: {PhotonNetwork.CurrentRoom.Name}";

        // UI 교체
        beforeRoom.SetActive(false);
        afterRoom.SetActive(true);
        gameUi.SetActive(true);
    }

    private void Update() 
    {
        if (PhotonNetwork.InRoom){
            // Room 정보 표시(Player 수)
            PlayerNum.text = $"현재 플레이어 수: {PhotonNetwork.CurrentRoom.PlayerCount}";
        }
    }

    // 플레이어에게 팀 부여
    public void TeamMaker()
    {   
        // 1 vs 1
        if (PhotonNetwork.CurrentRoom.MaxPlayers == 2){
            // 방장은 BlueTeam, 다른 플레이어는 RedTeam Tag부여
            if (PhotonNetwork.IsMasterClient) SetTeam("BlueTeam");
            else SetTeam("RedTeam");
        }
        
        /* 3 vs 3
        else if (PhotonNetwork.CurrentRoom.MaxPlayers == threeVsThree) {
            int red = 0;
            int blue = 0;
            // 현재 방의 플레이어의 tag를 확인
            foreach(Player tag in tagPlayers) {  
                if (tag.tag == "RedTeam") red += 1;
                else if (tag.tag == "BlueTeam") blue += 1;
            }
            // 플레이어가 4명 이상일 시 tag 검사하여 팀 인원 조절 
            if (PhotonNetwork.CurrentRoom.PlayerCount > 3) {
                // red 팀이 3명일 시 blue팀, blue팀이 3명일 시 red팀으로 배정
                if (red == 3) player.tag = "BlueTeam";
                else if (blue == 3) player.GetPhotonView().RPC("SetRedTeam", RpcTar.get.All);
                // 각 팀이 3명이 안되었을 시 평소대로 tag 부여
                else {
                    // 3이하일 시 BlueTeam, 이상일 시 RedTeam
                    int teamNum = Random.Range(0, 2); 
                    if (teamNum == 0) pv.RPC("SetBlueTeam", RpcTarget.All);
                    else pv.RPC("SetRedTeam", RpcTarget.All);            
                }
            }
            else {
                // 3이하일 시 BlueTeam, 이상일 시 RedTeam
                int teamNum = Random.Range(0, 2); 
                if (teamNum == 0) pv.RPC("SetBlueTeam", RpcTarget.All);
                else pv.RPC("SetRedTeam", RpcTarget.All);;
            }
        }*/
    }
    
    [PunRPC]
    public void SetTeam(string team)
    {
        GameManager.instance.player.tag = team;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting) stream.SendNext(GameManager.instance.player.tag);                       // 데이터를 쓰기 (송신)
        else if (stream.IsReading) GameManager.instance.player.tag = (string)stream.ReceiveNext();    // 데이터를 읽기 (수신)
    }

    // 플레이어 생성 위치 설정
    void Spawn()
    {
        GameManager.instance.SetSpawn();
        // 캐릭터를 해당 Spawnpoint으로 이동
        GameManager.instance.player.transform.position = GameManager.instance.playerSpawnPoint.transform.position;
    }    
}
