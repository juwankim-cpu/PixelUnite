using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

// @이근혁
// 무기 착용 관리
public class EquipHead : MonoBehaviourPunCallbacks
{
    GameObject player; // 자신의 Player Object

    // Head_None 장착
    public void ActiveNoneHead()
    {
        player = GameManager.instance.player;
        Equip(player, "Head_None");
    }

    // Head_Knight 장착
    public void ActiveKnightHead()
    {
        player = GameManager.instance.player;
        Equip(player, "Head_Knight");
    }

    // Head_Soldier 장착
    public void ActiveSoldierHead()
    {
        player = GameManager.instance.player;
        Equip(player, "Head_Soldier");
    }

    // Head_Maid 장착
    public void ActiveMaidHead()
    {
        player = GameManager.instance.player;
        Equip(player, "Head_Maid");
    }

    // Head_Panty 장착
    public void ActivePantyHead()
    {
        player = GameManager.instance.player;
        Equip(player, "Head_Panty");
    }

    // 장비 착용 함수
    // 플레이어가 다른 장비를 장착하고 있을 경우 해당 장비 비활성화 후 착용
    // 기본 장비는 None으로 설정됨
    void Equip(GameObject player, string head)
    {
        Transform playerTrans = player.transform;
        foreach(Transform equipment in playerTrans.Find("Head").GetComponentsInChildren<Transform>(true)){
            if ((equipment.parent == playerTrans.Find("Head")) && (equipment.gameObject.activeSelf))
                equipment.gameObject.SetActive(false);
        }
        playerTrans.Find("Head").Find(head).gameObject.SetActive(true);
        GameManager.instance.nowEquipHead = head;
    }
}
