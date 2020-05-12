using Windows.UI.Xaml.Controls;
using java_menace.Physical_Objects;
using java_menace.Economy;
using java_menace.Characters;
using java_menace.Economy.Shop;
using System.Collections.Generic;
using System.Numerics;
using Windows.UI.Xaml.Media.Imaging;
using System;
using System.Diagnostics;
using Windows.UI.Xaml.Navigation;
using java_menace.Characters.Mobs;
using java_menace.Quest;

// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x416

namespace java_menace
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    /// 
    public sealed partial class MainPage : Page
    {
        List<Dictionary<PhysicalObject, Vector2>> Worlds;
        StageManager ManagerStage;
        PlayableCharacter MainCharacter;

        public MainPage()
        {
            InitializeComponent();
        }

        private void InitializeGame()
        {
            ManagerStage = new StageManager(Stage, GenerateWorlds(), MainCharacter);
            ManagerStage.LoadWorld(0);
        }

        private List<Dictionary<PhysicalObject, Vector2>> GenerateWorlds()
        {
            Worlds = new List<Dictionary<PhysicalObject, Vector2>>()
            {
                new Dictionary<PhysicalObject, Vector2>()
                {
                    { new World("Cimatec0"), new Vector2(0, 0)},
                    { MainCharacter, new Vector2(0, 0)},
                    { new JoliFox(), new Vector2(96, 96)},
                    { new Chest(), new Vector2(144, 144)},
                    { new Yemoja(), new Vector2(0, 96) },
                    { new TruffleMan(15), new Vector2(288, 288) }
                    
                },

                new Dictionary<PhysicalObject, Vector2>()
                {
                    { new World("Cimatec0"), new Vector2(0, 0)},
                    { MainCharacter, new Vector2(96, 96)},
                    { new JoliFox(), new Vector2(0, 0)},
                    { new Chest(), new Vector2(288, 288)},
                    { new Yemoja(), new Vector2(0, 96) }
                }
            };

            return Worlds;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var Index = (Tuple<int, string>) e.Parameter;

            switch (Index.Item1)
            {
                case 0:
                    MainCharacter = new Linus(Index.Item2);
                    break;

                case 1:
                    MainCharacter = new MrRobot(Index.Item2);
                    break;

                case 2:
                    MainCharacter = new Steve(Index.Item2);
                    break;
            }
        }

        private void Page_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            InitializeGame();
        }
    }
}
