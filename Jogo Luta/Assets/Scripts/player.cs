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
    private GeradorInimigos geradorScritpt;

    /// <summary>
    /// Variaveis de Vida do Jogador
    /// vidaMax: Vida máxima do jogador
    /// </summary>
    public int vidaMax;

    /// <summary>
    /// Variaveis de Vida do Jogador
    /// vidaAtual: Vida atual do jogador
    /// </summary>
    public int vidaAtual;


    //GRUPO: METODOS UNITY
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        //pega o Script do Gerador de inimigos pelo tipo
        geradorScritpt = FindObjectOfType<GeradorInimigos>();
        vidaAtual = vidaMax;
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
            if(vidaAtual >= 0){
                vidaAtual -= 10;
            }
        }
    }
    void ProcurarInimigoMorto(Collider2D Collision){
        //Contains verifica se o objeto colidido é um inimigo prefab
        if (geradorScritpt.ListinimigosGerados.Contains(Collision.gameObject))
        {
            Collider2D CollicionInimigo = Collision;
            if(DanoAoInimigo()){
                CollicionInimigo.gameObject.GetComponent<Inimigo>().PontuarComInimigo();
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        ProcurarInimigoMorto(collision);
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
