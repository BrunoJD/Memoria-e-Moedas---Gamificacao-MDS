using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;
using TMPro;
using UnityEngine.UI;


public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] Transform gridTransform;
    List<Casas> casas = new List<Casas>();
    

    public void OnCreateGameEvent(){
        foreach(Transform casa in gridTransform){
            Image icon = casa.Find("Player/NomeCasa/Icon").GetComponent<Image>();
            TMP_Text nomeText = casa.Find("Player/NomeCasa/Name").GetComponent<TMP_Text>();
            TMP_Text moedasText = casa.Find("Player/Moedas/Moedas").GetComponent<TMP_Text>();
            TMP_Text pontosText = casa.Find("Player/Pontos/Pontos").GetComponent<TMP_Text>();

            if (int.TryParse(moedasText.text.Replace(" moedas", ""), out int moedas) && int.TryParse(pontosText.text.Replace(" pontos", ""), out int pontos)){
                casas.Add(new Casas(icon.sprite, nomeText.text, int.Parse(moedasText.text.Replace(" moedas", "")), int.Parse(pontosText.text.Replace(" pontos", ""))));
            } else{
                Debug.LogException(new Exception("Texto informado não é um Número"));
            }
            print(casas[casas.Count-1].GetNomeCasa());
        }
        
        CasasData.SetCasas(casas);
        SceneManager.LoadScene("Jogo");
    }

    public void OnExitEvent(){
        Application.Quit();
    }
}
