using java_menace.Characters;
using java_menace.Movement;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace java_menace.Physical_Objects
{
    class StageManager
    {
        private PlayableCharacter MainCharacter { get; set; }
        private List<PhysicalObject> PhysicalObjects { get; set; }
        private List<Dictionary<PhysicalObject, Vector2>> Worlds { get; set; }
        private CollisionManager ManagerCollision;
        private Grid MainStage;

        public StageManager(Grid MainStage, List<Dictionary<PhysicalObject, Vector2>> Worlds, PlayableCharacter MainCharacter)
        {          
            this.MainStage = MainStage;
            this.Worlds = Worlds;
            ManagerCollision = new CollisionManager();
            PhysicalObjects = new List<PhysicalObject>();
            SetMainCharacter(MainCharacter);
        }

        public void LoadWorld(int WorldNumber)
        {
            while(PhysicalObjects.Count != 0)
            {
                RemovePhysicalObject(PhysicalObjects[0]);
            }

            foreach(var Content in Worlds[WorldNumber])
            {
                AddPhysicalObject(Content.Key);
                AddPhysicalObjectToStage(Content.Key, Content.Value);
            }
        }

        private void InitializeCharacter()
        {
            Window.Current.CoreWindow.KeyDown += this.MainCharacter.MovementHandler.KeyHandle;
            AddPhysicalObject(MainCharacter);
            InitializeCharacterNotebook();
        }

        private void InitializeCharacterNotebook()
        {
            Window.Current.CoreWindow.KeyDown += ManageTerminal;

            MainStage.Children.Add(MainCharacter.Notebook.Bash.GUI);
            MainCharacter.Notebook.Bash.GUI.SetValue(Canvas.ZIndexProperty,
                                                       1000);
        }

        public void SetMainCharacter(PlayableCharacter MainCharacter)
        {
            if(this.MainCharacter != null)
            {
                Window.Current.CoreWindow.KeyDown -= this.MainCharacter.MovementHandler.KeyHandle;
            }

            this.MainCharacter = MainCharacter;
            this.MainCharacter.Notebook.Bash.PhysicalObjects = PhysicalObjects;
            InitializeCharacter();
        }

        public void AddPhysicalObject(PhysicalObject Object)
        {
            if (!PhysicalObjects.Contains(Object))
            {
                PhysicalObjects.Add(Object);
            }
        }

        public void AddPhysicalObjectToStage(PhysicalObject Object, Vector2 Location)
        {
            if(!MainStage.Children.Contains(Object.ObjectImage) && PhysicalObjects.Contains(Object))
            {
                MainStage.Children.Add(Object.ObjectImage);

                if (Object.ColliderHandler != null)
                {
                    MainStage.Children.Add(Object.ColliderHandler.BoxCollider);
                    ManagerCollision.AddCollider(Object.ColliderHandler);
                }

                SetLocation(Object, Location.X, Location.Y);
            }
        }

        public void RemovePhysicalObject(PhysicalObject Object)
        {
            if (PhysicalObjects.Contains(Object))
            {
                PhysicalObjects.Remove(Object);

                if (MainStage.Children.Contains(Object.ObjectImage))
                {
                    MainStage.Children.Remove(Object.ObjectImage);

                    if (Object.ColliderHandler != null)
                    {
                        MainStage.Children.Remove(Object.ColliderHandler.BoxCollider);
                        ManagerCollision.RemoveCollider(Object.ColliderHandler);
                    }
                }
            }
        }

        private void SetLocation(PhysicalObject Object, double LocationX, double LocationY)
        {
            Object.ObjectImage.Margin = new Thickness(Object.ObjectImage.Margin.Left + LocationX, Object.ObjectImage.Margin.Top + LocationY,
                                                          Object.ObjectImage.Margin.Right, Object.ObjectImage.Margin.Bottom);
            Object.ObjectImage.SetValue(Canvas.ZIndexProperty,
                                                       Object.ObjectImage.Margin.Top + Object.Adjustment);
            if(Object.ColliderHandler != null)
            {
                SetColliderLocation(Object);
            }
        }

        private void SetColliderLocation(PhysicalObject Object)
        {
            Object.ColliderHandler.BoxCollider.Margin = Object.ObjectImage.Margin;
            Object.ColliderHandler.AdjustColliderLocation(Object.ColliderHandler.AdjustmentX, Object.ColliderHandler.AdjustmentY);
            Object.ColliderHandler.UpdateVerticesCoordinates();
            Object.ColliderHandler.BoxCollider.SetValue(Canvas.ZIndexProperty,
                                                       Object.ColliderHandler.BoxCollider.Margin.Top);      
        }

        private void ManageTerminal(CoreWindow sender, KeyEventArgs e)
        {
            if (e.VirtualKey.Equals(VirtualKey.Tab))
            {
                if (!MainCharacter.Notebook.Bash.IsRunning)
                {
                    MainCharacter.Notebook.Bash.OpenBash();
                    Window.Current.CoreWindow.KeyDown -= this.MainCharacter.MovementHandler.KeyHandle;
                }

                else
                {
                    MainCharacter.Notebook.Bash.CloseBash();
                    Window.Current.CoreWindow.KeyDown += MainCharacter.MovementHandler.KeyHandle;
                }
            }
        }

    }
}
