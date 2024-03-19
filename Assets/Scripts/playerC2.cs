using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerC2 : MonoBehaviour
{
    // horizontal movement
    public float xRange = 10.8f;
    public float speed = 10.0f;

    // jumping
    public float jumpHeightV = 50.0f;
    public float jumpHeightH = 25.0f;
    Rigidbody rBody;
    public bool isOnGround = true;
    public bool chargingJump = false;

    public Timer timer;

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // bounds
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }

        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }

        // horizontal movement
        if (Input.GetKey(KeyCode.RightArrow) && isOnGround && chargingJump == false && timer.TimerOn)
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }

        if (Input.GetKey(KeyCode.LeftArrow) && isOnGround && chargingJump == false && timer.TimerOn)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }

        // ------------------JUMPING----------------------- //

        // Charge Jump
        if (Input.GetKeyDown(KeyCode.Alpha4) && isOnGround && timer.TimerOn)
        {
            // begin charging jump
            chargingJump = true;

        }

        // release charge
        if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            // begin charging jump
            chargingJump = false;
        }

        // jump up
        if (Input.GetKeyUp(KeyCode.DownArrow) && isOnGround && chargingJump && timer.TimerOn)
        {
            // release jump
            rBody.AddForce(transform.up * jumpHeightV, ForceMode.Impulse);
            // prevents infinite jumping
            isOnGround = false;
        }
        // jump right
        if (Input.GetKeyUp(KeyCode.LeftArrow) && isOnGround && chargingJump && timer.TimerOn)
        {
            // release jump
            rBody.AddForce(transform.up * jumpHeightV, ForceMode.Impulse);
            rBody.AddForce(transform.right * jumpHeightH, ForceMode.Impulse);
            // prevents infinite jumping
            isOnGround = false;
        }

        // jump left
        if (Input.GetKeyUp(KeyCode.RightArrow) && isOnGround && chargingJump && timer.TimerOn)
        {
            // release jump
            rBody.AddForce(transform.up * jumpHeightV, ForceMode.Impulse);
            rBody.AddForce(transform.right * -jumpHeightH, ForceMode.Impulse);
            // prevents infinite jumping
            isOnGround = false;
        }
    }
    // resets jump after landing
    private void OnCollisionEnter(Collision collision)
    {
        isOnGround = true;
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }
    // tissue pickup
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
            // add time to timer
            Destroy(other.gameObject);
            timer.PowerUp();
            // maybe use this for how long till next one spawns->
            // StartCoroutine(PowerupCountdownRoutine());
        }
    }
}
