using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float velocidade_movimento;
    public float forca_pulo;
    private Rigidbody2D rb;
    private bool colidir_chao = false;
    private bool esta_atacando = false;
    public Animator animatorPlayer;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        //Pulo();
        ScaleSprit();
        Ataque();
    }
    void FixedUpdate()
    {
        VerificarMovimento();
        // Move o jogador baseado na física
    }
    void VerificarMovimento(){
        if(esta_atacando == false){
            Movimento();
            //Permite que o jogador se mova enquanto não ataca
        }
        else if(esta_atacando == true && colidir_chao == false){
            Movimento();
            //Permite que o jogador se mova enquanto ataca no ar
        }
    }
    void Movimento(){
        float input_x = Input.GetAxisRaw("Horizontal") * velocidade_movimento * Time.deltaTime;
        //Time.deltaTime deixa suave a movimentação

        Vector2 movimentos = new Vector2(input_x, rb.velocity.y);
        rb.velocity = movimentos;
        AnimacaoAndar();
    }
    void Pulo(){
        if(Input.GetButtonDown("Jump") && colidir_chao == true){
            rb.AddForce(new Vector2(0, forca_pulo), ForceMode2D.Impulse);
            //ForceMode2D.Impulse, efeito de impulso
        }
    }
    void Ataque()
    {
        if (Input.GetMouseButtonDown(0) && esta_atacando == false)
        {
            esta_atacando = true;
            animatorPlayer.SetBool("ataque_simples", true);
            animatorPlayer.SetBool("andar", false);
            //Iniciando a animação de ataque

            if(colidir_chao == true){
                rb.velocity = Vector2.zero;
                //Parando o movimento do personagem enquanto ataca no chao
            }
        }
    }
    void EncerrarAtaque(){
        animatorPlayer.SetBool("ataque_simples", false);
        esta_atacando = false;
        //Interrompendo a animação de ataque
    }
    void ScaleSprit(){
        if(esta_atacando == false){
            if(Input.GetAxisRaw("Horizontal") > 0){
            transform.localScale = new Vector3(1, 1, 1); // personagem indo para a direita
            }
            else if(Input.GetAxisRaw("Horizontal") < 0){
                transform.localScale = new Vector3(-1, 1, 1); // personagem indo para a esquerda
            }
        }
    }
    void AnimacaoAndar(){
        if(Input.GetAxisRaw("Horizontal") != 0){
            animatorPlayer.SetBool("andar", true);
        }
        else{
            animatorPlayer.SetBool("andar", false);
        }
        //Se o jogador estiver se movendo, a animação de andar é ativada
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
    //Verifica se o jogador está colidindo com o chão
}
