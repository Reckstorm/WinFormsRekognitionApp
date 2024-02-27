namespace WinFormsApp;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        Img_pb = new PictureBox();
        Load_btn = new Button();
        Remove_btn = new Button();
        Search_tb = new TextBox();
        Search_lb = new Label();
        Search_btn = new Button();
        ((System.ComponentModel.ISupportInitialize)Img_pb).BeginInit();
        SuspendLayout();
        // 
        // Img_pb
        // 
        Img_pb.BackColor = Color.WhiteSmoke;
        Img_pb.BorderStyle = BorderStyle.FixedSingle;
        Img_pb.Location = new Point(12, 12);
        Img_pb.Name = "Img_pb";
        Img_pb.Size = new Size(661, 426);
        Img_pb.SizeMode = PictureBoxSizeMode.StretchImage;
        Img_pb.TabIndex = 0;
        Img_pb.TabStop = false;
        // 
        // Load_btn
        // 
        Load_btn.Location = new Point(679, 12);
        Load_btn.Name = "Load_btn";
        Load_btn.Size = new Size(190, 29);
        Load_btn.TabIndex = 1;
        Load_btn.Text = "Load img";
        Load_btn.UseVisualStyleBackColor = true;
        Load_btn.Click += Load_btn_Click;
        // 
        // Remove_btn
        // 
        Remove_btn.Location = new Point(679, 47);
        Remove_btn.Name = "Remove_btn";
        Remove_btn.Size = new Size(190, 29);
        Remove_btn.TabIndex = 2;
        Remove_btn.Text = "Remove img";
        Remove_btn.UseVisualStyleBackColor = true;
        Remove_btn.Click += Remove_btn_Click;
        // 
        // Search_tb
        // 
        Search_tb.Location = new Point(679, 102);
        Search_tb.Name = "Search_tb";
        Search_tb.Size = new Size(190, 27);
        Search_tb.TabIndex = 3;
        // 
        // Search_lb
        // 
        Search_lb.AutoSize = true;
        Search_lb.Location = new Point(679, 79);
        Search_lb.Name = "Search_lb";
        Search_lb.Size = new Size(53, 20);
        Search_lb.TabIndex = 4;
        Search_lb.Text = "Search";
        // 
        // Search_btn
        // 
        Search_btn.Location = new Point(679, 135);
        Search_btn.Name = "Search_btn";
        Search_btn.Size = new Size(190, 29);
        Search_btn.TabIndex = 5;
        Search_btn.Text = "Search";
        Search_btn.UseVisualStyleBackColor = true;
        Search_btn.Click += Search_btn_Click;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(881, 450);
        Controls.Add(Search_btn);
        Controls.Add(Search_lb);
        Controls.Add(Search_tb);
        Controls.Add(Remove_btn);
        Controls.Add(Load_btn);
        Controls.Add(Img_pb);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        Name = "Form1";
        Text = "ImgTextReader";
        ((System.ComponentModel.ISupportInitialize)Img_pb).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private PictureBox Img_pb;
    private Button Load_btn;
    private Button Remove_btn;
    private TextBox Search_tb;
    private Label Search_lb;
    private Button Search_btn;
}