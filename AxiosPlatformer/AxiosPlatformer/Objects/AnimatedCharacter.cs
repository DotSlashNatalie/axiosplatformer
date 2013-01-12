using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Axios.Engine;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace AxiosPlatformer.Objects
{
    class AnimatedCharacter : SimpleDrawableAxiosGameObject
    {
        public enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }

        public Direction? _direction = Direction.Down;
        protected Texture2D _playersheet = null;
        protected List<Texture2D> _characterdown = null;
        protected List<Texture2D> _characterup = null;
        protected List<Texture2D> _characterleft = null;
        protected List<Texture2D> _characterright = null;

        protected int _currentFrame = 0;
        protected int _prevFrame = 0;
        protected int _endFrame = 0;
        protected int _startFrame = 0;
        protected float _interval = 150;
        protected float _timer = 0;
        protected bool Animate = false;

        protected float Walkingspeed = 5;

        public override void LoadContent(AxiosGameScreen gameScreen)
        {
            base.LoadContent(gameScreen);

            
        }

        protected void Move(Direction direction)
        {
            Animate = true;
            switch (direction)
            {
                case Direction.Left:
                    _direction = Direction.Left;
                    BodyPart.ApplyLinearImpulse(new Vector2(-Walkingspeed, 0));
                    break;
                case Direction.Right:
                    _direction = Direction.Right;
                    BodyPart.ApplyLinearImpulse(new Vector2(Walkingspeed, 0));
                    break;
                case Direction.Up:
                    _direction = Direction.Up;
                    BodyPart.ApplyLinearImpulse(new Vector2(0, -200f), BodyPart.Position);
                    //dyPart.LinearVelocity = 
                    break;
                /*case Direction.Down:
                    _direction = Direction.Down;
                    BodyPart.LinearVelocity = new Vector2(0, Walkingspeed);
                    break;*/
            }
        }

        protected void UpdateAnimation()
        {
            if (_direction == Direction.Left)
            {
                //pull from left sprite sheet
                Texture = _characterleft[_currentFrame];
            }
            else if (_direction == Direction.Up)
            {
                Texture = _characterup[_currentFrame];
            }
            //else if (_direction == Direction.Down)
            //{
            //    Texture = _characterdown[_currentFrame];
            //}
            else if (_direction == Direction.Right)
            {
                Texture = _characterright[_currentFrame];
            }
        }

        public override void Update(AxiosGameScreen gameScreen, GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameScreen, gameTime, otherScreenHasFocus, coveredByOtherScreen);
            HandleAnimation((float)gameTime.ElapsedGameTime.TotalMilliseconds);
            UpdateAnimation();
        }

        protected void HandleAnimation(float gameTime)
        {
            _startFrame = 0;
            switch (_direction)
            {
                case Direction.Left:
                    _endFrame = _characterleft.Count;
                    break;
                case Direction.Right:
                    _endFrame = _characterright.Count;
                    break;
                //case Direction.Down:
                //    _endFrame = _characterdown.Count;
                //    break;
                case Direction.Up:
                    _endFrame = _characterup.Count;
                    break;
            }
            --_endFrame;

            if (_currentFrame <= _startFrame || _currentFrame >= _endFrame)
                _currentFrame = _startFrame;
            else
                _currentFrame = _prevFrame;

            _timer += gameTime;

            if (_timer > _interval) // check timer
            {
                if (Animate)
                    _currentFrame++; // go to the next frame of animation
                _timer = 0f; // reset the timer
            }

            if (_currentFrame > (_endFrame))
            {
                _currentFrame = _startFrame;
            }

            _prevFrame = _currentFrame;
        }

    }
}
