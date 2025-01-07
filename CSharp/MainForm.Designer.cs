namespace EKArchive
{
    partial class MainForm
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.DatePicker = new System.Windows.Forms.DateTimePicker();
            this.FetchDataButton = new System.Windows.Forms.Button();
            this.DataGrid = new System.Windows.Forms.DataGridView();
            this.Godzina = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Alert = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BusinessDateLabel = new System.Windows.Forms.Label();
            this.SourceDateLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // DatePicker
            // 
            this.DatePicker.Location = new System.Drawing.Point(12, 12);
            this.DatePicker.Name = "DatePicker";
            this.DatePicker.Size = new System.Drawing.Size(200, 20);
            this.DatePicker.TabIndex = 0;
            // 
            // FetchDataButton
            // 
            this.FetchDataButton.Location = new System.Drawing.Point(242, 13);
            this.FetchDataButton.Name = "FetchDataButton";
            this.FetchDataButton.Size = new System.Drawing.Size(98, 23);
            this.FetchDataButton.TabIndex = 1;
            this.FetchDataButton.Text = "Pobierz dane";
            this.FetchDataButton.UseVisualStyleBackColor = true;
            this.FetchDataButton.Click += new System.EventHandler(this.FetchDataButton_Click);
            // 
            // DataGrid
            // 
            this.DataGrid.AllowUserToAddRows = false;
            this.DataGrid.AllowUserToDeleteRows = false;
            this.DataGrid.AllowUserToResizeRows = false;
            this.DataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Godzina,
            this.Alert});
            this.DataGrid.Location = new System.Drawing.Point(12, 100);
            this.DataGrid.Name = "DataGrid";
            this.DataGrid.ReadOnly = true;
            this.DataGrid.Size = new System.Drawing.Size(352, 565);
            this.DataGrid.TabIndex = 2;
            // 
            // Godzina
            // 
            this.Godzina.HeaderText = "Godzina";
            this.Godzina.Name = "Godzina";
            this.Godzina.ReadOnly = true;
            // 
            // Alert
            // 
            this.Alert.HeaderText = "Alert";
            this.Alert.Name = "Alert";
            this.Alert.ReadOnly = true;
            this.Alert.Width = 200;
            // 
            // BusinessDateLabel
            // 
            this.BusinessDateLabel.AutoSize = true;
            this.BusinessDateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.BusinessDateLabel.Location = new System.Drawing.Point(12, 64);
            this.BusinessDateLabel.Name = "BusinessDateLabel";
            this.BusinessDateLabel.Size = new System.Drawing.Size(138, 20);
            this.BusinessDateLabel.TabIndex = 3;
            this.BusinessDateLabel.Text = "Doba handlowa:";
            // 
            // SourceDateLabel
            // 
            this.SourceDateLabel.AutoSize = true;
            this.SourceDateLabel.Location = new System.Drawing.Point(13, 40);
            this.SourceDateLabel.Name = "SourceDateLabel";
            this.SourceDateLabel.Size = new System.Drawing.Size(80, 13);
            this.SourceDateLabel.TabIndex = 4;
            this.SourceDateLabel.Text = "Data publikacji:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 674);
            this.Controls.Add(this.SourceDateLabel);
            this.Controls.Add(this.BusinessDateLabel);
            this.Controls.Add(this.DataGrid);
            this.Controls.Add(this.FetchDataButton);
            this.Controls.Add(this.DatePicker);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Archiwum Energetyczny Kompas";
            ((System.ComponentModel.ISupportInitialize)(this.DataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker DatePicker;
        private System.Windows.Forms.Button FetchDataButton;
        private System.Windows.Forms.DataGridView DataGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Godzina;
        private System.Windows.Forms.DataGridViewTextBoxColumn Alert;
        private System.Windows.Forms.Label BusinessDateLabel;
        private System.Windows.Forms.Label SourceDateLabel;
    }
}

