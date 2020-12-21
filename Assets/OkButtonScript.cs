using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OkButtonScript : MonoBehaviour
{

    public GameObject canvasToDisable;
    public GameObject playerToActivate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void disableCavas()
    {
        canvasToDisable.SetActive(false);
        playerToActivate.SetActive(true);
    }
}
