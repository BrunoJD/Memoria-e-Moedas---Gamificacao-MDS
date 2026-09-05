using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class CasasData : MonoBehaviour
{

    public static CasasData Instance;
    public static List<Casas> casas = new List<Casas>();

    public static void SetCasas(List<Casas> casasLista){
        casas = casasLista;
    }
    public static List<Casas> GetCasas(){
        return casas;
    }

    private void Awake(){
        if (Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else{
            Destroy(gameObject);
        }
    }
}
