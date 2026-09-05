using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CasaSlotUI : MonoBehaviour
{
    [SerializeField] Image casaSprite;
    [SerializeField] TMP_Text casaNome;
    [SerializeField] TMP_Text casaPontos;
    [SerializeField] TMP_Text casaMoedas;

    public void SetSlot(Sprite sprite, string nome, int pontos, int moedas){
        casaSprite.sprite = sprite;
        casaNome.text = nome;
        casaPontos.text = $"{pontos} pontos";
        casaMoedas.text = $"{moedas} moedas";
    }
}
