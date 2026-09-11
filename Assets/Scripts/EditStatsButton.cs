using UnityEngine;
using TMPro;

public class EditStatsButton : MonoBehaviour
{
    public Animator animator;
    public GameController gameController;
    public TMP_Text casaMoedas;
    public TMP_Text casaPontos;
    public CasaSlotUIGame slotEdit;
    [System.NonSerialized] public Casas casaEdit;

    public void OnExitEvent(){
        animator.SetTrigger("CanExit");
    }

    public void DesableObject(){
        gameObject.SetActive(false);
        int moedas = int.Parse(casaMoedas.text.Replace(" moedas", ""));
        int pontos = int.Parse(casaPontos.text.Replace(" pontos", ""));
        gameController.ConfirmarEdicao(moedas, pontos, slotEdit, casaEdit);

        print(moedas + " "+ pontos);
    }
}