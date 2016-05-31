namespace DrzewaDecyzyjne
{
    partial class Drzewa
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
            this.btnWczytaj = new System.Windows.Forms.Button();
            this.rtbSystemDecyzyjny = new System.Windows.Forms.RichTextBox();
            this.gbxSystemDecyzyjny = new System.Windows.Forms.GroupBox();
            this.ofdSystemDecyzyjny = new System.Windows.Forms.OpenFileDialog();
            this.btnBudujDrzewo = new System.Windows.Forms.Button();
            this.tvDrzewoDecyzyjne = new System.Windows.Forms.TreeView();
            this.gbxSystemDecyzyjny.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnWczytaj
            // 
            this.btnWczytaj.Location = new System.Drawing.Point(23, 29);
            this.btnWczytaj.Name = "btnWczytaj";
            this.btnWczytaj.Size = new System.Drawing.Size(75, 23);
            this.btnWczytaj.TabIndex = 0;
            this.btnWczytaj.Text = "Wczytaj";
            this.btnWczytaj.UseVisualStyleBackColor = true;
            this.btnWczytaj.Click += new System.EventHandler(this.btnWczytaj_Click);
            // 
            // rtbSystemDecyzyjny
            // 
            this.rtbSystemDecyzyjny.Location = new System.Drawing.Point(23, 69);
            this.rtbSystemDecyzyjny.Name = "rtbSystemDecyzyjny";
            this.rtbSystemDecyzyjny.ReadOnly = true;
            this.rtbSystemDecyzyjny.Size = new System.Drawing.Size(112, 132);
            this.rtbSystemDecyzyjny.TabIndex = 1;
            this.rtbSystemDecyzyjny.Text = "";
            // 
            // gbxSystemDecyzyjny
            // 
            this.gbxSystemDecyzyjny.Controls.Add(this.rtbSystemDecyzyjny);
            this.gbxSystemDecyzyjny.Controls.Add(this.btnWczytaj);
            this.gbxSystemDecyzyjny.Location = new System.Drawing.Point(12, 12);
            this.gbxSystemDecyzyjny.Name = "gbxSystemDecyzyjny";
            this.gbxSystemDecyzyjny.Size = new System.Drawing.Size(166, 227);
            this.gbxSystemDecyzyjny.TabIndex = 2;
            this.gbxSystemDecyzyjny.TabStop = false;
            this.gbxSystemDecyzyjny.Text = "System Decyzyjny";
            // 
            // ofdSystemDecyzyjny
            // 
            this.ofdSystemDecyzyjny.FileName = "System Decyzyjny";
            // 
            // btnBudujDrzewo
            // 
            this.btnBudujDrzewo.Location = new System.Drawing.Point(207, 21);
            this.btnBudujDrzewo.Name = "btnBudujDrzewo";
            this.btnBudujDrzewo.Size = new System.Drawing.Size(108, 23);
            this.btnBudujDrzewo.TabIndex = 3;
            this.btnBudujDrzewo.Text = "Buduj drzewo";
            this.btnBudujDrzewo.UseVisualStyleBackColor = true;
            this.btnBudujDrzewo.Click += new System.EventHandler(this.btnBudujDrzewo_Click);
            // 
            // tvDrzewoDecyzyjne
            // 
            this.tvDrzewoDecyzyjne.BackColor = System.Drawing.SystemColors.Menu;
            this.tvDrzewoDecyzyjne.Location = new System.Drawing.Point(207, 81);
            this.tvDrzewoDecyzyjne.Name = "tvDrzewoDecyzyjne";
            this.tvDrzewoDecyzyjne.Size = new System.Drawing.Size(277, 276);
            this.tvDrzewoDecyzyjne.TabIndex = 5;
            // 
            // Drzewa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 385);
            this.Controls.Add(this.tvDrzewoDecyzyjne);
            this.Controls.Add(this.btnBudujDrzewo);
            this.Controls.Add(this.gbxSystemDecyzyjny);
            this.Name = "Drzewa";
            this.Text = "Drzewa decyzyjne";
            this.gbxSystemDecyzyjny.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnWczytaj;
        private System.Windows.Forms.RichTextBox rtbSystemDecyzyjny;
        private System.Windows.Forms.GroupBox gbxSystemDecyzyjny;
        private System.Windows.Forms.OpenFileDialog ofdSystemDecyzyjny;
        private System.Windows.Forms.Button btnBudujDrzewo;
        private System.Windows.Forms.TreeView tvDrzewoDecyzyjne;
    }
}

