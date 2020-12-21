using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class WhichPlayerToSpawn : MonoBehaviour
{

    public GameObject canvasToDisable;
    public Camera cameraToDisable;

/*    public string playerName;
    public TMP_Text playerNameTextField;*/

    PhotonView pv;

    // Start is called before the first frame update

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
    }
    void Start()
    {
       // Cursor.lockState = CursorLockMode.None;
        if (pv.IsMine)
        {
/*            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;*/
        }
        if(!pv.IsMine)
        {
            canvasToDisable.SetActive(false);
            Destroy(cameraToDisable);
        }


    }

    // Update is called once per frame
    void Update()
    {
/*        if(pv.IsMine)
        {
            playerName = playerNameTextField.text;
        }*/
    }

    public void onRegularPlayerClicked()
    {
        if(pv.IsMine)
        {
         //   Cursor.visible = false;
          //  Cursor.lockState = CursorLockMode.Locked;
            canvasToDisable.SetActive(false);
            Destroy(cameraToDisable);

                PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player"), new Vector3(35f, 20, 20), Quaternion.identity);


            //pv.RPC("spawnPlayer", RpcTarget.All);
        }

    }

    public void onVrClicked()
    {
        if (pv.IsMine)
        {
           // Cursor.visible = false;
           // Cursor.lockState = CursorLockMode.Locked;
            canvasToDisable.SetActive(false);
            Destroy(cameraToDisable);
            //PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "NetworkPlayer"), new Vector3(35f, 15, 20), Quaternion.identity);
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "OVRPlayerController"), new Vector3(35f, 15, 20), Quaternion.identity);
            //pv.RPC("spawnVRPlayer", RpcTarget.All);
           
        }
    }

/*    [PunRPC]
    public void spawnPlayer()
    {
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player"), new Vector3(35f, 20, 20), Quaternion.identity);
    }


    [PunRPC]
    public void spawnVRPlayer()
    {
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "OVRPlayerController"), new Vector3(35f, 15, 20), Quaternion.identity);
    }*/
}
