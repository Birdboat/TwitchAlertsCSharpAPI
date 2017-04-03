namespace ApplicationAsync
{
    partial class Form1
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
            this.authCodeBox = new System.Windows.Forms.TextBox();
            this.setauthkey = new System.Windows.Forms.Button();
            this.accessUrlButton = new System.Windows.Forms.Button();
            this.accesstokenlabel = new System.Windows.Forms.Label();
            this.getuserinfobutton = new System.Windows.Forms.Button();
            this.userinfotree = new System.Windows.Forms.TreeView();
            this.createdonationbutton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.donationamounttext = new System.Windows.Forms.TextBox();
            this.donationidentifiertext = new System.Windows.Forms.TextBox();
            this.donationnametext = new System.Windows.Forms.TextBox();
            this.donationcombobox = new System.Windows.Forms.ComboBox();
            this.donationmessagetext = new System.Windows.Forms.TextBox();
            this.donationtimepicker = new System.Windows.Forms.DateTimePicker();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.getdonationsbutton = new System.Windows.Forms.Button();
            this.createalertbutton = new System.Windows.Forms.Button();
            this.alerttype = new System.Windows.Forms.ComboBox();
            this.alertmessage = new System.Windows.Forms.TextBox();
            this.alertsoundhref = new System.Windows.Forms.TextBox();
            this.alertimagehref = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.setcolorbutton = new System.Windows.Forms.Button();
            this.getlegacytokenbutton = new System.Windows.Forms.Button();
            this.getlegacytokentext = new System.Windows.Forms.TextBox();
            this.alertduration = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // authCodeBox
            // 
            this.authCodeBox.Location = new System.Drawing.Point(12, 12);
            this.authCodeBox.Name = "authCodeBox";
            this.authCodeBox.Size = new System.Drawing.Size(244, 20);
            this.authCodeBox.TabIndex = 0;
            // 
            // setauthkey
            // 
            this.setauthkey.Location = new System.Drawing.Point(276, 9);
            this.setauthkey.Name = "setauthkey";
            this.setauthkey.Size = new System.Drawing.Size(95, 23);
            this.setauthkey.TabIndex = 1;
            this.setauthkey.Text = "Set Authkey";
            this.setauthkey.UseVisualStyleBackColor = true;
            this.setauthkey.Click += new System.EventHandler(this.setauthkey_Click);
            // 
            // accessUrlButton
            // 
            this.accessUrlButton.Location = new System.Drawing.Point(377, 9);
            this.accessUrlButton.Name = "accessUrlButton";
            this.accessUrlButton.Size = new System.Drawing.Size(95, 23);
            this.accessUrlButton.TabIndex = 2;
            this.accessUrlButton.Text = "Get Access URL";
            this.accessUrlButton.UseVisualStyleBackColor = true;
            this.accessUrlButton.Click += new System.EventHandler(this.accessUrlButton_Click);
            // 
            // accesstokenlabel
            // 
            this.accesstokenlabel.AutoSize = true;
            this.accesstokenlabel.Location = new System.Drawing.Point(12, 38);
            this.accesstokenlabel.Name = "accesstokenlabel";
            this.accesstokenlabel.Size = new System.Drawing.Size(79, 13);
            this.accesstokenlabel.TabIndex = 3;
            this.accesstokenlabel.Text = "Access Token:";
            // 
            // getuserinfobutton
            // 
            this.getuserinfobutton.Location = new System.Drawing.Point(12, 80);
            this.getuserinfobutton.Name = "getuserinfobutton";
            this.getuserinfobutton.Size = new System.Drawing.Size(84, 23);
            this.getuserinfobutton.TabIndex = 5;
            this.getuserinfobutton.Text = "Get User Info";
            this.getuserinfobutton.UseVisualStyleBackColor = true;
            this.getuserinfobutton.Click += new System.EventHandler(this.getuserinfobutton_Click);
            // 
            // userinfotree
            // 
            this.userinfotree.Location = new System.Drawing.Point(12, 109);
            this.userinfotree.Name = "userinfotree";
            this.userinfotree.Size = new System.Drawing.Size(244, 126);
            this.userinfotree.TabIndex = 7;
            this.userinfotree.Tag = "";
            // 
            // createdonationbutton
            // 
            this.createdonationbutton.Location = new System.Drawing.Point(620, 80);
            this.createdonationbutton.Name = "createdonationbutton";
            this.createdonationbutton.Size = new System.Drawing.Size(97, 23);
            this.createdonationbutton.TabIndex = 9;
            this.createdonationbutton.Text = "Create Donation";
            this.createdonationbutton.UseVisualStyleBackColor = true;
            this.createdonationbutton.Click += new System.EventHandler(this.createdonationbutton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(576, 112);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(564, 138);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Identifier:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(522, 164);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Donation Amount:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(561, 218);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Message:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(562, 190);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Currency:";
            // 
            // donationamounttext
            // 
            this.donationamounttext.Location = new System.Drawing.Point(620, 161);
            this.donationamounttext.Name = "donationamounttext";
            this.donationamounttext.Size = new System.Drawing.Size(162, 20);
            this.donationamounttext.TabIndex = 15;
            // 
            // donationidentifiertext
            // 
            this.donationidentifiertext.Location = new System.Drawing.Point(620, 135);
            this.donationidentifiertext.Name = "donationidentifiertext";
            this.donationidentifiertext.Size = new System.Drawing.Size(162, 20);
            this.donationidentifiertext.TabIndex = 16;
            // 
            // donationnametext
            // 
            this.donationnametext.Location = new System.Drawing.Point(620, 109);
            this.donationnametext.Name = "donationnametext";
            this.donationnametext.Size = new System.Drawing.Size(162, 20);
            this.donationnametext.TabIndex = 17;
            // 
            // donationcombobox
            // 
            this.donationcombobox.FormattingEnabled = true;
            this.donationcombobox.Location = new System.Drawing.Point(620, 187);
            this.donationcombobox.Name = "donationcombobox";
            this.donationcombobox.Size = new System.Drawing.Size(162, 21);
            this.donationcombobox.TabIndex = 18;
            // 
            // donationmessagetext
            // 
            this.donationmessagetext.Location = new System.Drawing.Point(620, 215);
            this.donationmessagetext.Name = "donationmessagetext";
            this.donationmessagetext.Size = new System.Drawing.Size(162, 20);
            this.donationmessagetext.TabIndex = 19;
            // 
            // donationtimepicker
            // 
            this.donationtimepicker.Location = new System.Drawing.Point(579, 241);
            this.donationtimepicker.Name = "donationtimepicker";
            this.donationtimepicker.Size = new System.Drawing.Size(203, 20);
            this.donationtimepicker.TabIndex = 21;
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(12, 289);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(244, 150);
            this.treeView1.TabIndex = 22;
            // 
            // getdonationsbutton
            // 
            this.getdonationsbutton.Location = new System.Drawing.Point(12, 260);
            this.getdonationsbutton.Name = "getdonationsbutton";
            this.getdonationsbutton.Size = new System.Drawing.Size(84, 23);
            this.getdonationsbutton.TabIndex = 23;
            this.getdonationsbutton.Text = "Get Donations";
            this.getdonationsbutton.UseVisualStyleBackColor = true;
            this.getdonationsbutton.Click += new System.EventHandler(this.getdonationsbutton_Click);
            // 
            // createalertbutton
            // 
            this.createalertbutton.Location = new System.Drawing.Point(620, 267);
            this.createalertbutton.Name = "createalertbutton";
            this.createalertbutton.Size = new System.Drawing.Size(97, 23);
            this.createalertbutton.TabIndex = 24;
            this.createalertbutton.Text = "Create Alert";
            this.createalertbutton.UseVisualStyleBackColor = true;
            this.createalertbutton.Click += new System.EventHandler(this.createalertbutton_Click);
            // 
            // alerttype
            // 
            this.alerttype.FormattingEnabled = true;
            this.alerttype.Location = new System.Drawing.Point(620, 296);
            this.alerttype.Name = "alerttype";
            this.alerttype.Size = new System.Drawing.Size(162, 21);
            this.alerttype.TabIndex = 25;
            // 
            // alertmessage
            // 
            this.alertmessage.Location = new System.Drawing.Point(620, 375);
            this.alertmessage.Name = "alertmessage";
            this.alertmessage.Size = new System.Drawing.Size(162, 20);
            this.alertmessage.TabIndex = 26;
            // 
            // alertsoundhref
            // 
            this.alertsoundhref.Location = new System.Drawing.Point(620, 349);
            this.alertsoundhref.Name = "alertsoundhref";
            this.alertsoundhref.Size = new System.Drawing.Size(162, 20);
            this.alertsoundhref.TabIndex = 27;
            // 
            // alertimagehref
            // 
            this.alertimagehref.Location = new System.Drawing.Point(620, 323);
            this.alertimagehref.Name = "alertimagehref";
            this.alertimagehref.Size = new System.Drawing.Size(162, 20);
            this.alertimagehref.TabIndex = 28;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(543, 326);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 13);
            this.label6.TabIndex = 29;
            this.label6.Text = "Image HREF:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(576, 304);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 13);
            this.label7.TabIndex = 30;
            this.label7.Text = "Type:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(561, 378);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 13);
            this.label8.TabIndex = 31;
            this.label8.Text = "Message:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(541, 352);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(73, 13);
            this.label9.TabIndex = 32;
            this.label9.Text = "Sound HREF:";
            // 
            // setcolorbutton
            // 
            this.setcolorbutton.Location = new System.Drawing.Point(620, 427);
            this.setcolorbutton.Name = "setcolorbutton";
            this.setcolorbutton.Size = new System.Drawing.Size(97, 23);
            this.setcolorbutton.TabIndex = 35;
            this.setcolorbutton.Text = "Set Color";
            this.setcolorbutton.UseVisualStyleBackColor = true;
            this.setcolorbutton.Click += new System.EventHandler(this.setcolorbutton_Click);
            // 
            // getlegacytokenbutton
            // 
            this.getlegacytokenbutton.Location = new System.Drawing.Point(12, 445);
            this.getlegacytokenbutton.Name = "getlegacytokenbutton";
            this.getlegacytokenbutton.Size = new System.Drawing.Size(108, 23);
            this.getlegacytokenbutton.TabIndex = 36;
            this.getlegacytokenbutton.Text = "Get Legacy Token";
            this.getlegacytokenbutton.UseVisualStyleBackColor = true;
            this.getlegacytokenbutton.Click += new System.EventHandler(this.button1_Click);
            // 
            // getlegacytokentext
            // 
            this.getlegacytokentext.Location = new System.Drawing.Point(126, 447);
            this.getlegacytokentext.Name = "getlegacytokentext";
            this.getlegacytokentext.ReadOnly = true;
            this.getlegacytokentext.Size = new System.Drawing.Size(130, 20);
            this.getlegacytokentext.TabIndex = 37;
            // 
            // alertduration
            // 
            this.alertduration.Location = new System.Drawing.Point(620, 401);
            this.alertduration.Name = "alertduration";
            this.alertduration.Size = new System.Drawing.Size(162, 20);
            this.alertduration.TabIndex = 38;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(564, 404);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(50, 13);
            this.label10.TabIndex = 39;
            this.label10.Text = "Duration:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 513);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.alertduration);
            this.Controls.Add(this.getlegacytokentext);
            this.Controls.Add(this.getlegacytokenbutton);
            this.Controls.Add(this.setcolorbutton);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.alertimagehref);
            this.Controls.Add(this.alertsoundhref);
            this.Controls.Add(this.alertmessage);
            this.Controls.Add(this.alerttype);
            this.Controls.Add(this.createalertbutton);
            this.Controls.Add(this.getdonationsbutton);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.donationtimepicker);
            this.Controls.Add(this.donationmessagetext);
            this.Controls.Add(this.donationcombobox);
            this.Controls.Add(this.donationnametext);
            this.Controls.Add(this.donationidentifiertext);
            this.Controls.Add(this.donationamounttext);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.createdonationbutton);
            this.Controls.Add(this.userinfotree);
            this.Controls.Add(this.getuserinfobutton);
            this.Controls.Add(this.accesstokenlabel);
            this.Controls.Add(this.accessUrlButton);
            this.Controls.Add(this.setauthkey);
            this.Controls.Add(this.authCodeBox);
            this.Name = "Form1";
            this.Text = "Async Application";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox authCodeBox;
        private System.Windows.Forms.Button setauthkey;
        private System.Windows.Forms.Button accessUrlButton;
        private System.Windows.Forms.Label accesstokenlabel;
        private System.Windows.Forms.Button getuserinfobutton;
        private System.Windows.Forms.TreeView userinfotree;
        private System.Windows.Forms.Button createdonationbutton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox donationamounttext;
        private System.Windows.Forms.TextBox donationidentifiertext;
        private System.Windows.Forms.TextBox donationnametext;
        private System.Windows.Forms.ComboBox donationcombobox;
        private System.Windows.Forms.TextBox donationmessagetext;
        private System.Windows.Forms.DateTimePicker donationtimepicker;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Button getdonationsbutton;
        private System.Windows.Forms.Button createalertbutton;
        private System.Windows.Forms.ComboBox alerttype;
        private System.Windows.Forms.TextBox alertmessage;
        private System.Windows.Forms.TextBox alertsoundhref;
        private System.Windows.Forms.TextBox alertimagehref;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button setcolorbutton;
        private System.Windows.Forms.Button getlegacytokenbutton;
        private System.Windows.Forms.TextBox getlegacytokentext;
        private System.Windows.Forms.TextBox alertduration;
        private System.Windows.Forms.Label label10;
    }
}

