using UnityEngine;
using UnityEngine.UI;

public class HeartbeatController : MonoBehaviour
{
    public AudioClip SlowHeartBeat;
    public AudioClip FastHeartBeat;
    public float SwitchIntervall = 20f;
    public Image image;
    public Texture slowHearbeatTexture;
    public Texture fastHearbeatTexture;

    public HeartBeatRate heartBeatRate = HeartBeatRate.SLOW;

    private AudioSource audioSource;
    private float timeElapsed = 0f;
    private bool isPaused = false;

    public float updateStep = 0.1f;
    public int sampleDataLength = 1024;

    private float currentUpdateTime = 0f;

    public float clipLoudness;
    private float[] clipSampleData;

    public GameObject player;
    public float sizeFactor = 1;

    public float minSize = 0;
    public float maxSize = 500;

    private void Awake()
    {
        clipSampleData = new float[sampleDataLength];
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        updateHeartBeatRate(heartBeatRate);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameStateManager.Instance.gameState == GameState.IN_GAME)
        {
            if(!audioSource.isPlaying)
            {
                Play();
            }
        } else if(audioSource.isPlaying)
        {
            Pause();
        }

        if (isPaused) return;

        this.timeElapsed += Time.deltaTime;

        if (this.timeElapsed >= this.SwitchIntervall)
        {
            switch (this.heartBeatRate)
            {
                case HeartBeatRate.SLOW:
                    updateHeartBeatRate(HeartBeatRate.FAST);
                    break;
                case HeartBeatRate.FAST:
                    updateHeartBeatRate(HeartBeatRate.SLOW);
                    break;
            }
            this.timeElapsed = 0;
        }
        currentUpdateTime += Time.deltaTime;
        if (currentUpdateTime >= updateStep)
        {
            currentUpdateTime = 0f;
            audioSource.clip.GetData(clipSampleData, audioSource.timeSamples); //I read 1024 samples, which is about 80 ms on a 44khz stereo clip, beginning at the current sample position of the clip.
            clipLoudness = 0f;
            foreach (var sample in clipSampleData)
            {
                clipLoudness += Mathf.Abs(sample);
            }
            clipLoudness /= sampleDataLength; //clipLoudness is what you are looking for

            clipLoudness *= sizeFactor;
            clipLoudness = Mathf.Clamp(clipLoudness, minSize, maxSize);
            player.transform.localScale = new Vector3(clipLoudness, clipLoudness, clipLoudness);
        }

        }


    public void Play()
    {
        this.audioSource.Play();
        this.isPaused = false;
    }

    public void Pause()
    {
        this.audioSource.Stop();
        this.isPaused = true;
    }

    private void updateHeartBeatRate(HeartBeatRate newHeartBeat)
    {
        this.heartBeatRate = newHeartBeat;
        switch(this.heartBeatRate)
        {
            case HeartBeatRate.SLOW:
                this.audioSource.clip = SlowHeartBeat;
                this.image.material.SetTexture("_PatternTex", slowHearbeatTexture);
                this.image.material.SetFloat("_SpeedX", 0.5f);
                break;
            case HeartBeatRate.FAST:
                this.audioSource.clip = FastHeartBeat;
                this.image.material.SetTexture("_PatternTex", fastHearbeatTexture);
                this.image.material.SetFloat("_SpeedX", 0.75f);
                break;
        }
        this.audioSource.Play();
    }
   


   
}

public enum HeartBeatRate
{
    SLOW, FAST
}
