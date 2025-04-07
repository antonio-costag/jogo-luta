using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PontosDisplay : MonoBehaviour
{
    public Text textPontos;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        textPontos.text = "Pontos: " + PontosPleyer.pontos.ToString();
    }
}
