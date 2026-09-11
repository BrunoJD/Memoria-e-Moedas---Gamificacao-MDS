using TMPro;
using UnityEngine;

public class PontosButton : MonoBehaviour
{
    [SerializeField] TMP_Text pontosText;
    public void OnAddEvent(){
        int pontos = int.Parse(pontosText.text.Replace(" pontos", ""));
        pontos = pontos + 100;
        pontosText.text = pontos + " pontos";
    }

    public void OnRemoveEvent(){
        int pontos = int.Parse(pontosText.text.Replace(" pontos", ""));
        if (pontos > 0){ 
            pontos = pontos - 100;
            pontosText.text = pontos + " pontos";
        }
    }
}
