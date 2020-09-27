namespace Proj_IceCream
{
    partial class Popup
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
            this.questionLabel = new System.Windows.Forms.Label();
            this.Yesbutton = new System.Windows.Forms.Button();
            this.Nobutton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // questionLabel
            // 
            this.questionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.questionLabel.Location = new System.Drawing.Point(-1, 9);
            this.questionLabel.Name = "questionLabel";
            this.questionLabel.Size = new System.Drawing.Size(635, 49);
            this.questionLabel.TabIndex = 0;
            this.questionLabel.Text = "Popup question";
            this.questionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.questionLabel.Click += new System.EventHandler(this.questionLabel_Click);
            // 
            // Yesbutton
            // 
            this.Yesbutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Yesbutton.Location = new System.Drawing.Point(222, 88);
            this.Yesbutton.Name = "Yesbutton";
            this.Yesbutton.Size = new System.Drawing.Size(81, 35);
            this.Yesbutton.TabIndex = 1;
            this.Yesbutton.Text = "Ano";
            this.Yesbutton.UseVisualStyleBackColor = true;
            this.Yesbutton.Click += new System.EventHandler(this.Yesbutton_Click);
            // 
            // Nobutton
            // 
            this.Nobutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Nobutton.Location = new System.Drawing.Point(309, 88);
            this.Nobutton.Name = "Nobutton";
            this.Nobutton.Size = new System.Drawing.Size(81, 35);
            this.Nobutton.TabIndex = 2;
            this.Nobutton.Text = "Nie";
            this.Nobutton.UseVisualStyleBackColor = true;
            this.Nobutton.Click += new System.EventHandler(this.Nobutton_Click);
            // 
            // Popup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 151);
            this.Controls.Add(this.Nobutton);
            this.Controls.Add(this.Yesbutton);
            this.Controls.Add(this.questionLabel);
            this.MaximumSize = new System.Drawing.Size(650, 190);
            this.MinimumSize = new System.Drawing.Size(650, 190);
            this.Name = "Popup";
            this.Text = "Popup";
            this.Load += new System.EventHandler(this.Popup_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label questionLabel;
        private System.Windows.Forms.Button Yesbutton;
        private System.Windows.Forms.Button Nobutton;
    }
}