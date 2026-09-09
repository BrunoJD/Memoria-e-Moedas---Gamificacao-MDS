using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using TMPro;
using PrimeTween;
using UnityEngine.EventSystems;

public class CardsController : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] Card cardPrefab;
    [SerializeField] Transform gridTransform;
    [SerializeField] Sprite[] spritesConceito;
    [SerializeField] Sprite[] spritesDefinicao;
    [SerializeField] ScoreManagement scoreManagement;
    [SerializeField] GameController gameController;
    List<int> listaDeIdCartas = new List<int>();
    List<Card> listaCartas = new List<Card>();
    Card primeiraCartaSelecionada;
    Card segundaCartaSelecionada;
    int paresEncontrados = 0;

    [Header("Zoom Card Screen")]
    public GameObject zoomCard;
    ZoomCards scriptZoomCard;

    [Header("Feedback Screen")]
    public GameObject feedback;
    public Image fundoFeedback;
    public TMP_Text textoFeedback;

    void PrepararCartas(){
        CriarListaIds();
        CriarCartas();
        MisturarCartas();
    }

    void CriarListaIds()
    {
        listaDeIdCartas.Clear();

        for (int i = 0; i < GetNumberCards(); i++){
            listaDeIdCartas.Add(i+1);
        }
    }

    void CriarCartas(){
        listaCartas.Clear();
        int idCarta = 0;

        for (int grupo = 0; grupo < GetNumberGrups(); grupo++){
            for (int repetirCarta = 0; repetirCarta < 5; repetirCarta++){
                Card card = Instantiate(cardPrefab, gridTransform);
                card.setSpriteEscondido(spritesConceito[grupo]);

                card.idGroupo = grupo;
                card.tipo = CardType.Conceito;

                ConfigurarIdCarta(card, listaDeIdCartas[idCarta]);

                card.controller = this;
                listaCartas.Add(card);
                card.transform.localScale = Vector3.zero;
                idCarta++;
            }
        }

        for (int grupo = 0; grupo < GetNumberGrups(); grupo++){
            for (int definicao = 0; definicao < 5; definicao++){
                Card card = Instantiate(cardPrefab, gridTransform);
                int spriteIndice = (grupo*5) + definicao;
                card.setSpriteEscondido(spritesDefinicao[spriteIndice]);

                card.idGroupo = grupo;
                card.tipo = CardType.Definicao;

                ConfigurarIdCarta(card, listaDeIdCartas[idCarta]);

                card.controller = this;
                listaCartas.Add(card);
                card.transform.localScale = Vector3.zero;
                idCarta++;
            }
        }
    }

    void ConfigurarIdCarta(Card card, int id){
        if (id < 10){
            card.idCarta.text = "0"+id;
        } else{
            card.idCarta.text = id.ToString();
        }
    }

    void MisturarCartas(){
        for (int i = listaCartas.Count-1; i>0; i--){
            int j = Random.Range(0, i+1);

            Card temp = listaCartas[i];
            listaCartas[i] = listaCartas[j];
            listaCartas[j] = temp;
        }

        for (int i = 0; i < listaCartas.Count; i++){
            listaCartas[i].transform.SetSiblingIndex(i);
        }
    }

    public void Seleciona(Card carta){
        if(carta.encontrada) return;
        if(carta.isSelected) return;
        if(scriptZoomCard.IsZoomShow()) return;

        carta.showCard();

        if (primeiraCartaSelecionada == null){
           primeiraCartaSelecionada = carta;
           scriptZoomCard.SetCard1Sprite(carta);
           return; 
        }

        if (segundaCartaSelecionada == null){
           segundaCartaSelecionada = carta;
           scriptZoomCard.SetCard2Sprite(carta);

           ChecarPar(primeiraCartaSelecionada, segundaCartaSelecionada);

           primeiraCartaSelecionada = null;
           segundaCartaSelecionada = null;
        }
    }

    void ChecarPar(Card carta1, Card carta2){
        MostrarFeedback();

        if (carta1.tipo == carta2.tipo){
            ParIncorreto(carta1, carta2);
            return;
        }

        if (carta1.idGroupo == carta2.idGroupo){
            ParCorreto(carta1, carta2);
            scoreManagement.PremiarCasa();
        } else{
            ParIncorreto(carta1, carta2);
        }
    }

    void ParCorreto(Card carta1, Card carta2){
        carta1.encontrada = true;
        carta2.encontrada = true;
        paresEncontrados++;

        fundoFeedback.color = new Color32(50, 220, 100, 255);
        textoFeedback.text = "Par Correto!!";
    }

    void ParIncorreto(Card carta1, Card carta2){
        fundoFeedback.color = new Color32(255, 50, 90,255);
        textoFeedback.text = "Par Incorreto...";

        Tween.Delay(1.5f, () =>
        {
            carta1.hideCard();
            carta2.hideCard();
        });
    }

    void MostrarFeedback(){
        if (feedback.transform.localScale != Vector3.one){   
            Tween.Scale(feedback.transform, Vector3.one, 0.2f).OnComplete(() =>
                Tween.Delay(2f, () => Tween.Scale(feedback.transform, Vector3.zero, 0.2f))
            );
        }
    }

    IEnumerator AnimarCartas()
    {
        EventSystem eventSystem = EventSystem.current;

        eventSystem.enabled = false;

        foreach (Transform carta in gridTransform)
        {
            Tween.Scale(carta, Vector3.one, 0.25f, Ease.OutBack);
            yield return new WaitForSeconds(0.05f);
        }

        gameController.TelaRounds();

        eventSystem.enabled = true;
    }

    public int GetNumberCards(){
        return spritesDefinicao.Length*2;
    }

    public int GetNumberGrups(){
        return spritesConceito.Length;
    }

    public int GetPairsFound(){
        return paresEncontrados;
    }

    void Start(){
        scriptZoomCard = zoomCard.GetComponent<ZoomCards>();
        PrepararCartas();
        StartCoroutine(AnimarCartas());
        gridTransform.gameObject.SetActive(true);
    }  
}