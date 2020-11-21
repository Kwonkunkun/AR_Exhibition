using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialMgr : MonoBehaviour
{
    public Material bigBall;
    public Material smallBall;
    public Material seaFood;

    public float attenstionVal = 0.0f;
    public float meditationVal = 0.0f;

    void Update()
    {
        CalculateValue();
        ChangeIntensity();
    }

    void CalculateValue()
    {
        attenstionVal = (float)((float)DisplayData.Instance.Attention / 100.0f);
        meditationVal = (float)((float)DisplayData.Instance.Meditation / 100.0f);
    }

    void ChangeIntensity()
    {
        bigBall.SetFloat("_intensity", attenstionVal * 2f);
        smallBall.SetFloat("_intensity", attenstionVal * 1.5f);
        seaFood.SetFloat("_intensity", attenstionVal * 3f);
    }
}
