using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Inimigo : MonoBehaviour
{
    public float velocidade;
    public Rigidbody2D rb;
    private bool colidir_chao = false;
    private GeradorInimigos geradorScritpt;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        geradorScritpt = FindObjectOfType<GeradorInimigos>();

        //Pega o colisor do filho do jogador;
        //ColliderPlayer = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).GetComponent<Collider2D>();
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
        //Movimentar o inimigo para a esquerda
        if(colidir_chao == true){
            rb.velocity = new Vector2(-1, rb.velocity.y) * velocidade * Time.deltaTime;
        }
        else{
            rb.velocity = rb.velocity.normalized;
        }
    }
    void DestruirInimigo(){
        //Verifica se o inimigo está na lista de inimigos gerados e remove ele da lista antes de destruir
        //Se o inimigo não estiver na lista, apenas destrua o objeto
        if(geradorScritpt.ListinimigosGerados.Contains(gameObject)){
            geradorScritpt.ListinimigosGerados.Remove(gameObject);
            Destroy(gameObject);
        }
        else{
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// Acrecenta 1 ponto ao jogador quando o inimigo colide com a esapada e destroi o inimigo
    /// <para>Essa função é chamada quando o inimigo colide com o jogador.</para>
    /// </summary>
    public void PontuarComInimigo(){
        PontosPleyer.pontos += 1;
        DestruirInimigo();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Se o inimigo colidir com outro inimigo, ele deve ignorar a colisão
        if(collision.gameObject.CompareTag("Inimigo")
        || collision.gameObject.CompareTag("Player")){
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.collider, true);   
        }

        if(collision.gameObject.tag == "Fim"){
            DestruirInimigo();
        }

        if (collision.gameObject.CompareTag("Chao"))
        {
            colidir_chao = true;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chao"))
        {
            colidir_chao = false;
        }
    }
}
