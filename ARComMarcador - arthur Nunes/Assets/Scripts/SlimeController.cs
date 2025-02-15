using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{

    private Rigidbody rb;
    private Animation anim;
    public int Hp; 
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
       StartCoroutine(Dead());
    }

    public void TakeDamage(int damage) {

        anim.Play("Damage");
        Hp -= damage;

    }

    public IEnumerator Dead(){
        if(Hp <= 0){
            anim.Play("Dead");
            yield return new WaitForSeconds(1f);
            Destroy(this);
        }
    }
}
