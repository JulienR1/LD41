using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Obstacle : MonoBehaviour {

    private Renderer myRenderer;
    public Material opaqueMat, transparentMat;

    protected virtual void Start()
    {
        myRenderer = GetComponent<Renderer>();
        myRenderer.material = opaqueMat;
    }

    //----------------------------------------------------------------------------------------------------
    // Update the material used to render the object
    //----------------------------------------------------------------------------------------------------
    public void SetVisibility(bool isVisible)
    {
        if (!isVisible)
            myRenderer.material = transparentMat;
        else
            myRenderer.material = opaqueMat;
    }
}