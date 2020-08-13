namespace l01
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
      
        /// </summary>
        private void InitializeComponent()
        {

         
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
           

            this.postroenie = new System.Windows.Forms.ToolStripMenuItem();
            this.фонаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.чайникаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
           
            this.ребраToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
           
            this.SuspendLayout();
            this.timer1.Interval = 30;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            //this.timer2.Interval = 3000000;
            //this.timer2.Tick += new System.EventHandler(this.timer2_Tick);

            this.фонаToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.фонаToolStripMenuItem.ForeColor = System.Drawing.Color.Gray;
            this.фонаToolStripMenuItem.Name = "фонаToolStripMenuItem";
            this.фонаToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.фонаToolStripMenuItem.Text = "Фона";
            //this.фонаToolStripMenuItem.Click += new System.EventHandler(this.фонаToolStripMenuItem_Click);
           

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);       
            this.ClientSize = new System.Drawing.Size(624, 442);
         

            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion
        private System.Windows.Forms.ToolStripMenuItem postroenie;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.ToolStripMenuItem фонаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem чайникаToolStripMenuItem;
      //  private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.ToolStripMenuItem ребраToolStripMenuItem;

      
    }
}

