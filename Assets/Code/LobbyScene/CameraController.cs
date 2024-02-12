using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// @이근혁
// 카메라 움직임 제어 Script
// 현재 적용 X 카메라 고정 상태
public class CameraController : MonoBehaviour
{
    private WaitForSecondsRealtime wait = new WaitForSecondsRealtime(0.001f);
    private GameObject player;
    private Transform trans;
    private float camDistance = -7;
    private Vector3 modifyPos;

    private void Awake()
    {
        trans = GetComponent<Transform>();
        modifyPos = new Vector3 (0, 0, camDistance);
    }

    private void Update() 
    {
        // Player가 정해지지 않았으면(방에 입장하지 않았으면) 카메라-플레이어 동기화 안함
        if (GameManager.instance.player == null || !GameManager.instance.gameStart) return;
        // Player가 정해졌다면(방에 입장했다면) player 초기화
        else if (player == null) {
            player = GameManager.instance.player;
            // 확대 연출
            CamControll();
        }
        // 카메라가 플레이어를 비춤
        trans.position = player.transform.position + modifyPos;
    }

    // 카메라 회전 방지
    private void LateUpdate()
    {
        trans.rotation = Quaternion.identity;
    }

    // 캐릭터 생성 시 카메라 줌 아웃 연출
    private IEnumerator CamDelay(Vector3 pos)
    {
        Vector3 position = pos;
        yield return wait;
        trans.localPosition = position; 
    }

    private void CamControll()
    {
        Vector3 pos = new Vector3 (player.transform.position.x, player.transform.position.y, -2);   // 카메라 위치 계산
        trans.position = pos;       // 카메라 위치 설정
        // Player를 근접에서 먼거리로 줌아웃
        for (float camZ = trans.transform.position.z; camZ >= camDistance; camZ -= 0.05f){
            pos.z = camZ;
            StartCoroutine(CamDelay(pos));      
        }
    }
    
}
