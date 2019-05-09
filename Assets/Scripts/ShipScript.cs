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
        constrainTo2D();
        if (shouldFire()) 
            fire();
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
            original: bulletPrefab,
            position: transform.position + transform.up,
            rotation: transform.rotation);
    }

    private void handleArrowKeys()
    {
        if (Input.GetKey("up")) addForce(transform.up * upForce);
        if (Input.GetKey("down")) addForce(-transform.up * upForce);
        if (Input.GetKey("left")) addTorque(transform.forward * sideForce);
        if (Input.GetKey("right")) addTorque(-transform.forward * sideForce);
    }

    private void addForce(Vector3 f)
    {
        rigidbody_.AddForce(
            force: f,
            mode: ForceMode.Force);
    }

    private void addTorque(Vector3 t)
    {
        rigidbody_.AddTorque(
            torque: t,
            mode: ForceMode.Force);
    }

    private void constrainTo2D()
    {
        transform.position =
            new Vector3(
                x: transform.position.x,
                y: transform.position.y,
                z: 0);

        transform.rotation =
            Quaternion.Euler(
                x: 0,
                y: 0,
                z: transform.rotation.eulerAngles.z);
    }
}
