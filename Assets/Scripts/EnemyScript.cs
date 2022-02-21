using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    Rigidbody2D rb2D;
    Animator anim;
    SpriteRenderer sprR;
    [SerializeField, Range(0.1f, 10f)]
    float moveSpeed = 3f;
    [SerializeField]
    Vector2 direction = Vector2.right;
    [SerializeField, Range(0.1f, 10f)]
    float idlingTime = 2f;

    [SerializeField, Range(0.1f, 5f)]
    float rayDistance = 2f;

    float waitRayDistance;
    [SerializeField]
    Color rayColor = Color.white;
    [SerializeField]
    LayerMask limitLayer;
    [SerializeField]
    Vector3 rayOrigin;

//IENUMERATORS
    IEnumerator patroling;
    IEnumerator idling;


    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprR = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        StartIA();
    }

    void StartIA()
    {
        patroling = PatrolingRoutine();
        StartCoroutine(patroling);
    }

     IEnumerator PatrolingRoutine()
    {
        anim.SetFloat("Blend", 1f);
        waitRayDistance = rayDistance;
        while(true)
        {
           // Debug.Log("Aqui wait es: " + waitRayDistance);
            rb2D.position += direction * moveSpeed * Time.deltaTime;
            if(collision)
            {
                //Debug.Log("Ray ahora: " + rayDistance);
                rayDistance = 0f;
                StartIdling();
                yield break;
            }else
            {
                
            }
            yield return null;
        }    
    }

    void StartIdling()
    {
        idling = IdlingRoutine();
        StartCoroutine(idling);
    }

    IEnumerator IdlingRoutine()
    {
        Debug.Log("me paro");
        anim.SetFloat("Blend", 0f);
        yield return new WaitForSeconds(idlingTime);
        StartPatroling();
        Debug.Log("entra");
        yield return new WaitForSeconds(idlingTime);
        Debug.Log("yaesperé");
        rayDistance = 0.6f;
    }

    void StartPatroling()
    {   
        Debug.Log("sigo");
        direction = direction == Vector2.right ? Vector2.left : Vector2.right;
        sprR.flipX = FlipSpriteX;
        patroling = PatrolingRoutine();
        StartCoroutine(patroling);
        
        
    }

    void Update()
    {
    
    }

    bool FlipSpriteX => direction == Vector2.right ? false : true;

    bool collision => Physics2D.Raycast(transform.position + rayOrigin, Vector2.right, rayDistance, limitLayer) || 
    Physics2D.Raycast(transform.position + rayOrigin, Vector2.left, rayDistance, limitLayer);

    void OnDrawGizmosSelected()
    {
        Gizmos.color = rayColor;
        Gizmos.DrawRay(transform.position+rayOrigin, Vector3.right * rayDistance);
        Gizmos.DrawRay(transform.position+rayOrigin, Vector3.left * rayDistance);
    }
}
