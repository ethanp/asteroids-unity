using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipScript : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject bulletEmitterLeft;
    [SerializeField] GameObject bulletEmitterRight;

    float timeGunLastFired = 0f;
    [SerializeField] float backoffBetweenFires;

    [SerializeField] float forwardForce;
    [SerializeField] float sideForce;


    [SerializeField] ParticleSystem leftJet;
    [SerializeField] ParticleSystem rightJet;
    [SerializeField] ParticleSystem frontJet;
    [SerializeField] ParticleSystem rearJet;
    [SerializeField] ParticleSystem upJet;
    [SerializeField] ParticleSystem downJet;

    Dictionary<string, ParticleSystem> emitters;
    Rigidbody rigidbody_;

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
        maybeFire();
    }

    void FixedUpdate()
    {
        handleArrowKeys();
    }

    /** 
     * Check for space bar press and maintain slow cadence.
     * We do allow user to _hold_ the space bar.
     */
    void maybeFire()
    {
        bool shouldFire =
            Input.GetKey("space")
                && Time.time - timeGunLastFired > backoffBetweenFires;

        if (!shouldFire)
            return;

        timeGunLastFired = Time.time;

        createBulletAt(bulletEmitterLeft);
        createBulletAt(bulletEmitterRight);
    }

    void createBulletAt(GameObject obj) {
        GameObject bullet = Instantiate(
            original: bulletPrefab,
            position: obj.transform.position,
            rotation: transform.rotation);
        bullet.transform
            .Rotate(
                xAngle: 90,
                yAngle: 0,
                zAngle: 0);
        bullet.GetComponent<Rigidbody>().velocity = transform.forward * 60;
    }

    void handleArrowKeys()
    {
        if (Input.GetKey("up"))
        {
            GameManager.instance.audioManager_.PlayReverseThruster();
            addForce(transform.forward * forwardForce);
        }
        if (Input.GetKey("down"))
        {
            GameManager.instance.audioManager_.PlayForwardThruster();
            addForce(-transform.forward * forwardForce);
        }
        if (Input.GetKey("left"))
        {
            GameManager.instance.audioManager_.PlayTurnThruster();
            addTorque(-transform.up * sideForce);
        }
        if (Input.GetKey("right"))
        {
            GameManager.instance.audioManager_.PlayTurnThruster();
            addTorque(transform.up * sideForce);
        }
        if (Input.GetKey("w"))
        {
            GameManager.instance.audioManager_.PlayTurnThruster();
            addTorque(-transform.right * sideForce);
        }
        if (Input.GetKey("s"))
        {
            GameManager.instance.audioManager_.PlayTurnThruster();
            addTorque(transform.right * sideForce);
        }

        foreach (KeyValuePair<string, ParticleSystem> kvp in emitters)
            if (!Input.GetKey(kvp.Key)) kvp.Value.Stop();
            else if (!kvp.Value.isEmitting) kvp.Value.Play();
    }

    void addForce(Vector3 force)
    {
        rigidbody_.AddForce(
            force: force,
            mode: ForceMode.Force);
    }

    void addTorque(Vector3 torque)
    {
        rigidbody_.AddTorque(
            torque: torque,
            mode: ForceMode.Force);
    }
}
