using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    public float PPU;
    public Transform player;
    void Awake()
    {
        player = FindObjectOfType<PlayerController>().transform;
        QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
        this.gameObject.GetComponent<Camera>().orthographicSize = (Screen.height / PPU / 2f);
    }
}
