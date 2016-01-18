using UnityEngine;
using System.Collections;
using System;

public class ShipMove : MonoBehaviour {

    //public variables
    public float lockDegrees = 45f;

    public float thrust = 10.0f;
    public Rigidbody2D LeftThruster;
    public Rigidbody2D RightThruster;
    public GameObject leftPropulsion;
    public GameObject rightPropulsion;
    public GameObject smokePrefab;

    public float smokeDeltaTime = 1.0f;

    public float maxSpeed = 10.0f;

    public bool reverseControls = false;

    //local variables
    private bool leftThrustOn = false;
    private bool rightThrustOn = false;

    private bool useTouch = false;

    private float lastTimeSmokeLeft;
    private float lastTimeSmokeRight;
    private Vector3 smokeOffset = new Vector3(0, 0, 10.0f);

    private Rigidbody2D rb;

    void Start() {
        lastTimeSmokeLeft = Time.time;
        lastTimeSmokeRight = Time.time;
        rb = GetComponent<Rigidbody2D>();
    }

	void Update () {
        updateInput();
        if (reverseControls) {
            bool temp = leftThrustOn;
            leftThrustOn = rightThrustOn;
            rightThrustOn = temp;
        }
        leftPropulsion.SetActive(leftThrustOn);
        rightPropulsion.SetActive(rightThrustOn);

        FixSpeed();

        createSmoke();
    }

    private void FixSpeed() {
        float speed = rb.velocity.magnitude;
        if (speed > maxSpeed) {
            rb.velocity = rb.velocity * maxSpeed / speed;
        }
    }

    private void createSmoke() {
        if (leftThrustOn && Time.time > lastTimeSmokeLeft + smokeDeltaTime) {
            lastTimeSmokeLeft = Time.time;
            Instantiate(smokePrefab, (Vector3)LeftThruster.position + smokeOffset, gameObject.transform.rotation);
        }
        if (rightThrustOn && Time.time > lastTimeSmokeRight + smokeDeltaTime) {
            lastTimeSmokeRight = Time.time;
            Instantiate(smokePrefab, (Vector3)RightThruster.position + smokeOffset, gameObject.transform.rotation);
        }
    }

    void updateInput() {
        bool leftInput = Input.GetKey("left");
        bool rightInput = Input.GetKey("right");
        if (leftInput || rightInput) {
            useTouch = false;
        }
        if (!useTouch) {
            leftThrustOn = leftInput;
            rightThrustOn = rightInput;
        }
    }

    void FixedUpdate() {
        Vector2 thrustVector = new Vector2(0, thrust);
        if (leftThrustOn) {
            LeftThruster.AddRelativeForce(thrustVector);
        }
        if (rightThrustOn) {
            RightThruster.AddRelativeForce(thrustVector);
        }

       
    }

    public void toggleBoost(bool isLeft, bool isOn) {
        useTouch = true;
        if (isLeft) {
            leftThrustOn = isOn;
        } else {
            rightThrustOn = isOn;
        }
    }
    public void toggleBoostLeftOn(bool on) {
        toggleBoost(true, on);
    }
    public void toggleBoostRightOn(bool on) {
        toggleBoost(false, on);
    }

    public void setReverseControls(bool reverse) {
        reverseControls = reverse;
    }
}
