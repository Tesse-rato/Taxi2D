using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rua_Script : MonoBehaviour {

    Renderer render = new Renderer();

	// Use this for initialization
	void Start ()
    {
        render = GameObject.Find("Rua").GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        render.material.mainTextureScale = new Vector2(0f, 0.5f);

	}
}
