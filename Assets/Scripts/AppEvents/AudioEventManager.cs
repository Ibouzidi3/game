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

    private UnityAction<Vector3> boxDestructionEventListener;

    private UnityAction<Vector3> coinCollectEventListener;

    private UnityAction<Vector3> laserHitEventListener;

    void Awake()
    {

        boxDestructionEventListener = new UnityAction<Vector3>(boxDestructionEventHandler);

        coinCollectEventListener = new UnityAction<Vector3>(coinCollectionEventHandler);

        laserHitEventListener = new UnityAction<Vector3>(laserHitEventHandler);
    }


        void Start()
        {



        }


        void OnEnable()
        {

            EventManager.StartListening<BoxDestructionEvent, Vector3>(boxDestructionEventListener);
            EventManager.StartListening<CoinCollectionEvent, Vector3>(coinCollectEventListener);
            EventManager.StartListening<LaserHitEvent, Vector3>(laserHitEventListener);

    }

        void OnDisable()
        {

            EventManager.StopListening<BoxDestructionEvent, Vector3>(boxDestructionEventListener);
            EventManager.StopListening<CoinCollectionEvent, Vector3>(coinCollectEventListener);
            EventManager.StopListening<LaserHitEvent, Vector3>(laserHitEventListener);
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


}