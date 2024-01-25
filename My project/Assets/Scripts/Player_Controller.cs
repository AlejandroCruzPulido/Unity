using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Controller : MonoBehaviour
{
    public float speed = 0;
    public float jumpHeight = 2.0f; // Añade una variable para la altura del salto
    private bool isGrounded; // Añade una variable para verificar si el jugador está en el suelo

    private Rigidbody rb;
    private float movementX;
    private float movementY;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);

        if (isGrounded && Keyboard.current.spaceKey.wasPressedThisFrame) // Verifica si el jugador está en el suelo y si se presionó la tecla espacio
        {
            rb.AddForce(new Vector3(0, jumpHeight, 0), ForceMode.Impulse); // Añade una fuerza hacia arriba para hacer saltar al jugador
            isGrounded = false; // Establece que el jugador no está en el suelo después de saltar
        }

        if (rb.position.y < 1)
            isGrounded = true;
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }
    void OnTriggerEnter(Collider other)
    {
        other.gameObject.SetActive(false);
    }
}
