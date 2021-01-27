
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
            this.SuspendLayout();
            // 
            // buttonGender
            // 
            this.buttonGender.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonGender.Location = new System.Drawing.Point(60, 42);
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
            this.buttonBarbers.Location = new System.Drawing.Point(206, 43);
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
            this.buttonClients.Location = new System.Drawing.Point(354, 45);
            this.buttonClients.Name = "buttonClients";
            this.buttonClients.Size = new System.Drawing.Size(95, 40);
            this.buttonClients.TabIndex = 2;
            this.buttonClients.Text = "Clients";
            this.buttonClients.UseVisualStyleBackColor = true;
            this.buttonClients.Click += new System.EventHandler(this.buttonClients_Click);
            // 
            // buttonClient
            // 
            this.buttonClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonClient.Location = new System.Drawing.Point(494, 42);
            this.buttonClient.Name = "buttonClient";
            this.buttonClient.Size = new System.Drawing.Size(93, 43);
            this.buttonClient.TabIndex = 3;
            this.buttonClient.Text = "Client";
            this.buttonClient.UseVisualStyleBackColor = true;
            this.buttonClient.Click += new System.EventHandler(this.buttonClient_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(618, 173);
            this.Controls.Add(this.buttonClient);
            this.Controls.Add(this.buttonClients);
            this.Controls.Add(this.buttonBarbers);
            this.Controls.Add(this.buttonGender);
            this.Name = "Form1";
            this.Text = "Barbers";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonGender;
        private System.Windows.Forms.Button buttonBarbers;
        private System.Windows.Forms.Button buttonClients;
        private System.Windows.Forms.Button buttonClient;
    }
}

