using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

// @이근혁
// static 변수 설정 및 전반적 게임 진행 Script
public class GameManager : MonoBehaviour
{
    [Header("Reference")]
    public static GameManager instance;         // GameManager
    public GameObject spawnPoints;              // SpawnPointGroup
    public GameObject[] blueCrystals;           // BlueCrystal
    public GameObject[] redCrystals;            // RedCrystal
    public GameObject gameUi;                   // GameUi
    public GameObject player;                   // Player

    [Header("Game Start")]
    public bool gameStart = false;              // 게임이 시작되었는지 여부
    public float startTime = 20f;               // 시작까지의 시간

    [Header("Game Over")]
    public GameObject gameOver;                 // GameOver
    public Text gameOverText;                   // GameOverText
    
    [Header("InGame Value")]
    public string nowEquipWeapon;               // 현재 착용중인 무기 이름
    public string nowEquipHead;                 // 현재 착용중인 머리 이름
    public string nowEquipBody;                 // 현재 착용중인 몸 이름
    [SerializeField]
    private int maxHealth = 100;                // 플레이어 체력
    public int currentHealth;                   // 플레이어 현재 체력
    public GameObject playerSpawnPoint;         // 플레이어 스폰 위치
    public GameObject playerNameUi;             // 생성된 playerName
    

    [Header("Weapon Property")]
    public int swordDamage = 20;                // Sword 데미지
    public int axeDamage = 20;                  // Axe 데미지
    public int spearDamage = 20;                // Spear 데미지
    public int gunDamage = 20;                  // Gun 데미지
    public int bowDamage = 20;                  // Bow 데미지

    void Awake()
    {
        instance = this;
        nowEquipWeapon = "Sword";
        nowEquipHead = "Head_None";
        nowEquipBody = "Body_None";
        currentHealth = maxHealth;
    }

    public void FindCharacter()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject obj in players){
            PhotonView pv = obj.GetPhotonView();
            if (pv.IsMine) {
                player = obj;
                return;
            }
        }
    }

    private void Update() 
    {
        // Player Name 위치 설정
        if (playerNameUi != null) {
            if (startTime <= 0) playerNameUi.transform.localPosition = gameObject.transform.position + (70 * Vector3.up);
            else playerNameUi.transform.localPosition = (player.transform.position * 19) + (Vector3.down * 120);
        }

        // 게임 시작
        if (gameStart){
            PlayerDie();
            GameOver();
        }
    }

    // 플레이어 생성 위치 설정
    public void SetSpawn()
    {
        // blueteam 캐릭터 spawn 위치를 배열에 저장
        if (player.tag == "BlueTeam") playerSpawnPoint = spawnPoints.transform.Find("BlueSpawnPoint").gameObject;
        // redteam 캐릭터 spawn 위치를 배열에 저장
        else if (player.tag == "RedTeam") playerSpawnPoint = spawnPoints.transform.Find("RedSpawnPoint").gameObject;
    }

    void PlayerDie()
    {
        if (currentHealth <= 0) {
            player.transform.position = playerSpawnPoint.transform.position;
            currentHealth = maxHealth;
        }
    }

    void GameOver()
    {
        int destroyedBlueCystal = 0;
        int destroyedRedCystal = 0;
        foreach (GameObject crystal in blueCrystals) {
            if (!crystal.activeSelf) 
                destroyedBlueCystal += 1;
        }
        foreach (GameObject crystal in redCrystals) {
            if (!crystal.activeSelf) 
                destroyedRedCystal += 1;
        }
        if (destroyedBlueCystal == 3) {
            gameOver.SetActive(true);
            gameOverText.text = "Red Team Win";
        }
        else if (destroyedRedCystal == 3) {
            gameOver.SetActive(true);
            gameOverText.text =  "Blue Team Win";
        }
    }

    // 플레이어 UI 생성
    public void InstantPlayerUI()
    {
        playerNameUi = PhotonNetwork.Instantiate("playerName", gameObject.transform.position + (70 * Vector3.up), gameObject.transform.rotation);
        playerNameUi.transform.SetParent(gameUi.transform);
        playerNameUi.GetComponent<Text>().text = PhotonNetwork.NickName;
    } 
}
