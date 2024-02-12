using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject Line;         // 해당 라인(Top, Mid, Bottom)
    private float lineDist = 4;     // 라인 중앙에서 떨어진 거리

    // 포탈 접촉 시 해당 위치로 이동하는 함수
    private void OnTriggerEnter2D(Collider2D other) 
    {
        // Blue팀일 시 이용하는 포탈(각 라인의 왼쪽으로 이동)
        if (gameObject.tag == "BluePortal"){
            other.transform.position = Line.transform.position + (lineDist * Vector3.left);
        }
        // Red팀일 시 이용하는 포탈(각 라인의 오른쪽으로 이동)
        else if (gameObject.tag == "RedPortal") {
            other.transform.position = Line.transform.position + (lineDist * Vector3.right);
        }
    }
}