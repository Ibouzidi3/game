using UnityEngine;
using System.Collections;

public class PowerUpsCollectorComponent : MonoBehaviour, PowerUpsCollector
{
    public float defaultSpeed = 7.0f;
    public float speedIncreaseFactor = 1.5f;
    private float speed;
    public bool shield;
    public int shieldEffectInSec = 5;
    public GameObject shieldText;
    public GameObject shieldAura;
    private GameObject shieldAuraClone;
    private Coroutine shieldRoutine;
    public int speedEffectInSec = 5;
    public GameObject speedText;
    public GameObject speedAura;
    private GameObject speedAuraClone;
    private static Coroutine speedRoutine;
    private GameObject player;
    public void Start()
    {
        player = this.gameObject;
        shield = false;
        speed = defaultSpeed;
    }
   
    public void ActivateShield()
    {
        if (shieldRoutine != null)
        {
            StopCoroutine(shieldRoutine);
            DeactivateShield();
        }
        shieldRoutine = StartCoroutine(_ActivateShield());
    }

    private IEnumerator _ActivateShield()
    {

        ShowText(shieldText);
        ShowShieldAura();
        EnableShield();
        yield return new WaitForSeconds(shieldEffectInSec);
        DeactivateShield();
    }

    private void DeactivateShield()
    {
        DisableShield();
        HideShieldAura();
    }

    public float GetSpeed()
    {
        return speed;
    }

    public bool HasShield()
    {
        return shield;
    }

    public void SpeedUp()
    {
        if (speedRoutine != null)
        {
            StopCoroutine(speedRoutine);
            _SetToNormalSpeed();
        }
        speedRoutine = StartCoroutine(_SpeedUp());
    }

    private IEnumerator _SpeedUp()
    {

        ShowText(speedText);
        ShowSpeedAura();
        IncreaseSpeed();
        yield return new WaitForSeconds(speedEffectInSec);
        _SetToNormalSpeed();
    }

    private void _SetToNormalSpeed()
    {
        HideSpeedAura();
        SetToNormalSpeed();
    }


    #region private methods
    private void EnableShield()
    {
        shield = true;
    }

    private void DisableShield()
    {
        shield = false;
    }

    private void IncreaseSpeed()
    {
        speed = defaultSpeed * speedIncreaseFactor;
    }

    private void SetToNormalSpeed()
    {
        speed = defaultSpeed;
    }

    public void ShowShieldAura()
    {
        if (shieldAura != null)
        {
            shieldAuraClone = Instantiate(shieldAura, player.transform);
            shieldAuraClone.SetActive(true);
        }
    }

    public void HideShieldAura()
    {
        if (shieldAuraClone != null)
            GameObject.Destroy(shieldAuraClone.gameObject, 0);
    }

    public void ShowText(GameObject text)
    {
        if (text != null)
            TextHelper.ShowText(player, text);
    }

    public void ShowSpeedAura()
    {
        if (speedAura != null)
        {
            speedAuraClone = Instantiate(speedAura, player.transform);
            speedAuraClone.SetActive(true);
        }
    }

    public void HideSpeedAura()
    {
        if (speedAuraClone != null)
            GameObject.Destroy(speedAuraClone.gameObject,0);
    }
    #endregion
}
