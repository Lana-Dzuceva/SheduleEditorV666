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
            this.buttonSave = new System.Windows.Forms.Button();
            columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Учителя внутри";
            columnHeader1.Width = 163;
            // 
            // listViewIn
            // 
            this.listViewIn.AllowDrop = true;
            this.listViewIn.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnHeader1});
            this.listViewIn.HideSelection = false;
            this.listViewIn.Location = new System.Drawing.Point(41, 11);
            this.listViewIn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.listViewIn.MultiSelect = false;
            this.listViewIn.Name = "listViewIn";
            this.listViewIn.Size = new System.Drawing.Size(200, 300);
            this.listViewIn.TabIndex = 0;
            this.listViewIn.UseCompatibleStateImageBehavior = false;
            this.listViewIn.View = System.Windows.Forms.View.Details;
            this.listViewIn.DragDrop += new System.Windows.Forms.DragEventHandler(this.listView_DragDrop);
            this.listViewIn.DragEnter += new System.Windows.Forms.DragEventHandler(this.listView_DragEnter);
            this.listViewIn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listView_MouseDown);
            // 
            // listViewOut
            // 
            this.listViewOut.AllowDrop = true;
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
            this.listViewOut.DragDrop += new System.Windows.Forms.DragEventHandler(this.listView_DragDrop);
            this.listViewOut.DragEnter += new System.Windows.Forms.DragEventHandler(this.listView_DragEnter);
            this.listViewOut.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listView_MouseDown);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Учителя снаружи";
            this.columnHeader2.Width = 173;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(420, 329);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(130, 27);
            this.buttonSave.TabIndex = 2;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // FormEditTPCell
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.listViewOut);
            this.Controls.Add(this.listViewIn);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FormEditTPCell";
            this.Text = "Редактирование предпочтения";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormEditTPCell_FormClosing);
            this.Load += new System.EventHandler(this.FormEditTPCell_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewIn;
        private System.Windows.Forms.ListView listViewOut;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button buttonSave;
    }
}