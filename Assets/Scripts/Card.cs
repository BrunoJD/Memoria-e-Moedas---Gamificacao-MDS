using UnityEngine;
using UnityEngine.UI;
using TMPro;
using PrimeTween;
using System.Reflection.Metadata.Ecma335;

public class Card : MonoBehaviour
{

    [SerializeField] Image imagemPrincipal;
    [SerializeField] public TextMeshProUGUI idCarta;

    public Sprite spriteVerso;
    public Sprite spriteEscondido;
    public bool encontrada = false;
    public bool isSelected;

    public int[] conceitoId = null;
    public int definicaoId = -1;
    public int tipo;
    public CardsController controller;

    public void OnCardClick()
    {
        controller.Seleciona(this);
    }

    public void setSpriteEscondido(Sprite cardSprite){
        spriteEscondido = cardSprite;
    }

    public void showCard(){
        Tween.Rotation(transform, new Vector3 (0f, -180f, 0f), 0.5f);

        Tween.Delay(0.15f, () => {
            imagemPrincipal.sprite = spriteEscondido; 
            transform.localScale = new Vector3(-1, 1, 1); 
            idCarta.gameObject.SetActive(false); 
            isSelected = true;
        });
    }

    public void hideCard(){
        Tween.Rotation(transform, new Vector3 (0f, 0f, 0f), 0.5f);

        Tween.Delay(0.15f, () => {
            imagemPrincipal.sprite = spriteVerso;
            transform.localScale = new Vector3(1, 1, 1);
            idCarta.gameObject.SetActive(true);
            isSelected = false;
        });
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
