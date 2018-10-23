using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_BOOT : MonoBehaviour {

    [Header("Velocidade dos BOOT")]
    public float velocidadeMAX = 80f;
    public float velocidadeMIN = 60f;
    [Header("Configuração Realismo")]
    public float direcao = 10f;
    public float transAuxFaixa;

    private bool colidiuCantos = false;
    private bool voltarPosicao = false;
    private float velocidade_carro;
    private float temp;
    private Rigidbody2D rdb2d;
    
    // Use this for initialization
    private void Start()
    {
        rdb2d = this.gameObject.GetComponent<Rigidbody2D>();
        velocidade_carro = Random.Range(velocidadeMIN, velocidadeMAX) * -1;
    }

    // Update is called once per frame
    private void Update()
    {
        if (transform.position.x < -2.50 || transform.position.x > 2.50)
        {
            rdb2d.velocity = new Vector2((transform.position.x * Time.deltaTime) * -30, rdb2d.velocity.y);
        }

        Destroy_obj();
    }

    private void FixedUpdate()
    {
        AlinhaCarro();
        ControleDirecao();
        RetornaFaixa(voltarPosicao);
        BatidaMureta(colidiuCantos);
        
    }

    private void AlinhaCarro()
    {
        if (transform.rotation.z < 0 )
        {
            rdb2d.angularVelocity = 800 * Time.deltaTime;
        }
        else if (transform.rotation.z > 0)
        {
            rdb2d.angularVelocity = -800 * Time.deltaTime;
        }
    }
    
    private void ControleDirecao()
    {
        if (transform.rotation.z < -0.002f && transform.rotation.z > -0.89)
        {
            temp = transform.rotation.z * -1;
            rdb2d.velocity = new Vector2((direcao * temp) * (Time.deltaTime * 100), rdb2d.velocity.y);

        }
        else if (transform.rotation.z > 0.002f && transform.rotation.z < 0.89)
        {
            temp = transform.rotation.z * -1;
            rdb2d.velocity = new Vector2((direcao * temp) * (Time.deltaTime * 100), rdb2d.velocity.y);
        }
        
        rdb2d.velocity = new Vector2(rdb2d.velocity.x, velocidade_carro * 10 * Time.deltaTime);
    }
    
    private void RetornaFaixa(bool retornar)  // Metodo com defeito
    {
        if (retornar)
        {
            if (transform.localPosition.x < -0.3f && transform.rotation.z > -20.3f)
            {
                rdb2d.angularVelocity = (transform.localPosition.x * 1000) * Time.deltaTime;
            }
            else if (transform.localPosition.x > 0.3f && transform.rotation.z < 20.3f)
            {
                rdb2d.angularVelocity = (transform.localPosition.x * 1000) * Time.deltaTime;
            }
        }
    }
    
    private void Destroy_obj()
    {
        if (this.gameObject.transform.position.y < -6 || this.transform.rotation.z < -0.98f || this.transform.rotation.z > 0.98f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("AI_BOOT"))
        {
            EvitaBatida(collision);
        }
        if (collision.gameObject.CompareTag("ColisoresLateral"))
        {
            colidiuCantos = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        voltarPosicao = true;
    }

    private void EvitaBatida(Collider2D colisor)
    {
        
        if (transform.position.y < colisor.gameObject.transform.position.y)
        {
            if (colisor.gameObject.transform.position.x < transform.position.x)
            {
                rdb2d.velocity = new Vector2(100 * Time.deltaTime, rdb2d.velocity.y);
            }
            else
            {
                rdb2d.velocity = new Vector2(-100 * Time.deltaTime, rdb2d.velocity.y);
            }
        }
        else if (transform.position.y > colisor.gameObject.transform.position.y)
        {
            if (transform.position.x < colisor.gameObject.transform.position.x)
            {
                rdb2d.velocity = new Vector2(-100 * Time.deltaTime, rdb2d.velocity.y);
            }
            else
            {
                rdb2d.velocity = new Vector2(100 * Time.deltaTime, rdb2d.velocity.y);
            }
        }
        
    }
    private void BatidaMureta(bool bateu)
    {
        if (bateu)
        {
            rdb2d.velocity = new Vector2(rdb2d.velocity.x, Time.deltaTime * -1000f);
        }
    }

}
