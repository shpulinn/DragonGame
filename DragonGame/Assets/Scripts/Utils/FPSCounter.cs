using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    public TextMeshProUGUI fpsText; // Ссылка на текстовый элемент UI для отображения FPS
    private float deltaTime = 0.0f;

    private float minFPS = float.MaxValue;  // Минимальный FPS
    private float maxFPS = float.MinValue;  // Максимальный FPS
    private float fps = 0.0f;               // Текущий FPS

    private float totalFPS = 0f;            // Сумма всех FPS для вычисления среднего значения
    private int frameCount = 0;             // Количество обновлений для среднего FPS

    private void Update()
    {
        // Рассчитываем текущий FPS
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        fps = 1.0f / deltaTime;

        // Обновляем минимальный и максимальный FPS
        if (fps < minFPS)
        {
            minFPS = fps;
        }

        if (fps > maxFPS)
        {
            maxFPS = fps;
        }

        // Считаем общее количество FPS и количество кадров
        totalFPS += fps;
        frameCount++;

        // Вычисляем средний FPS
        float averageFPS = totalFPS / frameCount;

        // Обновляем текст в UI с текущими показателями
        if (fpsText != null)
        {
            fpsText.text = string.Format(
                "FPS: {0:0.} (Min: {1:0.} Max: {2:0.} Avg: {3:0.})", 
                fps, minFPS, maxFPS, averageFPS);
        }
    }
}

