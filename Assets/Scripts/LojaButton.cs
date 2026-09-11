using UnityEngine;
using TMPro;

public class LojaButton : MonoBehaviour
{
    public Animator animator;
    public GameController gameController;
    public TMP_Text casaMoedas;

    public void OnExitEvent(){
        animator.SetTrigger("CanExit");
    }

    public void DesableObject(){
        gameObject.SetActive(false);
        int moedas = int.Parse(casaMoedas.text.Replace(" moedas", ""));
        gameController.FinalizarLoja(moedas);
    }
}