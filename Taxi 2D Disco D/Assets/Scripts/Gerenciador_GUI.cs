using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Gerenciador_GUI : MonoBehaviour {

    [HideInInspector]
    public static Gerenciador_GUI instance;
    [HideInInspector]
    public float dinheiroAntesDoJogo;

    // Canvas Principal
    public CanvasModel canvasModel;
    private Canvas canvasMenu;
    private Text txtRecorde;
    private Text txtpcCombustivel;
    private Text txtnvCombustivel;
    private Text txtpcControle;
    private Text txtnvControle;
    private Text txtpcChassi;
    private Text txtnvChassi;
    private Text txtpcFreio;
    private Text txtnvFreio;
    private Text txtpcMotor;
    private Text txtnvMotor;

    private Image imgControle;
    private Image imgCombus;
    private Image imgChassi;
    private Image imgFreio;
    private Image imgMotor;

    // Canvas do jogo
    private Image imgCombustivel;
    private Canvas canvasJogo;
    private Text txtDinheiro;
    private Button btnResume;
    private Image btnimgResume;
    private Text txtMoedas;
    private Text txtScore;
    private bool click;
    private bool rodando;

    // Canvas Pause //
    private Canvas canvasPause;
    private Text txtPausePrincipal;
    private Text txtPauseRecorde;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        
    }

    // Use this for initialization
    void Start () {

        canvasMenu = GameObject.Find("CanvasMenu").GetComponent<Canvas>();
        canvasJogo = GameObject.Find("CanvasJogo").GetComponent<Canvas>();
        canvasPause = GameObject.Find("CanvasPause").GetComponent<Canvas>();
        imgCombus = GameObject.Find("PanelButton_Tanque").GetComponent<Image>();
        imgChassi = GameObject.Find("PanelButton_Chassi").GetComponent<Image>();
        imgFreio = GameObject.Find("PanelButton_Freio").GetComponent<Image>();
        imgMotor = GameObject.Find("PanelButton_Motor").GetComponent<Image>();
        imgControle = GameObject.Find("PanelButton_Controle").GetComponent<Image>();
        imgCombustivel = GameObject.Find("imgCombustivel").GetComponent<Image>();
        btnResume = GameObject.Find("btnResume").GetComponent<Button>();
        btnimgResume = GameObject.Find("btnResume").GetComponent<Image>();

        txtDinheiro = GameObject.Find("txtDinheiro").GetComponent<Text>();
        txtRecorde = GameObject.Find("txtRecorde").GetComponent<Text>();
        txtMoedas = GameObject.Find("txtMoedas").GetComponent<Text>();
        txtScore = GameObject.Find("txtScore").GetComponent<Text>();

        txtpcCombustivel = GameObject.Find("pcCombustivel").GetComponent<Text>();
        txtnvCombustivel = GameObject.Find("nvCombustivel").GetComponent<Text>();
        txtpcControle = GameObject.Find("pcControle").GetComponent<Text>();
        txtnvControle = GameObject.Find("nvControle").GetComponent<Text>();
        txtpcChassi = GameObject.Find("pcChassi").GetComponent<Text>();
        txtnvChassi = GameObject.Find("nvChassi").GetComponent<Text>();
        txtpcFreio = GameObject.Find("pcFreio").GetComponent<Text>();
        txtnvFreio = GameObject.Find("nvFreio").GetComponent<Text>();
        txtpcMotor = GameObject.Find("pcMotor").GetComponent<Text>();
        txtnvMotor = GameObject.Find("nvMotor").GetComponent<Text>();

        txtPausePrincipal = GameObject.Find("txtPausePrincipal").GetComponent<Text>();
        txtPauseRecorde = GameObject.Find("txtPauseRecorde").GetComponent<Text>();

        if (PlayerPrefs.GetInt(GerenciadorJogo.primeiravez) == 0)
        {
            canvasModel = new CanvasModel
            {
                pcCombustivel = 50f,
                nvCombustivel = 0,
                pcChassi = 50f,
                nvChassi = 0f,
                pcControle = 50f,
                nvControle = 0f,
                pcFreio = 5000f,
                nvFreio = 0f,
                pcMotor = 50f,
                nvMotor = 0f,

                score = 0f,
                moedas = 100f,
                recorde = 0f,
            };
            SalvaModeloCanvas(canvasModel);
            AtualizaCanvasMenu();
        }
        else
        {
            canvasModel = new CanvasModel();
            canvasModel.CarregaModeloCanvas();
            AtualizaCanvasMenu();
        }

        canvasMenu.enabled = true;
        canvasJogo.enabled = false;
        canvasPause.enabled = false;
        click = false;
        rodando = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (rodando)
        {
            //canvasModel.moedas = GerenciadorJogo.instance.moedas;
            txtMoedas.text = ((int)canvasModel.moedas).ToString();
            imgCombustivel.fillAmount = CarroPlayer.instance.combustivel * 0.01f;
            txtScore.text = GerenciadorJogo.instance.score.ToString();
        }

        if (canvasMenu.enabled)
        {
            txtRecorde.text = "Recorde: " + canvasModel.recorde;
        }
	}

    public void BotaoStart()
    {
        Debug.Log("Start");
        dinheiroAntesDoJogo = canvasModel.moedas;
        txtPausePrincipal.text = "Pausado";
        btnResume.enabled = true;
        btnimgResume.color = new Color(1,1,1,1);
        canvasMenu.enabled = false;
        canvasJogo.enabled = true;
        canvasPause.enabled = false;
        SalvaModeloCanvas(canvasModel);
        CarroPlayer.instance.combustivel = 100;
        CarroPlayer.instance.SalvaEstadoJogador();
        GerenciadorJogo.instance.score = 0f;
        PlayerPrefs.SetInt(GerenciadorJogo.primeiravez, 1);
        rodando = true;
    }

    public void BotaoCombustivel()
    {
        if (CarroPlayer.instance.consumo > 0.005 && canvasModel.moedas >= canvasModel.pcCombustivel && canvasModel.nvMotor < 20)
        {
            // Nivel Maximo 24 = R$ 841.706,00
            canvasModel.moedas -= canvasModel.pcCombustivel;
            canvasModel.nvCombustivel++;
            CarroPlayer.instance.consumo -= 0.004f;
            StartCoroutine(PiscaCor(imgCombus, new Color(0, 1, 0)));
            canvasModel.pcCombustivel *= 1.5f;
            AtualizaCanvasMenu();
            //SalvaModeloCanvas(canvasModel);
        }
        else if(canvasModel.nvMotor > 23)
        {
            txtnvCombustivel.text = "Nivel Max";
            txtpcCombustivel.text = "-";
            StartCoroutine(PiscaCor(imgCombus, new Color(0,0,1,0.5f)));
        }
        else
        {
            StartCoroutine(PiscaCor(imgCombus, new Color(1,0,0)));
        }
    }

    public void BotaoChassi()
    {
        if ( canvasModel.moedas >= canvasModel.pcChassi && canvasModel.nvChassi < 37)
        {
            CarroPlayer.instance.rbd2dcarro.mass += 4.9f;
            canvasModel.moedas -= canvasModel.pcChassi;
            canvasModel.pcChassi *= 1.3f;
            canvasModel.nvChassi++;
            StartCoroutine(PiscaCor(imgChassi, new Color (0,1,0,1)));
            AtualizaCanvasMenu();
        }
        else if (canvasModel.nvChassi >= 37)
        {
            txtnvChassi.text = "Nivel Max";
            txtpcChassi.text = "-";
            StartCoroutine(PiscaCor(imgChassi, new Color (0,0,1,1)));
        }
        else
        {
            StartCoroutine(PiscaCor(imgChassi, new Color(1,0,0,1)));
        }
    }

    public void BotaoFreio()
    {
        if ( canvasModel.moedas > canvasModel.pcFreio && canvasModel.nvFreio < 3)
        {
            canvasModel.moedas -= canvasModel.pcFreio;
            CarroPlayer.instance.freio += 50;
            canvasModel.nvFreio++;
            canvasModel.pcFreio *= 10;
            StartCoroutine(PiscaCor(imgFreio, new Color(0,1,0,1)));
            AtualizaCanvasMenu();
        }
        else if (canvasModel.nvFreio >= 3)
        {
            txtnvFreio.text = "Nivel Max";
            txtpcFreio.text = "-";
            StartCoroutine(PiscaCor(imgFreio, new Color(0, 0, 1, 1)));
        }
        else
        {
            StartCoroutine(PiscaCor(imgFreio, new Color(1, 0, 0, 1)));
        }
    }

    public void BotaoMotor()
    {
        if (canvasModel.moedas > canvasModel.pcMotor && canvasModel.nvMotor < 10)
        {
            canvasModel.moedas -= canvasModel.pcMotor;
            canvasModel.nvMotor++;
            canvasModel.pcMotor *= 2.6f;
            CarroPlayer.instance.forcaY += 8;
            AtualizaCanvasMenu();
            StartCoroutine(PiscaCor(imgMotor, new Color(0, 1, 0, 1)));
        }
        else if ( canvasModel.nvMotor >= 10)
        {
            txtnvMotor.text = "Nivel Max";
            txtpcMotor.text = "-";
            StartCoroutine(PiscaCor(imgMotor, new Color (0,0,1,1)));
        }
        else
        {
            StartCoroutine(PiscaCor(imgMotor, new Color(1, 0, 0, 1)));
        }

    }

    public void BotaoControle()
    {
        if ( canvasModel.moedas > canvasModel.pcControle && canvasModel.nvControle < 4)
        {
            canvasModel.moedas -= canvasModel.pcControle;
            canvasModel.nvControle++;
            CarroPlayer.instance.dirigibilidade *= 1.4f;
            CarroPlayer.instance.estabilidade *= 1.3f;
            canvasModel.pcControle *= 4;
            AtualizaCanvasMenu();
            StartCoroutine(PiscaCor(imgControle, new Color(0, 1, 0, 1)));
        }
        else if ( canvasModel.nvMotor >= 4)
        {
            txtnvControle.text = "Nivel Max";
            txtpcControle.text = "-";
            StartCoroutine(PiscaCor(imgControle, new Color(0, 0, 1, 1)));
        }
        else
        {
            StartCoroutine(PiscaCor(imgControle, new Color(1, 0, 0, 1)));
        }
    }

    public void BotaoPause()
    {
        if (!click)
        {
            canvasPause.enabled = true;
            Debug.Log(Time.timeScale);
            Time.timeScale = 0;
            click = !click;

        }
        else
        {
            canvasPause.enabled = false;
            Time.timeScale = 1;
            click = !click;
        }
    }

    public void BotaoMenu()
    {
        SalvaModeloCanvas(canvasModel);
        canvasModel.CarregaModeloCanvas();
        AtualizaCanvasMenu();
        canvasJogo.enabled = false;
        canvasPause.enabled = false;
        CarroPlayer.instance.travado = true;
        canvasMenu.enabled = true;
        BotaoPause();
    }

    public void GameOver(string recorde)
    {
        if (recorde != "")
        {
            txtPauseRecorde.text = "Novo Recorde: " + canvasModel.recorde;
        }
        txtPauseRecorde.text = "Recorde: " + canvasModel.recorde;
        txtPausePrincipal.text = "Perdeu :(";
        //canvasModel.moedas = dinheiroAntesDoJogo;
        btnResume.enabled = false;
        btnimgResume.color = new Color(0.25f,0.25f,0.25f,1);
        BotaoPause();
    }

    public void LimpaPlayerPrefs()
    {
        txtDinheiro.text = "BrF";
        txtMoedas.text = "BrF";
        txtnvChassi.text = "BrF";
        txtnvCombustivel.text = "BrF";
        txtnvControle.text = "BrF";
        txtnvFreio.text = "BrF";
        txtnvMotor.text = "BrF";
        txtpcChassi.text = "BrF";
        txtpcCombustivel.text = "BrF";
        txtpcControle.text = "BrF";
        txtpcFreio.text = "BrF";
        txtpcMotor.text = "BrF";
        txtRecorde.text = "BrF";
        PlayerPrefs.DeleteAll();
    }

    IEnumerator PiscaCor(Image img, Color color)
    {
        for (float i = 0.5f; i > 0; i -= 0.1f)
        {
            img.color = color;
            yield return new WaitForSeconds(0.03f);
            img.color = new Color(0, 0, 0, 0.6f);
            yield return new WaitForSeconds(0.03f);
        }
    }

    public class CanvasModel // Classe modelo dos calores do Carro
    {
        public float pcCombustivel, pcChassi, pcFreio, pcMotor, pcControle;
        public float nvCombustivel, nvChassi, nvFreio, nvMotor, nvControle;
        public float score, moedas, recorde;

        public CanvasModel()
        {
        }

        public void CarregaModeloCanvas()
        {
            pcCombustivel = PlayerPrefs.GetFloat(GerenciadorJogo.pcConsumo);
            nvCombustivel = PlayerPrefs.GetFloat(GerenciadorJogo.nvConsumo);
            pcControle = PlayerPrefs.GetFloat(GerenciadorJogo.pcControle);
            nvControle = PlayerPrefs.GetFloat(GerenciadorJogo.nvControle);
            pcChassi = PlayerPrefs.GetFloat(GerenciadorJogo.pcChassi);
            nvChassi = PlayerPrefs.GetFloat(GerenciadorJogo.nvChassi);
            pcFreio = PlayerPrefs.GetFloat(GerenciadorJogo.pcFreio);
            nvFreio = PlayerPrefs.GetFloat(GerenciadorJogo.nvFreio);
            pcMotor = PlayerPrefs.GetFloat(GerenciadorJogo.pcMotor);
            nvMotor = PlayerPrefs.GetFloat(GerenciadorJogo.nvMotor);
            recorde = PlayerPrefs.GetFloat(GerenciadorJogo.recorde);
            moedas = PlayerPrefs.GetFloat(GerenciadorJogo.moedasData);
            score = PlayerPrefs.GetFloat(GerenciadorJogo.scoreData);
        }

    }

    public void AtualizaCanvasMenu()
    {
        txtRecorde.text = " Recorde: " + canvasModel.recorde.ToString();
        txtDinheiro.text = "Dinheiro: " + ((int)canvasModel.moedas).ToString();

        txtPauseRecorde.text = "Recorde: " + canvasModel.recorde.ToString();
        
        txtpcCombustivel.text = ((int)canvasModel.pcCombustivel).ToString();
        txtnvCombustivel.text = "Nivel: " + canvasModel.nvCombustivel.ToString();
        txtpcControle.text = ((int)canvasModel.pcControle).ToString();
        txtnvControle.text = "Nivel: " + canvasModel.nvControle.ToString();
        txtpcChassi.text = ((int)canvasModel.pcChassi).ToString();
        txtnvChassi.text = "Nivel: " + canvasModel.nvChassi.ToString();
        txtpcFreio.text = ((int)canvasModel.pcFreio).ToString();
        txtnvFreio.text = "Nivel: " + canvasModel.nvFreio.ToString();
        txtpcMotor.text = ((int)canvasModel.pcMotor).ToString();
        txtnvMotor.text = "Nivel: " + canvasModel.nvMotor.ToString();

        // PlayerPrefs CanvasJogo //
        txtMoedas.text = canvasModel.moedas.ToString();
        txtScore.text = "Pontos: " + canvasModel.score;
    }

    public void SalvaModeloCanvas(CanvasModel canvas)
    {
        PlayerPrefs.SetFloat(GerenciadorJogo.pcControle, canvas.pcControle);
        PlayerPrefs.SetFloat(GerenciadorJogo.nvControle, canvas.nvControle);
        PlayerPrefs.SetFloat(GerenciadorJogo.pcConsumo, canvas.pcCombustivel);
        PlayerPrefs.SetFloat(GerenciadorJogo.nvConsumo, canvas.nvCombustivel);
        PlayerPrefs.SetFloat(GerenciadorJogo.pcChassi, canvas.pcChassi);
        PlayerPrefs.SetFloat(GerenciadorJogo.nvChassi, canvas.nvChassi);
        PlayerPrefs.SetFloat(GerenciadorJogo.pcFreio, canvas.pcFreio);
        PlayerPrefs.SetFloat(GerenciadorJogo.nvFreio, canvas.nvFreio);
        PlayerPrefs.SetFloat(GerenciadorJogo.pcMotor, canvas.pcMotor);
        PlayerPrefs.SetFloat(GerenciadorJogo.nvMotor, canvas.nvMotor);
        PlayerPrefs.SetFloat(GerenciadorJogo.moedasData, canvas.moedas);
        PlayerPrefs.SetFloat(GerenciadorJogo.scoreData, canvas.score);
        PlayerPrefs.SetFloat(GerenciadorJogo.recorde, canvas.recorde);
    }
}
