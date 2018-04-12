using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Char_COLL : MonoBehaviour {


    [HideInInspector] public bool jumpMode = true;
    [HideInInspector] public bool grounded;
    private Char_CTRL charController;


    private Vector3 newVelocity;
    public Vector3 DirectionVector;




	// Use this for initialization
	void Start () 
    {
        charController = GetComponent<Char_CTRL>();
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "JumpPad")
        {
            jumpMode = true;
            print("befor JumpPad" + newVelocity);

            DirectionVector = other.GetComponent<JumpPad>().velocity;

            newVelocity = new Vector3(charController.physSetttings.velocity.x * DirectionVector.x, 
                                      DirectionVector.y, 
                                      charController.physSetttings.velocity.z * DirectionVector.z);
            
            newVelocity = transform.TransformDirection(newVelocity);
            /*
            newVelocity = new Vector3(charController.physSetttings.velocity.x + DirectionVector.x ,
                                      DirectionVector.y ,
                                      charController.physSetttings.velocity.z + DirectionVector.z); //creating the new player velocity
                                      */
            print("exit JumpPad" + newVelocity);

        }
        if (other.tag == "Punch")
        {
            this.GetComponent<Rigidbody>().velocity += transform.TransformDirection(other.GetComponent<Projectile_CTRL>().shootDir);


            //rBody.velocity += transform.TransformDirection(collider.GetComponent<Projectile_CTRL>().shootDir + new Vector3(knockbackValue, knockbackValue, knockbackValue));
            //rBody.AddForce(dir * knockbackValue);


            if (other.GetComponent<Projectile_CTRL>().isShooting)
            {
                other.GetComponent<Projectile_CTRL>().isShooting = false;
            }
        }

    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "JumpPad")
        {
            charController.physSetttings.power = newVelocity; //velocity change here >> collider ground stops sometimes velocity
            charController.physSetttings.velocity = newVelocity;

            grounded = false;

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "JumpPad")
        {
            print("exit JumpPad");


        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            grounded = true;
            jumpMode = false;

            print("grounded true");
        }
        
    }
    private void OnCollisionStay(Collision collision)
    {
        
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            grounded = false;
            print("grounded false");

        }
    }
}   
