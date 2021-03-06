﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine.Gui.Base
{
    public class Texture
    {
        private int _currentFrameIndex;
        private int _elapsedMilliSecond;
        private int _frameBegin;
        private int _frameEnd;
        private Asf _texture;

        public Asf Data
        {
            get { return _texture; }
            set { _texture = value; }
        }

        public int CurrentFrameIndex
        {
            get { return _currentFrameIndex; }
            set
            {
                if (_texture != null && _texture.FrameCounts > 1)
                {
                    if(_frameBegin == _frameEnd)
                        _currentFrameIndex = value%_texture.FrameCounts;
                    else
                    {
                        if (value >= _frameEnd)
                            _currentFrameIndex = _frameBegin;
                        else if (value < _frameBegin)
                            _currentFrameIndex = _frameBegin;
                        else
                            _currentFrameIndex = value;
                    }
                }
                else _currentFrameIndex = 0;
            }
        }

        public Texture2D CurrentTexture
        {
            get
            {
                if (_texture != null)
                    return _texture.GetFrame(CurrentFrameIndex);
                else return null;
            }
        }

        public int Width
        {
            get { return (_texture == null) ? 0 : _texture.Width; }
        }

        public int Height
        {
            get { return (_texture == null) ? 0 : _texture.Height; }
        }

        public Texture() { }

        public Texture(Asf asf)
        {
            _texture = asf;
        }

        public Texture(Asf asf, int frameBegin, int count)
        {
            _texture = asf;
            _frameBegin = frameBegin;
            _frameEnd = frameBegin + count;
        }

        public void Update(GameTime gameTime)
        {
            if (_texture != null && _texture.FrameCounts > 1)
            {
                _elapsedMilliSecond += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (_elapsedMilliSecond > _texture.Interval)
                {
                    _elapsedMilliSecond -= _texture.Interval;
                    CurrentFrameIndex++;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            if (_texture != null)
            {
                var texture = _texture.GetFrame(CurrentFrameIndex);
                if(texture == null) return;
                 spriteBatch.Draw(
                     texture, 
                     position, 
                     Color.White);
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, int width, int height)
        {
            if (_texture != null)
            {
                var texture = _texture.GetFrame(CurrentFrameIndex);
                if (texture == null) return;
                spriteBatch.Draw(texture, 
                    new Rectangle((int)position.X, (int)position.Y, width, height), 
                    Color.White);
            }
        }
    }
}