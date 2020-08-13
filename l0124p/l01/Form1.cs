using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace l01
{
    public partial class Form1 : Form
    {
        Device dr; //3D устройство
        PresentParameters ppr = new PresentParameters(); //Параметры 3D устройства
        Mesh myp, myp1, myp2, myp3; //Mesh объект
        Material matmyp; //Материал для Mesh объекта

        Vector3 cp = new Vector3(10, 0, 0), cp0, ct = new Vector3(0, 0, 0), ct0, cup = new Vector3(0, 0, 1), cup0;
        Point cur0; //Положение указателя при нажатии ПКМ
        float pi = (float)Math.PI;

        float sx = 1.0f, sy = 1.0f, sz = 1.0f;
        float angle = 0.0f;


        public Form1()
        {
            InitializeComponent();
        }

        //Инициализация 3D устройства Low
        private bool InitializeGraphicsL()
        {
            try
            {
                ppr.Windowed = true; //Оконный режим
                ppr.SwapEffect = SwapEffect.Discard; //Гарантированный режим переключения заднего буфера
                ppr.EnableAutoDepthStencil = true; //Активация буфера глубины и трафарета
                ppr.AutoDepthStencilFormat = DepthFormat.D16; //Формат буфера глубины и трафарета
                ppr.MultiSample = MultiSampleType.None; //Без сглаживания
                //Попытка создать 3D устройство с программной обработкой вершин
                dr = new Device(0, DeviceType.Hardware, this, CreateFlags.SoftwareVertexProcessing, ppr);
                return true;
            }
            catch
            {
                return false;
            }
        }

        //Инициализация 3D устройства Hi
        private bool InitializeGraphicsH()
        {
            try
            {
                ppr.Windowed = true;
                ppr.SwapEffect = SwapEffect.Discard;
                ppr.EnableAutoDepthStencil = true;
                ppr.AutoDepthStencilFormat = DepthFormat.D24S8;
                ppr.MultiSample = MultiSampleType.FourSamples; //Четвертый уровень сглаживания
                //Попытка создать 3D устройство с аппаратной обработкой вершин
                dr = new Device(0, DeviceType.Hardware, this, CreateFlags.HardwareVertexProcessing, ppr);
                return true;
            }
            catch
            {
                return false;
            }
        }

        //Загрузка формы
        private void Form1_Load(object sender, EventArgs e)
        {
            //Попытка инициализации 3D устройства
            if (!this.InitializeGraphicsH())
            {
                if (!this.InitializeGraphicsL())
                {
                    MessageBox.Show("Невозможно инициализировать Direct3D устройство. Программа будет закрыта.");
                    this.Close();
                }
            }
            myp = Mesh.Cylinder(dr, 0, 0.5f, 2, 20, 1); //конус
            myp1 = Mesh.Sphere(dr, 1, 20, 10); //Сфера
            myp2 = Mesh.Torus(dr, 0.3f, 1.5f, 20, 20); //Тор

            myp3 = Mesh.Cylinder(dr, 1, 1, 2, 20, 1); //цилиндр


            timer1.Enabled = true; //Включение таймера
        }

        //Обзор мышкой
        private void Form1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Vector3 cn0 = ct0 - cp0;
            //Облет вокруг цели
            if (e.Button == MouseButtons.Right)
            {
                float alpha = (float)(e.X - cur0.X) / this.ClientSize.Width * pi * 2;
                float beta = (float)(e.Y - cur0.Y) / this.ClientSize.Height * pi * 2;
                cup = Vector3.TransformCoordinate(cup0, Matrix.RotationAxis(Vector3.Cross(cn0, cup0), -beta));
                cp = ct0 - Vector3.TransformCoordinate(cn0, Matrix.RotationAxis(Vector3.Cross(cn0, cup0), -beta) * Matrix.RotationAxis(cup, -alpha));
            }
            //Панорамирование
            if (e.Button == MouseButtons.Middle)
            {
                float kd = this.ClientSize.Height / Vector3.Length(cp0);
                float dx = (float)(e.X - cur0.X) / kd;
                float dy = (float)(e.Y - cur0.Y) / kd;
                Vector3 dc = cup0 * dy - Vector3.Normalize(Vector3.Cross(cn0, cup0)) * dx;
                ct = ct0 + dc;
                cp = cp0 + dc;
            }
        }




        //Вывод по таймеру
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                float ar = (float)this.ClientSize.Width / (float)this.ClientSize.Height; //Пересчет соотношения сторон
                dr.BeginScene();
                dr.RenderState.CullMode = Cull.None; //Вывод правых и левых треугольников 
                dr.RenderState.ZBufferEnable = true; //Использование буфера глубины
                dr.RenderState.NormalizeNormals = true; //Использование нормализации нормалей
                dr.RenderState.FillMode = (ребраToolStripMenuItem.Text == "Ребра") ? FillMode.Solid : FillMode.WireFrame; //Треугольники/Ребра

                //Настройка источника света
                dr.Lights[0].Type = LightType.Directional; //Тип - параллельный
                dr.Lights[0].Diffuse = System.Drawing.Color.White; //Цвет
                dr.Lights[0].Direction = ct - cp; //Направление
                dr.Lights[0].Enabled = true;

                dr.Clear(ClearFlags.ZBuffer | ClearFlags.Target, фонаToolStripMenuItem.ForeColor, 1f, 0); //Очистка буфера глубины и фона
                dr.Transform.View = Matrix.LookAtRH(cp, ct, cup); //Матрица вида
                dr.Transform.Projection = Matrix.PerspectiveFovRH(pi / 4f, ar, 1f, 200f); //Матрица проекции
                                                                                          //dr.Transform.World = Matrix.RotationX((float)Math.PI / 2f); //Мировая матрица

                dr.Transform.World = Matrix.RotationYawPitchRoll(
                              (float)Math.PI, (float)Math.PI * -2.0f,
                             (float)Math.PI) * Matrix.Translation(0.0f, -4.0f, 1.0f);
                myp.DrawSubset(0);

                dr.Transform.World = Matrix.RotationYawPitchRoll(
                (float)Math.PI, (float)Math.PI * -2.0f,
               (float)Math.PI * 4.0f) * Matrix.Translation(0.0f, -4.0f, 0.0f);
                myp1.DrawSubset(0);



                //dr.Transform.World = Matrix.Scaling(sx, sy, sz) * Matrix.RotationX(11) *
                //    Matrix.Translation(0.0f, 4.0f, 0.0f);
                //sx += 0.01f;
                //sy += 0.01f;
                //sz += 0.01f;
                //if (sx >= 2 || sy >= 2 || sz >= 2)
                //{
                //    dr.Transform.World = Matrix.Scaling(2, 2, 2) * Matrix.RotationX(11) *
                //    Matrix.Translation(0.0f, 4.0f, 0.0f);

                //}

                //myp2.DrawSubset(0);
            




                dr.Transform.World = Matrix.RotationX(angle) * Matrix.Translation(0.0f, 1.0f, 0.0f);
                angle += 0.01f;


                if (angle >= 1.57)
                {
                    float angle1 = 1.57f;
                    dr.Transform.World = Matrix.RotationX(angle1) *
                            Matrix.Translation(0.0f, 1.0f, 0.0f);
                }
                myp3.DrawSubset(0);
                dr.Transform.World = Matrix.Scaling(1, 1, 1) * Matrix.RotationX(11) *
                      Matrix.Translation(0.0f, 4.0f, 0.0f);
                if (angle >= 1.57) {
                    
                    dr.Transform.World = Matrix.Scaling(sx, sy, sz) * Matrix.RotationX(11) *
                        Matrix.Translation(0.0f, 4.0f, 0.0f);

                 


                    sx += 0.01f;
                        sy += 0.01f;
                        sz += 0.01f;
                        if (sx >= 2 || sy >= 2 || sz >= 2)
                        {
                            dr.Transform.World = Matrix.Scaling(2, 2, 2) * Matrix.RotationX(11) *
                            Matrix.Translation(0.0f, 4.0f, 0.0f);
                       


                    }
                }
                myp2.DrawSubset(0);






                dr.EndScene();
                dr.Present();
            }
            catch
            {//gg
                this.Close();
            }
        }

    }
}

