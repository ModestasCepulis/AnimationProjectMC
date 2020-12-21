using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using TMPro;

public class CakeHealthController : MonoBehaviourPunCallbacks, IPunObservable
{

    public GameObject AllCake;
    public GameObject SeventyFiveCake;
    public GameObject HalfCake;
    public GameObject TwentyfivePercentCake;
    public TMP_Text CakeEyes;
    public GameObject CakeEyesObject;
    public TMP_Text CakeMouth;
    public GameObject CakeMouthObject;

    public int currentCakeHealth = 100;
    public int maxCakeHealth = 100;

    private PhotonView PV;

    public CakeHealth cakeHealth;

    public GameObject winnerText;
    public GameObject loserText;


    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();

        currentCakeHealth = maxCakeHealth;
        cakeHealth.setMaxHealth(maxCakeHealth);
    }

    // Update is called once per frame
    void Update()
    {
        // cakeHealth.setHealth(currentCakeHealth);
/*        if (PV.IsMine)
        {*/
            PV.RPC("SyncHealth", RpcTarget.All);
            PV.RPC("setAppropriateGameObjectsToTrue", RpcTarget.All);

        if(currentCakeHealth <= 0)
        {
            winnerText.SetActive(true);

            Invoke("DestroyTheWinnerText", 15);
        }

/*        }*/
    }

    public void DestroyTheWinnerText()
    {
        Destroy(winnerText);
    }

    private void OnTriggerEnter(Collider other)
    {
/*        if (PV.IsMine)
        {*/
            if (other.gameObject.tag == "Sword")
            {
                if (currentCakeHealth >= 0 && currentCakeHealth <= maxCakeHealth)
                {
                    PV.RPC("takeDamage", RpcTarget.All);
                }
            }
/*        }*/

    }

    [PunRPC]
    public void SyncHealth()
    {
        cakeHealth.setHealth(currentCakeHealth);
    }

    [PunRPC]
    public void takeDamage()
    {
        if(currentCakeHealth >= 2)
        {
            currentCakeHealth -= 2;
        }
        cakeHealth.setHealth(currentCakeHealth);
    }

    [PunRPC]
    public void setAppropriateGameObjectsToTrue()
    {
        if(currentCakeHealth <= 100)
        {
            AllCake.SetActive(true);
            CakeEyesObject.SetActive(true);
            CakeMouthObject.SetActive(true);
            SeventyFiveCake.SetActive(false);
            HalfCake.SetActive(false);
            TwentyfivePercentCake.SetActive(false);
        }
        if (currentCakeHealth <= 75)
        {
            AllCake.SetActive(false);
            CakeMouth.text = "-";
            SeventyFiveCake.SetActive(true);
            HalfCake.SetActive(false);
            TwentyfivePercentCake.SetActive(false);
        }
        if (currentCakeHealth <= 50)
        {
            AllCake.SetActive(false);
            CakeMouth.text = ".";
            CakeEyes.text = "-      -";
            SeventyFiveCake.SetActive(false);
            HalfCake.SetActive(true);
            TwentyfivePercentCake.SetActive(false);
        }
        if (currentCakeHealth <= 25)
        {
            AllCake.SetActive(false);
            CakeMouth.text = "-";
            SeventyFiveCake.SetActive(false);
            HalfCake.SetActive(false);
            TwentyfivePercentCake.SetActive(true);
        }
        if (currentCakeHealth <= 0)
        {
            AllCake.SetActive(false);
            SeventyFiveCake.SetActive(false);
            HalfCake.SetActive(false);
            TwentyfivePercentCake.SetActive(false);
            CakeEyesObject.SetActive(false);
            CakeMouthObject.SetActive(false);
        }
    }

    public virtual void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(currentCakeHealth);
        }
        else if(stream.IsReading)
        {
            currentCakeHealth = (int)stream.ReceiveNext();
        }
    }

}
