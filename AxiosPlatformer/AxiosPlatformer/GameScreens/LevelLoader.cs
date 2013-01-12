using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Axios.Engine;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using FarseerPhysics.Dynamics;
using Axios.Engine.Gleed2D;
using Axios.Engine.File;
using FarseerPhysics.SamplesFramework;
using System.IO.Compression;
using AxiosPlatformer.Objects;

namespace AxiosPlatformer.GameScreens
{
#if WINDOWS
    class GameConsole : AxiosCommandConsole
    {
        public GameConsole(AxiosGameScreen gameScreen)
            : base(gameScreen)
        {
            //LoadDefault();
            
        }

        protected override void LoadContent()
        {
            LoadDefault();
            base.LoadContent();
        }

    }
#endif
    class LevelLoader : AxiosGameScreen
    {
        private int level = 1;
        //start from first level
        public LevelLoader()
        {
            EnableCameraControl = true;
        }

        public LevelLoader(int level)
        {
            this.level = level;
        }

        public override bool LoadRectangleItem(RectangleItem rectangleitem)
        {
            base.LoadRectangleItem(rectangleitem);

            if (rectangleitem.CustomProperties.Keys.Contains("Playerstart") && (bool)rectangleitem.CustomProperties.Keys.Contains("Playerstart"))
            {
                AddGameObject(new Player(ConvertUnits.ToSimUnits(rectangleitem.Position)));
                return false;
            }
            return true;
        }

        public override void Activate(bool instancePreserved)
        {
            base.Activate(instancePreserved);
            AxiosTitleFile file = new AxiosTitleFile("Levels/level" + level.ToString() + ".xml.gz");
            GZipStream zipstream = new GZipStream(file.GetStream(FileMode.Open), CompressionMode.Decompress);
            Level = Level.FromStream(zipstream, this);
            World.Gravity = new Vector2(0, 10f);
        }

        public override bool LoadTextureItem(TextureItem textureitem)
        {
            base.LoadTextureItem(textureitem);

            if (textureitem.Layer.CustomProperties.Keys.Contains("Collision") && (bool)textureitem.Layer.CustomProperties.Keys.Contains("Collision"))
            {
                Textureobject tobj = new Textureobject(textureitem.texture);
                tobj.Position = ConvertUnits.ToSimUnits(textureitem.Position);
                AddGameObject(tobj);
            }
            return true;
        }
    }
}
