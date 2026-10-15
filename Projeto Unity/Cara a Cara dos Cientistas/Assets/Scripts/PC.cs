using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PC : MonoBehaviour
{
    public GameObject pergunta, contador;
    public GameObject avisosScript, fimScript;
    public static bool vezDoJogador;
    List<Cientista> listaPC, aRemover;
    public static string palpitePC;
    string[] perguntas;
    int perguntaAtual;
    List<int> perguntasFeitas;
    bool respondeuErrado;

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

        // copia os cientistas da rodada para uma lista do PC
        listaPC = new List<Cientista>();
        for (int i = 0; i < Cientistas.cientistasRodada.Length; i++)
        {
            listaPC.Add(Cientistas.cientistasRodada[i]);
            //print(Cientistas.cientistasRodada[i]); // DEBUG
        }

        contador.GetComponentInChildren<Text>().text = listaPC.Count.ToString(); // inicializa o contador do PC
        avisosScript.GetComponent<Avisos>().atualizaContadorPC(listaPC.Count); // inicializa o texto do diálogo do contador

        perguntas = new string[7];
        perguntas[0] = "Seu(sua) cientista é brasileirx?";
        perguntas[1] = "Seu(sua) cientista é homem?";
        perguntas[2] = "Seu(sua) cientista é brancx?";
        perguntas[3] = "Seu(sua) cientista é da área de linguagens?";
        perguntas[4] = "Seu(sua) cientista é da área de matemática?";
        perguntas[5] = "Seu(sua) cientista é da área de ciências naturais?";
        perguntas[6] = "Seu(sua) cientista é da área de ciências humanas?";
        //print("Perguntas PC: " + perguntas.Length); // DEBUG

        perguntasFeitas = new List<int>();
        aRemover = new List<Cientista>();

        pergunta.SetActive(false);
        vezDoJogador = true;
        respondeuErrado = false;
        print("vez do jogador: " + vezDoJogador); // DEBUG
    }

    // inicia a jogada do PC
    public void Entrar()
    {
        if (vezDoJogador) // jogador tentou passar a vez sem fazer nenhuma pergunta ou palpite
        {
            avisosScript.GetComponent<Avisos>().Entrar(0); // abre dialogo de "voce tem que fazer uma pergunta"
            return;
        }

        //pergunta = perguntaAleatoria();
        //print(perguntaAleatoria());

        pergunta.GetComponentInChildren<Text>().text = perguntaAleatoria(); // seleciona uma pergunta aleatoria para o PC fazer ao jogador

        pergunta.SetActive(true);
        Menu.janelaAberta++;
        print("Pergunta do PC"); // DEBUG
    }

    // ativada quando o jogador responde a pergunta do PC (sim=0 e nao=1)
    public void Responde(int i)
    {
        respondeuErrado = false; // no primeiro momento, assume que o jogador respondeu corretamente

        // escolhe a funçao adequada de acordo com a pergunta feita para remover os cientistas eliminados da lista do PC
        switch (perguntaAtual)
        {
            case 0:
                removePorNacionalidade(i);
                break;
            case 1:
                removePorSexo(i);
                break;
            case 2:
                removePorCor(i);
                break;
            case 3:
                removePorArea(i);
                break;
            case 4:
                removePorArea(i);
                break;
            case 5:
                removePorArea(i);
                break;
            case 6:
                removePorArea(i);
                break;
        }

        // se o jogador respondeu errado, retorna para responder novamente
        if (respondeuErrado)
            return;

        // atualiza o contador do PC na tela
        contador.GetComponentInChildren<Text>().text = listaPC.Count.ToString(); // atualiza o contador do PC
        avisosScript.GetComponent<Avisos>().atualizaContadorPC(listaPC.Count); // atualiza o texto do diálogo do contador

        // fecha a janela da pergunta
        pergunta.SetActive(false);
        Menu.janelaAberta--;

        if (listaPC.Count == 1) // só resta 1 cientista, PC acertou
        {
            // PC ganhou!
            palpitePC = listaPC[0].nome;
            fimScript.GetComponent<Fim>().fimPerdeu(2); // abre tela de "perdeu"
            print("Seu cientista é " + palpitePC); // DEBUG
        }
        print("Contador do PC: " + listaPC.Count); // DEBUG

        // finaliza a jogada do PC
        vezDoJogador = true;
        Card.palpite = false;
        print("vez do jogador: " + vezDoJogador); // DEBUG

    }

    // seleciona uma pergunta aleatoria dentre as possiveis
    string perguntaAleatoria()
    {
        perguntaAtual = Random.Range(0, 7);

        // se a pergunta ja foi feita, escolhe outra
        while (perguntasFeitas.Contains(perguntaAtual) || perguntaAtual == 7)
        {
            perguntaAtual = Random.Range(0, 7);
        }

        // add a pergunta escolhida na lista de perguntas feitas
        perguntasFeitas.Add(perguntaAtual);

        return perguntas[perguntaAtual];
    }

    // remove xs cientistas descartados da lista do PC por Nacionalidade (sim=0 e nao=1)
    void removePorNacionalidade(int i)
    {

        if (i == 0) // resposta: SIM, é brasileirx
        {
            if (Cientistas.cientistaJogador.nacionalidade == "nbra") // x cientista nao é brasileirx
            {
                avisosScript.GetComponent<Avisos>().Entrar(2); // popup "deve responder certo"
                print("Você não deve responder errado!"); // DEBUG
                respondeuErrado = true; // volta pro jogador responder corretamente
                return;
            }

            // se x cientista realmente for brasileirx, copia todos xs nao brasileirxs da lista do PC para outra lista
            foreach (Cientista c in listaPC)
            {
                if (c.nacionalidade == "nbra")
                    aRemover.Add(c);
            }

            // remove da lista do PC todos xs cientistas copiados no foreach anterior
            foreach (Cientista r in aRemover)
            {
                listaPC.Remove(r);
                //print("Removido: " + r.nome); // DEBUG
            }
        }
        else // resposta: NAO é brasileiro
        {
            if (Cientistas.cientistaJogador.nacionalidade == "bra") // x cientista é brasileirx
            {
                avisosScript.GetComponent<Avisos>().Entrar(2); // popup "deve responder certo"
                print("Você não deve responder errado!"); // DEBUG
                respondeuErrado = true; // volta pro jogador responder corretamente
                return;
            }

            // se x cientista realmente nao for brasileirx, copia todos xs brasileirxs da lista do PC para outra lista
            foreach (Cientista c in listaPC)
            {
                if (c.nacionalidade == "bra")
                    aRemover.Add(c);
            }

            // remove da lista do PC todos xs cientistas copiados no foreach anterior
            foreach (Cientista r in aRemover)
            {
                listaPC.Remove(r);
                //print("Removido: " + r.nome); // DEBUG
            }
        }

        // limpa a lista de cientistas a serem removidos para a prox rodada
        aRemover.Clear();
    }

    // remove xs cientistas descartados da lista do PC por Sexo (sim=0 e nao=1)
    void removePorSexo(int i)
    {
        if (i == 0) // resposta: SIM, é homem
        {
            if (Cientistas.cientistaJogador.sexo == "mulher") // a cientista é mulher
            {
                avisosScript.GetComponent<Avisos>().Entrar(2); // popup "deve responder certo"
                print("Você não deve responder errado!"); // DEBUG
                respondeuErrado = true; // volta pro jogador responder corretamente
                return;
            }

            // se o cientista realmente for homem, copia todos as mulheres da lista do PC para outra lista
            foreach (Cientista c in listaPC)
            {
                if (c.sexo == "mulher")
                    aRemover.Add(c);
            }

            // remove da lista do PC todos xs cientistas copiados no foreach anterior
            foreach (Cientista r in aRemover)
            {
                listaPC.Remove(r);
                //print("Removido: " + r.nome); // DEBUG
            }
        }
        else // resposta: NAO é homem
        {
            if (Cientistas.cientistaJogador.sexo == "homem") // o cientista é homem
            {
                avisosScript.GetComponent<Avisos>().Entrar(2); // popup "deve responder certo"
                print("Você não deve responder errado!"); // DEBUG
                respondeuErrado = true; // volta pro jogador responder corretamente
                return;
            }

            // se o cientista realmente nao for homem, copia todos os homens da lista do PC para outra lista
            foreach (Cientista c in listaPC)
            {
                if (c.sexo == "homem")
                    aRemover.Add(c);
            }

            // remove da lista do PC todos xs cientistas copiados no foreach anterior
            foreach (Cientista r in aRemover)
            {
                listaPC.Remove(r);
                //print("Removido: " + r.nome); // DEBUG
            }
        }

        // limpa a lista de cientistas a serem removidos para a prox rodada
        aRemover.Clear();
    }

    // remove xs cientistas descartados da lista do PC por Cor (sim=0 e nao=1)
    void removePorCor(int i)
    {
        if (i == 0) // resposta: SIM, é brancx
        {
            if (Cientistas.cientistaJogador.cor == "pretx") // x cientista é pretx
            {
                avisosScript.GetComponent<Avisos>().Entrar(2); // popup "deve responder certo"
                print("Você não deve responder errado!"); // DEBUG
                respondeuErrado = true; // volta pro jogador responder corretamente
                return;
            }

            // se x cientista realmente for brancx, copia todos xs pretxs da lista do PC para outra lista
            foreach (Cientista c in listaPC)
            {
                if (c.cor == "pretx")
                    aRemover.Add(c);
            }

            // remove da lista do PC todos xs cientistas copiados no foreach anterior
            foreach (Cientista r in aRemover)
            {
                listaPC.Remove(r);
                //print("Removido: " + r.nome); // DEBUG
            }
        }
        else // resposta: NAO é brancx
        {
            if (Cientistas.cientistaJogador.cor == "brancx") // x cientista é brancx
            {
                avisosScript.GetComponent<Avisos>().Entrar(2); // popup "deve responder certo"
                print("Você não deve responder errado!"); // DEBUG
                respondeuErrado = true; // volta pro jogador responder corretamente
                return;
            }

            // se x cientista realmente nao for brancx, copia todos xs brancxs da lista do PC para outra lista
            foreach (Cientista c in listaPC)
            {
                if (c.cor == "brancx")
                    aRemover.Add(c);
            }

            // remove da lista do PC todos xs cientistas copiados no foreach anterior
            foreach (Cientista r in aRemover)
            {
                listaPC.Remove(r);
                //print("Removido: " + r.nome); // DEBUG
            }
        }

        // limpa a lista de cientistas a serem removidos para a prox rodada
        aRemover.Clear();
    }

    // remove xs cientistas descartados da lista do PC por Area (sim=0 e nao=1)
    void removePorArea(int i)
    {
        if (perguntaAtual == 3) // pergunta: É de linguagens?
        {
            if (i == 0) // resposta: SIM
            {
                if (Cientistas.cientistaJogador.area != "LGG") // x cientista nao é de linguagens
                {
                    avisosScript.GetComponent<Avisos>().Entrar(2); // popup "deve responder certo"
                    print("Você não deve responder errado!"); // DEBUG
                    respondeuErrado = true; // volta pro jogador responder corretamente
                    return;
                }

                // se x cientista realmente for de linguagens, copia todos os que nao sao de linguagens da lista do PC para outra lista
                foreach (Cientista c in listaPC)
                {
                    if (c.area != "LGG")
                        aRemover.Add(c);
                }

                // remove da lista do PC todos xs cientistas copiados no foreach anterior
                foreach (Cientista r in aRemover)
                {
                    listaPC.Remove(r);
                    //print("Removido: " + r.nome); // DEBUG
                }

                // se o PC ja descobriu a area, todas as perguntadas relacionada a Area "foram respondidas"
                perguntasFeitas.Add(3);
                perguntasFeitas.Add(4);
                perguntasFeitas.Add(5);
                perguntasFeitas.Add(6);
            }
            else // resposta: NAO
            {
                if (Cientistas.cientistaJogador.area == "LGG") // x cientista é de linguagens
                {
                    avisosScript.GetComponent<Avisos>().Entrar(2); // popup "deve responder certo"
                    print("Você não deve responder errado!"); // DEBUG
                    respondeuErrado = true; // volta pro jogador responder corretamente
                    return;
                }

                // se x cientista realmente nao for de linguagens, copia todos os que sao de linguagens da lista do PC para outra lista
                foreach (Cientista c in listaPC)
                {
                    if (c.area == "LGG")
                        aRemover.Add(c);
                }

                // remove da lista do PC todos xs cientistas copiados no foreach anterior
                foreach (Cientista r in aRemover)
                {
                    listaPC.Remove(r);
                    //print("Removido: " + r.nome); // DEBUG
                }
            }
        }

        else if (perguntaAtual == 4) // pergunta: É de matemática?
        {
            if (i == 0) // resposta: SIM
            {
                if (Cientistas.cientistaJogador.area != "MAT") // x cientista nao é de matematica
                {
                    avisosScript.GetComponent<Avisos>().Entrar(2); // popup "deve responder certo"
                    print("Você não deve responder errado!"); // DEBUG
                    respondeuErrado = true; // volta pro jogador responder corretamente
                    return;
                }

                // se x cientista realmente for de matematica, copia todos os que nao sao de matematica da lista do PC para outra lista
                foreach (Cientista c in listaPC)
                {
                    if (c.area != "MAT")
                        aRemover.Add(c);
                }

                // remove da lista do PC todos xs cientistas copiados no foreach anterior
                foreach (Cientista r in aRemover)
                {
                    listaPC.Remove(r);
                    //print("Removido: " + r.nome); // DEBUG
                }

                // se o PC ja descobriu a area, todas as perguntadas relacionada a Area "foram respondidas"
                perguntasFeitas.Add(3);
                perguntasFeitas.Add(4);
                perguntasFeitas.Add(5);
                perguntasFeitas.Add(6);
            }
            else // resposta: NAO
            {
                if (Cientistas.cientistaJogador.area == "MAT") // x cientista é de matematica
                {
                    avisosScript.GetComponent<Avisos>().Entrar(2); // popup "deve responder certo"
                    print("Você não deve responder errado!"); // DEBUG
                    respondeuErrado = true; // volta pro jogador responder corretamente
                    return;
                }

                // se x cientista realmente nao for de matematica, copia todos os que sao de matematica da lista do PC para outra lista
                foreach (Cientista c in listaPC)
                {
                    if (c.area == "MAT")
                        aRemover.Add(c);
                }

                // remove da lista do PC todos xs cientistas copiados no foreach anterior
                foreach (Cientista r in aRemover)
                {
                    listaPC.Remove(r);
                    //print("Removido: " + r.nome); // DEBUG
                }
            }
        }

        else if (perguntaAtual == 5) // pergunta: É de ciências naturais?
        {
            if (i == 0) // resposta: SIM
            {
                if (Cientistas.cientistaJogador.area != "CN") // x cientista nao é de ciências naturais
                {
                    avisosScript.GetComponent<Avisos>().Entrar(2); // popup "deve responder certo"
                    print("Você não deve responder errado!"); // DEBUG
                    respondeuErrado = true; // volta pro jogador responder corretamente
                    return;
                }

                // se x cientista realmente for de ciências naturais, copia todos os que nao sao de ciências naturais da lista do PC para outra lista
                foreach (Cientista c in listaPC)
                {
                    if (c.area != "CN")
                        aRemover.Add(c);
                }

                // remove da lista do PC todos xs cientistas copiados no foreach anterior
                foreach (Cientista r in aRemover)
                {
                    listaPC.Remove(r);
                    //print("Removido: " + r.nome); // DEBUG
                }

                // se o PC ja descobriu a area, todas as perguntadas relacionada a Area "foram respondidas"
                perguntasFeitas.Add(3);
                perguntasFeitas.Add(4);
                perguntasFeitas.Add(5);
                perguntasFeitas.Add(6);
            }
            else // resposta: NAO
            {
                if (Cientistas.cientistaJogador.area == "CN") // x cientista é de ciências naturais
                {
                    avisosScript.GetComponent<Avisos>().Entrar(2); // popup "deve responder certo"
                    print("Você não deve responder errado!"); // DEBUG
                    respondeuErrado = true; // volta pro jogador responder corretamente
                    return;
                }

                // se x cientista realmente nao for de ciências naturais, copia todos os que sao de ciências naturais da lista do PC para outra lista
                foreach (Cientista c in listaPC)
                {
                    if (c.area == "CN")
                        aRemover.Add(c);
                }

                // remove da lista do PC todos xs cientistas copiados no foreach anterior
                foreach (Cientista r in aRemover)
                {
                    listaPC.Remove(r);
                    //print("Removido: " + r.nome); // DEBUG
                }
            }
        }

        else // pergunta: É de ciências humanas?
        {
            if (i == 0) // resposta: SIM
            {
                if (Cientistas.cientistaJogador.area != "CHS") // x cientista nao é de ciências humanas
                {
                    avisosScript.GetComponent<Avisos>().Entrar(2); // popup "deve responder certo"
                    print("Você não deve responder errado!"); // DEBUG
                    respondeuErrado = true; // volta pro jogador responder corretamente
                    return;
                }

                // se x cientista realmente for de ciências humanas, copia todos os que nao sao de ciências humanas da lista do PC para outra lista
                foreach (Cientista c in listaPC)
                {
                    if (c.area != "CHS")
                        aRemover.Add(c);
                }

                // remove da lista do PC todos xs cientistas copiados no foreach anterior
                foreach (Cientista r in aRemover)
                {
                    listaPC.Remove(r);
                    //print("Removido: " + r.nome); // DEBUG
                }

                // se o PC ja descobriu a area, todas as perguntadas relacionada a Area "foram respondidas"
                perguntasFeitas.Add(3);
                perguntasFeitas.Add(4);
                perguntasFeitas.Add(5);
                perguntasFeitas.Add(6);
            }
            else // resposta: NAO
            {
                if (Cientistas.cientistaJogador.area == "CHS") // x cientista é de ciências humanas
                {
                    avisosScript.GetComponent<Avisos>().Entrar(2); // popup "deve responder certo"
                    print("Você não deve responder errado!"); // DEBUG
                    respondeuErrado = true; // volta pro jogador responder corretamente
                    return;
                }

                // se x cientista realmente nao for de ciências humanas, copia todos os que sao de ciências humanas da lista do PC para outra lista
                foreach (Cientista c in listaPC)
                {
                    if (c.area == "CHS")
                        aRemover.Add(c);
                }

                // remove da lista do PC todos xs cientistas copiados no foreach anterior
                foreach (Cientista r in aRemover)
                {
                    listaPC.Remove(r);
                    //print("Removido: " + r.nome); // DEBUG
                }
            }
        }

        // limpa a lista de cientistas a serem removidos para a prox rodada
        aRemover.Clear();
    }
}
