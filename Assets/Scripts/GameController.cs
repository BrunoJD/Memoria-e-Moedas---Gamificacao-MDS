using UnityEngine;
using PrimeTween;
using TMPro;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    [SerializeField] CardsController cardsController;
    [SerializeField] ZoomCards scriptZoomCard;
    [SerializeField] Transform gridTransform;
    [SerializeField] GameObject RoundsScreen;
    [SerializeField] GameObject ShopScreen;
    [SerializeField] GameObject EditScreen;
    int numeroCartas;
    int paresEncontrados;
    public GameObject vitoria;
    public TextMeshProUGUI contadorPares;
    Casas casaAtual;
    ScoreManagement scoreManagement;

    void TelaVitoria(){
        if(paresEncontrados == numeroCartas/2 && !vitoria.activeSelf && !scriptZoomCard.IsZoomShow()){
            vitoria.SetActive(true);
            Tween.Scale(vitoria.transform, Vector3.one, 0.2f);
            gridTransform.gameObject.SetActive(false);
        }
    }

    public void TelaRounds(){
        RoundsScreen.gameObject.SetActive(true);

        int rodadaAtual = scoreManagement.GetRodadaAtual();
        casaAtual = scoreManagement.TurnoAtual();

        TMP_Text roundCount = RoundsScreen.transform.Find("Panel/RoundCount").GetComponent<TMP_Text>();
        Image casaIcon = RoundsScreen.transform.Find("Casa/CasaIcon").GetComponent<Image>();
        TMP_Text casaName = RoundsScreen.transform.Find("Casa/CasaName").GetComponent<TMP_Text>();

        roundCount.text = "Rodada " + rodadaAtual;
        casaIcon.sprite = casaAtual.GetCasaSprite();
        casaName.text = casaAtual.GetNomeCasa();
    }

    public void TelaShop(){
        ShopScreen.gameObject.SetActive(true);
        casaAtual = scoreManagement.TurnoAtual();

        Image casaIcon = ShopScreen.transform.Find("Casa/CasaIcon").GetComponent<Image>();
        TMP_Text casaName = ShopScreen.transform.Find("Casa/CasaName").GetComponent<TMP_Text>();
        TMP_Text casaMoedas = ShopScreen.transform.Find("Moedas/CasaMoedas").GetComponent<TMP_Text>();

        casaIcon.sprite = casaAtual.GetCasaSprite();
        casaName.text = casaAtual.GetNomeCasa();
        casaMoedas.text = casaAtual.GetMoedas() + " moedas";
    }
    public void TelaEdit(Casas casaEdit){
        EditScreen.gameObject.SetActive(true);

        Image casaIcon = EditScreen.transform.Find("Casa/CasaIcon").GetComponent<Image>();
        TMP_Text casaName = EditScreen.transform.Find("Casa/CasaName").GetComponent<TMP_Text>();
        TMP_Text casaMoedas = EditScreen.transform.Find("Moedas/CasaMoedas").GetComponent<TMP_Text>();
        TMP_Text casaPontos = EditScreen.transform.Find("Pontos/CasaPontos").GetComponent<TMP_Text>();

        casaIcon.sprite = casaEdit.GetCasaSprite();
        casaName.text = casaEdit.GetNomeCasa();
        casaMoedas.text = casaEdit.GetMoedas() + " moedas";
        casaPontos.text = casaEdit.GetPontos() + " pontos";
    }

    public void FinalizarLoja(int moedas){
        casaAtual.SetNumeroMoedas(moedas);
        scoreManagement.AtualizarLoja(moedas);
        scoreManagement.MudarTruno();
        TelaRounds();
    }

    public void ConfirmarEdicao(int moedas, int pontos, CasaSlotUIGame slotEdit, Casas casaEdit){
        casaEdit.SetNumeroMoedas(moedas);
        casaEdit.SetNumeroPontos(pontos);

        slotEdit.SetMoedas(moedas);
        slotEdit.SetPontos(pontos);
    }

    void Start()
    {
        numeroCartas = cardsController.GetNumberCards();
        vitoria.SetActive(false);
        scoreManagement = GetComponent<ScoreManagement>();
    }

    void Update()
    {
        paresEncontrados = cardsController.GetPairsFound();
        contadorPares.text = "Pares Encontrados: " + paresEncontrados.ToString();
        TelaVitoria();
    }
}
