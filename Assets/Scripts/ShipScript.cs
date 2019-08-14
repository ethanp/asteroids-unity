using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipScript : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject bulletEmitterLeft;
    [SerializeField] private GameObject bulletEmitterRight;

    private float timeGunLastFired = 0f;
    [SerializeField] private float backoffBetweenFires;

    [SerializeField] private float forwardForce;
    [SerializeField] private float sideForce;


    [SerializeField] private ParticleSystem leftJet;
    [SerializeField] private ParticleSystem rightJet;
    [SerializeField] private ParticleSystem frontJet;
    [SerializeField] private ParticleSystem rearJet;
    [SerializeField] private ParticleSystem upJet;
    [SerializeField] private ParticleSystem downJet;

    private Dictionary<string, ParticleSystem> emitters;
    private Rigidbody rigidbody_;

    void Start()
    {
        emitters = new Dictionary<string, ParticleSystem>
        {
            { "left", rightJet },
            { "right", leftJet },
            { "up", rearJet },
            { "down", frontJet },
            { "w", downJet },
            { "s", upJet }
            // TODO: Add mappings "a" -> "left", "d" -> "right".
        };

        rigidbody_ = GetComponent<Rigidbody>();
    }

    // Update is called once per frame.
    void Update()
    {
        if (shouldFire())
            fire();
    }

    private void FixedUpdate()
    {
        handleArrowKeys();
    }

    /** 
     * Check for space bar press and maintain slow cadence.
     * We do allow user to _hold_ the space bar.
     */
    private bool shouldFire()
    {
        bool shouldFire =
            Input.GetKey("space")
                && Time.time - timeGunLastFired > backoffBetweenFires;

        if (shouldFire)
            timeGunLastFired = Time.time;

        return shouldFire;
    }

    private void fire()
    {
        GameObject leftBullet = Instantiate(
            original: bulletPrefab,
            position: bulletEmitterLeft.transform.position,
            rotation: transform.rotation);
        leftBullet.transform
            .Rotate(
                xAngle: 90,
                yAngle: 0,
                zAngle: 0);
        //bullet.GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity;
        leftBullet.GetComponent<Rigidbody>().velocity = transform.forward * 30;

        GameObject rightBullet = Instantiate(
            original: bulletPrefab,
            position: bulletEmitterRight.transform.position,
            rotation: transform.rotation);
        rightBullet.transform
            .Rotate(
                xAngle: 90,
                yAngle: 0,
                zAngle: 0);
        //bullet.GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity;
        rightBullet.GetComponent<Rigidbody>().velocity = transform.forward * 60;
    }

    private void handleArrowKeys()
    {
        if (Input.GetKey("up")) addForce(transform.forward * forwardForce);
        if (Input.GetKey("down")) addForce(-transform.forward * forwardForce);
        if (Input.GetKey("left")) addTorque(-transform.up * sideForce);
        if (Input.GetKey("right")) addTorque(transform.up * sideForce);
        if (Input.GetKey("w")) addTorque(-transform.right * sideForce);
        if (Input.GetKey("s")) addTorque(transform.right * sideForce);

        foreach (KeyValuePair<string, ParticleSystem> kvp in emitters)
            if (!Input.GetKey(kvp.Key)) kvp.Value.Stop();
            else if (!kvp.Value.isEmitting) kvp.Value.Play();
    }

    private void addForce(Vector3 force)
    {
        rigidbody_.AddForce(
            force: force,
            mode: ForceMode.Force);
    }

    private void addTorque(Vector3 torque)
    {
        rigidbody_.AddTorque(
            torque: torque,
            mode: ForceMode.Force);
    }
}
