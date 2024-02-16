using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class Player_Controller : MonoBehaviour
{
    public float speed = 0;
    public float jumpHeight = 2.0f;
    private bool isGrounded;

    private Rigidbody rb;
    private float movementX;
    private float movementY;
    private int count;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public Transform initialPosition;

    private int pointsToWin;

    public TextMeshProUGUI fallMessageText;
    private float fallMessageDuration = 2f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        winTextObject.SetActive(false);
        SetPointsToWin();
        count = 0;
        SetCountText();
        fallMessageText.gameObject.SetActive(false);
    }

    void SetPointsToWin()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        switch (sceneName)
        {
            case "SampleScene":
                pointsToWin = 8;
                break;
            case "Nivel2":
                pointsToWin = 1;
                break;
            case "Nivel3":
                pointsToWin = 6;
                break;
            default:
                pointsToWin = 8;
                break;
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);

        if (isGrounded && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            rb.AddForce(new Vector3(0, jumpHeight, 0), ForceMode.Impulse);
            isGrounded = false;
        }

        if (rb.position.y < 1)
            isGrounded = true;

        if (rb.position.y < -10)
        {
            ShowFallMessage("Try Again ;)");
            ResetPlayerPosition();
        }
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= pointsToWin)
        {
            winTextObject.SetActive(true);
            Invoke("WinLevel", 5f);
        }
    }

    void ShowFallMessage(string message)
    {
        fallMessageText.text = message;
        fallMessageText.gameObject.SetActive(true);
        Invoke("HideFallMessage", fallMessageDuration);
    }

    void HideFallMessage()
    {
        fallMessageText.gameObject.SetActive(false);
    }

    void ResetPlayerPosition()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = initialPosition.position;
    }

    void WinLevel()
    {
        winTextObject.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }
}
