using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [Header("Timer Settings")]
    public float startTime = 60f;

    [Header("UI")]
    public Text timeText;   

    private float currentTime;
    private bool isRunning = true;

    void Start()
    {
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
        }

        UpdateTimerText();
    }

    private void UpdateTimerText()
    {
        int seconds = Mathf.CeilToInt(currentTime);
        timeText.text = "Time left:\n" + seconds;
    }
}


