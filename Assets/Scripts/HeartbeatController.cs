using UnityEngine;

public class HeartbeatController : MonoBehaviour
{
    public AudioClip SlowHeartBeat;
    public AudioClip FastHeartBeat;
    public float SwitchIntervall = 20f;

    public HeartBeatRate heartBeatRate = HeartBeatRate.SLOW;

    private AudioSource audioSource;
    private float timeElapsed = 0f;
    private bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.gameState == GameState.PAUSED)
        {
            if(audioSource.isPlaying)
            {
                Pause();
            }
        } else if(!audioSource.isPlaying)
        {
            Play();
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
                break;
            case HeartBeatRate.FAST:
                this.audioSource.clip = FastHeartBeat;
                break;
        }
        this.audioSource.Play();
    }
}

public enum HeartBeatRate
{
    SLOW, FAST
}
