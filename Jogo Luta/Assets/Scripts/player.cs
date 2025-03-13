using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float velocidade_movimento;
    public float forca_pulo;
    private Rigidbody2D rb;
    private bool colidir_chao = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        Pulo();
        ScaleSprit();
    }
    void FixedUpdate()
    {
        Movimento(); // Move o jogador baseado na física
    }
    void Movimento(){
        float input_x = Input.GetAxis("Horizontal") * velocidade_movimento * Time.deltaTime;
        //Time.deltaTime deixa suave a movimentação
        float input_y = rb.velocity.y;

        Vector2 movimentos = new Vector2(input_x, input_y);
        //transform.position += movimentos * Time.deltaTime * velocidade;
        rb.velocity = movimentos;
    }
    void Pulo(){
        if(Input.GetButtonDown("Jump") && colidir_chao == true){
            rb.AddForce(new Vector2(0, forca_pulo), ForceMode2D.Impulse);
            //ForceMode2D.Impulse, efeito de impulso
        }
    }
    void ScaleSprit(){
        if(Input.GetAxis("Horizontal") > 0){
            transform.localScale = new Vector3(1, 1, 1); // personagem indo para a direita
        }
        else if(Input.GetAxis("Horizontal") < 0){
            transform.localScale = new Vector3(-1, 1, 1); // personagem indo para a esquerda
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Chao"){
            colidir_chao = true;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Chao"){
            colidir_chao = false;
        }
    }
}
