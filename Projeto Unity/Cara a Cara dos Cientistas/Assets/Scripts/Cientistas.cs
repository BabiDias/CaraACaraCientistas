using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cientistas : MonoBehaviour
{
    public Cientista[] cientistasTodos;
    public static Cientista cientistaPC, cientistaJogador;
    public static Cientista[] cientistasRodada;

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
        cientistasRodada = RandomList();

        cientistaPC = EscolheCientista(cientistasRodada); // seleciona o cientista do PC
        cientistaJogador = EscolheCientista(cientistasRodada); // seleciona o cientista do jogador

        while (cientistaPC == cientistaJogador) // caso o mesmo cientista tehna sido selecionado pro PC e pro jogador, troca o do jogador
        {
            cientistaJogador = EscolheCientista(cientistasRodada);
        }

        print("Cientista Jogador: " + cientistaJogador); // DEBUG
        print("Cientista PC: " + cientistaPC); // DEBUG
    }

    // retorna uma lista com 12 cientistas aleatorios dentre todos os cientistas do jogo
    public Cientista[] RandomList()
    {
        int n = 12;
        Cientista[] outList = new Cientista[n];
        List<int> list = new List<int>();
        for (int i=0; i<n; i++)
        {
            int index = Random.Range(0, cientistasTodos.Length);

            while (list.Contains(index) || index == cientistasTodos.Length)
            {
                index = Random.Range(0, cientistasTodos.Length);
            }

            list.Add(index);
            outList[i] = cientistasTodos[index];
            //print(outList[i].nome);
        }

        return outList;
    }

    // escolhe um cientista dentre os cientistas ativos na rodada
    public Cientista EscolheCientista(Cientista[] list)
    {
        int index = Random.Range(0, list.Length);

        while (index == list.Length)
        {
            index = Random.Range(0, list.Length);
        }

        return list[index];
    }
}
