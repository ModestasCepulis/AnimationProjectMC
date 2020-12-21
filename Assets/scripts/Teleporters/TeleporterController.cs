using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterController : MonoBehaviour
{
   // public GameObject player;
   // private GameObject playerObject;
    public GameObject teleportPosition;
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
        Debug.Log("Collision happened");
        other.transform.position = new Vector3(teleportPosition.transform.position.x, teleportPosition.transform.position.y, teleportPosition.transform.position.z);
    }
}
