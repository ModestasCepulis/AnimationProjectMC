using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class networkVRplayer : MonoBehaviourPunCallbacks, IPunObservable
{
    public GameObject avatar;
    /*    public Transform playerGlobal;
        public Transform playerLocal;*/

    public GameObject playerGlobal;
    public GameObject playerLocal;

    private Transform playerGlobalTransform;
    private Transform playerLocalTransform;

/*    public GameObject centerEyeAnchor;
    public GameObject leftEyeAnchor;
    public GameObject rightEyeAnchor;*/

    private PhotonView pv;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Im instantiated");
        pv = GetComponent<PhotonView>();

        if(pv.IsMine)
        {
            /*            playerGlobal = GameObject.Find("OVRPlayerController").transform;

                        playerLocal = playerGlobal.Find("OVRCameraRig");*/

            playerGlobalTransform = playerGlobal.transform;

/*            centerEyeAnchor = GameObject.Find("OVRCameraRig/TrackingSpace/CenterEyeAnchor");
            leftEyeAnchor = GameObject.Find("OVRCameraRig/TrackingSpace/LeftEyeAnchor");          
            rightEyeAnchor = GameObject.Find("OVRCameraRig/TrackingSpace/RightEyeAnchor");

            centerEyeAnchor.SetActive(true);
            leftEyeAnchor.SetActive(true);
            rightEyeAnchor.SetActive(true);*/

/*            this.transform.SetParent(playerLocal);
            this.transform.localPosition = Vector3.zero;*/

           // avatar.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(playerGlobalTransform.position);
            stream.SendNext(playerGlobalTransform.rotation);
            stream.SendNext(playerGlobalTransform.localPosition);
            stream.SendNext(playerGlobalTransform.localRotation);
        }
        else if (stream.IsReading)
        {
            this.transform.position = (Vector3)stream.ReceiveNext();
            this.transform.rotation = (Quaternion)stream.ReceiveNext();
            avatar.transform.localPosition = (Vector3)stream.ReceiveNext();
            avatar.transform.localRotation = (Quaternion)stream.ReceiveNext();
        }
    }
}
