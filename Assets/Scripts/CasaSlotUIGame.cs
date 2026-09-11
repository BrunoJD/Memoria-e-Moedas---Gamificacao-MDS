using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Unity.VisualScripting;


public class CasaSlotUIGame : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image casaSprite;
    [SerializeField] TMP_Text casaNome;
    [SerializeField] TMP_Text casaPontos;
    [SerializeField] TMP_Text casaMoedas;
    GameController gameController;
    Casas casa;

    public void SetSlot(Sprite sprite, string nome, int pontos, int moedas, Casas casa){
        casaSprite.sprite = sprite;
        casaNome.text = nome;
        SetPontos(pontos);
        SetMoedas(moedas);
        this.casa = casa;
    }

    public void SetPontos(int pontos){
        casaPontos.text = $"{pontos} pontos";
    }
    public void SetMoedas(int moedas){
        casaMoedas.text = $"{moedas} moedas";
    }

    public void OnPointerClick(PointerEventData eventData){
        gameController.TelaEdit(casa);
        EditStatsButton botao = GameObject.Find("Tabuleiro/EditStats").GetComponent<EditStatsButton>();
        botao.slotEdit = this;
        botao.casaEdit = casa;
    }

    void Start(){
        gameController = GameObject.Find("Game Controller").GetComponent<GameController>();
    }
}
