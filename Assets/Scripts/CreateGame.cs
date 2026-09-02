using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class CreateGame : MonoBehaviour
{

    [SerializeField] Transform gridTransform;
    [SerializeField] GameObject playerEditPrefab;
    public TextMeshProUGUI contadorText;
    public Button botaoSubir;
    public Button botaoBaixar;
    int contadorPlayers = 0;
    List<GameObject> edits = new List<GameObject>();

    public void SubirContador(){
        contadorPlayers++;
        contadorText.text = contadorPlayers.ToString();
        edits.Add(Instantiate(playerEditPrefab, gridTransform));
    }

    public void BaixarContador(){
        if(contadorPlayers > 0){       
            contadorPlayers--;
            contadorText.text = contadorPlayers.ToString();
            GameObject ultimo = edits[edits.Count-1];
            Destroy(ultimo);
            edits.RemoveAt(edits.Count-1);

        }
    }
}
