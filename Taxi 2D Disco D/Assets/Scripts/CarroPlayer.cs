using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEngine.UI;

public class CarroPlayer : MonoBehaviour {

    public static CarroPlayer instance;
    
    [HideInInspector]
    public Rigidbody2D rbd2dcarro;
    [HideInInspector]
    public float combustivel;
    [HideInInspector]
    public SpriteRenderer render;
    [HideInInspector]
    public bool travado;
    [HideInInspector]
    public CarroModel carroModel;

    [Header("Tempo em Segundos")]
    public float tempoDescontrolado = 2;
    public float tempoIvulneravel = 5;
    public float tempoEnergia = 10;

    [Header("Propriedades do Carro")]
    public float dirigibilidade;
    public float estabilidade;
    public float pesoChassi;
    public float consumo;
    public float forcaX;
    public float forcaY;
    public float freio;

    private bool viraEsquerda;
    private bool ivulneravel;
    private bool viraDireita;
    private bool acelerando;
    private float width;
    //private Button btnViraEsquerda;
    //private Button btnViraDireita;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }else
        {
            Destroy(this.gameObject);
        }

    }


    // Use this for initialization
    void Start ()
    {
        //btnViraDireita = GameObject.Find("btnViraDireita").GetComponent<Button>();
        //btnViraEsquerda = GameObject.Find("btnViraEsquerda").GetComponent<Button>();
        render = gameObject.GetComponent<SpriteRenderer>();
        rbd2dcarro = this.gameObject.GetComponent<Rigidbody2D>();
        rbd2dcarro.angularDrag = 70f;
        combustivel = 100f;
        travado = true;
        ivulneravel = false;
        viraDireita = false;
        acelerando = false;
        viraEsquerda = true;
        width = Screen.width;
        carroModel = new CarroModel();

        if (PlayerPrefs.GetInt(GerenciadorJogo.primeiravez) == 0)
        {
            Debug.Log("PrimeiraVez");
            carroModel = new CarroModel
            {
                dirigibilidade = 3000f,
                estabilidade = 10000f,
                pesoChassi = 20f,
                consumo = 0.1f,
                forcaX = 3000f,
                forcaY = 25f,
                freio = 5f,
            };
            SalvaEstadoJogador();
        }
            
        carroModel.CarregaEstadojogador();
        ConstroiPropriedadesCarro();
        ConstroiModelo();
       

    }
	
	// Update is called once per frame
	void Update ()
    {
        


    }

    public void CoroutineFunc(string x)
    {
        if (x == "Ivulneravel(Clone)" && !ivulneravel)
        {
            StartCoroutine(Magica());
        }
        else if (x == "Energia(Clone)")
        {
            StartCoroutine(Energia());
        }
        else if (x == "Oleo(Clone)")
        {
            StartCoroutine(PerdaDeControle());
        }
    }

    IEnumerator Magica()
    {
        rbd2dcarro.mass *=10;
        ivulneravel = true;
        for ( float i = 0f; i < tempoIvulneravel; i += 0.1f )
        {
            render.enabled = false;
            yield return new WaitForSeconds(0.05f);
            render.enabled = true;
            yield return new WaitForSeconds(0.05f);
        }
        rbd2dcarro.mass /= 10;
        ivulneravel = false;
    }

    IEnumerator Energia()
    {
        forcaY *= 2.5f;
        yield return new WaitForSeconds(tempoEnergia);
        forcaY /= 2.5f;   
    }

    IEnumerator PerdaDeControle()
    {
        Debug.Log("Estabilidade");
        estabilidade = 10000;
        yield return new WaitForSeconds(tempoDescontrolado);
        estabilidade = 10000;
    }

    
//        if (!travado) // Controle antigo do carro
//        {
//            rbd2dcarro.freezeRotation = false;
//            if (combustivel > 0)
//            {
//                combustivel -= consumo;
//            }
//        }
//        else
//        {
//            transform.position = new Vector3(0, -3.3f, 0f);
//transform.eulerAngles = new Vector3(0, 0, 0);
//        }
        

