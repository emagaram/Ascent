using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    public float PPU;
    void Awake()
    {
        QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
        this.gameObject.GetComponent<Camera>().orthographicSize = (Screen.height / PPU / 2f);
    }
}
