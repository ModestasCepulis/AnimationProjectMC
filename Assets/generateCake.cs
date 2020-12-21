using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class generateCake : MonoBehaviour
{
  //  public GameObject cake;
    GameObject CakeBoss;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.InstantiateRoomObject(Path.Combine("PhotonPrefabs", "CakeController"), new Vector3(38f, 0, -6135.982f), Quaternion.identity);
        // PhotonNetwork.InstantiateRoomObject(Path.Combine("PhotonPrefabs", "HamObject"), new Vector3(731f, 157.9f, -594.4f), Quaternion.identity);
        PhotonNetwork.InstantiateRoomObject(Path.Combine("PhotonPrefabs", "HamObject"), new Vector3(586.4f, 95.1f, -158.993f), Quaternion.identity);


    }

    // Update is called once per frame
    void Update()
    {

    }
}
