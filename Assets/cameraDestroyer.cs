using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraDestroyer : MonoBehaviour
{

    private PhotonView PV;
    // Start is called before the first frame update

    public GameObject lefteyeanchor;
    public GameObject centerEyeAnchor;
    public GameObject rightEyeanchor;

    public static cameraDestroyer Instance;

/*
    //public Component[] cameras;

    private GameObject varGameObject;

    public AudioListener audioListenerToDelete;*/






    private void Awake()
    {
        PV = GetComponent<PhotonView>();
     //   varGameObject = GameObject.FindWithTag("VRscriptDelete");

    }
    void Start()
    {
        Instance = this;
        if (!PV.IsMine)
        {
            //  Destroy(GetComponentInChildren<Camera>().gameObject);
            // lefteyeanchor =

            /*            cameras = GetComponentsInChildren<Camera>();

                        foreach (Camera camera in cameras)
                        {
                            Destroy(camera.gameObject);
                        }*/


            //varGameObject.GetComponent<OVRScreenFade>().enabled = false;
            lefteyeanchor.SetActive(false);
            centerEyeAnchor.SetActive(false);
/*          Destroy(centerEyeAnchor);
            Destroy(audioListenerToDelete);*/
            rightEyeanchor.SetActive(false);
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
