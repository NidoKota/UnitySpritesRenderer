using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpritesRenderer.Scripts;

namespace SpritesRenderer.Scripts
{
    public class SpritePropWrapper : MonoBehaviour
    {
        [SerializeField] private SpriteProp _spriteProp = SpriteProp.None;
        public SpriteProp SpriteProp => _spriteProp;
    }
}