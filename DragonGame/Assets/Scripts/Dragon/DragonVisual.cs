using System;
using System.Collections;
using UnityEngine;

public class DragonVisual : MonoBehaviour
{
    [SerializeField] private Material mat;

    private void Start()
    {
        mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, 0);
    }

    public void StartFade()
    {
        StartCoroutine(FadeInOut());
    }

    public void StopFade()
    {
        StopAllCoroutines();
        mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, 0);
    }
    
    IEnumerator FadeInOut()
    {
        var material = mat;
        //forever
        while (true)
        {
            // fade out
            yield return Fade(material, 0);
            // wait
            yield return new WaitForSeconds(.1f);
            // fade in
            yield return Fade(material, .4f);  
            // wait
            yield return new WaitForSeconds(.1f);
        }
    }

    IEnumerator Fade(Material mat, float targetAlpha)
    {
        while(mat.color.a != targetAlpha)
        {
            var newAlpha = Mathf.MoveTowards(mat.color.a, targetAlpha, 1 * Time.deltaTime);
            mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, newAlpha);
            yield return null;
        }
    }
}