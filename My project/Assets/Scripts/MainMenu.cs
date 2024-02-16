using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Nivel1()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Nivel2()
    {
        SceneManager.LoadScene("Nivel2");
    }

    public void Nivel3()
    {
        SceneManager.LoadScene("Nivel3");
    }
}
