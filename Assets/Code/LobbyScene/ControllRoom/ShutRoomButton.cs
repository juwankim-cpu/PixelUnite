using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// @이근혁
// 방 설정 UI 닫기
public class ShutRoomButton : MonoBehaviour
{
    public GameObject RoomButton;

    public void Shut()
    {
        RoomButton.gameObject.SetActive(false);
    }
}
