using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private GameObject ship_;

    [SerializeField] private float distanceBehindShip_;
    [SerializeField] private float distanceAboveShip_;
    [SerializeField] private float angleToFaceDownwards_;

    void Update()
    {
        if (!ship_) ship_ = GameManager.instance.ship_;
        slowlyVaryBackgroundColor();
        updatePositionToFollowShip();
        updateRotationToLookAtShip();
    }

    /** Background color slowly varies over time. */
    private void slowlyVaryBackgroundColor()
    {
        GetComponent<Camera>().backgroundColor =
            new Color(
                r: .2f,
                g: Mathf.Abs(Mathf.Cos(Time.time / 10)) / 3,
                b: Mathf.Abs(Mathf.Cos(Time.time / 10)) / 2,
                a: .8f);
    }

    private void updatePositionToFollowShip()
    {
        Vector3 ship = ship_.transform.position;
        Vector3 back = -ship_.transform.forward * distanceBehindShip_;
        Vector3 up = ship_.transform.up * distanceAboveShip_;
        Vector3 newPosition = ship + back + up;

        float followSpeed = Time.deltaTime * 2;

        transform.position =
            Vector3.Lerp(
                a: transform.position,
                b: newPosition,
                t: followSpeed);
    }

    private void updateRotationToLookAtShip()
    {
        transform.LookAt(ship_.transform, ship_.transform.up);
        transform.Rotate(angleToFaceDownwards_, 0, 0);
    }
}
