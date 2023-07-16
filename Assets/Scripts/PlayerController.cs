using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 0.1f;
    private Rigidbody rb;
    public Camera playerCamera;
    public bool inverseCamera = false;

    public GameObject ball;
    private Rigidbody ballRigidbody;
    public AudioSource ballHitSound;

    public float shootStrengthMin;
    public float shootStrengthMax;
    private float shootStrength;
    public float carryKickPower;

    public float sensitivity = 1f;
    private float rotationX;
    private float rotationY;

    public Slider strengthSlider;

    // Start is called before the first frame update
    void Start()
    {
        strengthSlider.minValue = shootStrengthMin;
        strengthSlider.maxValue = shootStrengthMax;

        ballRigidbody = ball.GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        
    }

    private void Update()
    {
        // Reset ball [Debug]
        if(Input.GetKeyDown(KeyCode.K))
        {
            ball.transform.position = new Vector3(0, 0.28f, 0.14f);
            ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
            ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }



        strengthSlider.value = shootStrength;

        float ballDistance = Mathf.Abs((ballRigidbody.transform.position - rb.transform.position).magnitude);
        if (ballDistance <= 2f)
        {
            // Change ball color
            ball.GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
        }
        else
        {
            // Reset ball color
            ball.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        }

        // Interacting with ball
        if (Input.GetKey(KeyCode.Mouse0)) // Charge
        {
            if (shootStrength < shootStrengthMax)
                shootStrength += 2000 * Time.deltaTime;
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            shootBall();
        }
        else
        {
            carryBall();
        }




        rb.angularVelocity = Vector3.zero;

        // Move forward/backward
        rb.transform.position += Input.GetAxis("Vertical") * movementSpeed * rb.transform.forward * Time.deltaTime;

        // Move left/right
        rb.transform.position += Input.GetAxis("Horizontal") * movementSpeed * rb.transform.right * Time.deltaTime;
    }

    void LateUpdate()
    {
        // Camera
        rotationX = Input.GetAxis("Mouse X") * sensitivity * Time.fixedDeltaTime;
        rb.transform.Rotate(0, rotationX, 0);
        rotationY = Input.GetAxis("Mouse Y") * sensitivity * Time.fixedDeltaTime;
        playerCamera.transform.Rotate(-rotationY, 0, 0);
    }

    void shootBall()
    {
        float ballDistance = Mathf.Abs((ballRigidbody.transform.position - rb.transform.position).magnitude);
        if (ballDistance <= 2f)
        {
            // Shoot ball
            Vector3 direction = playerCamera.transform.forward;
            direction.y *= 2;
            direction.y = Mathf.Clamp(direction.y, -0.32f, 0.32f);
            ballRigidbody.AddForce(direction * shootStrength);
            ballHitSound.Play();
        }
        shootStrength = shootStrengthMin;
    }

    void carryBall()
    {
        float ballDistance = Mathf.Abs((ballRigidbody.transform.position - rb.transform.position).magnitude);
        if (ballDistance <= 2f)
        {
            // kick ball
            Vector3 direction = playerCamera.transform.forward;
            direction.y = 0;
            ballRigidbody.AddForce(direction * carryKickPower);
            ballHitSound.Play();
        }
        shootStrength = shootStrengthMin;
    }
}
