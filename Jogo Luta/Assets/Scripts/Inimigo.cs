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
        //Movimentar o inimigo para a esquerda
        if(colidir_chao == true){
            rb.velocity = new Vector2(-1, rb.velocity.y) * velocidade * Time.deltaTime;
        }
    }
    
    /// <summary>
    /// Destruir o inimigo
    /// <para>Essa função é chamada quando o inimigo colide com o jogador.</para>
    /// </summary>
    public void DestruirInimigo(){
        Destroy(gameObject);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chao"))
        {
            colidir_chao = true;
        }

        // Se o inimigo colidir com o jogador, ele deve ignorar a colisão
        if(collision.gameObject.CompareTag("Player")){
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.collider, true);   
        }
    }
}
