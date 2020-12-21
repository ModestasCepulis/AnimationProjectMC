using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class spawnArea : MonoBehaviour
{
    PhotonView PV;


    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(PV.IsMine)
        {
            if (other.gameObject.tag == "Player")
            {
                PV.RPC("spawnObject", RpcTarget.All);
            }
        }
    }

    [PunRPC]
    public void spawnObject()
    {
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "EmptyBecker"), new Vector3(118, 72, -124), Quaternion.identity);
    }
}
