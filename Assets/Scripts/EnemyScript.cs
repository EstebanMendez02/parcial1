using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    Rigidbody2D rb2D;
    Animator anim;
    SpriteRenderer sprR;
    PlayerScript ps;
    [SerializeField, Range(0.1f, 10f)]
    float moveSpeed = 3f;
    [SerializeField]
    Vector2 direction = Vector2.right;
    [SerializeField, Range(0.1f, 10f)]
    float idlingTime = 2f;

    [SerializeField, Range(0.1f, 5f)]
    float rayDistance = 2f, rayDistance2 = 2f, rayDistance3 = 2f;

    [SerializeField]
    Color rayColor = Color.white, rayColor2 = Color.red, rayColor3 = Color.blue;
    [SerializeField]
    LayerMask limitLayer;
    [SerializeField]
    LayerMask playerLayer;
    [SerializeField]
    Vector3 rayOrigin, rayOrigin2, rayOrigin3;



//IENUMERATORS
    IEnumerator patroling;
    IEnumerator idling;
    IEnumerator attack;
    IEnumerator lastRoutine;
    [SerializeField] 
    AnimationClip attackClip;
    [SerializeField]
    float attackClipOffset = 1f;

    bool isAttacking = false;


    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprR = GetComponent<SpriteRenderer>();
        ps = GetComponent<PlayerScript>();

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
        while(true)
        {
            if(Attack && !isAttacking)
            {
                isAttacking = true;
                lastRoutine = PatrolingRoutine();
                StartAttack();
                yield break;
            }
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

//ataque
    IEnumerator AttackingRoutine()
    {
        if(AttackLeft && (direction == Vector2.right) )
        {
            sprR.flipX = !FlipSpriteX;
        }
        if(AttackRight && (direction == Vector2.left) )
        {
            sprR.flipX = !FlipSpriteX;
        }
        anim.SetTrigger("attack");
        yield return new WaitForSeconds(attackClip.length + attackClipOffset);
        sprR.flipX = FlipSpriteX;
        StartCoroutine(lastRoutine);
        isAttacking = false;
    }

    void StartAttack()
    {
        attack = AttackingRoutine();
        StartCoroutine(attack);
        MakeDamage();
    }
    void Update()
    {
    
    }

    public void MakeDamage()
    {
        GameManager.instance.GetPlayer.RunAnimationDamage();
    }

    bool FlipSpriteX => direction == Vector2.right ? false : true;

    bool Attack => Physics2D.Raycast(transform.position + rayOrigin2, Vector2.right, rayDistance2, playerLayer) || 
    Physics2D.Raycast(transform.position + rayOrigin2, Vector2.left, rayDistance2, playerLayer);
    bool AttackLeft => Physics2D.Raycast(transform.position + rayOrigin2, Vector2.left, rayDistance2, playerLayer);
    bool AttackRight => Physics2D.Raycast(transform.position + rayOrigin2, Vector2.right, rayDistance2, playerLayer);
    bool collision => Physics2D.Raycast(transform.position + rayOrigin, Vector2.right, rayDistance, limitLayer) || 
    Physics2D.Raycast(transform.position + rayOrigin, Vector2.left, rayDistance, limitLayer);
    public bool top => Physics2D.Raycast(transform.position + rayOrigin3, Vector3.up, rayDistance3, playerLayer);

    void OnDrawGizmosSelected()
    {
        Gizmos.color = rayColor;
        Gizmos.DrawRay(transform.position+rayOrigin, Vector3.right * rayDistance);
        Gizmos.DrawRay(transform.position+rayOrigin, Vector3.left * rayDistance);
        Gizmos.color = rayColor2;
        Gizmos.DrawRay(transform.position+rayOrigin2, Vector3.right * rayDistance2);
        Gizmos.DrawRay(transform.position+rayOrigin2, Vector3.left * rayDistance2);
        Gizmos.color = rayColor3;
        Gizmos.DrawRay(transform.position+rayOrigin3, Vector3.up * rayDistance3);
        
    }
}
