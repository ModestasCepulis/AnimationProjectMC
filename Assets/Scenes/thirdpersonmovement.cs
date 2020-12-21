using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class thirdpersonmovement : MonoBehaviourPunCallbacks, IPunObservable
{
    //banana
    // Start is called before the first frame update

    private float speed = 80f;
    //public CharacterController controller;
    private float turnSmoothTime = 0.11f;
    float turnSmoothVelocity;
    Vector3 movecAmount;
    Vector3 smoothMoveVelocity;

    public Transform cam;
    public Animator anim;

    //parent/child stuff
    private GameObject theObjectToAttach;
    public GameObject thePartToAttachTo;
    public GameObject theSpotWhereToInstantiate;
    private GameObject newItem;

    private BoxCollider weaponCollider;

    //healthbar
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;

    //stamina
    public int maxStamina = 100;
    public float currentStamina;

    public StaminaBar staminaBar;
    private Coroutine regen;
    private Coroutine regenHealth;

    bool isAttacking = false;

    //jumping
    private Rigidbody rb;
    float gravity = -16f;
    float jumpheight = 22f;
    Vector3 velocity;

    //for checking ground
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    bool isGrounded;
    //object to attach
    public GameObject sword;

    //test
    private PhotonView PV;
    Vector3 direction;

    //camera stuff
    public GameObject cameraHolder;
    public float mouseSensitivity;
    float verticalLookRotation;

    //gravity
    public float gravityMultiplyer;

    //playerName
    public TMP_Text playerNameAsObject;
    public TMP_Text inputtedPlayerName;

    //canvas of nickname
    public GameObject canvasToEnable;

    //ball to attach
    public GameObject spotWhereToAttachABall;
    GameObject ball;
    public GameObject spotToShootTheBallFrom;
    public static thirdpersonmovement Instance;





    private void Awake()
    {
        PV = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Instance = this;

        if(!PV.IsMine)
        {
            destroyPlayerCamera();
            //Destroy(rb);
            //Cursor.visible = true;
        }

        theObjectToAttach = sword;
        InstantiateItem();

        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);

        currentStamina = maxStamina;
        staminaBar.setMaxStamina(maxStamina);

        if(PV.IsMine)
        {
            
        }
    }

    public void destroyPlayerCamera()
    {
        Destroy(GetComponentInChildren<Camera>().gameObject);
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + transform.TransformDirection(movecAmount) * Time.fixedDeltaTime);

    }
    private void LateUpdate()
    {
        //transform.LookAt(transform.position + cam.forward);
    }

    // Update is called once per frame
    void Update()
    {
        if (PV.IsMine)
        {
            PV.RPC("syndStaminaAndHealth", RpcTarget.All);

            playerNameAsObject.text = inputtedPlayerName.text;

            if (currentHealth >= 100)
            {
                anim.SetBool("isDamaged", false);




                float horizontal = Input.GetAxisRaw("Horizontal");
                float vertical = Input.GetAxisRaw("Vertical");

                anim.SetFloat("Horizontal", horizontal);
                anim.SetFloat("Vertical", vertical);

                Vector3 moveDir = new Vector3(horizontal, 0, vertical).normalized;


                movecAmount = Vector3.SmoothDamp(movecAmount, moveDir * speed, ref smoothMoveVelocity, 0.15f);

                //camera

                transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * mouseSensitivity);



/*                verticalLookRotation += Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
                verticalLookRotation = Mathf.Clamp(verticalLookRotation, -45f, 45f);

                cameraHolder.transform.localEulerAngles = Vector3.left * verticalLookRotation;*/

                /*  

                Vector3 move = transform.right * horizontal + transform.forward * vertical;



                transform.Translate(horizontal, 0, vertical);
                direction = new Vector3(horizontal, 0f, vertical).normalized;

                if (direction.magnitude >= 0.1f)
                {
                    float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                    float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                    transform.rotation = Quaternion.Euler(0f, angle, 0f);

                    Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                    controller.Move(moveDirection.normalized * speed * Time.deltaTime);
                }*/

                ProcessLayerAnimation();
                ProcessJumpRunSettings();
                ProcessDamage();

                ProcessThrowing();

            }
            else
            {

                anim.SetBool("isDamaged", true);

            }

        }
    }

    public void ProcessThrowing()
    {
/*        if(Input.GetKeyDown(KeyCode.F))
        {
            Vector3 ballPosition = transform.position + transform.forward + spotToShootTheBallFrom.transform.position;

            ball = PhotonNetwork.InstantiateRoomObject(Path.Combine("PhotonPrefabs", "BallToGrab"), ballPosition, Quaternion.identity);
        }*/
       
    }

    public void disableCavas()
    {
        // Cursor.lockState = CursorLockMode.Locked;
       // Cursor.visible = false;
        canvasToEnable.SetActive(false);
        currentHealth = 100;
    }

    public virtual void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {

            stream.SendNext(currentHealth);
            stream.SendNext(currentStamina);
            stream.SendNext(playerNameAsObject.text);
            //this is my component, (etc my player)
        }
        else if(stream.IsReading)
        {
           currentHealth = (int)stream.ReceiveNext();
           currentStamina = (float)stream.ReceiveNext();
           playerNameAsObject.text = (string)stream.ReceiveNext();
        }
    }


    public void ProcessDamage()
    {
        if(currentHealth <= 0)
        {
            speed = 0;
        }
    }

    private IEnumerator rechargeStamina(float waitFirstTime, float waitSecondTime)
    {
        yield return new WaitForSeconds(waitFirstTime);

        while(currentStamina < maxStamina)
        {
            currentStamina += 3;
            staminaBar.setStamina(currentStamina);
            yield return new WaitForSeconds(waitSecondTime);
        }
        regen = null;

    }

    private IEnumerator rechargeHealth(float waitFirstTime, float waitSecondTime)
    {
        yield return new WaitForSeconds(waitFirstTime);

        while (currentHealth < maxHealth)
        {
            currentHealth += 3;
            healthBar.setHealth(currentHealth);
            yield return new WaitForSeconds(waitSecondTime);
        }
        regenHealth = null;
        anim.SetBool("isDamaged", false);

    }

    public void ProcessLayerAnimation()
    {
        //processing attacking
        if(currentStamina >= 20)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                anim.SetLayerWeight(1, 1);
                weaponCollider.enabled = true;
                Invoke("cancelFightAnimationInTime", 0.8f);

                //health


                //stamina
                if (currentStamina > 0)
                {
                    PV.RPC("takeAwayStamina", RpcTarget.All, 20f);
                    staminaBar.setStamina(currentStamina);

                    if (regen != null)
                    {
                        StopCoroutine(regen);
                    }

                    regen = StartCoroutine(rechargeStamina(0.5f, 0.1f));
                }
            }
        }
            if (Input.GetKeyUp(KeyCode.E))
            {   
                weaponCollider.enabled = false;
            }
        //end
        //processing jumping

        //velocity.y += gravity * Time.deltaTime;
        //controller.Move(velocity * Time.deltaTime);

        if(currentStamina >= 30)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
               // velocity.y = Mathf.Sqrt(jumpheight * -0.5f * gravity);

               rb.AddForce(transform.up * 60000f);

               // gameObject.transform.position = new Vector3(transform.position.x, transform.position.y * 2, transform.position.z);

                PV.RPC("takeAwayStamina", RpcTarget.All, 30f);
                staminaBar.setStamina(currentStamina);

                if (regen != null)
                {
                    StopCoroutine(regen);
                }

                regen = StartCoroutine(rechargeStamina(2, 0.1f));
            }

        }

       //end

    }

    private void OnTriggerEnter(Collider other)
    {
        if(!PV.IsMine)
        {
            if (other.gameObject.tag == "Sword")
            {
                //theObjectToAttach.GetComponent<BoxCollider>().enabled = false;
                Debug.Log("Damage shouldve been done");
                PV.RPC("takeDamage", RpcTarget.All, 20);

                //takeDamage(20);
            }


        }


    }

    private void OnTriggerStay(Collider other)
    { 
    }

    void cancelFightAnimationInTime()
    {
        anim.SetLayerWeight(1, 0);
    }

    public void ProcessJumpRunSettings()
    {
        //running
        if(currentStamina > 0)
        {
            if(Input.GetKey(KeyCode.LeftShift))
            {
                speed = 100;

                PV.RPC("takeAwayStamina", RpcTarget.All, 0.2f);
                staminaBar.setStamina(currentStamina);

                if (regen != null)
                {
                    StopCoroutine(regen);
                }

                regen = StartCoroutine(rechargeStamina(0.2f, 0.1f));
            }
            else
            {
                speed = 80;
            }
        }
        if(currentStamina < 0)
        {
            speed = 80;
        }


    }

    public void InstantiateItem()
    {
        newItem = Instantiate(theObjectToAttach, new Vector3(theSpotWhereToInstantiate.transform.position.x, theSpotWhereToInstantiate.transform.position.y, theSpotWhereToInstantiate.transform.position.z), Quaternion.Euler(new Vector3(100, -1.6f, -1.6f)));
        weaponCollider = newItem.GetComponent<BoxCollider>();
        newItem.tag = "Sword";
        newItem.transform.parent = thePartToAttachTo.transform;

    }
    [PunRPC]
    public void takeDamage(int damage)
    {
        currentHealth -= damage;

        if (regenHealth != null)
        {
            StopCoroutine(regenHealth);
        }

        regenHealth = StartCoroutine(rechargeHealth(3, 0.1f));

    }

    [PunRPC]
    public void takeAwayStamina(float usage)
    {
        currentStamina -= usage;

    }

    [PunRPC]
    public void syndStaminaAndHealth()
    {
        healthBar.setHealth(currentHealth);
        staminaBar.setStamina(currentStamina);
    }


}
