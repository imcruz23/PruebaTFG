using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [HideInInspector] public int score;
    // Start is called before the first frame update


    void Start()
    {
        score = 0;
    }

    void Update()
    {
    }

    public void AddScore(int s)
    {
        score += s;
    }
}
