using System;
using SharpGL;
using SharpGL.SceneGraph.Primitives;

namespace PF1S8v1
{
    /// <summary>
    ///  Klasa enkapsulira OpenGL kod i omogucava njegovo iscrtavanje i azuriranje.
    /// </summary>
    public class World : IDisposable
    {
        #region Atributi
        /// <summary>
        ///	 Scena 1 koja se prikazuje.
        /// </summary>
        private AssimpScene m_sceneFirst;

        /// <summary>
        ///	 Scena 2 koja se prikazuje.
        /// </summary>
        private AssimpScene m_sceneSecond;

        /// <summary>
        ///	Putanja Scena 1 koja se prikazuje.
        /// </summary>
        private String sceneFirstPath;

        /// <summary>
        ///	Putanja Scena 2 koja se prikazuje.
        /// </summary>
        private String sceneSecondPath;


        /// <summary>
        ///	Naziv fajla za Scenu 1 koja se prikazuje.
        /// </summary>
        private String sceneFirstFileName;

        /// <summary>
        ///	Naziv fajla za Scenu 2 koja se prikazuje.
        /// </summary>
        private String sceneSecondFileName;

        /// <summary>
        ///	 Ugao rotacije sveta oko X ose.
        /// </summary>
        private float m_xRotation = 0.0f;

        /// <summary>
        ///	 Ugao rotacije sveta oko Y ose.
        /// </summary>
        private float m_yRotation = 0.0f;

        /// <summary>
        ///	 Udaljenost scene od kamere.
        /// </summary>
        private float m_sceneDistance = 90.0f;

        /// <summary>
        ///	 Sirina OpenGL kontrole u pikselima.
        /// </summary>
        private int m_width;

        /// <summary>
        ///	 Visina OpenGL kontrole u pikselima.
        /// </summary>
        private int m_height;

        #endregion Atributi

        #region Properties

        /// <summary>
        ///	 Scena koja se prikazuje.
        /// </summary>
        public AssimpScene FirstScene
        {
            get { return m_sceneFirst; }
            set { m_sceneFirst = value; }
        }

        public AssimpScene SecondScene
        {
            get { return m_sceneSecond;}
            set { m_sceneSecond = value; }
        }

        /// <summary>
        ///	 Ugao rotacije sveta oko X ose.
        /// </summary>
        public float RotationX
        {
            get { return m_xRotation; }
            set { m_xRotation = value; }
        }

        /// <summary>
        ///	 Ugao rotacije sveta oko Y ose.
        /// </summary>
        public float RotationY
        {
            get { return m_yRotation; }
            set { m_yRotation = value; }
        }

        /// <summary>
        ///	 Udaljenost scene od kamere.
        /// </summary>
        public float SceneDistance
        {
            get { return m_sceneDistance; }
            set { m_sceneDistance = value; }
        }

        /// <summary>
        ///	 Sirina OpenGL kontrole u pikselima.
        /// </summary>
        public int Width
        {
            get { return m_width; }
            set { m_width = value; }
        }

        /// <summary>
        ///	 Visina OpenGL kontrole u pikselima.
        /// </summary>
        public int Height
        {
            get { return m_height; }
            set { m_height = value; }
        }

        #endregion Properties

        #region Konstruktori

        /// <summary>
        ///  Konstruktor klase World.
        /// </summary>
        public World(String scenePath1, String scenePath2, String sceneFileNames, int width, int height, OpenGL gl)
        {

            sceneFirstPath = scenePath1;
            sceneSecondPath = scenePath2;

            string[] fileNames = sceneFileNames.Split(',');
            sceneFirstFileName = fileNames[0];
            sceneSecondFileName = fileNames[1];
            Console.WriteLine(fileNames[0]);
            Console.WriteLine(fileNames[1]);

            this.m_sceneFirst = new AssimpScene(sceneFirstPath, sceneFirstFileName, gl);
            this.m_sceneSecond = new AssimpScene(sceneSecondPath, sceneSecondFileName, gl);
            this.m_width = width;
            this.m_height = height;
        }

        /// <summary>
        ///  Destruktor klase World.
        /// </summary>
        ~World()
        {
            this.Dispose(false);
        }

        #endregion Konstruktori

        #region Metode

        /// <summary>
        ///  Korisnicka inicijalizacija i podesavanje OpenGL parametara.
        /// </summary>
        public void Initialize(OpenGL gl)
        {
            gl.ShadeModel(OpenGL.GL_FLAT);
            
            gl.Enable(OpenGL.GL_DEPTH_TEST); // Activated Z-buffer
            gl.Enable(OpenGL.GL_CULL_FACE);  // glCullFace — specify whether front- or back-facing polygons can be culled, initial value is GL_BACK

            m_sceneFirst.LoadScene();
            m_sceneFirst.Initialize();

            m_sceneSecond.LoadScene();
            m_sceneSecond.Initialize();
        }

        /// <summary>
        ///  Iscrtavanje OpenGL kontrole.
        /// </summary>
        public void Draw(OpenGL gl)
        {
            gl.ClearColor(0.1f, 0.2f, 0.8f, 1.0f);
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.LoadIdentity();
            gl.Translate(0.0f, 0.0f, -m_sceneDistance);
            gl.Rotate(m_xRotation, 0.0f, 0.0f, 0.0f);
            gl.Rotate(m_yRotation, 0.0f, 1.0f, 0.0f);
            
            DrawPodloga(gl);
            DrawStaza(gl);
            DrawZidovi(gl);
            DrawBolid1(gl);
            DrawBolid2(gl);
            DrawText(gl);

            gl.Flush();
        }

