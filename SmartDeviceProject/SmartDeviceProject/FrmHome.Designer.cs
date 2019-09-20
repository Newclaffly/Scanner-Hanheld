namespace SmartDeviceProject
{
    partial class FrmHome
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmHome));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.clear = new System.Windows.Forms.Button();
            this.exit = new System.Windows.Forms.Button();
            this.txtkb = new System.Windows.Forms.TextBox();
            this.txtpi = new System.Windows.Forms.TextBox();
            this.txttag = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // clear
            // 
            this.clear.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.clear.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.clear.Location = new System.Drawing.Point(30, 161);
            this.clear.Name = "clear";
            this.clear.Size = new System.Drawing.Size(71, 20);
            this.clear.TabIndex = 0;
            this.clear.Text = "Clear";
            this.clear.Click += new System.EventHandler(this.clear_Click_1);
            // 
            // exit
            // 
            this.exit.BackColor = System.Drawing.Color.Red;
            this.exit.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.exit.Location = new System.Drawing.Point(126, 161);
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(79, 20);
            this.exit.TabIndex = 1;
            this.exit.Text = "Exit";
            this.exit.Click += new System.EventHandler(this.exit_Click_1);
            // 
            // txtkb
            // 
            this.txtkb.Location = new System.Drawing.Point(88, 35);
            this.txtkb.Name = "txtkb";
            this.txtkb.Size = new System.Drawing.Size(117, 21);
            this.txtkb.TabIndex = 2;
            this.txtkb.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtkb_KeyPress_1);
            // 
            // txtpi
            // 
            this.txtpi.Location = new System.Drawing.Point(88, 83);
            this.txtpi.Name = "txtpi";
            this.txtpi.Size = new System.Drawing.Size(117, 21);
            this.txtpi.TabIndex = 3;
            this.txtpi.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtpi_KeyPress_1);
            // 
            // txttag
            // 
            this.txttag.Location = new System.Drawing.Point(88, 124);
            this.txttag.Name = "txttag";
            this.txttag.Size = new System.Drawing.Size(117, 21);
            this.txttag.TabIndex = 4;
            this.txttag.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txttag_KeyPress_1);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(14, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 20);
            this.label1.Text = "E-KANBAN";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(14, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 20);
            this.label2.Text = "PI-KANBAN";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(49, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 20);
            this.label3.Text = "TAG";
            // 
            // FrmHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txttag);
            this.Controls.Add(this.txtpi);
            this.Controls.Add(this.txtkb);
            this.Controls.Add(this.exit);
            this.Controls.Add(this.clear);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Menu = this.mainMenu1;
            this.Name = "FrmHome";
            this.Text = "FrmHome";
            this.Load += new System.EventHandler(this.FrmHome_Load_1);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button clear;
        private System.Windows.Forms.Button exit;
        private System.Windows.Forms.TextBox txtkb;
        private System.Windows.Forms.TextBox txtpi;
        private System.Windows.Forms.TextBox txttag;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}