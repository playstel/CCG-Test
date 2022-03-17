using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image), typeof(CanvasGroup))]
public class CardRenderer : MonoBehaviour
{
    public Material outlineMaterial;
    private Material defaultMaterial;
    private Image image;
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0;
        
        image = GetComponent<Image>();
        if(!image.material) { Debug.LogError("Failed to find default material"); return; }
        
        defaultMaterial = image.material;
    }

    public void EnableOutline(bool state)
    {
        if(!outlineMaterial) { Debug.LogError("Failed to find outline material"); return; }
        if(!defaultMaterial) { Debug.LogError("Failed to find default material"); return; }
        
        image.material = state ? outlineMaterial : defaultMaterial;
    }

    public void BlocksRaycasts(bool state)
    {
        _canvasGroup.blocksRaycasts = state;
    }

    public void DisableTransparency()
    {
        disableTransparency = true;
    }

    public void EnableTransparency()
    {
        enableTransparency = true;
    }

    private bool enableTransparency;
    private bool disableTransparency;
    private const float transparenceTime = 3f;
    private void Update()
    {
        if (disableTransparency)
        {
            _canvasGroup.alpha += Time.deltaTime * transparenceTime;
            if (_canvasGroup.alpha >= 1) disableTransparency = false;
        }
        
        if (enableTransparency)
        {
            _canvasGroup.alpha -= Time.deltaTime * transparenceTime;
            if (_canvasGroup.alpha <= 0) enableTransparency = false;
        }
    }
}
