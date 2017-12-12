using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Deeparteffects.Sdk;
using Deeparteffects.Sdk.Model;
using System.Threading;
using System.Net;
using System.IO;
using System.Drawing.Imaging;

namespace DoodleMaster
{
    enum ToolType
    {
        Pen,
        Eraser
    }

    public partial class Form1 : Form
    {
        Bitmap canvas;
        Graphics graphics;
        Pen pen;
        bool _MouseDown = false;
        ToolType tool = ToolType.Pen;

        List<Point> points = new List<Point>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            canvas = new Bitmap(picCanvas.Size.Width, picCanvas.Size.Height);
            //canvas = new Bitmap((Bitmap)Bitmap.FromFile("pa.png"), picCanvas.Size);
            graphics = Graphics.FromImage(canvas);
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            pen = new Pen(Color.Red, 5);

            ColorPiker.Visible = false;

            Console.WriteLine(StyleImage(File.ReadAllText("image.txt"), "01b08507-1b89-11e7-afe2-06d95fe194ed"));
        }

        Point prev = new Point(-1, -1);
        Point prev2 = new Point(-1, -1);

        private bool EmptyPt(Point p)
        {
            return p.X == -1 && p.Y == -1;
        }

        public string StyleImage(string hex, string style)
        {
            DeepArtEffectsClient c = new DeepArtEffectsClient("AKIAJWQH3DJ2DRD3CAJQ", "M8YdE1zwBcflStUxvwSmw3lcKRFNY2GUzlJX3r5b", "wkR3Ndn3sx4PyCfhx0ByQaGiLanfOazu6hSNmh1z");
            UploadRequest req = new UploadRequest();
            req.StyleId = style;
            req.ImageBase64Encoded = hex;
            var a = c.uploadImage(req);
            var b = c.getResult(a.SubmissionId);
            while (b.Status.ToUpper().Equals("PROCESSING"))
            {
                Console.WriteLine("Processing");
                System.Threading.Thread.Sleep(1000);
                b = c.getResult(a.SubmissionId);
            }
            Console.WriteLine("Finished");
            return b.Url;
        }

        private void timerClock_Tick(object sender, EventArgs e)
        {
            lblClock.Text = DateTime.Now.ToShortTimeString();
        }

        private void picCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            int x = picCanvas.PointToClient(Cursor.Position).X;
            int y = picCanvas.PointToClient(Cursor.Position).Y;
            prev2 = prev = new Point(x, y);
            points.Add(prev);
            points.Add(prev);
            Console.WriteLine("Click at " + prev.ToString());
            ColorPiker.Visible = false;
            graphics.FillEllipse(pen.Brush, new Rectangle(prev.X - 1, prev.Y - 1, (int)pen.Width, (int)pen.Width));
            _MouseDown = true;
        }

        private void picCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            _MouseDown = false;
            prev = new Point(-1, -1);
            prev2 = new Point(-1, -1);
        }

        private void paintTimer_Tick(object sender, EventArgs e)
        {
            if (_MouseDown)
            {
                int x = picCanvas.PointToClient(Cursor.Position).X;
                int y = picCanvas.PointToClient(Cursor.Position).Y;
                if (Math.Abs(y - prev.Y) < 1 && Math.Abs(x - prev.X) < 1)
                    return;
                prev2 = prev;
                prev = new Point(x, y);
                points.Add(prev);
                graphics.DrawCurve(pen, points.ToArray());
            }
            else
            {
                points.Clear();
            }
            picCanvas.Image = canvas;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tool == ToolType.Pen)
            {
                AnimationGB.Animate(ColorPiker, AnimationGB.Effect.Slide, 150, 180);
            }
            else
            {
                tool = ToolType.Pen;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ColorPiker.Visible = false;
            pen.Color = Color.White;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            ColorPiker.Visible = false;
            pen.Color = Color.Yellow;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            ColorPiker.Visible = false;
            pen.Color = Color.Orange;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            ColorPiker.Visible = false;
            pen.Color = Color.Green;
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            ColorPiker.Visible = false;
            pen.Color = Color.Red;
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            ColorPiker.Visible = false;
            pen.Color = Color.Purple;
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            ColorPiker.Visible = false;
            pen.Color = Color.Blue;
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            ColorPiker.Visible = false;
            pen.Color = Color.Pink;
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            ColorPiker.Visible = false;
            pen.Color = Color.Black;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            pen.Width = trackBar1.Value + 1;
        }

        public string ToBase64(Bitmap b)
        {
            canvas.Save("temp.jpg", ImageFormat.Jpeg);
            byte[] byteArray = File.ReadAllBytes(@"C:\Users\yotam\Pictures\_404.png");
            string base64String = Convert.ToBase64String(byteArray);
            return base64String;
        }

        public static Bitmap GetBitmapFromHttp(string url)
        {
            WebClient wc = new WebClient();
            Stream s = wc.OpenRead(url);
            Bitmap bmp = new Bitmap(s);
            return bmp;
        }

        int styleid = 0;

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            paintTimer.Enabled = false;
            
            Bitmap b = new Bitmap((Bitmap)Bitmap.FromFile("styled" + styleid++ + ".jpg"), canvas.Size);
            new Thread(() =>
            {
                List<Bitmap> layers = new List<Bitmap>();
                for (int i = 0; i < 20; i++)
                {
                    Bitmap m = new Bitmap(canvas.Size.Width, canvas.Size.Height);
                    for (int x = 0; x < m.Size.Width; x++)
                    {
                        for (int y = 0; y < m.Size.Height; y++)
                        {
                            m.SetPixel(x, y, y < i * 0.05 * m.Size.Height ? b.GetPixel(x, y) : ((Bitmap)picCanvas.Image).GetPixel(x, y));
                        }
                    }
                    layers.Add(m);
                }

                for (int i = 0; i < 20; i++)
                {
                    picCanvas.Image = layers[i];
                    Thread.Sleep(50);
                }
                picCanvas.Image = b;
            }).Start(); ;
        }
    }
}
