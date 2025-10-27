using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    public AudioSource normalMusic;
    public AudioSource battleMusic;

    private int chasingEnemies = 0;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        PlayNormalMusic();
    }

    public void EnemyStartedChasing()
    {
        chasingEnemies++;
        UpdateMusic();
    }

    public void EnemyStoppedChasing()
    {
        chasingEnemies = Mathf.Max(0, chasingEnemies - 1);
        UpdateMusic();
    }

    private void UpdateMusic()
    {
        if (chasingEnemies > 0)
            PlayBattleMusic();
        else
            PlayNormalMusic();
    }

    private void PlayNormalMusic()
    {
        if (!normalMusic.isPlaying)
        {
            battleMusic.Stop();
            normalMusic.Play();
        }
    }

    private void PlayBattleMusic()
    {
        if (!battleMusic.isPlaying)
        {
            normalMusic.Stop();
            battleMusic.Play();
        }
    }
}
