using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Avisos : MonoBehaviour
{
    public GameObject pergunte, passe, errado, contadorPC;

    // Start is called before the first frame update
    void Start()
    {
        pergunte.SetActive(false);
        passe.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // abre o aviso/popup de acordo com o i passado
    public void Entrar(int i)
    {
        switch (i)
        {
            case 0: // jogador deve fazer uma pergunta
                pergunte.SetActive(true);
                Menu.janelaAberta++;
                break;
            case 1: // jogador deve passar a vez
                if (!passe.activeSelf)
                {
                    passe.SetActive(true);
                    Menu.janelaAberta++;
                }
                break;
            case 2: // jogador respondeu errado a pergunta do PC
                errado.SetActive(true);
                break;
            case 3: // popup com o contador do PC
                if (Menu.janelaAberta > 0) return;
                contadorPC.SetActive(true);
                Menu.janelaAberta++;
                break;
        }
    }

    // fecha o aviso/popup
    public void Fechar(int i)
    {
        switch (i)
        {
            case 0:
                pergunte.SetActive(false);
                Menu.janelaAberta--;
                break;
            case 1:
                passe.SetActive(false);
                Menu.janelaAberta--;
                break;
            case 2:
                errado.SetActive(false);
                break;
            case 3:
                contadorPC.SetActive(false);
                Menu.janelaAberta--;
                break;
        }
    }

    // atualiza o contador do PC na tela
    public void atualizaContadorPC (int i)
    {
        contadorPC.transform.GetChild(1).GetComponent<Text>().text = "O PC tem " + i.ToString() + "\ncientistas ativxs.";
    }
}
