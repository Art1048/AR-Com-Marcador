using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightController : MonoBehaviour
{

    private Rigidbody rb;
    private Animation anim;
    private Joystick joystick;
    private bool isAttacking;
    public Transform attack;
    public LayerMask enemyLayers;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animation>();
        joystick = FindObjectOfType<Joystick>();
        isAttacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        float X = joystick.Horizontal;
        float Z = joystick.Vertical;

        Vector3 Movement = new Vector3(X, 0, Z);

        rb.velocity = Movement * 200f;

        if (X != 0 && Z != 0)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x,
                                        Mathf.Atan2(X, Z) * Mathf.Rad2Deg,
                                        transform.eulerAngles.z);
        }

        if (!isAttacking)
        {
            if (X != 0 || Z != 0)
            {
                anim.Play("Walk");
            }
            else
            {
                anim.Play("Wait");
            }

        }

    }

    public void Attack()
    {

        StartCoroutine(Attacking());

        Collider[] hitEnemies;
        
        hitEnemies = Physics.OverlapSphere(attack.position, 0.15f, enemyLayers);

        foreach (Collider enemy in hitEnemies)
        {
            enemy.gameObject.SendMessage("TakeDamage", 2);
        }

        isAttacking = false;

    }

    private IEnumerator Attacking(){
        isAttacking = true;
        anim.Play("Attack");
        yield return new WaitForSeconds(1f);
    }

    void OnDrawGizmosSelected()
    {
        if (attack == null)
        {
            return;
        }

        Gizmos.DrawSphere(attack.position, 0.15f);
    }
}