//        // Algoritmo que muda eixo X pela rotacao
//        if (transform.rotation.z< 0.75f && transform.rotation.z> -0.75f)
//        {
//            rbd2dcarro.velocity = new Vector2((-transform.rotation.z* forcaX) * Time.deltaTime, rbd2dcarro.velocity.y);
//        }
//        else
//        {
//            rbd2dcarro.velocity = new Vector2((transform.rotation.z* forcaX) * Time.deltaTime, rbd2dcarro.velocity.y);
//        }

//        // Algoritmo de Input do jogador eixoX
//        if (Input.GetKey(KeyCode.RightArrow) && transform.rotation.z< 0.80f)
//        {
//            rbd2dcarro.angularVelocity = -dirigibilidade* Time.deltaTime;
//        }
//        else if (Input.GetKey(KeyCode.LeftArrow) && transform.rotation.z > -0.80f)
//        {
//            rbd2dcarro.angularVelocity = dirigibilidade* Time.deltaTime;
//        }
//        else
//        {
//            if (transform.rotation.z< -0.03f )
//            {
//                rbd2dcarro.angularVelocity = (estabilidade* Time.deltaTime);
//            }
//            else if (transform.rotation.z > 0.03f )
//            {
//                rbd2dcarro.angularVelocity = (-estabilidade* Time.deltaTime);
//            }
//            else if (transform.rotation.z > -0.02f && transform.rotation.z< 0.02f)
//            {
//                transform.eulerAngles = new Vector3(0f, 0f, 0f); //Vector3.Lerp(this.transform.eulerAngles, new Vector3(0f, 0f, 0f), Time.deltaTime);
//            }
//        }

//        // Algortmo Input  do Jogador eixoY
//        if (Input.GetKey(KeyCode.UpArrow) && transform.position.y< 4)
//        {
//            rbd2dcarro.velocity = new Vector2(rbd2dcarro.velocity.x, forcaY* Time.deltaTime);
//rbd2dcarro.AddRelativeForce(new Vector2(0, (forcaY* Time.deltaTime) * 1000f));
//        }
//        else if (Input.GetKey(KeyCode.DownArrow))
//        {
//            rbd2dcarro.velocity = new Vector2(rbd2dcarro.velocity.x, (freio* Time.deltaTime) * -2);
//        }
//        else
//        {
//            rbd2dcarro.velocity = new Vector2(rbd2dcarro.velocity.x, freio* -Time.deltaTime);

