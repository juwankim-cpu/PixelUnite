using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// @이근혁
// 투사체가 나가는 위치 관리
public class Muzzle : MonoBehaviour
{
    public SpriteRenderer player;   // Player Spriter (부모의 Spriter)
    bool isReverse;                 // Player 좌우 반전 여부
    Vector3 muzzleLeft;             // Muzzle의 좌 위치
    Vector3 muzzleRight;            // Muzzle의 우 위치

    void Awake()
    {
        player = GetComponentsInParent<SpriteRenderer>()[1];
        // Muzzle의 현재 위치
        float muzzleX = gameObject.transform.localPosition.x;
        float muzzleY = gameObject.transform.localPosition.y;
        // Muzzle의 좌우 전환 시의 위치
        muzzleLeft = new Vector3 (muzzleX * -1, muzzleY, 0);
        muzzleRight = new Vector3 (muzzleX, muzzleY, 0);
    }

    // Player의 좌우 반전을 따라 Muzzle도 반전
    void LateUpdate()
    {
        // Player 방향이 좌인지 우인지 확인(isReverse가 false일 시 좌)
        isReverse = player.flipX;
        // Player 방향이 좌일 때
        if (!isReverse) {
            gameObject.transform.localPosition = muzzleLeft;
        }
        // Player 방향이 우일 때
        else {    
            gameObject.transform.localPosition = muzzleRight;
        }
            
    }
}
