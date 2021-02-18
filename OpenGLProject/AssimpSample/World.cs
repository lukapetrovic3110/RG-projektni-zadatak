using System;
using System.Drawing;
using System.Drawing.Imaging;
using SharpGL;
using SharpGL.SceneGraph.Primitives;
using SharpGL.SceneGraph.Quadrics;

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
        ///	 Sirina OpenGL kontrole u pikselima.
        /// </summary>
        private int m_width;

        /// <summary>
        ///	 Visina OpenGL kontrole u pikselima.
        /// </summary>
        private int m_height;

        private float eyeZ = 70.0f;
        private float eyeY = 30.0f;
        private float eyeX = 0.0f;
        private float centerZ = -15.0f;

        private float m_rightCarTranslateX = 5.0f;
        private float m_leftBolidRotateY = 0.0f;
        private float m_leftTranslateZ = 14.0f;
        private float m_rightTranslateZ = 15.0f;
        private float m_lightTranslate = 14.0f;

        private float ambientRed = 1.0f;
        private float ambientGreen = 1.0f;
        private float ambientBlue = 1.0f;

        // private Sphere shadedSphere;


        private enum Textures { Asfalt = 0, Metal = 1, Sljunak = 2 };
        private uint[] m_textures = null;
        private string[] m_textureImages = { "..//..//Textures//asfalt.jpg", "..//..//Textures//metal.jpg", "..//..//Textures//sljunak.jpg" };
        private int m_textureCount = Enum.GetNames(typeof(Textures)).Length;

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
            get { return m_sceneSecond; }
            set { m_sceneSecond = value; }
        }

        /// <summary>
        ///	 Ugao rotacije sveta oko X ose.
        /// </summary>
        public float RotationX
        {
            get { return m_xRotation; }
            set { if (value > 0 && value < 90) m_xRotation = value; }
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

        /// <summary>
        ///	 X pozicija kamere.
        /// </summary>
        public float EyeX
        {
            get { return eyeX; }
            set { eyeX = value; }
        }

        /// <summary>
        ///	 Y pozicija kamere.
        /// </summary>
        public float EyeY
        {
            get { return eyeY; }
            set { eyeY = value; }
        }

        /// <summary>
        ///	 Z pozicija kamere.
        /// </summary>
        public float EyeZ
        {
            get { return eyeZ; }
            set { if (value > 1) eyeZ = value; }
        }

        public float CenterZ
        {
            get { return centerZ; }
            set { centerZ = value; }
        }


        public float RightCarTranslateX
        {
            get { return m_rightCarTranslateX; }
            set { m_rightCarTranslateX = value; }
        }

        public float LeftBolidRotateY
        {
            get { return m_leftBolidRotateY; }
            set { m_leftBolidRotateY = value; }
        }

        public float LeftTranslateZ
        {
            get { return m_leftTranslateZ; }
            set { m_leftTranslateZ = value; }
        }

        public float RightTranslateZ
        {
            get { return m_rightTranslateZ; }
            set { m_rightTranslateZ = value; }
        }

        public float LightTranslate
        {
            get { return m_lightTranslate; }
            set { m_lightTranslate = value; }
        }

        public float AmbientRed
        {
            get { return ambientRed; }
            set { ambientRed = value; }
        }

        public float AmbientGreen
        {
            get { return ambientGreen; }
            set { ambientGreen = value; }
        }

        public float AmbientBlue
        {
            get { return ambientBlue; }
            set { ambientBlue = value; }
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

            m_textures = new uint[m_textureCount];
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

            gl.Enable(OpenGL.GL_COLOR_MATERIAL);
            gl.ColorMaterial(OpenGL.GL_FRONT, OpenGL.GL_AMBIENT_AND_DIFFUSE);
            gl.Enable(OpenGL.GL_NORMALIZE);
            CamFront();
            PodesavanjeTekstura(gl);
            PodesavanjeSvetla(gl);

            m_sceneFirst.LoadScene();
            m_sceneFirst.Initialize();

            m_sceneSecond.LoadScene();
            m_sceneSecond.Initialize();
        }

        public void CamBackAndRefresh()
        {
            eyeZ = 70.0f;
            eyeY = 30.0f;
            eyeX = 0.0f;
            centerZ = -15.0f;

            m_rightCarTranslateX = 5.0f;
            m_leftBolidRotateY = 0.0f;
            m_leftTranslateZ = 14.0f;
            m_rightTranslateZ = 15.0f;
            m_lightTranslate = 14.0f;

            ambientRed = 0.2f;
            ambientGreen = 0.2f;
            ambientBlue = 0.0f;
        }

        public void CamAnimation()
        {
            eyeZ = 36.0f;
            eyeY = 10.0f;
            eyeX = 0.0f;
            centerZ = -15.0f;

            m_rightCarTranslateX = 5.0f;
            m_leftBolidRotateY = 0.0f;
            m_leftTranslateZ = 14.0f;
            m_rightTranslateZ = 15.0f;
            m_lightTranslate = 14.0f;

            ambientRed = 0.2f;
            ambientGreen = 0.2f;
            ambientBlue = 0.0f;
        }

        public void CamFront()
        {
            eyeZ = -60.0f;
            eyeY = 30.0f;
            eyeX = 0.0f;
            centerZ = -15.0f;
       }


        /// <summary>
        ///  Iscrtavanje OpenGL kontrole.
        /// </summary>
        public void Draw(OpenGL gl)
        {
            gl.ClearColor(0.1f, 0.2f, 0.8f, 1.0f);
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.LoadIdentity();
            gl.LookAt(eyeX, eyeY, eyeZ, 0.0f, 0.0f, centerZ, 0.0f, 1.0f, 0.0f);
            gl.Rotate(m_xRotation, 1.0f, 0.0f, 0.0f);
            gl.Rotate(m_yRotation, 0.0f, 1.0f, 0.0f);

            DrawPodloga(gl);
            DrawStaza(gl);
            DrawZidovi(gl);
            DrawBolid1(gl);
            DrawBolid2(gl);
            DrawText(gl);

            gl.Flush();
        }


        public void PodesavanjeTekstura(OpenGL gl)
        {
            gl.Enable(OpenGL.GL_TEXTURE_2D); // omogucavanje upotrebe tekstura
            gl.TexEnv(OpenGL.GL_TEXTURE_ENV, OpenGL.GL_TEXTURE_ENV_MODE, OpenGL.GL_DECAL); //način stapanja teksture sa materijalom GL_DECAL

            gl.GenTextures(m_textureCount, m_textures);
            for (int i = 0; i < m_textureCount; ++i)
            {
                gl.BindTexture(OpenGL.GL_TEXTURE_2D, m_textures[i]);

                Bitmap image = new Bitmap(m_textureImages[i]);
                image.RotateFlip(RotateFlipType.RotateNoneFlipY);

                Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);
                BitmapData imageData = image.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                gl.Build2DMipmaps(OpenGL.GL_TEXTURE_2D, (int)OpenGL.GL_RGBA8, image.Width, image.Height, OpenGL.GL_BGRA, OpenGL.GL_UNSIGNED_BYTE, imageData.Scan0);

                gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MIN_FILTER, OpenGL.GL_LINEAR); //filteri za teksture
                gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MAG_FILTER, OpenGL.GL_LINEAR); //linearno filtriranje
                //wrapping GL_REPEAT po obema osama
                gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_WRAP_S, OpenGL.GL_REPEAT);
                gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_WRAP_T, OpenGL.GL_REPEAT);

                // Posto je kreirana tekstura slika nam vise ne treba
                image.UnlockBits(imageData);
                image.Dispose();
            }
        }


        public void PodesavanjeSvetla(OpenGL gl)
        {
            gl.Enable(OpenGL.GL_LIGHTING);         // ukljucujemo osvetljenje
            gl.Enable(OpenGL.GL_LIGHT0);           // svetlo0 koje se koristi za tackasti izvor
                     
            float[] ambientColor1 = { ambientRed, ambientGreen, ambientBlue, 1.0f };
            float[] diffuseColor = { 0.91f, 0.91f, 0.07f, 1.0f };  // svetlo-zuta boja
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_AMBIENT, ambientColor1);
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_DIFFUSE, diffuseColor);
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_SPOT_CUTOFF, 180.0f); // tackasti izvor
            //gore levo u odnosu na centar scene ODEREDITI POZICIJU
            float[] lightPosition1 = {-20.0f, 30.0f, 0.0f, 1.0f };
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_POSITION, lightPosition1);

       
            gl.Enable(OpenGL.GL_LIGHT1);// svetlo1 koje se koristi kao reflektor
            float[] whiteColor = { 1.0f, 1.0f, 1.0f, 1.0f };
            float[] ambientColor2 = { ambientRed, ambientGreen, ambientBlue, 1.0f };
            float[] lightPosition2 = { 0.0f, 25.0f, -20.0f, 1.0f };
            float[] light_direction = { 0.0f, -1.0f, 0.0f, 1.0f };
            gl.Light(OpenGL.GL_LIGHT1, OpenGL.GL_AMBIENT, ambientColor2);
            gl.Light(OpenGL.GL_LIGHT1, OpenGL.GL_DIFFUSE, whiteColor);
            gl.Light(OpenGL.GL_LIGHT1, OpenGL.GL_POSITION, lightPosition2);
            gl.Light(OpenGL.GL_LIGHT1, OpenGL.GL_SPOT_DIRECTION, light_direction);
            gl.Light(OpenGL.GL_LIGHT1, OpenGL.GL_SPOT_CUTOFF, 45.0f); // cut-off=45
           
        }


        public void DrawPodloga(OpenGL gl)
        {
            gl.Enable(OpenGL.GL_TEXTURE_2D);
            gl.BindTexture(OpenGL.GL_TEXTURE_2D, m_textures[(int)Textures.Sljunak]); //tekstura sljunka
            gl.MatrixMode(OpenGL.GL_TEXTURE);
            gl.Scale(3.0f, 2.0f, 2.0f);
            gl.LoadIdentity();
            gl.MatrixMode(OpenGL.GL_MODELVIEW);

            gl.PushMatrix();
            
            gl.Begin(OpenGL.GL_QUADS);
            gl.Normal(0.0f, 1.0f, 0.0f);
            gl.TexCoord(0.0f, 10.0f);
            gl.Vertex(-18.0f, 0.0f, -50.0f);
            gl.TexCoord(0.0f, 0.0f);
            gl.Vertex(-18.0f, 0.0f, 20.0f);
            gl.TexCoord(10.0f, 0.0f);
            gl.Vertex(18.0f, 0.0f, 20.0f);
            gl.TexCoord(10.0f, 10.0f);
            gl.Vertex(18.0f, 0.0f, -50.0f);
            gl.End();
            gl.PopMatrix();
            gl.Disable(OpenGL.GL_TEXTURE_2D);

        }

        public void DrawStaza(OpenGL gl)
        {
            gl.Enable(OpenGL.GL_TEXTURE_2D);
            gl.BindTexture(OpenGL.GL_TEXTURE_2D, m_textures[(int)Textures.Asfalt]); //tekstura asfalta
            gl.PushMatrix();
            gl.Color(0.8f, 0.9f, 0.8f, 1.0f); // Gray white color
            gl.Translate(0.0f, 0.1f, 0.0f);
            gl.Begin(OpenGL.GL_QUADS);
            gl.Normal(0.0f, 1.0f, 0.0f);
            gl.TexCoord(0.0f, 10.0f);
            gl.Vertex(-12.0f, 0.0f, -50.0f);
            gl.TexCoord(0.0f, 0.0f);
            gl.Vertex(-12.0f, 0.0f, 20.0f);
            gl.TexCoord(10.0f, 0.0f);
            gl.Vertex(12.0f, 0.0f, 20.0f);
            gl.TexCoord(10.0f, 10.0f);
            gl.Vertex(12.0f, 0.0f, -50.0f);
            gl.End();
            gl.PopMatrix();

            gl.Disable(OpenGL.GL_TEXTURE_2D);
        }

        public void DrawZidovi(OpenGL gl)
        {
            gl.Enable(OpenGL.GL_TEXTURE_2D);
            gl.BindTexture(OpenGL.GL_TEXTURE_2D, m_textures[(int)Textures.Metal]);
            Cube cube = new Cube();
            //right wall
            gl.PushMatrix();
            gl.Color(1.0f, 0.5f, 0.6f);
            gl.Translate(12.0f, 2.1f, -15.0f);
            gl.Scale(1.5f, 1.5f, 35.0f);
            gl.Translate(0.0f, 0.0f, 0.0f);
            cube.Render(gl, SharpGL.SceneGraph.Core.RenderMode.Render);
            gl.PopMatrix();
            // left wall
            gl.PushMatrix();
            gl.Color(1.0f, 0.5f, 0.6f);
            gl.Translate(-12.0f, 2.1f, -15.0f);
            gl.Scale(1.5f, 1.5f, 35.0f);
            gl.Translate(0.0f, 0.0f, 0.0f);
            cube.Render(gl, SharpGL.SceneGraph.Core.RenderMode.Render);
            gl.PopMatrix();
            gl.Disable(OpenGL.GL_TEXTURE_2D);

        }

        public void DrawBolid2(OpenGL gl)
        {
            gl.Enable(OpenGL.GL_TEXTURE_2D); // omogucavanje upotrebe tekstura
            gl.TexEnv(OpenGL.GL_TEXTURE_ENV, OpenGL.GL_TEXTURE_ENV_MODE, OpenGL.GL_MODULATE);
            gl.PushMatrix();
            gl.Translate(m_rightCarTranslateX, 1.1f, m_rightTranslateZ);
            gl.Scale(0.003f, 0.003f, 0.003f);
            gl.Rotate(0.0f, -180.0f, 0.0f);
            m_sceneSecond.Draw();
            gl.PopMatrix();
            gl.TexEnv(OpenGL.GL_TEXTURE_ENV, OpenGL.GL_TEXTURE_ENV_MODE, OpenGL.GL_DECAL);
            gl.Disable(OpenGL.GL_TEXTURE_2D);
        }

        public void DrawText(OpenGL gl)
        {
            gl.PushMatrix();
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            //Font Arial, 14 pt, underline, 2D text in light blue(cyan) in the lower right corner of the window 
            gl.Viewport(m_width * 3 / 4, 0, m_width / 4, m_height / 2);
            gl.DrawText(540, 120, 0.0f, 1.0f, 1.0f, "Courier New", 14, " ");
            gl.DrawText(540, 100, 0.0f, 1.0f, 1.0f, "Arial UNDERLINE", 14, "Predmet: Racunarska grafika");
            gl.DrawText(540, 80, 0.0f, 1.0f, 1.0f, "Arial UNDERLINE", 14, "Sk.god: 2020/21.");
            gl.DrawText(540, 60, 0.0f, 1.0f, 1.0f, "Arial UNDERLINE", 14, "Ime: Luka");
            gl.DrawText(540, 40, 0.0f, 1.0f, 1.0f, "Arial UNDERLINE", 14, "Prezime: Petrovic");
            gl.DrawText(540, 20, 0.0f, 1.0f, 1.0f, "Arial UNDERLINE", 14, "Sifra zad: 8.1");
            gl.PopMatrix();
        }


        public void DrawBolid1(OpenGL gl)
        {
            //  gl.Enable(OpenGL.GL_TEXTURE_2D); // omogucavanje upotrebe tekstura
            //  gl.TexEnv(OpenGL.GL_TEXTURE_ENV, OpenGL.GL_TEXTURE_ENV_MODE, OpenGL.GL_MODULATE);
            gl.PushMatrix();
            gl.Translate(-4.0f, 2.0f, m_leftTranslateZ);
            gl.Scale(2.0f, 2.0f, 2.0f);
            gl.Rotate(0.0f, m_leftBolidRotateY, 0.0f);
            m_sceneFirst.Draw();
            gl.PopMatrix();
            // gl.TexEnv(OpenGL.GL_TEXTURE_ENV, OpenGL.GL_TEXTURE_ENV_MODE, OpenGL.GL_DECAL);
            // gl.Disable(OpenGL.GL_TEXTURE_2D);
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
