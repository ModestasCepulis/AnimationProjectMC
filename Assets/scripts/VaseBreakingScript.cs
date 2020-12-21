using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaseBreakingScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator anim;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Sword")
        {
            anim.SetBool("HitVase", true);
            print("There was a collision between sword and vase");
        }
    }
}
