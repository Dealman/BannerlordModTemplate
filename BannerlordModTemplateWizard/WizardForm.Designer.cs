namespace BannerlordModTemplateWizard
{
    partial class WizardForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.gbBannerlordSettings = new System.Windows.Forms.GroupBox();
            this.bBrowsePath = new System.Windows.Forms.Button();
            this.lBannerlordPath = new System.Windows.Forms.Label();
            this.tbBannerlordPath = new System.Windows.Forms.TextBox();
            this.gbTemplateSettings = new System.Windows.Forms.GroupBox();
            this.cbUseLauncherMods = new System.Windows.Forms.CheckBox();
            this.cbIncludeReadme = new System.Windows.Forms.CheckBox();
            this.cbIncludeSubModule = new System.Windows.Forms.CheckBox();
            this.gbHarmony = new System.Windows.Forms.GroupBox();
            this.cbIncludeHarmony = new System.Windows.Forms.CheckBox();
            this.bConfirm = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.gbBannerlordSettings.SuspendLayout();
            this.gbTemplateSettings.SuspendLayout();
            this.gbHarmony.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(638, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Bannerlord Mod Template Settings";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // gbBannerlordSettings
            // 
            this.gbBannerlordSettings.Controls.Add(this.bBrowsePath);
            this.gbBannerlordSettings.Controls.Add(this.lBannerlordPath);
            this.gbBannerlordSettings.Controls.Add(this.tbBannerlordPath);
            this.gbBannerlordSettings.Location = new System.Drawing.Point(4, 22);
            this.gbBannerlordSettings.Name = "gbBannerlordSettings";
            this.gbBannerlordSettings.Size = new System.Drawing.Size(629, 41);
            this.gbBannerlordSettings.TabIndex = 1;
            this.gbBannerlordSettings.TabStop = false;
            this.gbBannerlordSettings.Text = "Bannerlord";
            // 
            // bBrowsePath
            // 
            this.bBrowsePath.Location = new System.Drawing.Point(547, 13);
            this.bBrowsePath.Name = "bBrowsePath";
            this.bBrowsePath.Size = new System.Drawing.Size(75, 20);
            this.bBrowsePath.TabIndex = 4;
            this.bBrowsePath.Text = "Browse";
            this.bBrowsePath.UseVisualStyleBackColor = true;
            this.bBrowsePath.Click += new System.EventHandler(this.bBrowsePath_Click);
            // 
            // lBannerlordPath
            // 
            this.lBannerlordPath.AutoSize = true;
            this.lBannerlordPath.Location = new System.Drawing.Point(6, 16);
            this.lBannerlordPath.Name = "lBannerlordPath";
            this.lBannerlordPath.Size = new System.Drawing.Size(165, 13);
            this.lBannerlordPath.TabIndex = 4;
            this.lBannerlordPath.Text = "M&B Bannerlord Installation Folder:";
            // 
            // tbBannerlordPath
            // 
            this.tbBannerlordPath.Location = new System.Drawing.Point(177, 13);
            this.tbBannerlordPath.Name = "tbBannerlordPath";
            this.tbBannerlordPath.ReadOnly = true;
            this.tbBannerlordPath.Size = new System.Drawing.Size(364, 20);
            this.tbBannerlordPath.TabIndex = 2;
            this.tbBannerlordPath.TabStop = false;
            // 
            // gbTemplateSettings
            // 
            this.gbTemplateSettings.Controls.Add(this.cbUseLauncherMods);
            this.gbTemplateSettings.Controls.Add(this.cbIncludeReadme);
            this.gbTemplateSettings.Controls.Add(this.cbIncludeSubModule);
            this.gbTemplateSettings.Location = new System.Drawing.Point(4, 69);
            this.gbTemplateSettings.Name = "gbTemplateSettings";
            this.gbTemplateSettings.Size = new System.Drawing.Size(629, 88);
            this.gbTemplateSettings.TabIndex = 7;
            this.gbTemplateSettings.TabStop = false;
            this.gbTemplateSettings.Text = "Mod Template Settings";
            // 
            // cbUseLauncherMods
            // 
            this.cbUseLauncherMods.AutoSize = true;
            this.cbUseLauncherMods.Location = new System.Drawing.Point(9, 65);
            this.cbUseLauncherMods.Name = "cbUseLauncherMods";
            this.cbUseLauncherMods.Size = new System.Drawing.Size(240, 17);
            this.cbUseLauncherMods.TabIndex = 2;
            this.cbUseLauncherMods.Text = "Use Same Mods as Selected in the Launcher";
            this.cbUseLauncherMods.UseVisualStyleBackColor = true;
            // 
            // cbIncludeReadme
            // 
            this.cbIncludeReadme.AutoSize = true;
            this.cbIncludeReadme.Location = new System.Drawing.Point(9, 42);
            this.cbIncludeReadme.Name = "cbIncludeReadme";
            this.cbIncludeReadme.Size = new System.Drawing.Size(302, 17);
            this.cbIncludeReadme.TabIndex = 1;
            this.cbIncludeReadme.Text = "Include Readme File (contains useful information and links)";
            this.cbIncludeReadme.UseVisualStyleBackColor = true;
            // 
            // cbIncludeSubModule
            // 
            this.cbIncludeSubModule.AutoSize = true;
            this.cbIncludeSubModule.Checked = true;
            this.cbIncludeSubModule.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIncludeSubModule.Location = new System.Drawing.Point(9, 19);
            this.cbIncludeSubModule.Name = "cbIncludeSubModule";
            this.cbIncludeSubModule.Size = new System.Drawing.Size(146, 17);
            this.cbIncludeSubModule.TabIndex = 0;
            this.cbIncludeSubModule.Text = "Include SubModule Class";
            this.cbIncludeSubModule.UseVisualStyleBackColor = true;
            // 
            // gbHarmony
            // 
            this.gbHarmony.Controls.Add(this.cbIncludeHarmony);
            this.gbHarmony.Location = new System.Drawing.Point(5, 163);
            this.gbHarmony.Name = "gbHarmony";
            this.gbHarmony.Size = new System.Drawing.Size(628, 44);
            this.gbHarmony.TabIndex = 6;
            this.gbHarmony.TabStop = false;
            this.gbHarmony.Text = "Harmony Settings";
            // 
            // cbIncludeHarmony
            // 
            this.cbIncludeHarmony.AutoSize = true;
            this.cbIncludeHarmony.Checked = true;
            this.cbIncludeHarmony.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIncludeHarmony.Enabled = false;
            this.cbIncludeHarmony.Location = new System.Drawing.Point(9, 19);
            this.cbIncludeHarmony.Name = "cbIncludeHarmony";
            this.cbIncludeHarmony.Size = new System.Drawing.Size(167, 17);
            this.cbIncludeHarmony.TabIndex = 0;
            this.cbIncludeHarmony.Text = "Include HarmonyLib in Project";
            this.cbIncludeHarmony.UseVisualStyleBackColor = true;
            // 
            // bConfirm
            // 
            this.bConfirm.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.bConfirm.Enabled = false;
            this.bConfirm.Location = new System.Drawing.Point(12, 213);
            this.bConfirm.Name = "bConfirm";
            this.bConfirm.Size = new System.Drawing.Size(75, 23);
            this.bConfirm.TabIndex = 8;
            this.bConfirm.Text = "Confirm";
            this.bConfirm.UseVisualStyleBackColor = true;
            this.bConfirm.Click += new System.EventHandler(this.bConfirm_Click);
            // 
            // bCancel
            // 
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancel.Location = new System.Drawing.Point(551, 213);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(75, 23);
            this.bCancel.TabIndex = 9;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // WizardForm
            // 
            this.AcceptButton = this.bConfirm;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bCancel;
            this.ClientSize = new System.Drawing.Size(638, 242);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bConfirm);
            this.Controls.Add(this.gbTemplateSettings);
            this.Controls.Add(this.gbHarmony);
            this.Controls.Add(this.gbBannerlordSettings);
            this.Controls.Add(this.label1);
            this.Name = "WizardForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bannerlord Mod Template Wizard";
            this.Shown += new System.EventHandler(this.WizardForm_Shown);
            this.gbBannerlordSettings.ResumeLayout(false);
            this.gbBannerlordSettings.PerformLayout();
            this.gbTemplateSettings.ResumeLayout(false);
            this.gbTemplateSettings.PerformLayout();
            this.gbHarmony.ResumeLayout(false);
            this.gbHarmony.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbBannerlordSettings;
        private System.Windows.Forms.Button bBrowsePath;
        private System.Windows.Forms.Label lBannerlordPath;
        private System.Windows.Forms.TextBox tbBannerlordPath;
        private System.Windows.Forms.GroupBox gbTemplateSettings;
        private System.Windows.Forms.CheckBox cbIncludeReadme;
        private System.Windows.Forms.CheckBox cbIncludeSubModule;
        private System.Windows.Forms.GroupBox gbHarmony;
        private System.Windows.Forms.CheckBox cbIncludeHarmony;
        private System.Windows.Forms.Button bConfirm;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.CheckBox cbUseLauncherMods;
    }
}