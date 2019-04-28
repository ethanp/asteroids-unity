using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipScript : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;

    private float timeGunLastFired = 0f;
    [SerializeField] private float backoffBetweenFires;

    [SerializeField] private float upForce;
    [SerializeField] private float sideForce;

    private Rigidbody rigidbody_;

    void Start()
    {
        rigidbody_ = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldFire())
        {
            fire();
        }
    }

    private void FixedUpdate()
    {
        handleArrowKeys();
    }

    private bool shouldFire()
    {

        if (Input.GetKey("space")
            && Time.time - timeGunLastFired > backoffBetweenFires)
        {
            timeGunLastFired = Time.time;
            return true;
        }
        else
        {
            return false;
        }
    }

    private void fire()
    {
        Instantiate(
            bulletPrefab,
            transform.position + transform.up,
            transform.rotation);
    }

    private void handleArrowKeys()
    {
        if (Input.GetKey("up"))
            rigidbody_.AddForce(transform.up * upForce, ForceMode.Force);
        if (Input.GetKey("down"))
            rigidbody_.AddForce(-transform.up * upForce, ForceMode.Force);
        if (Input.GetKey("left"))
            rigidbody_.AddTorque(-transform.right * sideForce, ForceMode.Force);
        if (Input.GetKey("right"))
            rigidbody_.AddTorque(transform.right * sideForce, ForceMode.Force);
    }
}
