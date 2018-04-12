using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char_CTRL : MonoBehaviour {

    [System.Serializable]
    public class MoveSettings
    {
        public float forwardSpeed;
        public float fBWhileJump;

        public float leftRightSpeed;
        public float leftRightJump;

        public float jumpHeight;
        public LayerMask ground;


    }
    [System.Serializable]
    public class PhysSetttings
    {
        public float fallMultiplier;
        public float lowJumpMultiplier;


        public Vector3 power;
        public Vector3 velocity = Vector3.zero;


    }
    [System.Serializable]
    public class InputSettings
    {
        public float inputDelay = 0.1f;
        public string FORWARD_AXIS = "Vertical";
        public string TURN_AXIS = "Horizontal";
        public string JUMP_AXIS = "Jump";
        public string SHOT_BUTTON = "Shot";
    }

    public MoveSettings moveSettings = new MoveSettings();
    public PhysSetttings physSetttings = new PhysSetttings();
    public InputSettings inputSettings = new InputSettings();


    private Char_COLL colliderCs;

    private Quaternion targetRotation;
    private Rigidbody rBody;
    private float forwardInput, turnInput, jumpInput;

    public Quaternion TargetRotation
    {
        get { return targetRotation; }
    }

    #region punchShotProperties
    [SerializeField]
    private GameObject punchProjectile;

    public ProjectileManager projTest;

    public string projectileName;
    public Vector3 EndPosition;
    #endregion

    void Start()
    {
        colliderCs = GetComponent<Char_COLL>();
        targetRotation = transform.rotation;
        if (GetComponent<Rigidbody>())
        {
            rBody = GetComponent<Rigidbody>();
        }
        else
        {
            Debug.Log("The character needs a rigidbody.");
        }
        forwardInput = turnInput = jumpInput = 0;
        Debug.Log("jopp");

        //punchProjectile = GameObject.Find(projectileName);
        //projTest = GameObject.Find(projectileName).GetComponent<ProjectileManager>();

    }
    void GetInput()
    {
        forwardInput = Input.GetAxis(inputSettings.FORWARD_AXIS);
        turnInput = Input.GetAxis(inputSettings.TURN_AXIS);
        jumpInput = Input.GetAxisRaw(inputSettings.JUMP_AXIS);

    }
    void Update()
    {
        GetInput();

        Jump();
        if (colliderCs.jumpMode)
        {
            print("jumpMode");
            MoveWhileJump();
            rBody.velocity = physSetttings.velocity;
        }
        else
        {
            print("walkMode");

            LeftRight();
            Run();
            Jump();
            rBody.velocity = transform.TransformDirection(physSetttings.velocity);
        }

        //PunchShot(punchProjectile, inputSettings.SHOT_BUTTON);

        //if (Input.GetButtonDown(inputSettings.SHOT_BUTTON))
        //{
        //    //punchProjectile.transform.position = Vector3.Lerp(punchProjectile.transform.position, EndPosition, 0.1f);
        //    //Lerp(punchProjectile);
        //    projTest.LerpLeft();
        //}

        //if (Input.GetKeyDown(KeyCode.M))
        //{
        //    projTest.LerpRight();
        //}

    }

    private void FixedUpdate()
    {

    }

    Vector3 Lerp(GameObject punchProj)
    {
        return punchProj.transform.position = Vector3.Lerp(punchProj.transform.position, EndPosition, 0.5f);
    }

    void MoveWhileJump()
    {
        if (Mathf.Abs(forwardInput) > 0)
        {
            //move forward
            physSetttings.velocity += transform.TransformDirection(new Vector3(0, 0, (moveSettings.forwardSpeed * forwardInput) / 10));
        }
        else if (Mathf.Abs(forwardInput) < 0)
        {
            //move backward
            physSetttings.velocity -= transform.TransformDirection(new Vector3(0, 0, (moveSettings.forwardSpeed * forwardInput) / 10));


        }

        if (Mathf.Abs(turnInput) > inputSettings.inputDelay)
        {
            //move left
            physSetttings.velocity += transform.TransformDirection(new Vector3(((moveSettings.leftRightSpeed * turnInput) / 10), 0, 0));

        }
        else if (Mathf.Abs(turnInput) < inputSettings.inputDelay)
        {
            //move right
            physSetttings.velocity += transform.TransformDirection(new Vector3(((moveSettings.leftRightSpeed * turnInput) / 10), 0, 0));

        }

        physSetttings.velocity.z = Mathf.Clamp(physSetttings.velocity.z, physSetttings.power.z - physSetttings.power.z / moveSettings.fBWhileJump,
                                                   physSetttings.power.z + physSetttings.power.z / moveSettings.fBWhileJump);

        physSetttings.velocity.x = Mathf.Clamp(physSetttings.velocity.x, physSetttings.power.x - physSetttings.power.x / moveSettings.leftRightJump,
                                              physSetttings.power.x + physSetttings.power.x / moveSettings.leftRightJump);

    }

    void Run()
    {

        //while walking
        if (Mathf.Abs(forwardInput) > inputSettings.inputDelay)
        {
            //move
            physSetttings.velocity.z = moveSettings.forwardSpeed * forwardInput;
            physSetttings.power.z = physSetttings.velocity.z;
        }
        else
        {
            //zero velocity
            physSetttings.velocity.z = 0;
            physSetttings.power.z = physSetttings.velocity.z;

        }


    }

    void LeftRight()
    {

        if (Mathf.Abs(turnInput) > inputSettings.inputDelay)
        {
            //move left and right
            physSetttings.velocity.x = moveSettings.leftRightSpeed * turnInput;
            physSetttings.power.x = physSetttings.velocity.x;

        }
        else
        {
            //zero velocity
            physSetttings.velocity.x = 0;
            physSetttings.power.x = physSetttings.velocity.x;


        }


    }
    void Jump()
    {
        if (jumpInput > 0 && colliderCs.grounded)
        {
            physSetttings.velocity.y = moveSettings.jumpHeight;
        }
        else if ((int)jumpInput == 0 && colliderCs.grounded)
        {
            //zero out our velocity.y
            physSetttings.velocity.y = 0;
        }
        else
        {
            if (!Input.GetKey(KeyCode.Space))
            {
                physSetttings.velocity += Vector3.up * Physics.gravity.y * (physSetttings.lowJumpMultiplier - 1) * Time.deltaTime;
            }
            //decrease velocity.y
            physSetttings.velocity += Vector3.up * Physics.gravity.y * (physSetttings.fallMultiplier - 1) * Time.deltaTime;
        }
    }

    //void PunchShot(GameObject punchProj, string shotInput )
    //{
    //    if (punchProj != null)
    //    {
    //        if (Input.GetButtonDown(shotInput))
    //        {
    //            StartCoroutine(Punching(punchProj, 20, 0.5f));
    //        }
    //    }

    //    else
    //        Debug.LogError("Dir fehlt dein Schläger!!");

    //}

    IEnumerator Punching(GameObject punchProj, float range, float time)
    {
        Vector3 savedTransform = punchProj.transform.position;
        punchProj.transform.position = Vector3.Lerp(punchProj.transform.position,
                                                     new Vector3(punchProj.transform.position.x,
                                                                 punchProj.transform.position.y,
                                                                 punchProj.transform.position.z + range),
                                                     time);
        //punchProj.transform.Translate(punchProj.transform.position.x * Time.deltaTime,
        //                    punchProj.transform.position.y * Time.deltaTime,
        //                    (punchProj.transform.position.z + range) * Time.deltaTime);

        yield return new WaitForSeconds(2);

        punchProj.transform.position = Vector3.Lerp(punchProj.transform.position, savedTransform, time);
    }
}
