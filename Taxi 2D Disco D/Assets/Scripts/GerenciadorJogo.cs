using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GerenciadorJogo : MonoBehaviour {

    public static GerenciadorJogo instance;

    [HideInInspector]
    public const string primeiravez = "PrimeiraVez", vlforcaX = "ForcaEixoX";
    public const string scoreData = "Score", moedasData = "Moedas", nvConsumo = "NivelCombustivel",
                        pcConsumo = "PrecoCombustivel", vlConsumo = "ValorConsumo", nvChassi = "NivelChassi", pcChassi = "PrecoChassi", vlChassi = "ValorChassi",
                        nvFreio = "NivelFreio", pcFreio = "PrecoFreio", vlFreio = "ValorFreio", nvMotor = "NivelMotor", vlMotor ="ValorMotor", pcMotor = "PrecoMotor",
                        nvControle  = "NivelControle", pcControle = "PrecoControle", vlControle = "ValorControle", vlEstabilidade = "ValorEstabilidade", recorde = "Recorde";

    
    public float moedas, score, distancia;

    [Header("Prefabs Carros")]
    public GameObject[] Carros_obj;

    private int  numeroAleatorio;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Use this for initialization
    void Start ()
    {
        numeroAleatorio = Carros_obj.Length;
        score = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!CarroPlayer.instance.travado)
        {
            score += Time.deltaTime * distancia;
        }
    }

    public void FechaJogo()
    {
        Debug.Log("Fechando Jogo");
        Application.Quit();
    }

    public void Instancia_Boot(GameObject obj)
    {
        GameObject CarroClone = Instantiate(Carros_obj[Random.Range(0,numeroAleatorio)], obj.transform);
    }
}
