using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

// @이근혁
// 무기 착용 관리
public class EquipWeapon : MonoBehaviourPunCallbacks
{
    GameObject player; // 자신의 Player Object

    // Sword 장착
    public void ActiveSword()
    {
        player = GameManager.instance.player;
        Equip(player, "Sword");
    }

    // Gun 장착
    public void ActiveGun()
    {
        player = GameManager.instance.player;
        Equip(player, "Gun");
    }

    // Axe 장착
    public void ActiveAxe()
    {
        player = GameManager.instance.player;
        Equip(player, "Axe");
    }

    // Spear 장착
    public void ActiveSpear()
    {
        player = GameManager.instance.player;
        Equip(player, "Spear");
    }

    // Bow 장착
    public void ActiveBow()
    {
        player = GameManager.instance.player;
        Equip(player, "Bow");
    }

    // 무기 착용 함수
    // 플레이어가 다른 무기를 장착하고 있을 경우 해당 무기 비활성화 후 착용
    // 기본 무기는 Sword로 설정됨
    void Equip(GameObject player, string weapon)
    {
        Transform playerTrans = player.transform;
        foreach(Transform equipment in playerTrans.Find("Weapon").GetComponentsInChildren<Transform>(true)){
            if ((equipment.parent == playerTrans.Find("Weapon")) && (equipment.gameObject.activeSelf))
                equipment.gameObject.SetActive(false);
        }
        playerTrans.Find("Weapon").Find(weapon).gameObject.SetActive(true);
        GameManager.instance.nowEquipWeapon = weapon;
    }
}