//        }

    private void FixedUpdate()
    {

        if (!travado) // Controle antigo do carro
        {
            if (combustivel > 0)
            {
                combustivel -= consumo;
            }

            Touch[] touchs = Input.touches;
            viraDireita = false;
            viraEsquerda = false;
            acelerando = false;

            for (int i = 0; i < Input.touchCount; i++)
            {
                if (i == 1)
                {
                    viraDireita = false;
                    viraEsquerda = false;
                    acelerando = true;
                }
                else if (i == 0)
                {
                    if (touchs[i].position.x > width / 2)
                    {
                        viraDireita = true;
                    }
                    else if (touchs[i].position.x < width / 2)
                    {
                        viraEsquerda = true;
                    }
                }
            }

        }
        else
        {
            transform.position = new Vector3(0, -3.3f, 0f);
            transform.eulerAngles = new Vector3(0, 0, 0);
        }


        // Algoritmo que muda eixo X pela rotacao
        if (transform.rotation.z < 0.75f && transform.rotation.z > -0.75f)
        {
            rbd2dcarro.velocity = new Vector2((-transform.rotation.z * forcaX) * Time.deltaTime, rbd2dcarro.velocity.y);
        }
        else
        {
            rbd2dcarro.velocity = new Vector2((transform.rotation.z * forcaX) * Time.deltaTime, rbd2dcarro.velocity.y);
        }

        // Algoritmo de Input do jogador eixoX
        if (viraDireita && transform.rotation.z < 0.80f)
        {
            rbd2dcarro.angularVelocity = -dirigibilidade * Time.deltaTime;
        }
        else if (viraEsquerda && transform.rotation.z > -0.80f)
        {
            rbd2dcarro.angularVelocity = dirigibilidade * Time.deltaTime;
        }
        else
        {
            if (transform.rotation.z < -0.03f)
            {
                rbd2dcarro.angularVelocity = (estabilidade * Time.deltaTime);
            }
            else if (transform.rotation.z > 0.03f)
            {
                rbd2dcarro.angularVelocity = (-estabilidade * Time.deltaTime);
            }
            else if (transform.rotation.z > -0.02f && transform.rotation.z < 0.02f)
            {
                transform.eulerAngles = new Vector3(0f, 0f, 0f); //Vector3.Lerp(this.transform.eulerAngles, new Vector3(0f, 0f, 0f), Time.deltaTime);
            }
        }

        // Algortmo Input  do Jogador eixoY
        if (acelerando && transform.position.y < 4)
        {
            rbd2dcarro.velocity = new Vector2(rbd2dcarro.velocity.x, forcaY * Time.deltaTime);
            rbd2dcarro.AddRelativeForce(new Vector2(0, (forcaY * Time.deltaTime) * 1000f));
        }
        else if (!acelerando)
        {
            rbd2dcarro.velocity = new Vector2(rbd2dcarro.velocity.x, (freio * Time.deltaTime) * -2);
        }
        //else
        //{
        //    rbd2dcarro.velocity = new Vector2(rbd2dcarro.velocity.x, freio * -Time.deltaTime);

        //}

        if ( transform.position.y < -6 || transform.rotation.z < -0.95f || transform.rotation.z > 0.95f)
        {
            string recorde;
            if (Gerenciador_GUI.instance.canvasModel.recorde < GerenciadorJogo.instance.score)
            {
                Debug.Log("Recorde");
                Gerenciador_GUI.instance.canvasModel.recorde = GerenciadorJogo.instance.score;
                recorde = Gerenciador_GUI.instance.canvasModel.recorde.ToString();
            }
            recorde = "";
            Gerenciador_GUI.instance.GameOver(recorde);
            Debug.Log("Morreu");
        }
                
    }

    //private void FixedUpdate()
    //{
    //    if (!travado)
    //    {
    //        rbd2dcarro.freezeRotation = false;
    //        if (combustivel > 0)
    //        {
    //            combustivel -= consumo;
    //        }
    //    }
    //    else
    //    {
    //        transform.position = new Vector3(0, -3.3f, 0f);
    //        transform.eulerAngles = new Vector3(0, 0, 0);
    //    }


    //    // Algoritmo que muda eixo X pela rotacao
    //    if (transform.rotation.z < 0.75f && transform.rotation.z > -0.75f)
    //    {
    //        rbd2dcarro.velocity = new Vector2((-transform.rotation.z * forcaX) * Time.deltaTime, rbd2dcarro.velocity.y);
    //    }
    //    else
    //    {
    //        rbd2dcarro.velocity = new Vector2((transform.rotation.z * forcaX) * Time.deltaTime, rbd2dcarro.velocity.y);
    //    }




    //    // Algoritmo de Input do jogador eixoX
    //    if (viraDireita && !viraEsquerda && transform.rotation.z < 0.80f)
    //    {
    //        rbd2dcarro.angularVelocity = -dirigibilidade * Time.deltaTime;
    //    }
    //    else if (viraEsquerda && !viraDireita && transform.rotation.z > -0.80f)
    //    {
    //        rbd2dcarro.angularVelocity = dirigibilidade * Time.deltaTime;
    //    }
    //    else
    //    {
    //        if (transform.rotation.z < -0.03f)
    //        {
    //            rbd2dcarro.angularVelocity = (estabilidade * Time.deltaTime);
    //        }
    //        else if (transform.rotation.z > 0.03f)
    //        {
    //            rbd2dcarro.angularVelocity = (-estabilidade * Time.deltaTime);
    //        }
    //        else if (transform.rotation.z > -0.02f && transform.rotation.z < 0.02f)
    //        {
    //            transform.eulerAngles = new Vector3(0f, 0f, 0f); //Vector3.Lerp(this.transform.eulerAngles, new Vector3(0f, 0f, 0f), Time.deltaTime);
    //        }
    //    }


    //    // Algortmo Input  do Jogador eixoY
    //    if (viraEsquerda && viraDireita && transform.position.y < 4)
    //    {
    //        rbd2dcarro.velocity = new Vector2(rbd2dcarro.velocity.x, forcaY * Time.deltaTime);
    //        rbd2dcarro.AddRelativeForce(new Vector2(0, (forcaY * Time.deltaTime) * 1000f));
    //    }
    //    //else if (Input.GetKey(KeyCode.DownArrow))
    //    //{
    //    //    rbd2dcarro.velocity = new Vector2(rbd2dcarro.velocity.x, (freio * Time.deltaTime) * -2);
    //    //}
    //    else
    //    {
    //        rbd2dcarro.velocity = new Vector2(rbd2dcarro.velocity.x, freio * -Time.deltaTime);

    //    }

    //    if (transform.position.y < -6 || transform.rotation.z < -0.95f || transform.rotation.z > 0.95f)
    //    {
    //        string recorde;
    //        if (Gerenciador_GUI.instance.canvasModel.recorde < GerenciadorJogo.instance.score)
    //        {
    //            Debug.Log("Recorde");
    //            Gerenciador_GUI.instance.canvasModel.recorde = GerenciadorJogo.instance.score;
    //            recorde = Gerenciador_GUI.instance.canvasModel.recorde.ToString();
    //        }
    //        recorde = "";
    //        Gerenciador_GUI.instance.GameOver(recorde);
    //        Debug.Log("Morreu");
    //    }

    //}


    public class CarroModel
    {
        public float dirigibilidade;
        public float estabilidade;
        public float pesoChassi;
        public float consumo;
        public float forcaY;
        public float forcaX;
        public float freio;

        public void CarregaEstadojogador()
        {
            estabilidade = PlayerPrefs.GetFloat(GerenciadorJogo.vlEstabilidade);
            dirigibilidade = PlayerPrefs.GetFloat(GerenciadorJogo.vlControle);
            pesoChassi = PlayerPrefs.GetFloat(GerenciadorJogo.vlChassi);
            consumo = PlayerPrefs.GetFloat(GerenciadorJogo.vlConsumo);
            forcaY = PlayerPrefs.GetFloat(GerenciadorJogo.vlMotor);
            forcaX = PlayerPrefs.GetFloat(GerenciadorJogo.vlforcaX);
            freio = PlayerPrefs.GetFloat(GerenciadorJogo.vlFreio);
        }
    }

    public void ConstroiPropriedadesCarro()
    {
        dirigibilidade = carroModel.dirigibilidade;
        rbd2dcarro.mass = carroModel.pesoChassi;
        estabilidade = carroModel.estabilidade;
        consumo = carroModel.consumo;
        forcaY = carroModel.forcaY;
        forcaX = carroModel.forcaX;
        freio = carroModel.freio;
    }
    public void ConstroiModelo()
    {
        carroModel.dirigibilidade = dirigibilidade;
        carroModel.pesoChassi = rbd2dcarro.mass;
        carroModel.estabilidade = estabilidade;
        carroModel.consumo = consumo;
        carroModel.forcaX = forcaX;
        carroModel.forcaY = forcaY;
        carroModel.freio = freio;
    }

    public void SalvaEstadoJogador()
    {
        Debug.Log("Jogador Salvo");
        PlayerPrefs.SetFloat(GerenciadorJogo.vlEstabilidade, carroModel.estabilidade);
        PlayerPrefs.SetFloat(GerenciadorJogo.vlControle, carroModel.dirigibilidade);
        PlayerPrefs.SetFloat(GerenciadorJogo.vlChassi, carroModel.pesoChassi);
        PlayerPrefs.SetFloat(GerenciadorJogo.vlConsumo, carroModel.consumo);
        PlayerPrefs.SetFloat(GerenciadorJogo.vlMotor, carroModel.forcaY);
        PlayerPrefs.SetFloat(GerenciadorJogo.vlforcaX, carroModel.forcaX);
        PlayerPrefs.SetFloat(GerenciadorJogo.vlFreio, carroModel.freio);
    }

    public void StartCarroPlayer()
    {
        ConstroiModelo();
        SalvaEstadoJogador();
        rbd2dcarro.freezeRotation = false;
        transform.position = new Vector3(transform.position.x, -3.3f, 0f);
        travado = false;
    }
}
