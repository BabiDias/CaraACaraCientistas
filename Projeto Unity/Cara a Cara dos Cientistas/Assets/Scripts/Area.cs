using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Area : MonoBehaviour
{
    public GameObject area;
    public GameObject botaoLGG;
    public GameObject botaoMat;
    public GameObject botaoCN;
    public GameObject botaoCHS;
    public GameObject avisosScript;
    public GameObject botaoMenu;
    public Sprite[] botoesColoridos;
    GameObject escolhido;
    bool jogadorDescobriu;
    string textoMenu;
    List<GameObject> opcoes = new List<GameObject>();
    private int count;

    // Start is called before the first frame update
    void Start()
    {
        area.SetActive(false);
        opcoes.Add(botaoLGG);
        opcoes.Add(botaoMat);
        opcoes.Add(botaoCN);
        opcoes.Add(botaoCHS);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // começa tudo do zero cada vez que o jogador inicia um novo jogo
    public void IniciarJogo()
    {
        count = 0;
        jogadorDescobriu = false;

        // limpa qualquer modificação nos botoes feita da rodada anterior
        textoMenu = "ÁREA";
        botaoMenu.transform.GetChild(0).GetComponent<Text>().text = textoMenu;
        botaoMenu.GetComponent<Image>().sprite = botoesColoridos[0]; // botao default (azul)
        botaoLGG.GetComponent<Image>().sprite = botoesColoridos[1]; // botao default (amarelo)
        botaoMat.GetComponent<Image>().sprite = botoesColoridos[1]; // botao default (amarelo)
        botaoCN.GetComponent<Image>().sprite = botoesColoridos[1]; // botao default (amarelo)
        botaoCHS.GetComponent<Image>().sprite = botoesColoridos[1]; // botao default (amarelo)
    }

    // abre a janela da Area para o jogador fazer uma "pergunta"
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
            area.SetActive(true);
            Menu.janelaAberta++;
        }

    }

    // jogador seleciona uma das opções
    public void Escolher(GameObject escolhido)
    {
        // se o jogador ja descobriu a opcao correta, nao faca nada
        if (jogadorDescobriu)
            return;

        if (escolhido.GetComponent<Image>().sprite != botoesColoridos[1]) // essa opção já foi testada, não faça nada
            return;

        GameObject opcaoCorreta;

        if (!PC.vezDoJogador)
        {
            // abre dialogo de "voce deve passar a vez" (jogador tentou clicar em mais de uma opçao)
            avisosScript.GetComponent<Avisos>().Entrar(1);
            return;
        }

        // jogador "perde a vez" depois de fazer um palpite
        PC.vezDoJogador = false;
        print("vez do jogador: " + PC.vezDoJogador); // DEBUG

        // verifica qual das opçoes esta correta
        if (Cientistas.cientistaPC.area == "LGG")
        {
            opcaoCorreta = botaoLGG;
            textoMenu = "Linguagens";
        }
        else if (Cientistas.cientistaPC.area == "MAT")
        {
            opcaoCorreta = botaoMat;
            textoMenu = "Matemática";
        }
        else if (Cientistas.cientistaPC.area == "CN")
        {
            opcaoCorreta = botaoCN;
            textoMenu = "Ciências Naturais";
        }
        else
        {
            opcaoCorreta = botaoCHS;
            textoMenu = "Ciências Humanas";
        }

        //print("botao certo: " + Cientistas.cientistaPC.area); // DEBUG
       
        if (escolhido != opcaoCorreta) // a opção escolhida está incorreta
        {
            escolhido.GetComponent<Image>().sprite = botoesColoridos[3]; // pinta o botao de vermelho

            count++;
            if (count == 3) // se só restar 1 opção, ela é a correta
            {
                jogadorDescobriu = true;
                opcaoCorreta.GetComponent<Image>().sprite = botoesColoridos[2]; // pinta o botao de verde

                // altera o texto do botao do menu pra opcao correta e pinta de verde
                botaoMenu.transform.GetChild(0).GetComponent<Text>().text = textoMenu;
                botaoMenu.GetComponent<Image>().sprite = botoesColoridos[2]; // botao verde
            }
        }

        else // a opção escolhida está correta
        {
            jogadorDescobriu = true;
            opcaoCorreta.GetComponent<Image>().sprite = botoesColoridos[2]; // pinta o botao de verde

            for (int i=0; i<4; i++) // todas as outras opções estão erradas e podem ser descartadas
            {
                if (opcoes[i] != escolhido)
                    opcoes[i].GetComponent<Image>().sprite = botoesColoridos[3]; // pinta o botao de vermelho
            }

            // altera o texto do botao do menu pra opcao correta e pinta de verde
            botaoMenu.transform.GetChild(0).GetComponent<Text>().text = textoMenu;
            botaoMenu.GetComponent<Image>().sprite = botoesColoridos[2]; // botao verde
        }

    }

    // fecha a janela da Area
    public void Fechar()
    {
        area.SetActive(false);
        Menu.janelaAberta--;
    }

}
