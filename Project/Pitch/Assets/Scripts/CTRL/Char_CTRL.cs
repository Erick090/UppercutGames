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

    void Start()
    {
        colliderCs = GetComponent<Char_COLL>();
        targetRotation = transform.rotation;
        if(GetComponent<Rigidbody>())
        {
            rBody = GetComponent<Rigidbody>();
        }else
        {
            Debug.Log("The character needs a rigidbody.");
        }
        forwardInput = turnInput = jumpInput = 0;
        Debug.Log("jopp");
        
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


    }

    void MoveWhileJump()
    {
        if (Mathf.Abs(forwardInput) > 0)
        {
                //move forward
                physSetttings.velocity += transform.TransformDirection(new Vector3(0,0,(moveSettings.forwardSpeed * forwardInput) / 10));
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
        if(jumpInput > 0 && colliderCs.grounded)
        {
            physSetttings.velocity.y = moveSettings.jumpHeight;
        }
        else if((int)jumpInput == 0 && colliderCs.grounded)
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
                physSetttings.velocity += Vector3.up * Physics.gravity.y *(physSetttings.fallMultiplier - 1) * Time.deltaTime;
        }
    }

    
}
