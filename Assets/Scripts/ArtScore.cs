using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArtScore : MonoBehaviour
{
    TextMeshProUGUI textMesh;
    int score = 0;

    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    public void AddPoints(int points)
    {
        score += points;
        textMesh.text = $"Score: {score} pts";
    }
}