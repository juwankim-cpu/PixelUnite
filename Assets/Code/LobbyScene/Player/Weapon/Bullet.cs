using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


// @김주완 @이근혁
// 총알, 화살 등 투사체에 대한 함수 관리
public class Bullet : MonoBehaviour
{
    GameObject bullet;
    Rigidbody2D rigid;
    Transform trans;
    PhotonView pv;
    public float speed;
   
    void Awake()
    {
        trans = GetComponent<Transform>();
        rigid = GetComponent<Rigidbody2D>();
        pv  = GetComponent<PhotonView>();
    }

    private void OnEnable() 
    {
        int dir = GameManager.instance.player.GetComponent<SpriteRenderer>().flipX ? 1 : -1;
        rigid.velocity = Vector2.right * dir * speed;
    }

    // Collision 충돌 시 발생 함수
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        switch (collision.tag)
        {
            // 상대 Player와 부딫혔을 때 실행
            // 후에 플레이어들에게 다른 tag를 붙여 상대 구별 예정
            case "Player":
            // 상대 팀 Crystal과 부딫혔을 때 실행
            case "Crystal":
            // 맵 너머 Wall에 부딫혔을 때 실행
            case "Wall":
                DestroyBullet();
                break;
        }
    }

    // 투사체 비활성화
    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
