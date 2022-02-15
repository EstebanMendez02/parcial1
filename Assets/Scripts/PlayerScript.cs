using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    float speed = 3.0f;
    [SerializeField]
    float jumpForce = 7.0f;
    Rigidbody2D rb2D;
    [SerializeField, Range(0.01f, 10f)]
    float rayDistance = 2f;
    [SerializeField]
    Color rayColor = Color.red;
    [SerializeField]
    LayerMask groundLayer;
    [SerializeField]
    Vector3 rayOrigin;

    GameInputs gameInputs;

    void Awake()
    {
        gameInputs = new GameInputs();
    }

    void OnEnable()
    {
        gameInputs.Enable();
    }
    
    void OnDisable()
    {
        gameInputs.Disable();
    }
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        gameInputs.Gameplay.Jump.performed += _=> Jump();
        gameInputs.Gameplay.Jump.canceled += _=> JumpCanceled();
    }

    void FixedUpdate()
    {
        rb2D.position += (Vector2.right * Axis.x * speed * Time.fixedDeltaTime);
    }

    void Jump()
    {
        if(IsGrounding)
        {
            rb2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void JumpCanceled()
    {
        rb2D.velocity = new Vector2(rb2D.velocity.x, 0f);
    }

    Vector2 Axis => new Vector2(gameInputs.Gameplay.AxisX.ReadValue<float>(), gameInputs.Gameplay.AxisY.ReadValue<float>());

    bool IsGrounding => Physics2D.Raycast(transform.position + rayOrigin, Vector2.down, rayDistance, groundLayer);


    void OnDrawGizmosSelected()
    {
        Gizmos.color = rayColor;
        Gizmos.DrawRay(transform.position + rayOrigin, Vector2.down * rayDistance);
    }
}
