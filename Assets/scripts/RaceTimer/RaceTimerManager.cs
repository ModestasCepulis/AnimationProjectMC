using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RaceTimerManager : MonoBehaviour
{

    [SerializeField] TMP_Text text;
    [SerializeField] TMP_Text raceText;

    public GameObject gates;

    private float timeRemaining = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(timeRemaining);

        if(timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            text.text = timeRemaining.ToString("F0");
        }
        else
        {
            raceText.text = "GO! GO! GO!";
            text.text = "";
            gates.transform.Translate(new Vector3(gates.transform.position.x, -100, gates.transform.position.z) * 0.5f * Time.deltaTime);
        }
    }
}
