using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// @이근혁
// Gizmo로 Spawn point 생성
public class MyGizmo : MonoBehaviour
{
    public Color _color;            // 기즈모 색상
    public float _radius = 0.1f;    // 기즈모 크기

    void OnDrawGizmos()
    {
        // 기즈모의 색상 설정
        Gizmos.color = _color;
        // 구 형태의 기즈모
        Gizmos.DrawSphere(transform.position, _radius);
    }
}
