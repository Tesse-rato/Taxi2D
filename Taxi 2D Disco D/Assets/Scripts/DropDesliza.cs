using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDesliza : MonoBehaviour {

    private Transform transDropDesliza;
    public float velocidadeMaxima;
    public float posX_inicial;

    private float velocidade;

    // Use this for initialization
    void Start () {
        
        transDropDesliza = this.gameObject.GetComponent<Transform>();
        transDropDesliza.transform.position = new Vector3(posX_inicial, 6.5f , 0);
        velocidade = velocidadeMaxima;
    }
	
	// Update is called once per frame
	void Update () {

        transDropDesliza.transform.position = new Vector3(transDropDesliza.transform.position.x + (velocidade * Time.deltaTime), transDropDesliza.transform.position.y, 0f);

        if (transDropDesliza.transform.position.x > 2f)
        {
            transDropDesliza.position = new Vector3(1.95f, transDropDesliza.transform.position.y);
            velocidade *= -1;
        }
        else if (transDropDesliza.transform.position.x < -2f)
        {
            transDropDesliza.position = new Vector3(-1.95f, transDropDesliza.transform.position.y);
            velocidade *= -1;
        }
    }
}
