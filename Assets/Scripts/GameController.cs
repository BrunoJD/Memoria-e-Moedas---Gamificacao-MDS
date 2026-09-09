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
    [SerializeField] Sprite emptyActionCard;
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
        Image cartaAcao1 = RoundsScreen.transform.Find("CartaAcao/Carta1").GetComponent<Image>();
        Image cartaAcao2 = RoundsScreen.transform.Find("CartaAcao/Carta2").GetComponent<Image>();
        TMP_Text casaName = RoundsScreen.transform.Find("Casa/CasaName").GetComponent<TMP_Text>();
        TMP_Text descricaoAction = RoundsScreen.transform.Find("CartasAcao/Descricao").GetComponent<TMP_Text>();

        roundCount.text = "Rodada " + rodadaAtual;
        casaIcon.sprite = casaAtual.GetCasaSprite();
        casaName.text = casaAtual.GetNomeCasa();

        //if (casaAtual.GetActionCards() == null || casaAtual.GetActionCards()[0] == null){
            cartaAcao1.sprite = emptyActionCard;
        //}

        //if (casaAtual.GetActionCards() == null || casaAtual.GetActionCards()[1] == null){
            cartaAcao2.sprite = emptyActionCard;
        //}
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
