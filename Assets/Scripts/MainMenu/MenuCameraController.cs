using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCameraController : MonoBehaviour
{
    public GameObject titleCard;
    public GameObject mainMenu;
    public GameObject goldCount;
    private Quaternion mainMenuRotation = Quaternion.Euler(11.65f, 7.612f, 0f);
    private Vector3 mainMenuPosition = new Vector3(1f, 13.5f, -7.68f);

    private Quaternion characterCustomisationRotation = Quaternion.Euler(10.37f, 2.93f, 0f);
    private Vector3 characterCustomisationPosition = new Vector3(-3.3f, 12.7f, 2.6f);

    private Quaternion characterHeadCustomisationRotation = Quaternion.Euler(10.37f, 41.03f, 0f);
    private Vector3 characterHeadCustomisationPosition = new Vector3(-4.14f, 12.7f, 3.44f);

    private CameraState _currentState = CameraState.MAIN_MENU;

    public CameraState currentState
    {
        get { return _currentState; }
        set { _currentState = value; }
    }

    void Start()
    {
        // Pos 1 13.5 -7.68
        this.transform.rotation = Quaternion.Euler(-45f, 0, 0);
        this.transform.position = mainMenuPosition;
        
        titleCard.SetActive(true);
        mainMenu.SetActive(false);
        goldCount.SetActive(false);

        StartCoroutine(HideMainMenu());


    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case CameraState.CHARACTER_CUSTOMISATION_GENERAL:
                this.transform.rotation = Quaternion.SlerpUnclamped(this.transform.rotation, characterCustomisationRotation, Time.deltaTime * 2f);
                this.transform.position = Vector3.Slerp(this.transform.position, characterCustomisationPosition, Time.deltaTime * 2f);
                break;
            case CameraState.CHARACTER_CUSTIOMZIATION_HEAD:
                this.transform.rotation = Quaternion.SlerpUnclamped(this.transform.rotation, characterHeadCustomisationRotation, Time.deltaTime * 2f);
                this.transform.position = Vector3.Slerp(this.transform.position, characterHeadCustomisationPosition, Time.deltaTime * 2f);
                break;
            case CameraState.MAIN_MENU:
            default:
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, mainMenuRotation, Time.deltaTime * 0.3f);
                this.transform.position = Vector3.Slerp(this.transform.position, mainMenuPosition, Time.deltaTime *1.5f);
                break;

        }
    }

    IEnumerator HideMainMenu()
    {
        yield return new WaitForSeconds(4);

        titleCard.SetActive(false);
        mainMenu.SetActive(true);
        goldCount.SetActive(true);
    }

    public enum CameraState
    {
        MAIN_MENU,
        CHARACTER_CUSTOMISATION_GENERAL,
        CHARACTER_CUSTIOMZIATION_HEAD
    }
}
