using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TestPunch : MonoBehaviour {

     Rigidbody rBody;



    private void Start()
    {
        rBody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
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
}
