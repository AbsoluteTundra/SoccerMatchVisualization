using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private MeshRenderer Jersey;

    public void Start()
    {
        
    }

    public void SetJerseyColor(Color jerseyColor)
    {
        Jersey.material.color = jerseyColor;
    }
}
