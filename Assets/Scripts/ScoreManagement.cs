using System.Collections.Generic;
using UnityEngine;

public class ScoreManagement : MonoBehaviour
{
    [SerializeField] GameObject playerSlot;
    
    [SerializeField] Transform gridTransform;
    List<CasaSlotUIGame> listaSlots = new List<CasaSlotUIGame>();
    List<Casas> listaDadosPlayers;
    Casas casaAtual;
    public int rodadaAtual;
    public int turnoAtual;
    public int PontosGanhos;
    public int MoedasGanhas;

    private void AtualizarUI(){
        listaDadosPlayers = CasasData.GetCasas();

        foreach (Casas casa in listaDadosPlayers){
            GameObject newSlot = Instantiate(playerSlot, gridTransform);
            CasaSlotUIGame slotUI = newSlot.GetComponent<CasaSlotUIGame>();

            if(slotUI != null){
                slotUI.SetSlot(casa.GetCasaSprite(), casa.GetNomeCasa(), casa.GetPontos(), casa.GetMoedas(), casa);

                listaSlots.Add(slotUI);
            }
        }
    }

    public Casas TurnoAtual(){        
        casaAtual = listaDadosPlayers[turnoAtual];       
        return casaAtual;
    }

    public void MudarTruno(){
        turnoAtual++;
        if (turnoAtual > listaDadosPlayers.Count-1){
            turnoAtual = 0;
            MudarRound();
        }
    }

    public int MudarRound(){
        return rodadaAtual++;
    }

    public int GetRodadaAtual(){
        return rodadaAtual;
    }

    public void PremiarCasa(){
        int moedas = casaAtual.GetMoedas()+MoedasGanhas;
        int pontos = casaAtual.GetPontos()+PontosGanhos;

        casaAtual.SetNumeroMoedas(moedas);
        casaAtual.SetNumeroPontos(pontos);

        CasaSlotUIGame slotAtual = listaSlots[turnoAtual];
        slotAtual.SetSlot(casaAtual.GetCasaSprite(), casaAtual.GetNomeCasa(), casaAtual.GetPontos(), casaAtual.GetMoedas(), casaAtual);
    }

    public void AtualizarLoja(int moedas){
        CasaSlotUIGame slotAtual = listaSlots[turnoAtual];
        slotAtual.SetMoedas(moedas);
    }

    void Start()
    {
        rodadaAtual = 1;
        MoedasGanhas = 1;
        PontosGanhos = 100;
        AtualizarUI();
    }
}
