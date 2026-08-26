using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using TMPro;
using PrimeTween;
using Unity.VisualScripting;

public class CardsController : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] Card cardPrefab;
    [SerializeField] Transform gridTransform;
    [SerializeField] Sprite[] spritesConceito;
    [SerializeField] Sprite[] spritesDefinicao;

    List<int[]> listaDeIdConceito = new List<int[]>();
    List<int> listaDeIdDefinicao = new List<int>();
    List<int> listaDeIdCartas = new List<int>();

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
        for (int i = 0; i < GetNumberCards(); i += 5){
            for (int j = 0; j < 5; j++){
                listaDeIdConceito.Add(new int[] {i, i+1, i+2, i+3, i+4});
            }
        }

        for (int i = 0; i < spritesDefinicao.Length; i++){
            listaDeIdDefinicao.Add(i);
        }

        for (int i = 0; i < GetNumberCards(); i++){
            listaDeIdCartas.Add(i+1);
        }

        MisturarSpriteConceito(spritesConceito, listaDeIdConceito);
        MisturarSpriteDefinicao(spritesDefinicao, listaDeIdDefinicao);
        MisturarIdCartas(listaDeIdCartas);
        CriarCartas();
    }

    void MisturarSpriteConceito(Sprite[] listaDePares, List <int[]> listaDeIdPares){
        for (int i = listaDePares.Length - 1; i>0; i--){
            int j = Random.Range(0, i+1);

            Sprite temp = listaDePares[i];
            int[] tempId = listaDeIdPares[i];

            listaDePares[i] = listaDePares[j];
            listaDeIdPares[i] = listaDeIdPares[j];

            listaDePares[j] = temp;
            listaDeIdPares[j] = tempId;
        }
    }

    void MisturarSpriteDefinicao(Sprite[] listaDePares, List <int> listaDeIdPares){
        for (int i = listaDePares.Length - 1; i>0; i--){
            int j = Random.Range(0, i+1);

            Sprite temp = listaDePares[i];
            int tempId = listaDeIdPares[i];

            listaDePares[i] = listaDePares[j];
            listaDeIdPares[i] = listaDeIdPares[j];

            listaDePares[j] = temp;
            listaDeIdPares[j] = tempId;
        }
    }

    void MisturarIdCartas(List <int> listaDeIdCartas){
        for (int i = GetNumberCards() - 1; i>0; i--){
            int j = Random.Range(0, i+1);

            int tempId = listaDeIdCartas[i];
            listaDeIdCartas[i] = listaDeIdCartas[j];
            listaDeIdCartas[j] = tempId;
        }
    }

    void CriarCartas(){
        for(int i = 0; i < spritesConceito.Length; i++){
            Card card = Instantiate(cardPrefab, gridTransform);
            card.setSpriteEscondido(spritesConceito[i]);
            card.conceitoId = listaDeIdConceito[i];
            card.tipo = 0;

            if(listaDeIdCartas[i] < 10){
                card.idCarta.text = "0"+listaDeIdCartas[i].ToString();
            }
            else{
                card.idCarta.text = listaDeIdCartas[i].ToString();
            }

            card.controller = this;          
        }

        for(int i = 0; i < spritesDefinicao.Length; i++){

            Card card = Instantiate(cardPrefab, gridTransform);
            card.setSpriteEscondido(spritesDefinicao[i]);
            card.definicaoId = listaDeIdDefinicao[i];
            card.tipo = 1;

            if(listaDeIdCartas[i+25] < 10){
                card.idCarta.text = "0"+listaDeIdCartas[i+25].ToString();
            }
            else{
                card.idCarta.text = listaDeIdCartas[i+25].ToString();
            }

            card.controller = this;
        }
    }

    public void Seleciona(Card carta){
        if(carta.isSelected == false && !scriptZoomCard.IsZoomShow()){
        
            print(carta.definicaoId);
            carta.showCard();

            if(primeiraCartaSelecionada == null){
                primeiraCartaSelecionada = carta;
                scriptZoomCard.SetCard1Sprite(carta);

                return;
            }

            if(segundaCartaSelecionada == null){
                segundaCartaSelecionada = carta;
                scriptZoomCard.SetCard2Sprite(carta);

                ChecarPar(primeiraCartaSelecionada, segundaCartaSelecionada);
                primeiraCartaSelecionada = null;
                segundaCartaSelecionada = null;
            }
        }
    }

    void ChecarPar(Card carta1, Card carta2){
        Tween.Scale(feedback.transform, Vector3.one, 0.2f).OnComplete(() =>
            Tween.Delay(2f, () => Tween.Scale(feedback.transform, Vector3.zero, 0.2f))
        );

        if (carta1.tipo != carta2.tipo){
            Card cartaConceito;
            Card cartaDefinicao;

            if (carta1.tipo == 0){
                cartaConceito = carta1;
                cartaDefinicao = carta2;
            }else{
                cartaConceito = carta2;
                cartaDefinicao = carta1;
            }

            for (int i = 0; i < cartaConceito.conceitoId.Length; i++){
                if (cartaConceito.conceitoId[i] == cartaDefinicao.definicaoId){
                    carta1.encontrada = true;
                    carta2.encontrada = true;
                    paresEncontrados++;
                    
                    fundoFeedback.color = new Color32(50, 220, 100, 255);
                    textoFeedback.text = "Par Correto!!";

                    return;
                }
            }             
        }

        fundoFeedback.color = new Color32(255, 50, 90, 255);
        textoFeedback.text = "Par Incorreto...";
    }

    public int GetNumberCards(){
        return spritesDefinicao.Length*2;
    }

    public int GetPairsFound(){
        return paresEncontrados;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PrepararCartas();
        gridTransform.gameObject.SetActive(true);
        scriptZoomCard = zoomCard.GetComponent<ZoomCards>();
    }    

    // Update is called once per frame
    void Update()
    {
        
    }
}
