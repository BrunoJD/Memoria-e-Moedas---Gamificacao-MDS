using UnityEngine;
using UnityEngine.UI;
using PrimeTween;
using UnityEngine.InputSystem;

public class ZoomCards : MonoBehaviour
{
    [SerializeField] Image carta1Sprite;
    [SerializeField] Image carta2Sprite;
    [SerializeField] GameController gameController;

    Card carta1;
    Card carta2;

    bool isZoomShow;
    bool isFecharAllowed = false;

    Tween scaleTween;

    public void ShowZoomCard(){
        isZoomShow = true;
        isFecharAllowed = false;

        scaleTween.Stop();

        if (transform.localScale == Vector3.one){
            isFecharAllowed = true;
            return;
        }

        scaleTween = Tween.Scale(transform,Vector3.one,0.2f).OnComplete(() =>{
            isFecharAllowed = true;
        });
    }

    public void HideZoomCard(){
        if (!isZoomShow) return;

        isFecharAllowed = false;
        scaleTween.Stop();

        if (transform.localScale == Vector3.zero){
            isZoomShow = false;
            if (carta2Sprite.sprite != null) DisableZoomCard();

            return;
        }

        scaleTween = Tween.Scale(transform, Vector3.zero, 0.2f).OnComplete(() =>{
            isZoomShow = false;

            if (carta2Sprite.sprite != null) DisableZoomCard();
        });
    }

    void DisableZoomCard(){
        if (!carta1.encontrada || !carta2.encontrada){
            VirarCartas();
        }

        carta1Sprite.sprite = null;
        carta1Sprite.gameObject.SetActive(false);

        carta2Sprite.sprite = null;
        carta2Sprite.gameObject.SetActive(false);
        gameController.TelaRounds();
    }

    public void VirarCartas(){
        carta1.hideCard();
        carta2.hideCard();
    }

    public void SetCard1Sprite(Card carta){
        if (carta1Sprite.sprite == null){
            carta1 = carta;
            carta1Sprite.sprite = carta.spriteEscondido;
            carta1Sprite.gameObject.SetActive(true);

            ShowZoomCard();
        }
    }

    public void SetCard2Sprite(Card carta){
        if (carta2Sprite.sprite == null){
            carta2 = carta;
            carta2Sprite.sprite = carta.spriteEscondido;
            carta2Sprite.gameObject.SetActive(true);

            ShowZoomCard();
        }
    }

    public bool IsZoomShow(){
        return isZoomShow;
    }

    void Update(){
        if (IsZoomShow() && isFecharAllowed && Mouse.current.leftButton.wasPressedThisFrame){
            HideZoomCard();
        }
    }
}