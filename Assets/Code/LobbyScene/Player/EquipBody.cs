using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

// @이근혁
// 무기 착용 관리
public class EquipBody : MonoBehaviourPunCallbacks
{
    GameObject player; // 자신의 Player Object

     // Head_None 장착
    public void ActiveNoneHead()
    {
        player = GameManager.instance.player;
        Equip(player, "Body_None");
    }

    // Body_Knight 장착
    public void ActiveKnightBody()
    {
        player = GameManager.instance.player;
        Equip(player, "Body_Knight");
    }

    // Body_Soldier 장착
    public void ActiveSoldierBody()
    {
        player = GameManager.instance.player;
        Equip(player, "Body_Soldier");
    }

    // Body_Maid 장착
    public void ActiveMaidBody()
    {
        player = GameManager.instance.player;
        Equip(player, "Body_Maid");
    }

    // Body_Panty 장착
    public void ActivePantyBody()
    {
        player = GameManager.instance.player;
        Equip(player, "Body_Panty");
    }

    // 장비 착용 함수
    // 플레이어가 다른 장비를 장착하고 있을 경우 해당 장비 비활성화 후 착용
    // 기본 장비는 None으로 설정됨
    void Equip(GameObject player, string body)
    {
        Transform playerTrans = player.transform;
        foreach(Transform equipment in playerTrans.Find("Body").GetComponentsInChildren<Transform>(true)){
            if ((equipment.parent == playerTrans.Find("Body")) && (equipment.gameObject.activeSelf))
                equipment.gameObject.SetActive(false);
        }
        playerTrans.Find("Body").Find(body).gameObject.SetActive(true);
        GameManager.instance.nowEquipBody = body;
    }
}
