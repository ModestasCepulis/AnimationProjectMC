using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBoxGenerator : MonoBehaviour
{
    public GameObject[] firstLine;
    public GameObject[] secondLine;
    public GameObject[] thirdLine;
    public GameObject[] fourthLine;
    public GameObject[] fifthLine;



    // Start is called before the first frame update
    void Start()
    {
        int firstLineCubeRandomNumber = Random.Range(0, 4);
        firstLine[firstLineCubeRandomNumber].GetComponent<BoxCollider>().enabled = false;

        int secondLineCubeRandomNumber = Random.Range(0, 4);
        secondLine[secondLineCubeRandomNumber].GetComponent<BoxCollider>().enabled = false;

        int thirdLineCubeRandomNumber = Random.Range(0, 4);
        thirdLine[thirdLineCubeRandomNumber].GetComponent<BoxCollider>().enabled = false;

        int fourthLineCubeRandomNumber = Random.Range(0, 4);
        fourthLine[fourthLineCubeRandomNumber].GetComponent<BoxCollider>().enabled = false;


        int fifthLineCubeRandomNumber = Random.Range(0, 4);
        fifthLine[fifthLineCubeRandomNumber].GetComponent<BoxCollider>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
