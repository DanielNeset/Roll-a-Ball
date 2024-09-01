using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{

    public float speed = 0;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI timeText;
    public GameObject winTextObject;

    private Rigidbody rigidbody;
    private int count;
    private float movementX;
    private float movementY;
    private bool startTimer = false;
    private float timeRecorder = 0;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        winTextObject.SetActive(false);
        count = 0;
        SetCountText();
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rigidbody.AddForce(movement * speed);
    }

    private void LateUpdate()
    {
        if(transform.position.y < -10) {
            transform.position = new Vector3(0, 5, 0);
        }

        if(startTimer == true)
        {
            timeRecorder += Time.deltaTime;
            timeText.text = "Time: " + timeRecorder.ToString("F0") + "s";
        }

    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString() + "/13";
        if(count >= 13) {
            winTextObject.SetActive(true);
            startTimer = false;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            if (startTimer == false)
            {
                startTimer = true;
            }
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }

    }

}
