using TMPro;
using UnityEngine;

public class MoedasButton : MonoBehaviour
{
    [SerializeField] TMP_Text moedasText;
    public void OnAddEvent(){
        int moedas = int.Parse(moedasText.text.Replace(" moedas", ""));
        moedas++;
        moedasText.text = moedas + " moedas";
    }

    public void OnRemoveEvent(){
        int moedas = int.Parse(moedasText.text.Replace(" moedas", ""));
        if (moedas > 0){ 
            moedas--;
            moedasText.text = moedas + " moedas";
        }
    }
}
