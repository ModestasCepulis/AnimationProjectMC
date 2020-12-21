using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class playerManager : MonoBehaviour
{
    PhotonView pv;


    public string playerName;


    private void Awake()
    {
        pv = GetComponent<PhotonView>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if(pv.IsMine)
        {
            CreateController();
        }
    }

    void CreateController()
    {
        //-5928 is the boss for Z
        //20 is the spawn for Z
        // PhotonNetwork.NickName = playerName;

        //might not work cuz of this:
/*        if(PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "OVRPlayerController"), new Vector3(35f, 50, 20), Quaternion.identity);
        }
        else
        {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player"), new Vector3(35f, 20, 20), Quaternion.identity);
        }*/

       PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "FirstCanvas"), new Vector3(0, 0, 0), Quaternion.identity);


    }

}
