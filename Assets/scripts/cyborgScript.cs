using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cyborgScript : MonoBehaviour
{
    Rigidbody rb;

/*    public Animator anim;
    public KeyCode leftKeycode;
    public KeyCode rightKeycode;*/

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
/*        anim.Play("Idle");*/
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(transform.up * 1000);
        }


        /*        if (Input.GetKeyDown(leftKeycode))
                {
                    anim.SetFloat("Horizontal", -1);

                }
                if (Input.GetKeyUp(leftKeycode))
                {
                    anim.SetFloat("Horizontal", 0);

                }

                if (Input.GetKeyDown(rightKeycode))
                {
                    anim.SetFloat("xDirection", 1);

                }
                if (Input.GetKeyUp(rightKeycode))
                {
                    anim.SetFloat("xDirection", 0);

                }

                if(Input.GetKeyDown(KeyCode.LeftShift))
                {
                    anim.SetFloat("zDirection", 1);
                }
                if (Input.GetKeyUp(KeyCode.LeftShift))
                {
                    anim.SetFloat("zDirection", 0);
                }
            }*/
    }
}
