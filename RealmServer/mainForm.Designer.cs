namespace RealmServer
{
    partial class mainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.ConsoleInput = new System.Windows.Forms.RichTextBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // ConsoleInput
            // 
            this.ConsoleInput.BackColor = System.Drawing.Color.White;
            this.ConsoleInput.Dock = System.Windows.Forms.DockStyle.Top;
            this.ConsoleInput.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConsoleInput.Location = new System.Drawing.Point(0, 0);
            this.ConsoleInput.Margin = new System.Windows.Forms.Padding(10);
            this.ConsoleInput.Name = "ConsoleInput";
            this.ConsoleInput.ReadOnly = true;
            this.ConsoleInput.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.ConsoleInput.Size = new System.Drawing.Size(844, 329);
            this.ConsoleInput.TabIndex = 8;
            this.ConsoleInput.Text = "";
            // 
            // listView1
            // 
            this.listView1.AllowColumnReorder = true;
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(0, 333);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(353, 154);
            this.listView1.TabIndex = 9;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // mainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(844, 499);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.ConsoleInput);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "mainForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox ConsoleInput;
        private System.Windows.Forms.ListView listView1;
    }
}

