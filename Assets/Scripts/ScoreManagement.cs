using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManagement : MonoBehaviour
{
    [SerializeField] GameObject playerSlot;
    [SerializeField] Transform gridTransform;

    private void AtualizarUI(){
        List<Casas> listaDadosPlayers = CasasData.GetCasas();

        foreach (Casas casa in listaDadosPlayers){
            GameObject newSlot = Instantiate(playerSlot, gridTransform);
            CasaSlotUI slotUI = newSlot.GetComponent<CasaSlotUI>();

            if(slotUI != null){
                slotUI.SetSlot(casa.GetCasaSprite(), casa.GetNomeCasa(), casa.GetPontos(), casa.GetMoedas());
            }
        }
    }

    void Start()
    {
        AtualizarUI();
    }
}
