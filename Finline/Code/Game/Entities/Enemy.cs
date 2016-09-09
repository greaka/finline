﻿using System.Collections.Generic;

namespace Finline.Code.Game.Entities
{
    using System.Linq;

    using Finline.Code.Utility;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    public class Enemy : LivingEntity
    {
        public bool Shoot = false;

        public Enemy(ContentManager contentManager, Vector3 position)
        {
            this.ModelAnimation = new Animation(3);
            this.position = position;
            this.Angle = 0;
            var enemy = contentManager.Load<Model>("enemy");
            var enemyUnten = contentManager.Load<Model>("enemy_unten");

            this.Model = enemy;

            this.ModelAnimation.Add(enemy);
            this.ModelAnimation.Add(enemyUnten);
            this.ModelAnimation.Add(enemy);
        }

        public void Update(Vector3 playerPosition, List<EnvironmentObject> environmentObjects, GameTime gameTime)
        {
            var distance = this.position - playerPosition;
            var view = new Ray(this.position, distance);

            var any = environmentObjects.Any(obj => view.Intersects(new BoundingSphere(obj.Position, obj.Position.Length()))
                        != null && (this.position - obj.Position).Length() < 0.38f*distance.Length());

            if (any)
            {
                this.Shoot = false;
            }
            else
            {
                this.SetViewDirection(distance.Get2D());
                this.Shoot = true;
            }
        }
    }
}
