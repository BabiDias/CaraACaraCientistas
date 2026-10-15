using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cor : MonoBehaviour
{
    public GameObject cor;
    public GameObject botaoBrancx;
    public GameObject botaoPretx;
    public GameObject avisosScript;
    public GameObject botaoMenu;
    public Sprite[] botoesColoridos;
    GameObject escolhido;
    bool jogadorDescobriu;
    string textoMenu;

    // Start is called before the first frame update
    void Start()
    {
        cor.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // começa tudo do zero cada vez que o jogador inicia um novo jogo
    public void IniciarJogo()
    {
        jogadorDescobriu = false;

        // limpa qualquer modificação nos botoes feita da rodada anterior
        textoMenu = "COR";
        botaoMenu.transform.GetChild(0).GetComponent<Text>().text = textoMenu;
        botaoMenu.GetComponent<Image>().sprite = botoesColoridos[0]; // botao default (azul)
        botaoPretx.GetComponent<Image>().sprite = botoesColoridos[1]; // botao default (amarelo)
        botaoBrancx.GetComponent<Image>().sprite = botoesColoridos[1]; // botao default (amarelo)
    }

    // abre a janela da Cor para o jogador fazer uma "pergunta"
    public void Entrar()
    {
        if (!PC.vezDoJogador)
        {
            // abre dialogo de "voce deve passar a vez"
            avisosScript.GetComponent<Avisos>().Entrar(1);
            return;
        }
        else
        {
            cor.SetActive(true);
            Menu.janelaAberta++;
        }

    }

    // jogador seleciona uma das opções
    public void Escolher(GameObject escolhido)
    {
        // se o jogador ja descobriu a opcao correta, nao faca nada
        if (jogadorDescobriu)
            return;

        // só tem 2 opcoes, entao o jogador ja descobriu qual a correta independentemente do palpite
        jogadorDescobriu = true;

        GameObject opcaoCorreta, opcaoErrada;

        // jogador "perde a vez" depois de fazer um palpite
        PC.vezDoJogador = false;
        print("vez do jogador: " + PC.vezDoJogador); // DEBUG

        // verifica qual das opçoes esta correta
        if (Cientistas.cientistaPC.cor == "brancx")
        {
            opcaoCorreta = botaoBrancx;
            opcaoErrada = botaoPretx;
            textoMenu = "Brancx";
        }
        else
        {
            opcaoCorreta = botaoPretx;
            opcaoErrada = botaoBrancx;
            textoMenu = "Pretx";
        }

        // pinta os botoes das opçoes
        opcaoCorreta.GetComponent<Image>().sprite = botoesColoridos[2]; // botao verde
        opcaoErrada.GetComponent<Image>().sprite = botoesColoridos[3]; // botao vermelho

        // altera o texto do botao do menu pra opcao correta e pinta de verde
        botaoMenu.transform.GetChild(0).GetComponent<Text>().text = textoMenu;
        botaoMenu.GetComponent<Image>().sprite = botoesColoridos[2]; // botao verde
    }

    // fecha a janela da Cor
    public void Fechar()
    {
        cor.SetActive(false);
        Menu.janelaAberta--;
    }
}
