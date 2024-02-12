using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

// @이근혁
// 서버에 무기 방향 동기화
public class SyncGaze : MonoBehaviour
{
    SpriteRenderer equipSpriter;                // 장비 Spriter
    PhotonView pv;                              // 장비 PhotonView
    SpriteRenderer playerSpriter;               // Player Spriter (부모의 Spriter)
    bool isReverse;                             // Player 좌우 반전 여부

    void Awake()
    {
        playerSpriter = GetComponentsInParent<SpriteRenderer>()[1];
        equipSpriter = GetComponent<SpriteRenderer>();   
        pv = GetComponent<PhotonView>();
    }

    // Player 좌우 전환 시 무기 좌우 전환 함수
    void LateUpdate()
    {
        // Player 방향이 좌인지 우인지 확인(isReverse가 false일 시 좌)
        isReverse = playerSpriter.flipX;
        equipSpriter.flipX = isReverse;
    }

    private void OnEnable() 
    {
        pv.RPC("Equip", RpcTarget.AllBuffered);
    }

    private void OnDisable() 
    {
        pv.RPC("Disarm", RpcTarget.AllBuffered);
    }

    // 장비 활성화 시 동기화
    [PunRPC]
    void Equip()
    {
        gameObject.SetActive(true);
    }

    // 장비 비활성화 시 동기화
    [PunRPC]
    void Disarm()
    {
        gameObject.SetActive(false);
    }
}