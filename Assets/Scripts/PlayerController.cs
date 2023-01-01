using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    public Rigidbody2D rb;
    public float speed = 12f;
    private Vector2 movement;

    private void Start() {
        DontDestroyOnLoad(gameObject);
    }

    void Update() {

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate() {

        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}
