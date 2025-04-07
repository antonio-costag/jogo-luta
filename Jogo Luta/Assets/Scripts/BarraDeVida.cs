using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraDeVida : MonoBehaviour
{
    private Image barraDeVida;
    private Player playerScript;
    // Start is called before the first frame update
    void Start()
    {
        barraDeVida = GetComponent<Image>();
        barraDeVida.fillAmount = 1f;

        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdadeBarraDeVida(playerScript.vidaAtual, playerScript.vidaMax);
    }
    void UpdadeBarraDeVida(float vidaAtual, float vidaMaxima){
        barraDeVida.fillAmount = vidaAtual / vidaMaxima;
    }
}
