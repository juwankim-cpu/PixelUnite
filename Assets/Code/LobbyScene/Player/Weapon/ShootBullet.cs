using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

// @김주완 @이근혁
// 원거리 공격 Script
public class ShootBullet : MonoBehaviour
{
    PhotonView pvPlayer;        // Player PhotonView
    PhotonView pvWeapon;        // Weapon PhotonView
    string weapon;              // 현재 착용중인 무기
    GameObject bulletObject;    // 무기에 따른 투사체(prefab)
    Transform muzzleTrans;      // 투사체 발사 위치
    Animator Ani;            // Bow 애니메이터

    private void Awake() {
        pvPlayer = transform.parent.gameObject.GetComponent<PhotonView>();
        pvWeapon = GetComponent<PhotonView>();
        weapon = GameManager.instance.nowEquipWeapon;
        Ani = GetComponent<Animator>();
    }

    private void Update() 
    {
        // 본인의 Player Object가 아니면 return
        if (!pvPlayer.IsMine) return;
        // Space를 누르면 발사
        if (Input.GetKeyDown(KeyCode.Space)) {
            Shoot();
            switch(GameManager.instance.nowEquipWeapon){
                case "Bow":
                    AudioManager.instance.PlaySfx(AudioManager.Sfx.Bow);
                    break;
                case "Gun":
                    AudioManager.instance.PlaySfx(AudioManager.Sfx.Gun);
                    break;
            }
        }
        // 현재 장착중인 무기 설정
        weapon = GameManager.instance.nowEquipWeapon;
    }

    // 투사체 발사 함수
    void Shoot()
    {
        // 투사체 발사 위치
        muzzleTrans = gameObject.transform.Find("Muzzle").gameObject.transform;
        // 무기에 다른 투사체 찾기
        switch(weapon){
            case "Gun":
                //bulletObject = GameManager.instance.pool.Get(0);
                break;
            case "Bow":
                //bulletObject = GameManager.instance.pool.Get(1);
                break;         
        }
        // 투사체를 발사 위치에 이동
        bulletObject.transform.position = muzzleTrans.position;
        // 투사체 좌우 반전 설정
        if (!GameManager.instance.player.GetComponent<SpriteRenderer>().flipX) {
            bulletObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        pvWeapon.RPC("WeaponAttack", RpcTarget.All);
    }

    [PunRPC]
    void WeaponAttack()
    {
        if (weapon == "Bow")
            Ani.SetTrigger("Attack");
    }
}
