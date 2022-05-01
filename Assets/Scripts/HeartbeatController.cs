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
