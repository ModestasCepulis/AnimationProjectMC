using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.IO;

public class RoomManagerPV : MonoBehaviourPunCallbacks
{

    public static RoomManagerPV Instance;


    private void Awake()
    {
        if(Instance) //checks if room manager exists
        {
            Destroy(gameObject); //if it does delete this
            return;
        }
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    public override void OnEnable()
    {
        base.OnEnable();
        SceneManager.sceneLoaded += onSceneLoaded;
    }
    public override void OnDisable()
    {
        base.OnDisable();
        SceneManager.sceneLoaded -= onSceneLoaded;
    }

    void onSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if(scene.buildIndex == 1)
        {
            //in order to get the player name:
            //PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Canvas"), Vector3.zero, Quaternion.identity);

            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "playerManager"), Vector3.zero, Quaternion.identity);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
