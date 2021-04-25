using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject customizationPanel;
    public GameObject customizationGeneralPanel;
    public GameObject customizationHeadPanel;
    public GameObject customizationAccessoriesPanel;

    public Transform customizationPanelFocus;
    public Button customizationGeneralButton;
    public Button customizationHeadButton;
    public Button customizationAccessoriesButton;
    public MenuCameraController menuCameraController;

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
        customizationAccessoriesPanel.SetActive(false);
        mainPanel.SetActive (false);
        menuCameraController.currentState = MenuCameraController.CameraState.CHARACTER_CUSTOMISATION_GENERAL;
    }

    public void OnCharacterBackButtonPress ()
    {
        customizationPanel.SetActive (false);
        mainPanel.SetActive (true);
        menuCameraController.currentState = MenuCameraController.CameraState.MAIN_MENU;
    }

    public void OnCustomizationGeneralButtonPress ()
    {
        customizationHeadPanel.SetActive (false);
        customizationGeneralPanel.SetActive (true);
        customizationAccessoriesPanel.SetActive(false);
        customizationPanelFocus.SetParent(customizationGeneralButton.transform, false);
        customizationPanelFocus.localPosition = new Vector3 (0, customizationPanelFocus.localPosition.y, 0);
        menuCameraController.currentState = MenuCameraController.CameraState.CHARACTER_CUSTOMISATION_GENERAL;
    }

    public void OnCustomizationHeadButtonPress ()
    {
        customizationHeadPanel.SetActive (true);
        customizationGeneralPanel.SetActive (false);
        customizationAccessoriesPanel.SetActive(false);
        customizationPanelFocus.SetParent(customizationHeadButton.transform, false);
        customizationPanelFocus.localPosition = new Vector3 (0, customizationPanelFocus.localPosition.y, 0);
        menuCameraController.currentState = MenuCameraController.CameraState.CHARACTER_CUSTIOMZIATION_HEAD;
    }

    public void OnCustomizationAccessoriesButtonPress ()
    {
        customizationHeadPanel.SetActive(false);
        customizationGeneralPanel.SetActive(false);
        customizationAccessoriesPanel.SetActive(true);
        customizationPanelFocus.SetParent(customizationAccessoriesButton.transform, false);
        customizationPanelFocus.localPosition = new Vector3(0, customizationPanelFocus.localPosition.y, 0);
        menuCameraController.currentState = MenuCameraController.CameraState.CHARACTER_CUSTIOMZIATION_HEAD;

    }

}
