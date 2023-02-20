namespace SheduleEditorV6
{
    partial class FormEditTPCell
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
            System.Windows.Forms.ColumnHeader columnHeader1;
            this.listViewIn = new System.Windows.Forms.ListView();
            this.listViewOut = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // listViewIn
            // 
            this.listViewIn.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnHeader1});
            this.listViewIn.HideSelection = false;
            this.listViewIn.Location = new System.Drawing.Point(41, 11);
            this.listViewIn.Margin = new System.Windows.Forms.Padding(2);
            this.listViewIn.MultiSelect = false;
            this.listViewIn.Name = "listViewIn";
            this.listViewIn.Size = new System.Drawing.Size(200, 300);
            this.listViewIn.TabIndex = 0;
            this.listViewIn.UseCompatibleStateImageBehavior = false;
            this.listViewIn.View = System.Windows.Forms.View.Details;
            // 
            // listViewOut
            // 
            this.listViewOut.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.listViewOut.HideSelection = false;
            this.listViewOut.Location = new System.Drawing.Point(351, 11);
            this.listViewOut.MultiSelect = false;
            this.listViewOut.Name = "listViewOut";
            this.listViewOut.Size = new System.Drawing.Size(200, 300);
            this.listViewOut.TabIndex = 1;
            this.listViewOut.UseCompatibleStateImageBehavior = false;
            this.listViewOut.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Учителя внутри";
            columnHeader1.Width = 163;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Учителя снаружи";
            this.columnHeader2.Width = 173;
            // 
            // FormEditTPCell
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.listViewOut);
            this.Controls.Add(this.listViewIn);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormEditTPCell";
            this.Text = "FormEditTPCell";
            this.Load += new System.EventHandler(this.FormEditTPCell_Load);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.FormEditTPCell_MouseDoubleClick);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewIn;
        private System.Windows.Forms.ListView listViewOut;
        private System.Windows.Forms.ColumnHeader columnHeader2;
    }
}