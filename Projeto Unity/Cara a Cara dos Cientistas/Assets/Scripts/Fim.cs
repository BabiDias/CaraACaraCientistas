using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

// controla a tela final do jogo
public class Fim : MonoBehaviour
{
    public GameObject fim, ganhou, perdeu;

    // Start is called before the first frame update
    void Start()
    {
        fim.SetActive(false); // desativa a tela final
        ganhou.SetActive(false); // desativa a mensagem de "ganhou"
        perdeu.SetActive(false); // desativa a mensagem de "perdeu"
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void fimGanhou()
    {
        if (ColorUtility.TryParseHtmlString("#648E61", out Color greenBG))
            fim.transform.GetChild(0).GetComponent<SpriteRenderer>().color = greenBG; // background verde

        fim.SetActive(true); // ativa a tela final
        ganhou.SetActive(true); // ativa a mensagem de "ganhou"
    }

    public void fimPerdeu(int motivo)
    {
        if (ColorUtility.TryParseHtmlString("#A44C4C", out Color redBG))
            fim.transform.GetChild(0).GetComponent<SpriteRenderer>().color = redBG; // background vermelho

        fim.SetActive(true); // ativa a tela final
        perdeu.SetActive(true); // ativa a mensagem de "perdeu"
        print("O palpite do PC é " + PC.palpitePC); // DEBUG

        // escolhe a mensagem de "perdeu" de acordo com o motivo da perda
        if (motivo == 1) // jogador descartou o cientista correto
            perdeu.transform.GetChild(2).GetComponent<Text>().text = "Motivo: descartou o/a cientista corretx ("+ Cientistas.cientistaPC.nome + ")";
        else if (motivo == 2) // PC acertou o cientista primeiro
            perdeu.transform.GetChild(2).GetComponent<Text>().text = "Motivo: o PC acertou o seu cientista (" + PC.palpitePC + ")";
        else // o jogador ja fez 3 palpites incorretos
            perdeu.transform.GetChild(2).GetComponent<Text>().text = "Motivo: 3 palpites incorretos. O/A cientista corretx era " + Cientistas.cientistaPC.nome;
    }

    // volta para o menu inicial apos o fim do jogo.
    public void VoltarMenu()
    {
        fim.SetActive(false); // desativa a tela final
        ganhou.SetActive(false); // desativa a mensagem de "ganhou"
        perdeu.SetActive(false); // desativa a mensagem de "perdeu"
    }
}
