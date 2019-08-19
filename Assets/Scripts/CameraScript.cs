using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] float distanceBehindShip_;
    [SerializeField] float distanceAboveShip_;
    [SerializeField] float angleToFaceDownwards_;

    void Update()
    {
        slowlyVaryBackgroundColor();
        updateZoom();
        updatePositionToFollowShip();
        updateRotationToLookAtShip();
    }

    /** Background color slowly varies over time. */
    void slowlyVaryBackgroundColor()
    {
        GetComponent<Camera>().backgroundColor =
            new Color(
                r: .2f,
                g: Mathf.Abs(Mathf.Cos(Time.time / 10)) / 3,
                b: Mathf.Abs(Mathf.Cos(Time.time / 10)) / 2,
                a: .8f);
    }

    void updatePositionToFollowShip()
    {
        GameObject ship = GameManager.instance.ship_;
        Vector3 shipLoc = ship.transform.position;
        Vector3 backness = -ship.transform.forward * distanceBehindShip_;
        Vector3 upness = ship.transform.up * distanceAboveShip_;
        Vector3 distance = (backness + upness) * zoomLevel.Multiplier();
        Vector3 newPosition = shipLoc + distance;

        // This means that we always move a little bit closer to being pointed
        // in the direction that we want to be (with some kind of taylor series
        // [~exponential] backoff, or something like that).
        float followSpeed = Time.deltaTime * 2;

        transform.position =
            Vector3.Lerp(
                a: transform.position,
                b: newPosition,
                t: followSpeed);
    }

    void updateRotationToLookAtShip()
    {
        GameObject ship = GameManager.instance.ship_;
        transform.LookAt(ship.transform, ship.transform.up);
        transform.Rotate(angleToFaceDownwards_, 0, 0);
    }


    /** How far the camera is from the user's spaceship. */
    ZoomLevel zoomLevel = new ZoomLevel();

    void updateZoom()
    {
        if (Input.GetKeyDown("z"))
        {
            zoomLevel.CycleNext();
            Debug.Log("zoomMultiplier: " + zoomLevel.Multiplier());
        }
    }

    class ZoomLevel
    {
        static float[] zoomLevels = { 1f, 3f, 5f };

        int currIdx;

        public float Multiplier()
        {
            return zoomLevels[currIdx % zoomLevels.Length];
        }

        public void CycleNext()
        {
            ++currIdx;
        }
    }
}
