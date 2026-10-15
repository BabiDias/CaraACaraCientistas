using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public GameObject[] cards;
    public GameObject cardJogador;
    public GameObject fimScript;
    public GameObject avisosScript;
    public Sprite[] coresCards;
    public Sprite[] coresNomes;
    int erros;
    public static bool palpite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    // começa tudo do zero cada vez que o jogador inicia um novo jogo
    public void IniciarJogo()
    {
        erros = 0;
        palpite = false;

        if (ColorUtility.TryParseHtmlString("#FFFFFF", out Color white)) { }

        // escreve o nome e a área no card do jogador
        cardJogador.transform.GetChild(3).GetComponentInChildren<Text>().text = Cientistas.cientistaJogador.nome;
        cardJogador.transform.GetChild(2).GetComponentInChildren<Text>().text = Cientistas.cientistaJogador.area;

        cardJogador.transform.GetChild(4).GetComponent<Image>().sprite = Cientistas.cientistaJogador.imagem; // imagem dx cientista

        if (Cientistas.cientistaJogador.nacionalidade == "bra")
            cardJogador.transform.GetChild(5).gameObject.SetActive(true); // bandeira do Brasil


        for (int i = 0; i < cards.Length; i++)
        {
            // escreve o nome e a área nos cards da rodada
            cards[i].transform.GetChild(3).GetComponentInChildren<Text>().text = Cientistas.cientistasRodada[i].nome;
            cards[i].transform.GetChild(2).GetComponentInChildren<Text>().text = Cientistas.cientistasRodada[i].area;

            // limpa qualquer modificação de cor dos cards da rodada anterior
            Image[] imagesCards = cards[i].GetComponentsInChildren<Image>();
            foreach (Image j in imagesCards) // buttons background
            {
                j.color = white;
            }
            cards[i].GetComponent<Image>().sprite = coresCards[0]; // card default (amarelo)
            cards[i].transform.GetChild(3).GetComponent<Image>().sprite = coresNomes[0]; // banner nome default (amarelo)
            cards[i].transform.GetChild(4).GetComponent<Image>().sprite = Cientistas.cientistasRodada[i].imagem; // imagem dx cientista

            if (Cientistas.cientistasRodada[i].nacionalidade == "bra")
                cards[i].transform.GetChild(5).gameObject.SetActive(true); // bandeira do Brasil
        }
    }

    // descarta x cientista da rodada
    public void Descartar(GameObject card)
    {

        print("Janela aberta = " + Menu.janelaAberta); // DEBUG
        if (Menu.janelaAberta > 0) return; // se tem alguma janela aberta, nao faz nada

        if (ColorUtility.TryParseHtmlString("#74726B", out Color grayed)) { }

        if (card.transform.GetChild(3).GetComponentInChildren<Text>().text == Cientistas.cientistaPC.nome) // o jogador descartou a opção correta
        {
            fimScript.GetComponent<Fim>().fimPerdeu(1); // abre tela de "perdeu"
            print("PC ganhou!"); //DEBUG
            return;
        }

        if (card.GetComponent<Image>().color == grayed || card.GetComponent<Image>().sprite == coresCards[1]) // se já foi descartado, nao faz nada
            return;

        if (palpite) // o jogador já fez um palpite e deve passar a vez
        {
            avisosScript.GetComponent<Avisos>().Entrar(1);
            return;
        }

        // deixa o card descartado com a cor acinzentada
        card.GetComponent<Image>().color = grayed; // card background

        Image[] images = card.GetComponentsInChildren<Image>();
        foreach (Image i in images) // buttons background
        {
            i.color = grayed;
        }

    }

    // seleciona x cientista (palpite do jogador)
    public void Selecionar(GameObject card)
    {
        print("Janela aberta = " + Menu.janelaAberta); // DEBUG
        if (Menu.janelaAberta > 0) return; // se tem alguma janela aberta, nao faz nada

        if (ColorUtility.TryParseHtmlString("#74726B", out Color grayed)) { }

        if (card.GetComponent<Image>().color == grayed || card.GetComponent<Image>().sprite == coresCards[1]) // se já foi descartado, nao faz nada
        {
            print("entrou no return"); // DEBUG
            return;
        }
        
        if (palpite) // o jogador já vez um palpite e deve passar a vez
        {
            avisosScript.GetComponent<Avisos>().Entrar(1);
            return;
        }

        if (card.transform.GetChild(3).GetComponentInChildren<Text>().text == Cientistas.cientistaPC.nome) // opção correta
        {
            // ganha o jogo
            fimScript.GetComponent<Fim>().fimGanhou(); // abre tela de "ganhou"
            print("Jogador ganhou!"); //DEBUG
        }

        else // opção incorreta
        {
            erros++; // incrementa o numero de palpites incorretos do jogador

            Image[] images = card.GetComponentsInChildren<Image>();
            foreach (Image i in images) // buttons background
            {
                i.color = grayed;
            }

            card.GetComponent<Image>().sprite = coresCards[1]; // card vermelho
            card.transform.GetChild(3).GetComponent<Image>().sprite = coresNomes[1]; // banner nome vermelho

            PC.vezDoJogador = false; // jogador "perde" a vez
            print("vez do jogador: " + PC.vezDoJogador); // DEBUG

            palpite = true;

            // se o jogador fizer 3 palpites incorretos, perde o jogo
            if (erros == 3)
            {
                fimScript.GetComponent<Fim>().fimPerdeu(3); // abre tela de "perdeu"
                print("PC ganhou!"); //DEBUG
            }
        }
    }

}
