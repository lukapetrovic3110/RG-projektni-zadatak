using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using SharpGL.SceneGraph;
using SharpGL;
using Microsoft.Win32;
using Assimp;

namespace PF1S8v1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Atributi

        /// <summary>
        ///	 Instanca OpenGL "sveta" - klase koja je zaduzena za iscrtavanje koriscenjem OpenGL-a.
        /// </summary>
        World m_world = null;
        Animation animation = null;

        #endregion Atributi

        #region Konstruktori

        public MainWindow()
        {
            // Inicijalizacija komponenti
            InitializeComponent();

            // Kreiranje OpenGL sveta
            try
            {
               
                m_world = new World(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Formula 1 3D Models\\Bolid1"), Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Formula 1 3D Models\\Car2"), "Bolid1.3ds,Car tasergal xform N200214.3DS", (int)openGLControl.ActualWidth, (int)openGLControl.ActualHeight, openGLControl.OpenGL);

                animation = new Animation(m_world);
                DataContext = animation;
            }
            catch (Exception e)
            {
                MessageBox.Show("Neuspesno kreirana instanca OpenGL sveta. Poruka greške: " + e.Message, "Poruka", MessageBoxButton.OK);
                this.Close();
            }
        }

        #endregion Konstruktori

        /// <summary>
        /// Handles the OpenGLDraw event of the openGLControl1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="SharpGL.SceneGraph.OpenGLEventArgs"/> instance containing the event data.</param>
        private void openGLControl_OpenGLDraw(object sender, OpenGLEventArgs args)
        {
            // Iscrtavanje OpenGL sveta
            m_world.Draw(args.OpenGL);
        }

        /// <summary>
        /// Handles the OpenGLInitialized event of the openGLControl1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="SharpGL.SceneGraph.OpenGLEventArgs"/> instance containing the event data.</param>
        private void openGLControl_OpenGLInitialized(object sender, OpenGLEventArgs args)
        {
            m_world.Initialize(args.OpenGL);
        }

        /// <summary>
        /// Handles the Resized event of the openGLControl1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="SharpGL.SceneGraph.OpenGLEventArgs"/> instance containing the event data.</param>
        private void openGLControl_Resized(object sender, OpenGLEventArgs args)
        {
            m_world.Resize(args.OpenGL, (int)openGLControl.Width, (int)openGLControl.Height);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                
                case Key.I:
                    if (animation.AnimationNotActive)
                    {
                        m_world.RotationX -= 5.0f;
                    }
                    break;
                case Key.K:
                    if (animation.AnimationNotActive)
                    {
                        m_world.RotationX += 5.0f;
                    }
                    break;
                case Key.J:
                    if (animation.AnimationNotActive)
                    {
                        m_world.RotationY -= 5.0f;
                    }
                    break;
                case Key.L:
                    if (animation.AnimationNotActive)
                    {
                        m_world.RotationY += 5.0f;
                    }
                    break;
                case Key.Add:
                    if (animation.AnimationNotActive)
                    {
                        m_world.EyeZ -= 10.0f;
                    }
                    break;
                case Key.Subtract:
                    if (animation.AnimationNotActive)
                    {
                        m_world.EyeZ += 10.0f;
                    }
                    break;
                case Key.F4:
                    if (animation.AnimationNotActive)
                    {
                        this.Close(); 
                    }
                    
                    break;
                // dugme za animaciju
                case Key.V: animation.StartAnimation(); break;
            }
        }

        private void ColorSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (m_world != null && animation.AnimationNotActive)
            {
                m_world.AmbientRed = (float)ColorRed.Value;
                m_world.AmbientGreen = (float)ColorGreen.Value;
                m_world.AmbientBlue = (float)ColorBlue.Value;
            }
        }

        private void RotateLeftBolid_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (m_world != null && animation.AnimationNotActive)
                m_world.LeftBolidRotateY = (float)rotateLeftBolid.Value;
        }

        private void TranslateRightCar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (m_world != null && animation.AnimationNotActive)
                m_world.RightCarTranslateX = (float)translateRightCar.Value;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (m_world != null && animation.AnimationNotActive)
            {
                m_world.CamBackAndRefresh();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (m_world != null && animation.AnimationNotActive)
            {
                m_world.CamFront();
            }
        }
    }
}
