using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCodeMgr : MonoBehaviour
{
    //0 ~ 100
    //0 ~ 1
    public Material mat;

    void Update()
    {
        ChangeIntensity();
    }

    void ChangeIntensity()
    {
        mat.SetFloat("_intensity", (float)((float)DisplayData.Instance.Attention/100.0f));
    }
}
