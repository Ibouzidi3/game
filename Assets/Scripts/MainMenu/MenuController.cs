using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject customizationPanel;
    public GameObject customizationGeneralPanel;
    public GameObject customizationHeadPanel;

    public GameObject mainPanel;

    public void StartGame ()
    {
        SceneManager.LoadScene ("MainScene");
    }

    public void QuitGame ()
    {

        Debug.Log ("Quitting game");
        Application.Quit ();
    }

    public void MainMenu ()
    {
        SceneManager.LoadScene ("Menu");
    }

    public void OnCharacterButtonPress ()
    {
        customizationPanel.SetActive (true);
        mainPanel.SetActive (false);
    }

    public void OnCharacterBackButtonPress ()
    {
        customizationPanel.SetActive (false);
        mainPanel.SetActive (true);
    }

    public void OnCustomizationGeneralButtonPress ()
    {
        customizationHeadPanel.SetActive (false);
        customizationGeneralPanel.SetActive (true);
    }

    public void OnCustomizationHeadButtonPress ()
    {
        customizationHeadPanel.SetActive (true);
        customizationGeneralPanel.SetActive (false);

    }

}
