using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Itens : MonoBehaviour {

    public float combustivelAdicional;
    public float forcaObstaculo;

	// Use this for initialization
	void Start ()
    {
        // Player Perefs //
        combustivelAdicional = 20f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - Time.deltaTime * 5, transform.position.z);

        if (transform.position.y < -6f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            switch (gameObject.name)
            {
                case "Gasolina(Clone)":
                    CarroPlayer.instance.combustivel += combustivelAdicional;
                    if (CarroPlayer.instance.combustivel > 100f)
                    {
                        CarroPlayer.instance.combustivel = 100f;
                    }
                    break;
                case "Ouro(Clone)":
                    Gerenciador_GUI.instance.canvasModel.moedas += 50000f;
                    break;
                case "Oleo(Clone)":
                    CarroPlayer.instance.CoroutineFunc(gameObject.name);
                    break;
                case "Moeda(Clone)":
                    Gerenciador_GUI.instance.canvasModel.moedas += 2f;
                    break;
                case "Ivulneravel(Clone)":
                    CarroPlayer.instance.CoroutineFunc(gameObject.name);
                    break;
                case "Magia(Clone)":

                    break;
                case "Obstaculo(Clone)":
                    CarroPlayer.instance.rbd2dcarro.AddForce(new Vector2(0f, -forcaObstaculo * Time.deltaTime));
                    break;
                case "Energia(Clone)":
                    CarroPlayer.instance.CoroutineFunc(gameObject.name);
                    break;

            }

            Destroy(gameObject);
        }
    }
}
