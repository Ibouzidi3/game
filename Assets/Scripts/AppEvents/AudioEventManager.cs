using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioEventManager : MonoBehaviour
{

    public EventSound3D eventSound3DPrefab;
    public AudioClip boxDesctructionAudio = null;
    public AudioClip coinCollectAudio;
    public AudioClip laserHitAudio;
    public AudioClip checkpointAudio;
    public AudioClip speedUp;
    public AudioClip shieldPowerup;
    public AudioClip rollingBallAudio;
    public AudioClip swingingPendulumAudio;
    public AudioClip jumpableMashroomAudio;
    public AudioClip crashingStoneAudio;
    public AudioClip explosionAudio;

    private UnityAction<Vector3> boxDestructionEventListener;

    private UnityAction<Vector3> coinCollectEventListener;

    private UnityAction<Vector3> laserHitEventListener;
    private UnityAction<Vector3> speedPowerupEventListener;

    private UnityAction<Vector3> shieldPowerupEventListener;
    private UnityAction<Vector3> checkpointEventListener;
    private UnityAction<Vector3> rollingBallEventListener;
    private UnityAction<Vector3> swingingPendulumEventListener;
    private UnityAction<Vector3> jumpableMashroomEventListener;
    private UnityAction<Vector3> crashingStoneEventListener;
    private UnityAction<Vector3> explosionEventListener;

    void Awake()
    {

        boxDestructionEventListener = new UnityAction<Vector3>(boxDestructionEventHandler);

        coinCollectEventListener = new UnityAction<Vector3>(coinCollectionEventHandler);

        laserHitEventListener = new UnityAction<Vector3>(laserHitEventHandler);
        checkpointEventListener = new UnityAction<Vector3>(checkpointEventHandler);

        speedPowerupEventListener = new UnityAction<Vector3>(speedUpEventHandler);
        shieldPowerupEventListener = new UnityAction<Vector3>(shieldPowerupEventHandler);
        rollingBallEventListener = new UnityAction<Vector3>(rollingBallEventHandler);
        swingingPendulumEventListener = new UnityAction<Vector3>(swingingPendulumEventHandler);
        jumpableMashroomEventListener = new UnityAction<Vector3>(jumpableMashroomEventHandler);
        crashingStoneEventListener = new UnityAction<Vector3>(crashingStoneEventHandler);
        explosionEventListener = new UnityAction<Vector3>(explosionEventHandler);
    }


        void OnEnable()
        {

            EventManager.StartListening<BoxDestructionEvent, Vector3>(boxDestructionEventListener);
            EventManager.StartListening<CoinCollectionEvent, Vector3>(coinCollectEventListener);
            EventManager.StartListening<LaserHitEvent, Vector3>(laserHitEventListener);
            EventManager.StartListening<SpeedPowerupEvent, Vector3>(speedPowerupEventListener);
            EventManager.StartListening<ShieldPowerupEvent, Vector3>(shieldPowerupEventListener);
            EventManager.StartListening<CheckpointEvent, Vector3>(checkpointEventListener);
            EventManager.StartListening<RollingBallEvent, Vector3>(rollingBallEventListener);
            EventManager.StartListening<SwingingPendulumEvent, Vector3>(swingingPendulumEventListener);
            EventManager.StartListening<JumpableMashroomEvent, Vector3>(jumpableMashroomEventListener);
            EventManager.StartListening<CrashingStoneEvent, Vector3>(crashingStoneEventListener);
            EventManager.StartListening<ExplosiveBoxEvent, Vector3>(explosionEventListener);

    }

    void OnDisable()
        {

            EventManager.StopListening<BoxDestructionEvent, Vector3>(boxDestructionEventListener);
            EventManager.StopListening<CoinCollectionEvent, Vector3>(coinCollectEventListener);
            EventManager.StopListening<LaserHitEvent, Vector3>(laserHitEventListener);
            EventManager.StopListening<SpeedPowerupEvent, Vector3>(speedPowerupEventListener);
            EventManager.StopListening<ShieldPowerupEvent, Vector3>(shieldPowerupEventListener);
            EventManager.StopListening<CheckpointEvent, Vector3>(checkpointEventListener);
            EventManager.StopListening<RollingBallEvent, Vector3>(rollingBallEventListener);
            EventManager.StopListening<SwingingPendulumEvent, Vector3>(swingingPendulumEventListener);
            EventManager.StopListening<JumpableMashroomEvent, Vector3>(jumpableMashroomEventListener);
            EventManager.StopListening<CrashingStoneEvent, Vector3>(crashingStoneEventListener);
            EventManager.StopListening<ExplosiveBoxEvent, Vector3>(explosionEventListener);
    }





    void boxDestructionEventHandler(Vector3 worldPos)
        {
        //AudioSource.PlayClipAtPoint(this.boxAudio, worldPos);

        if (eventSound3DPrefab)
        {

            EventSound3D snd = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);

            snd.gameObject.AddComponent<MinionAudioCancelOnDeath>();

            snd.audioSrc.clip = boxDesctructionAudio;

            snd.audioSrc.minDistance = 5f;
            snd.audioSrc.maxDistance = 100f;

            snd.audioSrc.Play();
        }
    }

        void coinCollectionEventHandler(Vector3 worldPos)
        {

            if (eventSound3DPrefab)
            {

                EventSound3D snd = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);

                snd.gameObject.AddComponent<MinionAudioCancelOnDeath>();

                snd.audioSrc.clip = coinCollectAudio;

                snd.audioSrc.minDistance = 5f;
                snd.audioSrc.maxDistance = 100f;

                snd.audioSrc.Play();
            }
        }

    
     void laserHitEventHandler(Vector3 worldPos)
    { 

        if (eventSound3DPrefab)
        {

            EventSound3D snd = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);

            snd.audioSrc.clip = this.laserHitAudio;

            snd.audioSrc.minDistance = 10f;
            snd.audioSrc.maxDistance = 500f;

            snd.audioSrc.Play();
        }
    }


    void speedUpEventHandler(Vector3 worldPos)
    {

        if (eventSound3DPrefab)
        {

            EventSound3D snd = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);

            snd.gameObject.AddComponent<MinionAudioCancelOnDeath>();

            snd.audioSrc.clip = speedUp;

            snd.audioSrc.minDistance = 5f;
            snd.audioSrc.maxDistance = 100f;

            snd.audioSrc.Play();
        }
    }


    void shieldPowerupEventHandler(Vector3 worldPos)
    {

        if (eventSound3DPrefab)
        {

            EventSound3D snd = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);

            snd.gameObject.AddComponent<MinionAudioCancelOnDeath>();

            snd.audioSrc.clip = shieldPowerup;

            snd.audioSrc.minDistance = 5f;
            snd.audioSrc.maxDistance = 100f;

            snd.audioSrc.Play();
        }
    }


    void checkpointEventHandler(Vector3 worldPos)
    {

        if (eventSound3DPrefab)
        {

            EventSound3D snd = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);

            snd.audioSrc.clip = this.checkpointAudio;

            snd.audioSrc.minDistance = 10f;
            snd.audioSrc.maxDistance = 500f;

            snd.audioSrc.Play();
        }
    }

    void rollingBallEventHandler(Vector3 worldPos)
    {

        if (eventSound3DPrefab)
        {

            EventSound3D snd = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);

            snd.audioSrc.clip = this.rollingBallAudio;

            snd.audioSrc.minDistance = 10f;
            snd.audioSrc.maxDistance = 500f;

            snd.audioSrc.Play();
        }
    }

    void swingingPendulumEventHandler(Vector3 worldPos)
    {

        if (eventSound3DPrefab)
        {

            EventSound3D snd = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);

            snd.audioSrc.clip = this.swingingPendulumAudio;

            snd.audioSrc.minDistance = 10f;
            snd.audioSrc.maxDistance = 500f;

            snd.audioSrc.Play();
        }
    }


    void jumpableMashroomEventHandler(Vector3 worldPos)
    {

        if (eventSound3DPrefab)
        {

            EventSound3D snd = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);

            snd.audioSrc.clip = this.jumpableMashroomAudio;

            snd.audioSrc.minDistance = 10f;
            snd.audioSrc.maxDistance = 500f;

            snd.audioSrc.Play();
        }
    }



    void crashingStoneEventHandler(Vector3 worldPos)
    {

        if (eventSound3DPrefab)
        {

            EventSound3D snd = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);

            snd.audioSrc.clip = this.crashingStoneAudio;

            snd.audioSrc.minDistance = 10f;
            snd.audioSrc.maxDistance = 500f;

            snd.audioSrc.Play();
        }
    }



    void explosionEventHandler(Vector3 worldPos)
    {

        if (eventSound3DPrefab)
        {

            EventSound3D snd = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);

            snd.audioSrc.clip = this.explosionAudio;

            snd.audioSrc.minDistance = 10f;
            snd.audioSrc.maxDistance = 500f;

            snd.audioSrc.Play();
        }
    }


}