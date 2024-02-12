using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// @이근혁
// TitleScene 관리
public class TitleControl : MonoBehaviour
{
    private void Awake() 
    {
        StartCoroutine(ShowScene());
    }

    // Title를 1초간 보여준다.
    IEnumerator ShowScene()
    {
        yield return new WaitForSecondsRealtime(1.0f);
        SceneManager.LoadScene("2LoginScene");
    }
}
