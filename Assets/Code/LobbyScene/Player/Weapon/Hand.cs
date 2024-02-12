using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

// @이근혁
// 서버에 무기 방향 동기화
public class Hand : MonoBehaviour
{
    public SpriteRenderer weaponSpriter;        // 무기 Spriter
    public SpriteRenderer attackSpriter;        // 공격범위 Spriter
    BoxCollider2D coll;                         // 공격범위 Collider
    SpriteRenderer playerSpriter;               // Player Spriter (부모의 Spriter)
    PhotonView pv;                              // 무기 PhotonView
    bool isReverse;                             // Player 좌우 반전 여부
    Vector2 collOffset;                         // Collider 방향
    Vector2 xReverseCollOffset;                  // Collider X방향 반전

    void Awake()
    {
        playerSpriter = GetComponentsInParent<SpriteRenderer>()[1];
        weaponSpriter = GetComponent<SpriteRenderer>();
        coll = attackSpriter.GetComponent<BoxCollider2D>();
        collOffset = new Vector2(coll.offset.x, coll.offset.y);
        xReverseCollOffset = new Vector2(-coll.offset.x, coll.offset.y);
        pv = GetComponent<PhotonView>();
    }

    private void OnEnable() 
    {
        pv.RPC("Equip", RpcTarget.AllBuffered);
    }

    private void OnDisable() 
    {
        pv.RPC("Disarm", RpcTarget.AllBuffered);
    }

    // Player 좌우 전환 시 무기 좌우 전환 함수
    void LateUpdate()
    {
        // Player 방향이 좌인지 우인지 확인(isReverse가 false일 시 좌)
        isReverse = playerSpriter.flipX;
        switch(GameManager.instance.nowEquipWeapon)
        {
            // 좌우 전환 시 X축 반전
            case "Sword":
            case "Gun":
            case "Bow":
            case "Axe":
                if (isReverse) {
                    weaponSpriter.flipX = false;
                    attackSpriter.flipX = false;
                    coll.offset = collOffset;
                }
                else {
                    weaponSpriter.flipX = true;
                    attackSpriter.flipX = true;
                    coll.offset = xReverseCollOffset;
                }
                break;        
            // Spear는 좌우 전환 시 Y축 반전
            case "Spear":
                if (isReverse) {
                    weaponSpriter.flipY = false;
                    attackSpriter.flipX = false;
                    coll.offset = collOffset;
                }
                else {
                    weaponSpriter.flipY = true;
                    attackSpriter.flipX = true;
                    coll.offset = xReverseCollOffset;
                }
                break;
        }
    }

    // 무기 활성화 시 동기화
    [PunRPC]
    void Equip()
    {
        gameObject.SetActive(true);
    }

    // 무기 비활성화 시 동기화
    [PunRPC]
    void Disarm()
    {
        gameObject.SetActive(false);
    }
}