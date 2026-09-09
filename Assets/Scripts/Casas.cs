using UnityEngine;
using System.Collections.Generic;

public class Casas{
    Sprite iconCasa;
    string nomeCasa;
    int numeroMoedas;
    int numeroPontos;
    //public List<ActionCards> inventario = new List<ActionCards>();

    public Casas(Sprite iconCasa, string nomeCasa, int numeroMoedas, int numeroPontos){
        SetIconCasa(iconCasa);
        SetNomeCasa(nomeCasa);
        SetNumeroMoedas(numeroMoedas);
        SetNumeroPontos(numeroPontos);
    }

    public void SetIconCasa(Sprite iconCasa){
        this.iconCasa = iconCasa;
    }

    public void SetNomeCasa(string nomeCasa){
        this.nomeCasa = nomeCasa;
    }

    public void SetNumeroMoedas(int numeroMoedas){
        this.numeroMoedas = numeroMoedas;
    }

    public void SetNumeroPontos(int numeroPontos){
        this.numeroPontos = numeroPontos;
    }

    /* public void SetActionCard(ActionCards card){
        inventario.Add(card);
    } */

    public Sprite GetCasaSprite(){
        return iconCasa;
    }

    public string GetNomeCasa(){
        return nomeCasa;
    }

    public int GetMoedas(){
        return numeroMoedas;
    }

    public int GetPontos(){
        return numeroPontos;
    }

    /* public List<ActionCards> GetActionCards(){
        return inventario;
    } */
}
