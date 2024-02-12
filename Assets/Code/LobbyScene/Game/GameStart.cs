using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

// @이근혁
// 게임 시작과 준비 시 Ui 및 시스템 설정 Script
public class GameStart : MonoBehaviour
{
    public Text readyTime;                      // ReadyTime

    [Header("Deactive Ui")]
    public GameObject preUi;                    // AfterRoomUi

    [Header("Active Ui")]
    public GameObject gameWall;                 // GameWall
    public GameObject gameLine;                 // LineGroup
    public GameObject gamePortal;               // PortalGroup
    public GameObject gameCrystal;              // CrystalGroup

    // 게임 진행 Object 활성화
    public void ActiveObject()
    {
        gameWall.SetActive(true);               // Wall 활성화
        gameLine.SetActive(true);               // Line 활성화
        gamePortal.SetActive(true);             // Portal 활성화
        gameCrystal.SetActive(true);            // Crystal 활성화
    }
    
    // 이전 Ui 비활성화
    public void DeactiveUi()
    {
        preUi.SetActive(false);
        readyTime.gameObject.SetActive(false);
    }

    private void Update() 
    {
        // 방에 들어와 있을 때 실행
        if (PhotonNetwork.InRoom && !GameManager.instance.gameStart){
            Ready();
            if (GameManager.instance.startTime <= 0) {
                GameInit();
            } 
        }        
    }

    // 준비, 시작 버튼 활성화
    void Ready()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers) {
            GameManager.instance.startTime -= Time.deltaTime;
        }       
        else {
            GameManager.instance.startTime = 20f;
        }
        readyTime.text = ((int)GameManager.instance.startTime).ToString();
    }

    // 게임 시작
    public void GameInit()
    {
        ActiveObject();
        DeactiveUi();
        GameManager.instance.gameStart = true;
        GameManager.instance.player.transform.position = GameManager.instance.playerSpawnPoint.transform.position;
    }
}
