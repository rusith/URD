namespace URDTest
{
    partial class Test_form
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
            this.components = new System.ComponentModel.Container();
            this.textbox = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.undoButton = new System.Windows.Forms.Button();
            this.redoButton = new System.Windows.Forms.Button();
            this.display = new System.Windows.Forms.Label();
            this.appendButton = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.addButton = new System.Windows.Forms.Button();
            this.removeButton = new System.Windows.Forms.Button();
            this.list = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // textbox
            // 
            this.textbox.Location = new System.Drawing.Point(6, 20);
            this.textbox.Multiline = true;
            this.textbox.Name = "textbox";
            this.textbox.Size = new System.Drawing.Size(514, 65);
            this.textbox.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // undoButton
            // 
            this.undoButton.Location = new System.Drawing.Point(29, 202);
            this.undoButton.Name = "undoButton";
            this.undoButton.Size = new System.Drawing.Size(63, 28);
            this.undoButton.TabIndex = 2;
            this.undoButton.Text = "undo";
            this.undoButton.UseVisualStyleBackColor = true;
            this.undoButton.Click += new System.EventHandler(this.undoButton_Click);
            // 
            // redoButton
            // 
            this.redoButton.Location = new System.Drawing.Point(116, 202);
            this.redoButton.Name = "redoButton";
            this.redoButton.Size = new System.Drawing.Size(46, 28);
            this.redoButton.TabIndex = 3;
            this.redoButton.Text = "redo";
            this.redoButton.UseVisualStyleBackColor = true;
            this.redoButton.Click += new System.EventHandler(this.redoButton_Click);
            // 
            // display
            // 
            this.display.Location = new System.Drawing.Point(14, 88);
            this.display.Name = "display";
            this.display.Size = new System.Drawing.Size(514, 54);
            this.display.TabIndex = 4;
            // 
            // appendButton
            // 
            this.appendButton.Location = new System.Drawing.Point(526, 20);
            this.appendButton.Name = "appendButton";
            this.appendButton.Size = new System.Drawing.Size(53, 65);
            this.appendButton.TabIndex = 5;
            this.appendButton.Text = "Append";
            this.appendButton.UseVisualStyleBackColor = true;
            this.appendButton.Click += new System.EventHandler(this.appendButton_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(2, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(686, 183);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.appendButton);
            this.tabPage1.Controls.Add(this.textbox);
            this.tabPage1.Controls.Add(this.display);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(678, 157);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "property change";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.list);
            this.tabPage2.Controls.Add(this.removeButton);
            this.tabPage2.Controls.Add(this.addButton);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(678, 157);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "list change";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(212, 12);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(50, 28);
            this.addButton.TabIndex = 2;
            this.addButton.Text = "add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // removeButton
            // 
            this.removeButton.Location = new System.Drawing.Point(212, 46);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(52, 28);
            this.removeButton.TabIndex = 3;
            this.removeButton.Text = "remove";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // list
            // 
            this.list.FormattingEnabled = true;
            this.list.Items.AddRange(new object[] {
            "item 1"});
            this.list.Location = new System.Drawing.Point(6, 12);
            this.list.Name = "list";
            this.list.Size = new System.Drawing.Size(200, 134);
            this.list.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(254, 200);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(434, 34);
            this.label1.TabIndex = 7;
            this.label1.Text = "this is only a little demonstration of basic (property change and list change [ad" +
    "d and remove single item]) operations. you can do more than this with URD ";
            // 
            // Test_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 243);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.undoButton);
            this.Controls.Add(this.redoButton);
            this.Name = "Test_form";
            this.Text = "URD Test ";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textbox;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Button undoButton;
        private System.Windows.Forms.Button redoButton;
        private System.Windows.Forms.Label display;
        private System.Windows.Forms.Button appendButton;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListBox list;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Label label1;
    }
}

