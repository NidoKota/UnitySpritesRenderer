using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySpritesRenderer;

namespace UnitySpritesRenderer
{
    public class SpritePropWrapper : MonoBehaviour
    {
        [SerializeField] private SpriteProp _spriteProp = SpriteProp.None;
        public SpriteProp SpriteProp => _spriteProp;
    }
}