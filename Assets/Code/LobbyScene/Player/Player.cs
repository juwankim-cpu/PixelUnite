using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

// @김주완
// Player 이동 및 애니메이션 관련 Script
public class Player : MonoBehaviour
{
    [Header("Player Movement")]
    public Vector2 inputVec;                // 입력된 이동 방향
    public float speed = 3f;                // Player 이동 속도
    [SerializeField]
    private float lineMoveDelay = 2f;       // LineMove 쿨타임
    private float lineMoveCool = 0f;
    bool isMoving = false;                  // 현재 Player 이동 여부

    [Header("Player Property")]
    public Rigidbody2D rigid;               // Player RigidBody
    public Transform trans;                 // Player Transform
    public PhotonView pv;                   // Player PhotonView
    public SpriteRenderer sprite;           // Player Spriter
    public Animator ani;                    // Player Animator
    public Collider2D coll;                 // Player Collider
    
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        trans = GetComponent<Transform>();
        pv = GetComponent<PhotonView>();
        sprite = GetComponent<SpriteRenderer>();
        ani = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        // 본인의 플레이어 캐릭터만 컨트롤
        if (!pv.IsMine) return;    

        // 다음 이동 위치
        inputVec.x = Input.GetAxis("Horizontal");
        inputVec.y = Input.GetAxis("Vertical");
        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;

        // 이동
        rigid.MovePosition(rigid.position + nextVec);

        LineMove();
        lineMoveCool += Time.deltaTime;
        
        // isMoving 상태 변경
        if (nextVec != Vector2.zero) isMoving = true;
        else isMoving = false;

        // 캐릭터 회전 고정
        trans.rotation = Quaternion.identity;

        // 좌우 전환 동기화
        pv.RPC("FilpSprite", RpcTarget.All, inputVec);

        // 애니메이션 동기화
        if (isMoving) 
            pv.RPC("ChangeAnimationState", RpcTarget.All, true);
        else 
            pv.RPC("ChangeAnimationState", RpcTarget.All, false);
    }

    private void LateUpdate() 
    {
        trans.rotation = Quaternion.identity;
    }

    // 좌우 전환 동기화 RPC
    [PunRPC]
    void FilpSprite(Vector2 inputVec) 
    {
        //Change the direction of face (if except the "if", watch left when stop)
        if (inputVec.x != 0) {
            sprite.flipX = inputVec.x > 0;
        }
    }

    // 애니메이션 동기화 RPC
    [PunRPC]
    void ChangeAnimationState(bool state)
    {
        ani.SetBool("IsMoving", state);
    }

    void LineMove()
    {
        if (lineMoveCool >= lineMoveDelay) {
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.DownArrow)) {
                // Mid Line => Bottom Line
                if (transform.localPosition.y >= -0.75 && transform.localPosition.y <= 1.75) {
                    transform.position = new Vector3(transform.position.x, transform.position.y - 10, 0);
                }
                // Top Line => Mid Line
                else if (transform.localPosition.y >= 9.2 && transform.localPosition.y <= 11.8) {
                    transform.position = new Vector3(transform.position.x, transform.position.y - 10, 0);
                }
                lineMoveCool = 0;
            }
            else if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.UpArrow)) {
                // Mid Line => Top Li
                if (transform.localPosition.y > -0.75 && transform.localPosition.y < 1.75) {
                    transform.position = new Vector3(transform.position.x, transform.position.y + 10, 0);
                }
                // Bottom Line => Mid Line
                else if (transform.localPosition.y > -10.8 && transform.localPosition.y < -8.2) {
                    transform.position = new Vector3(transform.position.x, transform.position.y + 10, 0);
                }
                lineMoveCool = 0;
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D other) 
    {
        switch (other.tag){
            case "Sword":
                PlayerDamage(GameManager.instance.swordDamage);
                break;
            case "Axe":
                PlayerDamage(GameManager.instance.axeDamage);
                break;
            case "Spear":
                PlayerDamage(GameManager.instance.spearDamage);
                break;
            case "Gun":
                PlayerDamage(GameManager.instance.gunDamage);
                break;
            case "Bow":
                PlayerDamage(GameManager.instance.bowDamage);
                break;
        }
    }

    public void PlayerDamage(int damage)
    {
        GameManager.instance.currentHealth -= damage;
    }
}
