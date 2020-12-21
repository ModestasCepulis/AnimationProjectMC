using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transform_Cuba : MonoBehaviour
{

    private PhotonView PV;
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
        if(PV.IsMine)
        {
            PV.RPC("spinObject", RpcTarget.All);
        }

    }

    [PunRPC]
    public void spinObject()
    {
        gameObject.transform.Rotate(new Vector3(0, 0.2f, 0), Space.World);
    }

}
