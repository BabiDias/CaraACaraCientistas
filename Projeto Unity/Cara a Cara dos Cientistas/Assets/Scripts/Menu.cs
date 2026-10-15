using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Menu : MonoBehaviour
{
    public GameObject[] scripts;
    public GameObject[] janelas;
    public static int janelaAberta;

    // Start is called before the first frame update
    void Start()
    {
        janelaAberta = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // abre a janela da caracteristica escolhida, se nao tiver nenhuma outra janela ativa
    public void Abrir(int item)
    {
        print("Janela aberta = " + Menu.janelaAberta); // DEBUG

        switch (item)
        {
            case 0:
                if (janelaAberta == 0) scripts[0].GetComponent<Nacionalidade>().Entrar();
                break;
            case 1:
                if (janelaAberta == 0) scripts[1].GetComponent<Sexo>().Entrar();
                break;
            case 2:
                if (janelaAberta == 0) scripts[2].GetComponent<Cor>().Entrar();
                break;
            case 3:
                if (janelaAberta == 0) scripts[3].GetComponent<Area>().Entrar();
                break;
            case 4:
                if (janelaAberta == 0) scripts[4].GetComponent<PC>().Entrar();
                break;
        }

    }
}
