using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnitySpritesRenderer
{
    [ExecuteInEditMode]
    public class SetSprites : MonoBehaviour
    {
        private const int MAX_SPRITE_COUNT = 64;

        [SerializeField] private Material _targetMaterial;
        [SerializeField] private SpritePropWrapper[] _spritePropWrappers;

        private void Awake()
        {

        }

        private void Update()
        {
            if(_targetMaterial) SetSpriteProperties(_targetMaterial, _spritePropWrappers);
        }

        private static readonly int SpritePositionsID = Shader.PropertyToID("_SpritePositions");
        private static readonly int SpriteRotationsID = Shader.PropertyToID("_SpriteRotations");
        private static readonly int SpriteScalesID = Shader.PropertyToID("_SpriteScales");
        private static readonly int SpriteTilesID = Shader.PropertyToID("_SpriteTiles");
        private static readonly int SpriteOffsetsID = Shader.PropertyToID("_SpriteOffsets");
        private static readonly int SpriteAlphasID = Shader.PropertyToID("_SpriteAlphas");
        private static readonly int SpriteCountID = Shader.PropertyToID("_SpriteCount");

        private readonly Vector4[] _spritePositions = new Vector4[MAX_SPRITE_COUNT];
        private readonly float[] _spriteRotations = new float[MAX_SPRITE_COUNT];
        private readonly Vector4[] _spriteScales = new Vector4[MAX_SPRITE_COUNT];
        private readonly Vector4[] _spriteTiles = new Vector4[MAX_SPRITE_COUNT];
        private readonly Vector4[] _spriteOffsets = new Vector4[MAX_SPRITE_COUNT];
        private readonly float[] _spriteAlphas = new float[MAX_SPRITE_COUNT];

        private void SetSpriteProperties(Material material, SpritePropWrapper[] spritePropWrappers)
        {
            int spriteCount = spritePropWrappers.Length;

            for (int i = 0; i < spriteCount; i++)
            {
                SpriteProp current;

                if (spritePropWrappers[i] == null)
                {
                    current = SpriteProp.None;
                    current.SetActive(false);
                }
                else current = spritePropWrappers[i].SpriteProp;

                _spritePositions[i] = current.Position;
                _spriteRotations[i] = current.Rotation;
                _spriteScales[i] = current.Scale * new Vector2(current.FlipX ? -1 : 1, current.FlipY ? -1 : 1);
                _spriteTiles[i] = current.Tile;
                _spriteOffsets[i] = current.Offset;
                _spriteAlphas[i] = current.Alpha * (current.Active ? 1 : 0);
            }

            material.SetVectorArray(SpritePositionsID, _spritePositions);
            material.SetFloatArray(SpriteRotationsID, _spriteRotations);
            material.SetVectorArray(SpriteScalesID, _spriteScales);
            material.SetVectorArray(SpriteTilesID, _spriteTiles);
            material.SetVectorArray(SpriteOffsetsID, _spriteOffsets);
            material.SetFloatArray(SpriteAlphasID, _spriteAlphas);
            material.SetInt(SpriteCountID, spriteCount);
        }
    }
}