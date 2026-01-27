using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerManager : MonoBehaviour
{
    public float startTime = 60f;
    public string endSceneName = "ending";

    private Text timeText;
    private float currentTime;
    private bool isRunning = true;

    void Start()
    {
        timeText = GetComponentInChildren<Text>();
        currentTime = startTime;
        UpdateTimerText();
    }

    void Update()
    {
        if (!isRunning)
            return;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0f)
        {
            currentTime = 0f;
            isRunning = false;

            SceneManager.LoadScene(endSceneName);
            return;
        }

        UpdateTimerText();
    }

    private void UpdateTimerText()
    {
        timeText.text = "Time left:\n" + Mathf.CeilToInt(currentTime);
    }
}
