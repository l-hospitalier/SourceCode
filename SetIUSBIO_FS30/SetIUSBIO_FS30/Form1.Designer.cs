
namespace SetIUSBIO_FS30
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(166, 505);
            this.button1.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(138, 35);
            this.button1.TabIndex = 0;
            this.button1.Text = "設定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 21);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "アナログ入力数";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 69);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(171, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "内臓プルアップ無効";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 125);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(126, 21);
            this.label3.TabIndex = 3;
            this.label3.Text = "入力ピン設定";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 295);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(157, 21);
            this.label4.TabIndex = 4;
            this.label4.Text = "出力初期値設定";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 485);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 21);
            this.label5.TabIndex = 5;
            this.label5.Text = "PWM";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(166, 18);
            this.textBox1.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(31, 28);
            this.textBox1.TabIndex = 6;
            this.textBox1.Text = "0";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(231, 140);
            this.textBox2.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(102, 28);
            this.textBox2.TabIndex = 7;
            this.textBox2.Text = "00000000";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(231, 178);
            this.textBox3.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(102, 28);
            this.textBox3.TabIndex = 8;
            this.textBox3.Text = "00000000";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(231, 216);
            this.textBox4.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(102, 28);
            this.textBox4.TabIndex = 9;
            this.textBox4.Text = "00000000";
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(231, 254);
            this.textBox5.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(102, 28);
            this.textBox5.TabIndex = 10;
            this.textBox5.Text = "00000000";
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(231, 323);
            this.textBox6.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(102, 28);
            this.textBox6.TabIndex = 11;
            this.textBox6.Text = "00000000";
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(231, 361);
            this.textBox7.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(102, 28);
            this.textBox7.TabIndex = 12;
            this.textBox7.Text = "00000000";
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(231, 399);
            this.textBox8.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(102, 28);
            this.textBox8.TabIndex = 13;
            this.textBox8.Text = "00000000";
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(231, 437);
            this.textBox9.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(102, 28);
            this.textBox9.TabIndex = 14;
            this.textBox9.Text = "00000000";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(202, 65);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(131, 25);
            this.checkBox1.TabIndex = 15;
            this.checkBox1.Text = "ポート2無効";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(202, 90);
            this.checkBox2.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(131, 25);
            this.checkBox2.TabIndex = 16;
            this.checkBox2.Text = "ポート4無効";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(40, 515);
            this.checkBox3.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(71, 25);
            this.checkBox3.TabIndex = 17;
            this.checkBox3.Text = "有効";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(152, 143);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 21);
            this.label6.TabIndex = 18;
            this.label6.Text = "ポート1";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(152, 181);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 21);
            this.label7.TabIndex = 19;
            this.label7.Text = "ポート2";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(152, 219);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 21);
            this.label8.TabIndex = 20;
            this.label8.Text = "ポート3";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(152, 257);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(70, 21);
            this.label9.TabIndex = 21;
            this.label9.Text = "ポート4";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(152, 326);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(70, 21);
            this.label10.TabIndex = 22;
            this.label10.Text = "ポート1";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(152, 364);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(70, 21);
            this.label11.TabIndex = 23;
            this.label11.Text = "ポート2";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(152, 402);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(70, 21);
            this.label12.TabIndex = 24;
            this.label12.Text = "ポート3";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(152, 440);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(70, 21);
            this.label13.TabIndex = 25;
            this.label13.Text = "ポート4";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 555);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.checkBox3);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.textBox9);
            this.Controls.Add(this.textBox8);
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
    }
}

