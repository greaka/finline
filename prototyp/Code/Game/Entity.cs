﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using prototyp.Code.Game.Helper;

namespace prototyp.Code.Game
{
    public abstract class Entity
    {
        protected Model _model;
        protected Vector3 _position;
        protected float _angle;

        public Model GetModel => _model;

        public BoundingSphere GetBound
        {
            get
            {
                var sphere = _model.Meshes[0].BoundingSphere;
                sphere.Center += _position;
                sphere.Radius *= 0.8f;
                return sphere;
            }
        }

        protected void Draw(Vector3 cameraPosition, float aspectRatio, Vector3 playerPosition)
        {
            foreach (var mesh in _model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.PreferPerPixelLighting = true;


                    effect.World = GetWorldMatrix();

                    var cameraLookAtVector = playerPosition;
                    var cameraUpVector = Vector3.UnitZ;

                    ControlsHelper.ViewMatrix = Matrix.CreateLookAt(
                        cameraPosition, cameraLookAtVector, cameraUpVector);
                    effect.View = ControlsHelper.ViewMatrix;

                    float fieldOfView = Microsoft.Xna.Framework.MathHelper.PiOver4;
                    float nearClipPlane = 1;
                    float farClipPlane = 200;

                    effect.Projection = Matrix.CreatePerspectiveFieldOfView(
                        fieldOfView, aspectRatio, nearClipPlane, farClipPlane);
                }

                mesh.Draw();
            }
        }

        private Matrix GetWorldMatrix()
        {

            // this matrix moves the model "out" from the origin
            Matrix translationMatrix = Matrix.CreateTranslation(_position);

            // this matrix rotates everything around the origin
            Matrix rotationMatrix = Matrix.CreateRotationZ(_angle);

            // We combine the two to have the model move in a circle:
            Matrix combined = rotationMatrix * translationMatrix;

            return combined;
        }
    }
}
