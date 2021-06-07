using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Press.GameComponents;
using ComponentSetup = Press.GameComponents.Properties.Settings;
using ClientSetup = Press.GameClient.Properties.Settings;

namespace Press.GameClient
{
    public partial class MainWindow : Window
    {
        private GameScene _scene;
            
        public MainWindow()
        {
            InitializeComponent();
            _scene = new GameScene(GameSceneCanvas, ComponentSetup.Default.OpenedMapFilePath);
        }

        private void MainWindowOnKeyDown(object sender, KeyEventArgs e)
        {
            Test.Text = _scene.PointsFromTap(e);
        }
    }
}
