using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Axios.Engine;
using Microsoft.Xna.Framework.Graphics;
using Axios.Engine.Extensions;
using FarseerPhysics.Factories;
using FarseerPhysics.SamplesFramework;
using GameStateManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Axios.Engine.Gleed2D;

namespace AxiosPlatformer.Objects
{
    class Player : AnimatedCharacter
    {
        private Vector2 initialpos;
        public Player(Vector2 pos)
        {
            this.initialpos = pos;
        }

        private bool jumping = false;

        public override void Update(AxiosGameScreen gameScreen, GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameScreen, gameTime, otherScreenHasFocus, coveredByOtherScreen);

            //this.BodyPart.LinearVelocity = new Vector2(0, -5f);
        }

        public override void LoadContent(AxiosGameScreen gameScreen)
        {
            base.LoadContent(gameScreen);
            int xcount, ycount;
            List<Texture2D> walksheets = new List<Texture2D>();
            for(int i = 1; i < 12; i++)
                walksheets.Add( gameScreen.ScreenManager.Game.Content.Load<Texture2D>("tiles/character/walk/walk00" + i.ToString("D2")));
            //_playersheet =
            /*Texture2D[] tmpsheets = _playersheet.SplitFlat(192, 32, out xcount, out ycount);
            Texture2D[] tmpsheetsdown = tmpsheets[0].SplitFlat(24, 32, out xcount, out ycount);
            Texture2D[] tmpsheetsup = tmpsheets[1].SplitFlat(24, 32, out xcount, out ycount);
            Texture2D[] tmpsheetsleft = tmpsheets[2].SplitFlat(24, 32, out xcount, out ycount);
            Texture2D[] tmpsheetsright = tmpsheets[3].SplitFlat(24, 32, out xcount, out ycount);*/
            //_characterdown = walksheets.GetRange(0, 1);
            _characterup = walksheets.GetRange(0, 1);
            _characterleft = walksheets;
            _characterright = walksheets;

            Texture = walksheets[0];
            //offset the width by a little
            BodyPart = BodyFactory.CreateRectangle(gameScreen.World, ConvertUnits.ToSimUnits(Texture.Width - 5), ConvertUnits.ToSimUnits(Texture.Height), 2f);
            this.Origin = new Vector2(Texture.Width / 2f, Texture.Height / 2f);
            BodyPart.Mass = 1f;
            BodyPart.Position = initialpos;
            BodyPart.FixedRotation = true;
            BodyPart.UserData = this;
            BodyPart.BodyType = FarseerPhysics.Dynamics.BodyType.Dynamic;
            BodyPart.OnCollision += new FarseerPhysics.Dynamics.OnCollisionEventHandler(BodyPart_OnCollision);
            BodyPart.Friction = 1f;
            BodyPart.Restitution = 0f;

        }

        bool BodyPart_OnCollision(FarseerPhysics.Dynamics.Fixture fixtureA, FarseerPhysics.Dynamics.Fixture fixtureB, FarseerPhysics.Dynamics.Contacts.Contact contact)
        {
            if (fixtureB.UserData is Textureobject)
            {
                Textureobject item = (Textureobject)fixtureB.UserData;
                jumping = false;
            }
            return true;
        }

        public override void HandleInput(AxiosGameScreen gameScreen, InputState input, GameTime gameTime)
        {
            base.HandleInput(gameScreen, input, gameTime);
            //if (gameScreen.Console != null && gameScreen.Console.Active)
            //    return;
            PlayerIndex p;
            Animate = false;

            if (input.IsKeyPressed(Keys.A, PlayerIndex.One, out p))
            {
                Move(Direction.Left);
            }

            if (input.IsKeyPressed(Keys.W, PlayerIndex.One, out p) && !jumping)
            {
                Move(Direction.Up);
                jumping = true;
            }
            //else if (input.IsKeyPressed(Keys.S, PlayerIndex.One, out p))
            //{
            //    Move(Direction.Down);
            //}
            if (input.IsKeyPressed(Keys.D, PlayerIndex.One, out p))
            {
                Move(Direction.Right);
            }
            else
            {
                //BodyPart.LinearVelocity = Vector2.Zero;
            }

        }

        public override void OnMouseDown(AxiosGameScreen gameScreen, InputState input)
        {
            base.OnMouseDown(gameScreen, input);

            Console.WriteLine("clicked");
        }
    }
}