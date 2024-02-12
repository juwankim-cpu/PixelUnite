using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Attack : MonoBehaviour
{
    PhotonView pvPlayer;               // Player PhotonView
    PhotonView pvWeapon;               // Weapon PhotonView
    Animator ani;                      // Weapon 애니메이터
    public GameObject attackZone;      // Weapon AttackZone
    float currentCoolTime;
    [SerializeField]
    float attackCoolTime = 3;

    private void Awake() 
    {
        pvPlayer =  GetComponentsInParent<PhotonView>()[1];
        pvWeapon = GetComponent<PhotonView>();
        ani = GetComponent<Animator>();
    }

    private void Update() 
    {
        // 본인의 Player Object가 아니면 return
        if (!pvPlayer.IsMine) return;
        
        // Space를 누르면 공격
        if ((Input.GetKeyDown(KeyCode.Space)) && (currentCoolTime >= attackCoolTime)) {
            currentCoolTime = 0;
            pvWeapon.RPC("WeaponAttack", RpcTarget.All);

            switch(GameManager.instance.nowEquipWeapon) {
                case "Sword":
                    AudioManager.instance.PlaySfx(AudioManager.Sfx.Sword);
                    break;
                case "Axe":
                    AudioManager.instance.PlaySfx(AudioManager.Sfx.Axe);
                    break;
                case "Spear":
                    AudioManager.instance.PlaySfx(AudioManager.Sfx.Spear);
                    break;
                case "Gun":
                    AudioManager.instance.PlaySfx(AudioManager.Sfx.Gun);
                    break;
                case "Bow":
                    AudioManager.instance.PlaySfx(AudioManager.Sfx.Bow);
                    break;
            }
        }

        currentCoolTime += Time.deltaTime;
    }

    [PunRPC]
    void WeaponAttack()
    {
        if (GameManager.instance.nowEquipWeapon != "Gun")
             ani.SetTrigger("Attack");

        attackZone.SetActive(true);
        
        StartCoroutine(AttackCool());
    }

    IEnumerator AttackCool()
    {
        yield return new WaitForSecondsRealtime(0.05f);
        attackZone.SetActive(false);
    }
}

