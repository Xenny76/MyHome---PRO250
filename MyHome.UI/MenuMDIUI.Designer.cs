namespace MyHome.UI
{
    partial class MenuMDIUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuMDIUI));
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.tslblMdiChildAmount = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslblMdiChildNumber = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.frameWorkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.visualizationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showStatusBarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backupStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslblMdiChildAmount,
            this.tslblMdiChildNumber});
            this.statusStrip.Location = new System.Drawing.Point(0, 419);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(2, 0, 21, 0);
            this.statusStrip.Size = new System.Drawing.Size(992, 32);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip1";
            // 
            // tslblMdiChildAmount
            // 
            this.tslblMdiChildAmount.Name = "tslblMdiChildAmount";
            this.tslblMdiChildAmount.Size = new System.Drawing.Size(139, 25);
            this.tslblMdiChildAmount.Text = "Windows Open:";
            // 
            // tslblMdiChildNumber
            // 
            this.tslblMdiChildNumber.Name = "tslblMdiChildNumber";
            this.tslblMdiChildNumber.Size = new System.Drawing.Size(0, 25);
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainMenuToolStripMenuItem,
            this.frameWorkToolStripMenuItem,
            this.visualizationToolStripMenuItem,
            this.closeAllToolStripMenuItem,
            this.showStatusBarToolStripMenuItem,
            this.backupStripMenuItem,
            this.aboutToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(992, 33);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // frameWorkToolStripMenuItem
            // 
            this.frameWorkToolStripMenuItem.Name = "frameWorkToolStripMenuItem";
            this.frameWorkToolStripMenuItem.Size = new System.Drawing.Size(116, 29);
            this.frameWorkToolStripMenuItem.Text = "Framework";
            // 
            // visualizationToolStripMenuItem
            // 
            this.visualizationToolStripMenuItem.Name = "visualizationToolStripMenuItem";
            this.visualizationToolStripMenuItem.Size = new System.Drawing.Size(126, 29);
            this.visualizationToolStripMenuItem.Text = "Visualization";
            // 
            // closeAllToolStripMenuItem
            // 
            this.closeAllToolStripMenuItem.Name = "closeAllToolStripMenuItem";
            this.closeAllToolStripMenuItem.Size = new System.Drawing.Size(96, 29);
            this.closeAllToolStripMenuItem.Text = "Close All";
            this.closeAllToolStripMenuItem.Click += new System.EventHandler(this.CloseAllToolStripMenuItem_Click);
            // 
            // showStatusBarToolStripMenuItem
            // 
            this.showStatusBarToolStripMenuItem.Checked = true;
            this.showStatusBarToolStripMenuItem.CheckOnClick = true;
            this.showStatusBarToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showStatusBarToolStripMenuItem.Name = "showStatusBarToolStripMenuItem";
            this.showStatusBarToolStripMenuItem.Size = new System.Drawing.Size(155, 29);
            this.showStatusBarToolStripMenuItem.Text = "Show Status Bar";
            this.showStatusBarToolStripMenuItem.Click += new System.EventHandler(this.ShowStatusBarToolStripMenuItem_Click);
            // 
            // backupStripMenuItem
            // 
            this.backupStripMenuItem.Name = "backupStripMenuItem";
            this.backupStripMenuItem.Size = new System.Drawing.Size(85, 29);
            this.backupStripMenuItem.Text = "Backup";
            this.backupStripMenuItem.Click += new System.EventHandler(this.BackupStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(78, 29);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(55, 29);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // mainMenuToolStripMenuItem
            // 
            this.mainMenuToolStripMenuItem.Name = "mainMenuToolStripMenuItem";
            this.mainMenuToolStripMenuItem.Size = new System.Drawing.Size(117, 29);
            this.mainMenuToolStripMenuItem.Text = "Main Menu";
            this.mainMenuToolStripMenuItem.Click += new System.EventHandler(this.mainMenuToolStripMenuItem_Click);
            // 
            // MenuMDIUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::MyHome.UI.Properties.Resources.background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(992, 451);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MenuMDIUI";
            this.Text = "MyHome 2013";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MenuMDIUI_FormClosing);
            this.Load += new System.EventHandler(this.MenuMDIUI_Load);
            this.MdiChildActivate += new System.EventHandler(this.MenuMDIUI_MdiChildActivate);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem frameWorkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showStatusBarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel tslblMdiChildAmount;
        private System.Windows.Forms.ToolStripStatusLabel tslblMdiChildNumber;
        private System.Windows.Forms.ToolStripMenuItem backupStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem visualizationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mainMenuToolStripMenuItem;
    }
}

