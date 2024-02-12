using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PoolManager : MonoBehaviour
{
    public GameObject[] prefabs;
    
    public GameObject Get(int index)
    {
        GameObject select = null;

        select = PhotonNetwork.Instantiate(prefabs[index].name, transform.position, transform.rotation);
        select.transform.SetParent(GameManager.instance.player.transform);

        return select;
    }
}
