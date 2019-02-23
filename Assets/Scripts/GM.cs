using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GM : MonoBehaviour
{
    public GameObject girl;
    public GameObject bat;
    public GameObject levelChanger;
    public static bool GameIsPaused = true;
    public GameObject controlsOverlayUI;
    public static int score = 0;
    public Text scoreText;
    private AudioSource music;

    // Start is called before the first frame update
    void Awake()
    {
        Pause();
    }

    void Start()
    {
        score = 0;
        music = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString() + "/25";

        if (GameIsPaused && Input.anyKey) {
            Resume();
        }
    }

    void Resume() {
        controlsOverlayUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

        girl.GetComponent<Girl>().startThrowingGrenades();
        bat.GetComponent<Bat>().startDeflecting();
        music.Play();
    }

    void Pause() {
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void IncreaseScore() {
        score = score + 1;

        if (score == 25) {
            levelChanger.GetComponent<LevelChanger>().changeSceneTo(3);
        }
    }
}
