using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipScript : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;

    private float timeGunLastFired = 0f;
    [SerializeField] private float backoffBetweenFires;

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

        if (Input.GetKeyDown("space")
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
        GameObject newBullet = Instantiate(bulletPrefab);
        newBullet.transform.position = transform.position;
    }

    private void handleArrowKeys()
    {
        if (Input.GetKeyDown("up"))
        {
            Debug.Log("Going up.");
            rigidbody_.AddForce(transform.up * 100, ForceMode.Force);
        }
        if (Input.GetKeyDown("down"))
        {
            rigidbody_.AddForce(-transform.up* 100, ForceMode.Force);
        }
        if (Input.GetKeyDown("left"))
        {
            rigidbody_.AddTorque(-transform.right * 10, ForceMode.Force);
        }
        if (Input.GetKeyDown("right"))
        {
            rigidbody_.AddTorque(transform.right * 10, ForceMode.Force);
        }
    }
}
