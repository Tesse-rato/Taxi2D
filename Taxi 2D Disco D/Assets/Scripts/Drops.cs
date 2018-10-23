using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drops : MonoBehaviour {
    
    [Header("Velocidade")]
    public float tempoMaximo;
    public float tempoMinimo;

    private float tempoDelay;
    private bool primeiravez;

    private void Start()
    {
        tempoDelay = Time.time;
        primeiravez = true;
    }

    private void Update()
    {

    }

    private void OnTriggerExit2D(Collider2D collision){
        
        if (Time.time > tempoDelay && !primeiravez)
        {
            GerenciadorJogo.instance.Instancia_Boot(this.gameObject);
            this.tempoDelay += Random.Range(tempoMinimo,tempoMaximo);
        }
        primeiravez = false;
    }
}
