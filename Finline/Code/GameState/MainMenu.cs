﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Finline.Code.GameState
{
    using Finline.Code.Game.Controls;

    internal class MainMenu : DrawableGameComponent
    {
        public delegate void GetIngame();
        private readonly Controller controls;
        public static bool IsPressed;
        Dictionary<EMenuState, List<GuiElement>> _guiElements = new Dictionary<EMenuState, List<GuiElement>>();
       

        private readonly SpriteBatch _spriteBatch;


       
        
        private EMenuState _menuState;
        private SpriteFont _font;

        /// <summary>
        ///     Constructor to use the GUIElementlist to select the element
        /// </summary>
        public MainMenu(StateManager game, SpriteBatch sprite)
            : base(game)
        {
            this.controls = game.controls;

            // The Lists with all the elements

            _guiElements.Add(EMenuState.TitleScreen, new List<GuiElement>());
            _guiElements.Add(EMenuState.MainMenu, new List<GuiElement>());
            _guiElements.Add(EMenuState.CharacterScreen, new List<GuiElement>());
            _guiElements.Add(EMenuState.Option, new List<GuiElement>());
            _guiElements.Add(EMenuState.Credits, new List<GuiElement>());

            _spriteBatch = sprite;
            _menuState = EMenuState.TitleScreen;

            _guiElements[EMenuState.TitleScreen].Add(new GuiElement("Logo 2")); //Logo in the state Titlescreen


            //here are the elements in the state MainMenu

            _guiElements[EMenuState.MainMenu].Add(new GuiElement("MenuFrame"));
            _guiElements[EMenuState.MainMenu].Add(new GuiElement("NewGame"));
            _guiElements[EMenuState.MainMenu].Add(new GuiElement("Option"));
            _guiElements[EMenuState.MainMenu].Add(new GuiElement("Credits"));
            _guiElements[EMenuState.MainMenu].Add(new GuiElement("End"));


            //elements in the state characterScreen
            _guiElements[EMenuState.CharacterScreen].Add(new GuiElement("StartGame"));
            _guiElements[EMenuState.CharacterScreen].Add(new GuiElement("Back_to_MainMenu"));

            //here are the elements in the state Option
            _guiElements[EMenuState.Option].Add(new GuiElement("Back_to_MainMenu"));

            //here are the elements in the state Credits
            _guiElements[EMenuState.Credits].Add(new GuiElement("Back_to_MainMenu"));
        }

        protected override void LoadContent()
        {
            _font = Game.Content.Load<SpriteFont>("font");
            foreach (var elementList in this._guiElements.Values)
            {
                foreach (var element in elementList)
                {
                    element.LoadContent(this.Game.Content);
                    element.CenterElement(600, 800);
                    element.ClickEvent += this.OnClick;
                }
            }
            
            _guiElements[EMenuState.TitleScreen].Find(x => x.AssetName == "Logo 2").MoveElement(0, -50); //move the logo up in y-direction
            
            _guiElements[EMenuState.MainMenu].Find(x => x.AssetName == "MenuFrame").MoveElement(0, -50); // frame Movement
            _guiElements[EMenuState.MainMenu].Find(x => x.AssetName == "NewGame").MoveElement(0, -200); // move the "newgame" button up in y-direction
            _guiElements[EMenuState.MainMenu].Find(x => x.AssetName == "Option").MoveElement(0, -50); // move the "option" button down in y-direction
            _guiElements[EMenuState.MainMenu].Find(x => x.AssetName == "Credits").MoveElement(200 , 50); //move the "credits" button 200 in x-direction and 50 down in y-direction
            _guiElements[EMenuState.MainMenu].Find(x => x.AssetName == "End").MoveElement(0, 100); //move the "end" button down in y-direction
            
            _guiElements[EMenuState.CharacterScreen].Find(x => x.AssetName == "Back_to_MainMenu").MoveElement(0, 100); //move the "Back_to_MainMenu" button down in y-direction
            
            _guiElements[EMenuState.Option].Find(x => x.AssetName == "Back_to_MainMenu").MoveElement(0, 50); // move the "Back_to_MainMenu" button down in y-direction
            
            _guiElements[EMenuState.Credits].Find(x => x.AssetName == "Back_to_MainMenu").MoveElement(0, 50); //  move the "Back_to_MainMenu" button down in y-direction


        }

        public override void Update(GameTime gameTime)
        {
            if (_menuState == EMenuState.TitleScreen)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    _menuState = EMenuState.MainMenu;
            }

            foreach (var element in _guiElements[_menuState])
            {
                element.Update();
            }
        }


        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
         
            if (_menuState == EMenuState.Credits)
            {
                _spriteBatch.DrawString(_font,
                    "Minh Vuong Pham\n" + "Michl Steglich\n" + "Tim Stadelmann\n" + "Tino Nagelmueller\n",
                    new Vector2(300, 100), Color.Black);
            }

            foreach (var element in _guiElements[_menuState])
            {
                element.Draw(_spriteBatch);
            }

            _spriteBatch.End();
        }

        /// <summary>
        ///     when click on the element change to next state
        /// </summary>
        /// <param name="element"></param>
        private void OnClick(string element)
        {
            if (IsPressed) return;

            IsPressed = true;
            if (element == "MenuFrame")
                IsPressed = false;

            if (_menuState == EMenuState.TitleScreen)
            {
                _menuState = EMenuState.MainMenu;
            }

            if (element == "NewGame")
            {
                _menuState = EMenuState.CharacterScreen;
            }

            if (element == "StartGame")
            {
                _menuState = EMenuState.None;
                GoIngame?.Invoke();

            }

            if (element == "Option")
            {
                _menuState = EMenuState.Option;
            }

            if (element == "Credits")
            {
                _menuState = EMenuState.Credits;
            }

            if (element == "End")
            {
                _menuState = EMenuState.TitleScreen;
            }


            if (element == "Back_to_MainMenu")
            {
                _menuState = EMenuState.MainMenu;
            }
        }

        public event GetIngame GoIngame;

        /// <summary>
        ///     MenuState to manage all States
        /// </summary>
        private enum EMenuState
        {
            None,

            TitleScreen,

            MainMenu,

            CharacterScreen,

            Option,

            Credits


        }
    }
}