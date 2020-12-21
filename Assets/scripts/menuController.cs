using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuController : MonoBehaviour
{


    public void OnClickCharacterPick(int whichCharacter)
    {
        if(playerInfo.pi != null)
        {
            playerInfo.pi.mySelectedCharacter = whichCharacter;
            PlayerPrefs.SetInt("MyCharacter", whichCharacter);
        }
    }
}
