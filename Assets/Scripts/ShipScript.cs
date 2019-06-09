﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipScript : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;

    private float timeGunLastFired = 0f;
    [SerializeField] private float backoffBetweenFires;

    [SerializeField] private float forwardForce;
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
            fire();
    }

    private void FixedUpdate()
    {
        handleArrowKeys();
    }

    private bool shouldFire()
    {
        bool firing =
            Input.GetKey("space")
                && Time.time - timeGunLastFired > backoffBetweenFires;

        if (firing)
            timeGunLastFired = Time.time;

        return firing;
    }

    private void fire()
    {
        GameObject bullet = Instantiate(
            original: bulletPrefab,
            position: transform.position + transform.forward,
            rotation: transform.rotation);
        bullet.transform
            .Rotate(
                xAngle: 90,
                yAngle: 0,
                zAngle: 0);
        bullet.GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity;
    }

    private void handleArrowKeys()
    {
        if (Input.GetKey("up")) addForce(transform.forward * forwardForce);
        if (Input.GetKey("down")) addForce(-transform.forward * forwardForce);
        if (Input.GetKey("left")) addTorque(-transform.up * sideForce);
        if (Input.GetKey("right")) addTorque(transform.up * sideForce);
        if (Input.GetKey("w")) addTorque(-transform.right * sideForce);
        if (Input.GetKey("s")) addTorque(transform.right * sideForce);
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
}
