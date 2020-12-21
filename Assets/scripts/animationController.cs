using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationController : MonoBehaviour
{
    public Animator anim;
    public KeyCode leftKeycode;
    public KeyCode rightKeycode;
    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        anim.Play("Idle");
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        if (gameObject.tag.Equals("elf"))
        {
            if (Input.GetKeyDown(leftKeycode))
            {
                anim.SetFloat("xDirection", -1);

            }
            if (Input.GetKeyUp(leftKeycode))
            {
                anim.SetFloat("xDirection", 0);

            }

            if (Input.GetKeyDown(rightKeycode))
            {
                anim.SetFloat("xDirection", 1);

            }
            if (Input.GetKeyUp(rightKeycode))
            {
                anim.SetFloat("xDirection", 0);

            }

            if(Input.GetKeyDown(KeyCode.RightShift))
            {
                anim.SetFloat("zDirection", 1);
                rb.AddForce(new Vector3(0, 20, 0), ForceMode.Impulse);

            }
            if (Input.GetKeyUp(KeyCode.RightShift))
            {
                anim.SetFloat("zDirection", 0);
            }
        }
    }
}
