using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpritesRenderer.Scripts
{
    [Serializable]
    public struct SpriteProp
    {
        public static SpriteProp None = new SpriteProp(Vector2.zero, 0, Vector2.one, false, false, Vector2.one, Vector2.zero, 1, true);
        
        [SerializeField] private Vector2 _position;
        public Vector2 Position => _position;

        [SerializeField] private float _rotation;
        public float Rotation => _rotation;

        [SerializeField, Min(0)] private Vector2 _scale;
        public Vector2 Scale => _sprite ? GetSpriteScale() * _scale : _scale;
        public Vector2 PureScale => _scale;

        [SerializeField] private bool _flipX;
        public bool FlipX => _flipX;
        
        [SerializeField] private bool _flipY;
        public bool FlipY => _flipY;
        
        [SerializeField, Range(0, 1)] private float _alpha;
        public float Alpha => _alpha;
        
        [SerializeField] private bool _active;
        public bool Active => _active;

        [SerializeField] private Vector2 _tile;
        public Vector2 Tile => _sprite ? GetSpriteTile() : _tile;
        
        [SerializeField] private Vector2 _offset;
        public Vector2 Offset => _sprite ? GetSpriteOffset() : _offset;

        [SerializeField] private Sprite _sprite;
        public Sprite Sprite => _sprite;

        private Vector2 GetSpriteScale()
        {
            return new Vector2(_sprite.rect.size.x / _sprite.texture.width, _sprite.rect.size.y / _sprite.texture.height);
        }
        
        private Vector2 GetSpriteTile()
        {
            return new Vector2(_sprite.rect.size.x / _sprite.texture.width, _sprite.rect.size.y / _sprite.texture.height);
        }
        
        private Vector2 GetSpriteOffset()
        {
            return new Vector2(_sprite.rect.x / _sprite.texture.width, _sprite.rect.y / _sprite.texture.height);
        }
        
        public SpriteProp(SpriteProp spriteProp)
        {
            _position = spriteProp.Position;
            _rotation = spriteProp.Rotation;
            _scale = spriteProp.Scale;
            _flipX = spriteProp.FlipX;
            _flipY = spriteProp.FlipY;
            _alpha = spriteProp.Alpha;
            _active = spriteProp.Active;

            if (spriteProp._sprite)
            {
                _sprite = spriteProp._sprite;
                _tile = default;
                _offset = default;
            }
            else
            {
                _tile = spriteProp.Tile;
                _offset = spriteProp.Offset;
                _sprite = null;
            }
        }
        
        public SpriteProp(Vector2 position, float rotation, Vector2 scale, bool flipX, bool flipY, Vector2 tile, Vector2 offset, float alpha, bool active)
        {
            _position = position;
            _rotation = rotation;
            _scale = scale;
            _flipX = flipX;
            _flipY = flipY;
            _tile = tile;
            _offset = offset;
            _alpha = alpha;
            _active = active;

            _sprite = null;
        }

        public SpriteProp(Vector2 position, float rotation, Vector2 scale, bool flipX, bool flipY, Sprite sprite, float alpha, bool active)
        {
            _position = position;
            _rotation = rotation;
            _scale = scale;
            _flipX = flipX;
            _flipY = flipY;
            _sprite = sprite;
            _alpha = alpha;
            _active = active;

            _tile = default;
            _offset = default;
        }

        public void SetPosition(Vector2 position)
        {
            _position = position;
        }

        public void SetRotation(float rotation)
        {
            _rotation = rotation;
        }

        public void SetScale(Vector2 scale)
        {
            _scale = scale;
        }
        
        public void SetFlipX(bool flipX)
        {
            _flipX = flipX;
        }
        
        public void SetFlipY(bool flipY)
        {
            _flipY = flipY;
        }

        public void SetTile(Vector2 tile)
        {
            _tile = tile;
        }
        
        public void SetOffset(Vector2 offset)
        {
            _offset = offset;
        }

        public void SetAlpha(float alpha)
        {
            _alpha = alpha;
        }
        
        public void SetActive(bool active)
        {
            _active = active;
        }

        public void SetSprite(Sprite sprite)
        {
            _sprite = sprite;
        }
    }
}