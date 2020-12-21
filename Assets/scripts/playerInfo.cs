using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInfo : MonoBehaviour
{
    public static playerInfo pi;

    public int mySelectedCharacter;

    public GameObject[] allCharacters;


    private void OnEnable()
    {
        if (playerInfo.pi = null)
        {
            playerInfo.pi = this;
        }
        else
        {
            if (playerInfo.pi != this)
            {
                Destroy(gameObject);
                playerInfo.pi = this;
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("MyCharacter"))
        {
            mySelectedCharacter = PlayerPrefs.GetInt("MyCharacter");

        }
        else
        {
            mySelectedCharacter = 0;
            PlayerPrefs.SetInt("MyCharacter", mySelectedCharacter);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
