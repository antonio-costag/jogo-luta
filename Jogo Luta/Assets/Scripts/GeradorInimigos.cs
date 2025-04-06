using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorInimigos : MonoBehaviour
{
    public GameObject inimigoPrefab;
    private GameObject inimigoInstanciado;

    /// <summary>
    /// Lista de inimigos gerados
    /// <para>Essa lista é utilizada para armazenar os inimigos que foram instanciados.</para>
    /// </summary>
    public List<GameObject> ListinimigosGerados = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("InstaciarInimigo", 5f, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void InstaciarInimigo(){
        inimigoInstanciado = Instantiate(inimigoPrefab, transform.position, Quaternion.identity);
        ListinimigosGerados.Add(inimigoInstanciado);
        //Instancia o inimigo na posição do gerador
    }
}
