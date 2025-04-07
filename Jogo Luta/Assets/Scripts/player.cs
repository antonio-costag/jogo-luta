using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public float velocidade_movimento;
    public float forca_pulo;
    private Rigidbody2D rb;
    private bool colidir_chao = false;
    private bool esta_atacando = false;
    public Animator animatorPlayer;
    public int vidaMax;
    private int vida;


    //GRUPO: METODOS UNITY
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        vida = vidaMax;
    }
    void Update()
    {
        //Pulo();
        ScaleSprit();
        Ataque();
        print(vida);
    }
    void FixedUpdate()
    {
        VerificarMovimento();
        // Move o jogador baseado na física
    }


    //GRUPO: METODOS ENVOLVENDO MOVIMENTAÇÃO
    void VerificarMovimento(){
        if(esta_atacando == false){
            Movimento();
            //Permite que o jogador se mova enquanto não ataca
        }
        else{
            rb.velocity = Vector2.zero;
        }
    }
    void Movimento(){
        float input_x = Input.GetAxisRaw("Horizontal") * velocidade_movimento * Time.deltaTime;
        //Time.deltaTime deixa suave a movimentação

        Vector2 movimentos = new Vector2(input_x, rb.velocity.y);
        rb.velocity = movimentos;
        AnimacaoAndar();
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
    void Pulo(){
        if(Input.GetButtonDown("Jump") && colidir_chao == true){
            rb.AddForce(new Vector2(0, forca_pulo), ForceMode2D.Impulse);
            //ForceMode2D.Impulse, efeito de impulso
        }
    }


    //GRUPO: METODOS ENVOLVENDO COMBATE
    void Ataque()
    {
        if (Input.GetMouseButtonDown(0) && esta_atacando == false)
        {
            esta_atacando = true;
            animatorPlayer.SetBool("ataque_simples", true);
            animatorPlayer.SetBool("andar", false);
            //Iniciando a animação de ataque
        }
    }
    void EncerrarAtaque(){
        animatorPlayer.SetBool("ataque_simples", false);
        esta_atacando = false;
        //Interrompendo a animação de ataque
    }
    bool DanoAoInimigo(){
        //Verifica se o Inimigo pode levar dano
        if(GameObject.FindGameObjectWithTag("Espada").activeSelf){
            return true;
        }
        else{
            return false;
        }
    }


    //GRUPO: METODOS ENVOLVENDO COLISÃO
     void VidaPLyer(Collision2D collision){
        if(collision.gameObject.tag == "Inimigo"){
            vida -= vidaMax / 10;
        }
    }
    void ProcurarInimigoColisao(Collider2D Collision){
        //Pega o script do gerador de inimigos
        GeradorInimigos gerador = FindObjectOfType<GeradorInimigos>();
        //Contains verifica se o objeto colidido é um inimigo gerado
        if (gerador.ListinimigosGerados.Contains(Collision.gameObject))
        {
            Collider2D CollicionInimigo = Collision;
            if(DanoAoInimigo()){
                CollicionInimigo.gameObject.GetComponent<Inimigo>().DestruirInimigo();
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        ProcurarInimigoColisao(collision);
        //Verifica se a espada colidiu com um inimigoPrefab
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        VidaPLyer(collision);

        //Verifica se o jogador colidiu com o chão
        if(collision.gameObject.tag == "Chao"){
            colidir_chao = true;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        //Verifica se o jogador deixou de colidir com o chão
        if(collision.gameObject.tag == "Chao"){
            colidir_chao = false;
        }
    }
}