        public void DrawPodloga(OpenGL gl)
        { 
            gl.PushMatrix();
            gl.Color(0.8f, 0.9f, 0.0f, 0.0f);
            gl.Translate(0.0f, -9.2f, -5.0f);
            gl.Begin(OpenGL.GL_QUADS);
            gl.Vertex(-35.0f, -5.0f, -15000.0f);
            gl.Vertex(-35.0f, -5.0f, 20.0f);      
            gl.Vertex(35.0f, -5.0f, 20.0f);
            gl.Vertex(35.0f, -5.0f, -15000.0f);
            gl.End();
            gl.PopMatrix();
        }

        public void DrawStaza(OpenGL gl)
        {
            gl.PushMatrix();
            gl.Color(0.8f, 0.9f, 0.8f, 1.0f); // Gray white color
            gl.Translate(0.0f, -9.0f, -5.0f);
            gl.Begin(OpenGL.GL_QUADS);
            gl.Vertex(-25.0f, -5.0f, -15000.0f);
            gl.Vertex(-25.0f, -5.0f, 20.0f);
            gl.Vertex(25.0f, -5.0f, 20.0f);
            gl.Vertex(25.0f, -5.0f, -15000.0f);
            gl.End();
            gl.PopMatrix();
        }

        public void DrawZidovi(OpenGL gl)
        {
            Cube cube = new Cube();
            
            //right wall
            gl.PushMatrix();
            gl.Color(0.3f, 0.5f, 0.6f);
            gl.Translate(29.0f, -12.0f, -16000.0f);
            gl.Scale(3.0f, 3.0f, 16000.0f);
            gl.Translate(0.0f, 0.0f, 0.0f);
            cube.Render(gl, SharpGL.SceneGraph.Core.RenderMode.Render);
            gl.PopMatrix();
            
            // left wall
            gl.PushMatrix();
            gl.Color(0.3f, 0.5f, 0.6f);
            gl.Translate(-29.0f, -12.0f, -16000.0f);
            gl.Scale(3.0f, 3.0f, 16000.0f);
            gl.Translate(0.0f, 0.0f, 0.0f);
            cube.Render(gl, SharpGL.SceneGraph.Core.RenderMode.Render);
            gl.PopMatrix();

        }

        public void DrawBolid1(OpenGL gl)
        {
            gl.PushMatrix();
            gl.Translate(-9.0f, -10.0f, -7.0f);
            gl.Rotate(0.0f, -5.0f, 0.0f);
            gl.Scale(6.0f, 6.0f, 6.0f);
            gl.Translate(0.0f, 0.0f, 0.0f);
            m_sceneFirst.Draw();
            gl.PopMatrix();
        }

        public void DrawBolid2(OpenGL gl)
        {
            gl.PushMatrix();
            gl.Translate(12.0f, -14.0f, -6.0f);
            gl.Rotate(0.0f, 90.0f, 0.0f);
            gl.Scale(5.0f, 5.0f, 5.0f);
            gl.Translate(0.0f, 0.0f, 0.0f);
            m_sceneSecond.Draw();
            gl.PopMatrix();
        }


        public void DrawText(OpenGL gl)
        {
            gl.PushMatrix();
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            //Font Arial, 14 pt, underline, 2D text in light blue(cyan) in the lower right corner of the window 
            gl.Viewport(m_width * 3 / 4, 0, m_width / 4, m_height / 2);
            gl.DrawText(750, 120, 0.0f, 1.0f, 1.0f, "Courier New", 14, " ");
            gl.DrawText(750, 100, 0.0f, 1.0f, 1.0f, "Arial UNDERLINE", 14, "Predmet: Racunarska grafika");
            gl.DrawText(750, 80, 0.0f, 1.0f, 1.0f, "Arial UNDERLINE", 14, "Sk.god: 2020/21.");
            gl.DrawText(750, 60, 0.0f, 1.0f, 1.0f, "Arial UNDERLINE", 14, "Ime: Luka");
            gl.DrawText(750, 40, 0.0f, 1.0f, 1.0f, "Arial UNDERLINE", 14, "Prezime: Petrovic");
            gl.DrawText(750, 20, 0.0f, 1.0f, 1.0f, "Arial UNDERLINE", 14, "Sifra zad: 8.1");
            gl.PopMatrix();
        }

        /// <summary>
        /// Podesava viewport i projekciju za OpenGL kontrolu.
        /// </summary>
        public void Resize(OpenGL gl, int width, int height)
        {
            m_width = width;
            m_height = height;
            
            gl.Viewport(0, 0, m_width, m_height);     // create a view through the entire window

            gl.MatrixMode(OpenGL.GL_PROJECTION);      // set the active matrix stack to OpenGL.GL_PROJECTION - to define the projection

            gl.LoadIdentity();

            gl.Perspective(50f, (double)width / height, 1f, 20000f); // projection in perspective fov = 50, near = 1, far = 20,000

            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            
            gl.LoadIdentity();
        }

        /// <summary>
        ///  Implementacija IDisposable interfejsa.
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                m_sceneFirst.Dispose();
            }
        }

        #endregion Metode

        #region IDisposable metode

        /// <summary>
        ///  Dispose metoda.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable metode
    }
}
