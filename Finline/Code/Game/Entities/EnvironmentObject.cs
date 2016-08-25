﻿using Finline.Code.Constants;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Finline.Code.Game.Entities
{
    public class EnvironmentObject : Entity
    {
        private readonly GameConstants.EnvObjects _type;
        private readonly bool orbit = false;
        public bool Visible { get; set; }

        public GameConstants.EnvObjects Type => this._type;

        public EnvironmentObject(ContentManager contentManager, Vector3 position, GameConstants.EnvObjects model)
        {
            this.Visible = true;
            this._type = model;
            switch (model)
            {
                case GameConstants.EnvObjects.wallV:
                    this._sphereScaling = 0.4f;
                    break;
            }

            this._model = contentManager.Load<Model>(model.ToString());
            this.position = position;
            this._angle = 0;
        }

        public void Update(GameTime gameTime)
        {
            if (this.orbit)
            {
                this._angle += 0.1f;
            }
        }

        public override void Draw(Matrix viewMatrix, Matrix projectionMatrix)
        {
            if (this.Visible)
                base.Draw(viewMatrix, projectionMatrix);
        }
    }





}