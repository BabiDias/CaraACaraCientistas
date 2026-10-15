using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuInicial : MonoBehaviour
{
    public GameObject jogo, menuInicial;
    public GameObject[] scripts;
    public GameObject instrucoes;

    void Awake()
    {
        // Set to 1280x720, windowed mode (false)
        int width = 1088;
        int height = 680;
        Screen.SetResolution(width, height, FullScreenMode.Windowed);
    }

    // Start is called before the first frame update
    void Start()
    {
        menuInicial.SetActive(true);
        jogo.SetActive(false);
        instrucoes.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // quando o jogador inicia um novo jogo, chama o IniciarJogo de todos os scripts para zerar tudo.
    public void Iniciar()
    {
        menuInicial.SetActive(false);
        instrucoes.SetActive(false);
        jogo.SetActive(true);
        scripts[0].GetComponent<Cientistas>().IniciarJogo();
        scripts[1].GetComponent<Card>().IniciarJogo();
        scripts[2].GetComponent<PC>().IniciarJogo();
        scripts[3].GetComponent<Nacionalidade>().IniciarJogo();
        scripts[4].GetComponent<Sexo>().IniciarJogo();
        scripts[5].GetComponent<Cor>().IniciarJogo();
        scripts[6].GetComponent<Area>().IniciarJogo();
    }

    // abre a pagina de instruções
    public void Instrucoes()
    {
        // TODO: pagina de instruções
        instrucoes.SetActive(true);
    }

    // volta para o menu inicial apos o fim do jogo.
    public void VoltarMenu()
    {
        menuInicial.SetActive(true);
        jogo.SetActive(false);
    }
}
