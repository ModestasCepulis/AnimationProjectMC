using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class updateTheLocation : MonoBehaviourPunCallbacks, IPunObservable
{
    public GameObject theObject;

    private Transform playerGlobalTransform;

    private PhotonView pv;



    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<PhotonView>();

        if(pv.IsMine)
        {
            playerGlobalTransform = theObject.transform;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(theObject.transform.position);
            stream.SendNext(theObject.transform.rotation);
            stream.SendNext(theObject.transform.localPosition);
            stream.SendNext(theObject.transform.localRotation);
        }
        else if (stream.IsReading)
        {
            this.transform.position = (Vector3)stream.ReceiveNext();
            this.transform.rotation = (Quaternion)stream.ReceiveNext();
            theObject.transform.localPosition = (Vector3)stream.ReceiveNext();
            theObject.transform.localRotation = (Quaternion)stream.ReceiveNext();
        }
    }
}
