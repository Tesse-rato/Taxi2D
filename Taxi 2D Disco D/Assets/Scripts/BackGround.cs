using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGround : MonoBehaviour {

    public Renderer render;
    public float velocidade = 0.1f;

	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        render.material.mainTextureOffset += new Vector2(0f, velocidade * Time.deltaTime);
	}
}
