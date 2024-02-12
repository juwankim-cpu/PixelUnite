using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// @이근혁
// 방 설정 UI 열기
public class OpenRoomButton : MonoBehaviour
{
    public GameObject RoomButton;

    public void Open()
    {
        RoomButton.gameObject.SetActive(true);
    }
}
