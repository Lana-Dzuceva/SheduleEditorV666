namespace SheduleEditorV6
{
    partial class FormChooseAudience
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
            this.listViewAudienceDescription = new System.Windows.Forms.ListView();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listViewAudienceDescription
            // 
            this.listViewAudienceDescription.FullRowSelect = true;
            this.listViewAudienceDescription.HideSelection = false;
            this.listViewAudienceDescription.Location = new System.Drawing.Point(12, 12);
            this.listViewAudienceDescription.MultiSelect = false;
            this.listViewAudienceDescription.Name = "listViewAudienceDescription";
            this.listViewAudienceDescription.Size = new System.Drawing.Size(748, 219);
            this.listViewAudienceDescription.TabIndex = 0;
            this.listViewAudienceDescription.UseCompatibleStateImageBehavior = false;
            this.listViewAudienceDescription.View = System.Windows.Forms.View.Details;
            this.listViewAudienceDescription.SelectedIndexChanged += new System.EventHandler(this.listViewAudienceDescription_SelectedIndexChanged);
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(295, 253);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(150, 43);
            this.buttonOk.TabIndex = 1;
            this.buttonOk.Text = "Выбрать";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(12, 253);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(150, 43);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // FormChooseAudience
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 324);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.listViewAudienceDescription);
            this.Name = "FormChooseAudience";
            this.Text = "FormChooseAudience";
            this.Load += new System.EventHandler(this.FormChooseAudience_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewAudienceDescription;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
    }
}