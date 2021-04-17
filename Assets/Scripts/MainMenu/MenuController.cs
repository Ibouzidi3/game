using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject customizationPanel;
    public GameObject customizationGeneralPanel;
    public GameObject customizationHeadPanel;
    public Transform customizationPanelFocus;
    public Button customizationGeneralButton;
    public Button customizationHeadButton;

    public GameObject mainPanel;
    public TextMeshProUGUI goldCount;

    private void Start ()
    {
        goldCount.text = Gamestate.coins.ToString ();
    }

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
        customizationGeneralButton.Select ();
        customizationGeneralPanel.SetActive (true);
        customizationHeadPanel.SetActive (false);
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
        customizationPanelFocus.parent = customizationGeneralButton.transform;
        customizationPanelFocus.localPosition = new Vector3 (0, customizationPanelFocus.localPosition.y, 0);
    }

    public void OnCustomizationHeadButtonPress ()
    {
        customizationHeadPanel.SetActive (true);
        customizationGeneralPanel.SetActive (false);
        customizationPanelFocus.parent = customizationHeadButton.transform;
        customizationPanelFocus.localPosition = new Vector3 (0, customizationPanelFocus.localPosition.y, 0);
    }

}
