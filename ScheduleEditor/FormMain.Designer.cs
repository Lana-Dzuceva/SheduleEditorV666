namespace SheduleEditorV6
{
    partial class FormMain
    {
        /// <summary>
        /// Обязательная переменная конструктора.
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
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dataGridViewSchedule = new System.Windows.Forms.DataGridView();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tabControlGroups = new System.Windows.Forms.TabControl();
            this.listViewErrors = new System.Windows.Forms.ListView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.хммToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.предпочтенияУчителейToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.новоеРасписаниеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UploadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSchedule)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dataGridViewSchedule);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(800, 426);
            this.splitContainer1.SplitterDistance = 441;
            this.splitContainer1.TabIndex = 0;
            // 
            // dataGridViewSchedule
            // 
            this.dataGridViewSchedule.AllowDrop = true;
            this.dataGridViewSchedule.AllowUserToAddRows = false;
            this.dataGridViewSchedule.AllowUserToDeleteRows = false;
            this.dataGridViewSchedule.AllowUserToResizeColumns = false;
            this.dataGridViewSchedule.AllowUserToResizeRows = false;
            this.dataGridViewSchedule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSchedule.ColumnHeadersVisible = false;
            this.dataGridViewSchedule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewSchedule.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewSchedule.Name = "dataGridViewSchedule";
            this.dataGridViewSchedule.ReadOnly = true;
            this.dataGridViewSchedule.RowHeadersWidth = 51;
            this.dataGridViewSchedule.Size = new System.Drawing.Size(441, 426);
            this.dataGridViewSchedule.TabIndex = 0;
            this.dataGridViewSchedule.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewSchedule_CellContentDoubleClick);
            this.dataGridViewSchedule.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewSchedule_CellMouseDoubleClick);
            this.dataGridViewSchedule.DragDrop += new System.Windows.Forms.DragEventHandler(this.dataGridViewSchedule_DragDrop);
            this.dataGridViewSchedule.DragEnter += new System.Windows.Forms.DragEventHandler(this.dataGridViewSchedule_DragEnter);
            this.dataGridViewSchedule.DragOver += new System.Windows.Forms.DragEventHandler(this.dataGridViewSchedule_DragOver);
            this.dataGridViewSchedule.DragLeave += new System.EventHandler(this.dataGridViewSchedule_DragLeave);
            this.dataGridViewSchedule.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dataGridViewSchedule_MouseUp);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tabControlGroups);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.listViewErrors);
            this.splitContainer2.Size = new System.Drawing.Size(355, 426);
            this.splitContainer2.SplitterDistance = 288;
            this.splitContainer2.TabIndex = 0;
            // 
            // tabControlGroups
            // 
            this.tabControlGroups.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlGroups.Location = new System.Drawing.Point(0, 0);
            this.tabControlGroups.Name = "tabControlGroups";
            this.tabControlGroups.SelectedIndex = 0;
            this.tabControlGroups.Size = new System.Drawing.Size(355, 288);
            this.tabControlGroups.TabIndex = 0;
            // 
            // listViewErrors
            // 
            this.listViewErrors.BackColor = System.Drawing.Color.White;
            this.listViewErrors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewErrors.HideSelection = false;
            this.listViewErrors.Location = new System.Drawing.Point(0, 0);
            this.listViewErrors.Name = "listViewErrors";
            this.listViewErrors.Size = new System.Drawing.Size(355, 134);
            this.listViewErrors.TabIndex = 2;
            this.toolTip1.SetToolTip(this.listViewErrors, "zdjnv;zdkm");
            this.listViewErrors.UseCompatibleStateImageBehavior = false;
            this.listViewErrors.View = System.Windows.Forms.View.Details;
            this.listViewErrors.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listViewErrors_MouseDown);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.хммToolStripMenuItem,
            this.toolStripMenuItem1,
            this.предпочтенияУчителейToolStripMenuItem,
            this.новоеРасписаниеToolStripMenuItem,
            this.loadToolStripMenuItem,
            this.UploadToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // хммToolStripMenuItem
            // 
            this.хммToolStripMenuItem.Name = "хммToolStripMenuItem";
            this.хммToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.хммToolStripMenuItem.Text = "Хмм";
            this.хммToolStripMenuItem.Click += new System.EventHandler(this.hmmToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(12, 20);
            // 
            // предпочтенияУчителейToolStripMenuItem
            // 
            this.предпочтенияУчителейToolStripMenuItem.Name = "предпочтенияУчителейToolStripMenuItem";
            this.предпочтенияУчителейToolStripMenuItem.Size = new System.Drawing.Size(191, 20);
            this.предпочтенияУчителейToolStripMenuItem.Text = "Предпочтения преподавателей";
            this.предпочтенияУчителейToolStripMenuItem.Click += new System.EventHandler(this.TeacherPreferencesToolStripMenuItem_Click);
            // 
            // новоеРасписаниеToolStripMenuItem
            // 
            this.новоеРасписаниеToolStripMenuItem.Name = "новоеРасписаниеToolStripMenuItem";
            this.новоеРасписаниеToolStripMenuItem.Size = new System.Drawing.Size(122, 20);
            this.новоеРасписаниеToolStripMenuItem.Text = "Новое расписание";
            this.новоеРасписаниеToolStripMenuItem.Click += new System.EventHandler(this.новоеРасписаниеToolStripMenuItem_Click);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(117, 20);
            this.loadToolStripMenuItem.Text = "Загрузить данные";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // UploadToolStripMenuItem
            // 
            this.UploadToolStripMenuItem.Name = "UploadToolStripMenuItem";
            this.UploadToolStripMenuItem.Size = new System.Drawing.Size(120, 20);
            this.UploadToolStripMenuItem.Text = "Выгрузить данные";
            this.UploadToolStripMenuItem.Click += new System.EventHandler(this.UploadToolStripMenuItem_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.Text = "Редактор расписания";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSchedule)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dataGridViewSchedule;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TabControl tabControlGroups;
        private System.Windows.Forms.ListView listViewErrors;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem хммToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem предпочтенияУчителейToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem новоеРасписаниеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem UploadToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}

