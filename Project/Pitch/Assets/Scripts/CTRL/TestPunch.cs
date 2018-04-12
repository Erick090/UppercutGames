using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPunch : MonoBehaviour {

     Rigidbody rBody;



    private void Start()
    {
        rBody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Punch")
        {
            Vector3 dir = - this.transform.position;

            dir = -dir.normalized;

            print(collider.GetComponent<Projectile_CTRL>().shootDir);

            print("vorher: " + rBody.velocity);

            rBody.velocity = collider.GetComponent<Projectile_CTRL>().shootDir ;

            print("nachher: " + rBody.velocity);


            //rBody.velocity += transform.TransformDirection(collider.GetComponent<Projectile_CTRL>().shootDir + new Vector3(knockbackValue, knockbackValue, knockbackValue));
            //rBody.AddForce(dir * knockbackValue);


            if (collider.GetComponent<Projectile_CTRL>().isShooting)
            {
                collider.GetComponent<Projectile_CTRL>().isShooting = false;
            }
        }
    }
}
