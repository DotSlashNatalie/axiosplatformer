using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Axios.Engine;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;

namespace AxiosPlatformer.Objects
{
    class Textureobject : SimpleDrawableAxiosGameObject
    {
        public Textureobject(Texture2D texture)
        {
            this.Texture = texture;
        }

        public override void LoadContent(AxiosGameScreen gameScreen)
        {
            base.LoadContent(gameScreen);
            CreateBodyFromTexture(gameScreen);
            this.BodyPart.BodyType = BodyType.Static;
        }

        /*public override void Draw(AxiosGameScreen gameScreen, GameTime gameTime)
        {
            
        }*/
    }
}
