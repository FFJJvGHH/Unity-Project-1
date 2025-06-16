using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    private SpriteRenderer sr;
    [Header("Flash FX")]
    [SerializeField] private Material hitMat;
    private Material originMaterial;
    public float flashTime; 
    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        originMaterial = sr.material;
    }

    private IEnumerator FlashFX()
    {
        sr.material = hitMat;
        yield return new WaitForSeconds(flashTime);
        sr.material = originMaterial;
    }
}
