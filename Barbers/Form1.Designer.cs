
namespace Barbers
{
    partial class Form1
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
            this.buttonGender = new System.Windows.Forms.Button();
            this.buttonBarbers = new System.Windows.Forms.Button();
            this.buttonClients = new System.Windows.Forms.Button();
            this.buttonClient = new System.Windows.Forms.Button();
            this.buttonJournal = new System.Windows.Forms.Button();
            this.buttonReviews = new System.Windows.Forms.Button();
            this.labelMenu = new System.Windows.Forms.Label();
            this.buttonExit = new System.Windows.Forms.Button();
            this.buttonFeedBack = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonGender
            // 
            this.buttonGender.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonGender.Location = new System.Drawing.Point(27, 54);
            this.buttonGender.Name = "buttonGender";
            this.buttonGender.Size = new System.Drawing.Size(95, 43);
            this.buttonGender.TabIndex = 0;
            this.buttonGender.Text = "Genders";
            this.buttonGender.UseVisualStyleBackColor = true;
            this.buttonGender.Click += new System.EventHandler(this.buttonGender_Click);
            // 
            // buttonBarbers
            // 
            this.buttonBarbers.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonBarbers.Location = new System.Drawing.Point(157, 54);
            this.buttonBarbers.Name = "buttonBarbers";
            this.buttonBarbers.Size = new System.Drawing.Size(95, 42);
            this.buttonBarbers.TabIndex = 1;
            this.buttonBarbers.Text = "Barbers";
            this.buttonBarbers.UseVisualStyleBackColor = true;
            this.buttonBarbers.Click += new System.EventHandler(this.buttonBarbers_Click);
            // 
            // buttonClients
            // 
            this.buttonClients.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonClients.Location = new System.Drawing.Point(287, 54);
            this.buttonClients.Name = "buttonClients";
            this.buttonClients.Size = new System.Drawing.Size(95, 43);
            this.buttonClients.TabIndex = 2;
            this.buttonClients.Text = "Clients";
            this.buttonClients.UseVisualStyleBackColor = true;
            this.buttonClients.Click += new System.EventHandler(this.buttonClients_Click);
            // 
            // buttonClient
            // 
            this.buttonClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonClient.Location = new System.Drawing.Point(29, 127);
            this.buttonClient.Name = "buttonClient";
            this.buttonClient.Size = new System.Drawing.Size(93, 43);
            this.buttonClient.TabIndex = 3;
            this.buttonClient.Text = "Client";
            this.buttonClient.UseVisualStyleBackColor = true;
            this.buttonClient.Click += new System.EventHandler(this.buttonClient_Click);
            // 
            // buttonJournal
            // 
            this.buttonJournal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonJournal.Location = new System.Drawing.Point(157, 127);
            this.buttonJournal.Name = "buttonJournal";
            this.buttonJournal.Size = new System.Drawing.Size(95, 43);
            this.buttonJournal.TabIndex = 4;
            this.buttonJournal.Text = "Journal";
            this.buttonJournal.UseVisualStyleBackColor = true;
            this.buttonJournal.Click += new System.EventHandler(this.buttonLINQ_Click);
            // 
            // buttonReviews
            // 
            this.buttonReviews.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonReviews.Location = new System.Drawing.Point(287, 127);
            this.buttonReviews.Name = "buttonReviews";
            this.buttonReviews.Size = new System.Drawing.Size(95, 43);
            this.buttonReviews.TabIndex = 5;
            this.buttonReviews.Text = "Reviews";
            this.buttonReviews.UseVisualStyleBackColor = true;
            this.buttonReviews.Click += new System.EventHandler(this.buttonReviews_Click);
            // 
            // labelMenu
            // 
            this.labelMenu.AutoSize = true;
            this.labelMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelMenu.Location = new System.Drawing.Point(173, 9);
            this.labelMenu.Name = "labelMenu";
            this.labelMenu.Size = new System.Drawing.Size(62, 20);
            this.labelMenu.TabIndex = 6;
            this.labelMenu.Text = "MENU";
            // 
            // buttonExit
            // 
            this.buttonExit.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonExit.Location = new System.Drawing.Point(310, 195);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(72, 34);
            this.buttonExit.TabIndex = 7;
            this.buttonExit.Text = "Exit";
            this.buttonExit.UseVisualStyleBackColor = false;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // buttonFeedBack
            // 
            this.buttonFeedBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonFeedBack.Location = new System.Drawing.Point(91, 195);
            this.buttonFeedBack.Name = "buttonFeedBack";
            this.buttonFeedBack.Size = new System.Drawing.Size(100, 34);
            this.buttonFeedBack.TabIndex = 8;
            this.buttonFeedBack.Text = "FeedBack";
            this.buttonFeedBack.UseVisualStyleBackColor = true;
            this.buttonFeedBack.Click += new System.EventHandler(this.buttonFeedBack_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 241);
            this.Controls.Add(this.buttonFeedBack);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.labelMenu);
            this.Controls.Add(this.buttonReviews);
            this.Controls.Add(this.buttonJournal);
            this.Controls.Add(this.buttonClient);
            this.Controls.Add(this.buttonClients);
            this.Controls.Add(this.buttonBarbers);
            this.Controls.Add(this.buttonGender);
            this.Name = "Form1";
            this.Text = "Barbers";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonGender;
        private System.Windows.Forms.Button buttonBarbers;
        private System.Windows.Forms.Button buttonClients;
        private System.Windows.Forms.Button buttonClient;
        private System.Windows.Forms.Button buttonJournal;
        private System.Windows.Forms.Button buttonReviews;
        private System.Windows.Forms.Label labelMenu;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Button buttonFeedBack;
    }
}

