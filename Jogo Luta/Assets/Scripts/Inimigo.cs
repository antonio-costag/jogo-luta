using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Inimigo : MonoBehaviour
{
    public float velocidade;
    public Rigidbody2D rb;
    private bool colidir_chao = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    void FixedUpdate()
    {
        Movimentar();
    }
    void Movimentar(){
        if(colidir_chao == true){
            rb.velocity = new Vector2(-1, rb.velocity.y) * velocidade * Time.deltaTime;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chao"))
        {
            colidir_chao = true;
        }
    }
}
