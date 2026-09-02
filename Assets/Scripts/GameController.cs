using UnityEngine;
using PrimeTween;
using TMPro;

public class GameController : MonoBehaviour
{

    [SerializeField] CardsController cardsController;
    [SerializeField] ZoomCards scriptZoomCard;
    [SerializeField] Transform gridTransform;
    int numeroCartas;
    int paresEncontrados;
    public GameObject vitoria;
    public TextMeshProUGUI contadorPares;

    void TelaVitoria(){
        if(paresEncontrados == numeroCartas/2 && !vitoria.activeSelf && !scriptZoomCard.IsZoomShow()){
            vitoria.SetActive(true);
            Tween.Scale(vitoria.transform, Vector3.one, 0.2f);
            gridTransform.gameObject.SetActive(false);
        }
    }

    void Start()
    {
        numeroCartas = cardsController.GetNumberCards();
        vitoria.SetActive(false);
    }

    void Update()
    {
        paresEncontrados = cardsController.GetPairsFound();
        contadorPares.text = "Pares Encontrados: " + paresEncontrados.ToString();
        TelaVitoria();
    }
}
