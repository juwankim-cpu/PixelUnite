using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Crystal : MonoBehaviour, IPunObservable
{
    PhotonView pv;                              // Crystal PhotonView

    [Header("Crystal Info")]
    [SerializeField]
    private int crystalMaxHp = 100;             // Crystal 최대 체력
    public int crystalCurrentHp;                // Crystal 현재 체력

    private void Awake() 
    {
        pv = GetComponent<PhotonView>();
        crystalCurrentHp = crystalMaxHp;
    }

    private void Update() 
    {
        if (crystalCurrentHp <= 0)
            pv.RPC("CystalDestroy", RpcTarget.All);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        switch(other.tag){
            case "Sword":
                pv.RPC("CystalDamage", RpcTarget.All, GameManager.instance.swordDamage);
                break;
            case "Axe":
                pv.RPC("CystalDamage", RpcTarget.All, GameManager.instance.axeDamage);
                break;
            case "Spear":
                pv.RPC("CystalDamage", RpcTarget.All, GameManager.instance.spearDamage);
                break;
            case "Gun":
                pv.RPC("CystalDamage", RpcTarget.All, GameManager.instance.gunDamage);
                break;
            case "Bow":
                pv.RPC("CystalDamage", RpcTarget.All, GameManager.instance.bowDamage);
                break;
        }
    }

    [PunRPC]
    public void CystalDamage(int damage)
    {
        crystalCurrentHp -= damage;
    }

    [PunRPC]
    public void CystalDestroy()
    {
        gameObject.SetActive(false);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // 데이터를 쓰기 (송신)
            stream.SendNext(crystalCurrentHp);
        }
        else if (stream.IsReading)
        {
            // 데이터를 읽기 (수신)
            crystalCurrentHp = (int)stream.ReceiveNext();
        }
    }
}
