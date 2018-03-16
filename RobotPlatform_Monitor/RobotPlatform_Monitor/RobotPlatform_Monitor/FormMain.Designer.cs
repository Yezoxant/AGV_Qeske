namespace RobotPlatformMonitor
{
    partial class FormMain
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
            this.timer_connect = new System.Windows.Forms.Timer(this.components);
            this.timer_update = new System.Windows.Forms.Timer(this.components);
            this.textBox_fw_version = new System.Windows.Forms.TextBox();
            this.label40 = new System.Windows.Forms.Label();
            this.textBox_hotswap_state = new System.Windows.Forms.TextBox();
            this.label41 = new System.Windows.Forms.Label();
            this.textBox_RC_0 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_RC_1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_RC_2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_RC_3 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_RC_4 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_RC_5 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_RC_6 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox_RC_7 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox_RC_8 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox_RC_9 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox_vesc_fault_left = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBox_vesc_duty_left = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.textBox_vesc_rpm_left = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.textBox_vesc_iq_left = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.textBox_vesc_id_left = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.textBox_vesc_icurrent_left = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.textBox_vesc_mcurrent_left = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.textBox_vesc_tfet_left = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.textBox_vesc_vin_left = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.textBox_vesc_fault_right = new System.Windows.Forms.TextBox();
            this.textBox_vesc_duty_right = new System.Windows.Forms.TextBox();
            this.textBox_vesc_rpm_right = new System.Windows.Forms.TextBox();
            this.textBox_vesc_iq_right = new System.Windows.Forms.TextBox();
            this.textBox_vesc_id_right = new System.Windows.Forms.TextBox();
            this.textBox_vesc_icurrent_right = new System.Windows.Forms.TextBox();
            this.textBox_vesc_mcurrent_right = new System.Windows.Forms.TextBox();
            this.textBox_vesc_tfet_right = new System.Windows.Forms.TextBox();
            this.textBox_vesc_vin_right = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button_control_update = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.button_control_stop = new System.Windows.Forms.Button();
            this.numericUpDown_motor_gas = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_motor_steer = new System.Windows.Forms.NumericUpDown();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_motor_gas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_motor_steer)).BeginInit();
            this.SuspendLayout();
            // 
            // timer_connect
            // 
            this.timer_connect.Interval = 1000;
            this.timer_connect.Tick += new System.EventHandler(this.timer_connect_Tick);
            // 
            // timer_update
            // 
            this.timer_update.Interval = 500;
            this.timer_update.Tick += new System.EventHandler(this.timer_update_Tick);
            // 
            // textBox_fw_version
            // 
            this.textBox_fw_version.Location = new System.Drawing.Point(156, 38);
            this.textBox_fw_version.Name = "textBox_fw_version";
            this.textBox_fw_version.ReadOnly = true;
            this.textBox_fw_version.Size = new System.Drawing.Size(178, 20);
            this.textBox_fw_version.TabIndex = 64;
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(10, 41);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(90, 13);
            this.label40.TabIndex = 63;
            this.label40.Text = "Firmware Version:";
            // 
            // textBox_hotswap_state
            // 
            this.textBox_hotswap_state.BackColor = System.Drawing.SystemColors.Control;
            this.textBox_hotswap_state.ForeColor = System.Drawing.Color.Red;
            this.textBox_hotswap_state.Location = new System.Drawing.Point(156, 12);
            this.textBox_hotswap_state.Name = "textBox_hotswap_state";
            this.textBox_hotswap_state.ReadOnly = true;
            this.textBox_hotswap_state.Size = new System.Drawing.Size(178, 20);
            this.textBox_hotswap_state.TabIndex = 62;
            this.textBox_hotswap_state.Text = "Disconnected";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(10, 15);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(54, 13);
            this.label41.TabIndex = 61;
            this.label41.Text = "Controller:";
            // 
            // textBox_RC_0
            // 
            this.textBox_RC_0.Location = new System.Drawing.Point(90, 41);
            this.textBox_RC_0.Name = "textBox_RC_0";
            this.textBox_RC_0.ReadOnly = true;
            this.textBox_RC_0.Size = new System.Drawing.Size(120, 20);
            this.textBox_RC_0.TabIndex = 66;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 65;
            this.label1.Text = "RC Channel 0:";
            // 
            // textBox_RC_1
            // 
            this.textBox_RC_1.Location = new System.Drawing.Point(90, 67);
            this.textBox_RC_1.Name = "textBox_RC_1";
            this.textBox_RC_1.ReadOnly = true;
            this.textBox_RC_1.Size = new System.Drawing.Size(120, 20);
            this.textBox_RC_1.TabIndex = 68;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 67;
            this.label2.Text = "RC Channel 1:";
            // 
            // textBox_RC_2
            // 
            this.textBox_RC_2.Location = new System.Drawing.Point(90, 93);
            this.textBox_RC_2.Name = "textBox_RC_2";
            this.textBox_RC_2.ReadOnly = true;
            this.textBox_RC_2.Size = new System.Drawing.Size(120, 20);
            this.textBox_RC_2.TabIndex = 70;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 69;
            this.label3.Text = "RC Channel 2:";
            // 
            // textBox_RC_3
            // 
            this.textBox_RC_3.Location = new System.Drawing.Point(90, 119);
            this.textBox_RC_3.Name = "textBox_RC_3";
            this.textBox_RC_3.ReadOnly = true;
            this.textBox_RC_3.Size = new System.Drawing.Size(120, 20);
            this.textBox_RC_3.TabIndex = 72;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 13);
            this.label4.TabIndex = 71;
            this.label4.Text = "RC Channel 3:";
            // 
            // textBox_RC_4
            // 
            this.textBox_RC_4.Location = new System.Drawing.Point(90, 145);
            this.textBox_RC_4.Name = "textBox_RC_4";
            this.textBox_RC_4.ReadOnly = true;
            this.textBox_RC_4.Size = new System.Drawing.Size(120, 20);
            this.textBox_RC_4.TabIndex = 74;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 148);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 13);
            this.label5.TabIndex = 73;
            this.label5.Text = "RC Channel 4:";
            // 
            // textBox_RC_5
            // 
            this.textBox_RC_5.Location = new System.Drawing.Point(90, 171);
            this.textBox_RC_5.Name = "textBox_RC_5";
            this.textBox_RC_5.ReadOnly = true;
            this.textBox_RC_5.Size = new System.Drawing.Size(120, 20);
            this.textBox_RC_5.TabIndex = 76;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 174);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 13);
            this.label6.TabIndex = 75;
            this.label6.Text = "RC Channel 5:";
            // 
            // textBox_RC_6
            // 
            this.textBox_RC_6.Location = new System.Drawing.Point(90, 197);
            this.textBox_RC_6.Name = "textBox_RC_6";
            this.textBox_RC_6.ReadOnly = true;
            this.textBox_RC_6.Size = new System.Drawing.Size(120, 20);
            this.textBox_RC_6.TabIndex = 78;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 200);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(76, 13);
            this.label7.TabIndex = 77;
            this.label7.Text = "RC Channel 6:";
            // 
            // textBox_RC_7
            // 
            this.textBox_RC_7.Location = new System.Drawing.Point(90, 223);
            this.textBox_RC_7.Name = "textBox_RC_7";
            this.textBox_RC_7.ReadOnly = true;
            this.textBox_RC_7.Size = new System.Drawing.Size(120, 20);
            this.textBox_RC_7.TabIndex = 80;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 226);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(76, 13);
            this.label8.TabIndex = 79;
            this.label8.Text = "RC Channel 7:";
            // 
            // textBox_RC_8
            // 
            this.textBox_RC_8.Location = new System.Drawing.Point(90, 249);
            this.textBox_RC_8.Name = "textBox_RC_8";
            this.textBox_RC_8.ReadOnly = true;
            this.textBox_RC_8.Size = new System.Drawing.Size(120, 20);
            this.textBox_RC_8.TabIndex = 82;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 252);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(76, 13);
            this.label9.TabIndex = 81;
            this.label9.Text = "RC Channel 8:";
            // 
            // textBox_RC_9
            // 
            this.textBox_RC_9.Location = new System.Drawing.Point(90, 275);
            this.textBox_RC_9.Name = "textBox_RC_9";
            this.textBox_RC_9.ReadOnly = true;
            this.textBox_RC_9.Size = new System.Drawing.Size(120, 20);
            this.textBox_RC_9.TabIndex = 84;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 278);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(76, 13);
            this.label10.TabIndex = 83;
            this.label10.Text = "RC Channel 9:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label22);
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.textBox_vesc_fault_right);
            this.groupBox1.Controls.Add(this.textBox_vesc_duty_right);
            this.groupBox1.Controls.Add(this.textBox_vesc_rpm_right);
            this.groupBox1.Controls.Add(this.textBox_vesc_iq_right);
            this.groupBox1.Controls.Add(this.textBox_vesc_id_right);
            this.groupBox1.Controls.Add(this.textBox_vesc_icurrent_right);
            this.groupBox1.Controls.Add(this.textBox_vesc_mcurrent_right);
            this.groupBox1.Controls.Add(this.textBox_vesc_tfet_right);
            this.groupBox1.Controls.Add(this.textBox_vesc_vin_right);
            this.groupBox1.Controls.Add(this.textBox_vesc_fault_left);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.textBox_vesc_duty_left);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.textBox_vesc_rpm_left);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.textBox_vesc_iq_left);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.textBox_vesc_id_left);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.textBox_vesc_icurrent_left);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.textBox_vesc_mcurrent_left);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.textBox_vesc_tfet_left);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.textBox_vesc_vin_left);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Location = new System.Drawing.Point(252, 83);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(357, 282);
            this.groupBox1.TabIndex = 85;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Motor Status";
            // 
            // textBox_vesc_fault_left
            // 
            this.textBox_vesc_fault_left.Location = new System.Drawing.Point(94, 249);
            this.textBox_vesc_fault_left.Name = "textBox_vesc_fault_left";
            this.textBox_vesc_fault_left.ReadOnly = true;
            this.textBox_vesc_fault_left.Size = new System.Drawing.Size(120, 20);
            this.textBox_vesc_fault_left.TabIndex = 104;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 252);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(33, 13);
            this.label11.TabIndex = 103;
            this.label11.Text = "Fault:";
            // 
            // textBox_vesc_duty_left
            // 
            this.textBox_vesc_duty_left.Location = new System.Drawing.Point(94, 223);
            this.textBox_vesc_duty_left.Name = "textBox_vesc_duty_left";
            this.textBox_vesc_duty_left.ReadOnly = true;
            this.textBox_vesc_duty_left.Size = new System.Drawing.Size(120, 20);
            this.textBox_vesc_duty_left.TabIndex = 102;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 226);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(60, 13);
            this.label12.TabIndex = 101;
            this.label12.Text = "Duty cycle:";
            // 
            // textBox_vesc_rpm_left
            // 
            this.textBox_vesc_rpm_left.Location = new System.Drawing.Point(94, 197);
            this.textBox_vesc_rpm_left.Name = "textBox_vesc_rpm_left";
            this.textBox_vesc_rpm_left.ReadOnly = true;
            this.textBox_vesc_rpm_left.Size = new System.Drawing.Size(120, 20);
            this.textBox_vesc_rpm_left.TabIndex = 100;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(12, 200);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(32, 13);
            this.label13.TabIndex = 99;
            this.label13.Text = "Rpm:";
            // 
            // textBox_vesc_iq_left
            // 
            this.textBox_vesc_iq_left.Location = new System.Drawing.Point(94, 171);
            this.textBox_vesc_iq_left.Name = "textBox_vesc_iq_left";
            this.textBox_vesc_iq_left.ReadOnly = true;
            this.textBox_vesc_iq_left.Size = new System.Drawing.Size(120, 20);
            this.textBox_vesc_iq_left.TabIndex = 98;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(12, 174);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(19, 13);
            this.label14.TabIndex = 97;
            this.label14.Text = "Iq:";
            // 
            // textBox_vesc_id_left
            // 
            this.textBox_vesc_id_left.Location = new System.Drawing.Point(94, 145);
            this.textBox_vesc_id_left.Name = "textBox_vesc_id_left";
            this.textBox_vesc_id_left.ReadOnly = true;
            this.textBox_vesc_id_left.Size = new System.Drawing.Size(120, 20);
            this.textBox_vesc_id_left.TabIndex = 96;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(12, 148);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(19, 13);
            this.label15.TabIndex = 95;
            this.label15.Text = "Id:";
            // 
            // textBox_vesc_icurrent_left
            // 
            this.textBox_vesc_icurrent_left.Location = new System.Drawing.Point(94, 119);
            this.textBox_vesc_icurrent_left.Name = "textBox_vesc_icurrent_left";
            this.textBox_vesc_icurrent_left.ReadOnly = true;
            this.textBox_vesc_icurrent_left.Size = new System.Drawing.Size(120, 20);
            this.textBox_vesc_icurrent_left.TabIndex = 94;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(12, 122);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(67, 13);
            this.label16.TabIndex = 93;
            this.label16.Text = "Input current";
            // 
            // textBox_vesc_mcurrent_left
            // 
            this.textBox_vesc_mcurrent_left.Location = new System.Drawing.Point(94, 93);
            this.textBox_vesc_mcurrent_left.Name = "textBox_vesc_mcurrent_left";
            this.textBox_vesc_mcurrent_left.ReadOnly = true;
            this.textBox_vesc_mcurrent_left.Size = new System.Drawing.Size(120, 20);
            this.textBox_vesc_mcurrent_left.TabIndex = 92;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(12, 96);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(73, 13);
            this.label17.TabIndex = 91;
            this.label17.Text = "Motor current:";
            // 
            // textBox_vesc_tfet_left
            // 
            this.textBox_vesc_tfet_left.Location = new System.Drawing.Point(94, 67);
            this.textBox_vesc_tfet_left.Name = "textBox_vesc_tfet_left";
            this.textBox_vesc_tfet_left.ReadOnly = true;
            this.textBox_vesc_tfet_left.Size = new System.Drawing.Size(120, 20);
            this.textBox_vesc_tfet_left.TabIndex = 88;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(12, 70);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(55, 13);
            this.label19.TabIndex = 87;
            this.label19.Text = "Temp. fet:";
            // 
            // textBox_vesc_vin_left
            // 
            this.textBox_vesc_vin_left.Location = new System.Drawing.Point(94, 41);
            this.textBox_vesc_vin_left.Name = "textBox_vesc_vin_left";
            this.textBox_vesc_vin_left.ReadOnly = true;
            this.textBox_vesc_vin_left.Size = new System.Drawing.Size(120, 20);
            this.textBox_vesc_vin_left.TabIndex = 86;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(12, 44);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(25, 13);
            this.label20.TabIndex = 85;
            this.label20.Text = "Vin:";
            // 
            // textBox_vesc_fault_right
            // 
            this.textBox_vesc_fault_right.Location = new System.Drawing.Point(220, 249);
            this.textBox_vesc_fault_right.Name = "textBox_vesc_fault_right";
            this.textBox_vesc_fault_right.ReadOnly = true;
            this.textBox_vesc_fault_right.Size = new System.Drawing.Size(120, 20);
            this.textBox_vesc_fault_right.TabIndex = 114;
            // 
            // textBox_vesc_duty_right
            // 
            this.textBox_vesc_duty_right.Location = new System.Drawing.Point(220, 223);
            this.textBox_vesc_duty_right.Name = "textBox_vesc_duty_right";
            this.textBox_vesc_duty_right.ReadOnly = true;
            this.textBox_vesc_duty_right.Size = new System.Drawing.Size(120, 20);
            this.textBox_vesc_duty_right.TabIndex = 113;
            // 
            // textBox_vesc_rpm_right
            // 
            this.textBox_vesc_rpm_right.Location = new System.Drawing.Point(220, 197);
            this.textBox_vesc_rpm_right.Name = "textBox_vesc_rpm_right";
            this.textBox_vesc_rpm_right.ReadOnly = true;
            this.textBox_vesc_rpm_right.Size = new System.Drawing.Size(120, 20);
            this.textBox_vesc_rpm_right.TabIndex = 112;
            // 
            // textBox_vesc_iq_right
            // 
            this.textBox_vesc_iq_right.Location = new System.Drawing.Point(220, 171);
            this.textBox_vesc_iq_right.Name = "textBox_vesc_iq_right";
            this.textBox_vesc_iq_right.ReadOnly = true;
            this.textBox_vesc_iq_right.Size = new System.Drawing.Size(120, 20);
            this.textBox_vesc_iq_right.TabIndex = 111;
            // 
            // textBox_vesc_id_right
            // 
            this.textBox_vesc_id_right.Location = new System.Drawing.Point(220, 145);
            this.textBox_vesc_id_right.Name = "textBox_vesc_id_right";
            this.textBox_vesc_id_right.ReadOnly = true;
            this.textBox_vesc_id_right.Size = new System.Drawing.Size(120, 20);
            this.textBox_vesc_id_right.TabIndex = 110;
            // 
            // textBox_vesc_icurrent_right
            // 
            this.textBox_vesc_icurrent_right.Location = new System.Drawing.Point(220, 119);
            this.textBox_vesc_icurrent_right.Name = "textBox_vesc_icurrent_right";
            this.textBox_vesc_icurrent_right.ReadOnly = true;
            this.textBox_vesc_icurrent_right.Size = new System.Drawing.Size(120, 20);
            this.textBox_vesc_icurrent_right.TabIndex = 109;
            // 
            // textBox_vesc_mcurrent_right
            // 
            this.textBox_vesc_mcurrent_right.Location = new System.Drawing.Point(220, 93);
            this.textBox_vesc_mcurrent_right.Name = "textBox_vesc_mcurrent_right";
            this.textBox_vesc_mcurrent_right.ReadOnly = true;
            this.textBox_vesc_mcurrent_right.Size = new System.Drawing.Size(120, 20);
            this.textBox_vesc_mcurrent_right.TabIndex = 108;
            // 
            // textBox_vesc_tfet_right
            // 
            this.textBox_vesc_tfet_right.Location = new System.Drawing.Point(220, 67);
            this.textBox_vesc_tfet_right.Name = "textBox_vesc_tfet_right";
            this.textBox_vesc_tfet_right.ReadOnly = true;
            this.textBox_vesc_tfet_right.Size = new System.Drawing.Size(120, 20);
            this.textBox_vesc_tfet_right.TabIndex = 106;
            // 
            // textBox_vesc_vin_right
            // 
            this.textBox_vesc_vin_right.Location = new System.Drawing.Point(220, 41);
            this.textBox_vesc_vin_right.Name = "textBox_vesc_vin_right";
            this.textBox_vesc_vin_right.ReadOnly = true;
            this.textBox_vesc_vin_right.Size = new System.Drawing.Size(120, 20);
            this.textBox_vesc_vin_right.TabIndex = 105;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(91, 25);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(28, 13);
            this.label21.TabIndex = 115;
            this.label21.Text = "Left:";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(217, 25);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(35, 13);
            this.label22.TabIndex = 116;
            this.label22.Text = "Right:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBox_RC_0);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.textBox_RC_9);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.textBox_RC_1);
            this.groupBox2.Controls.Add(this.textBox_RC_8);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.textBox_RC_2);
            this.groupBox2.Controls.Add(this.textBox_RC_7);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.textBox_RC_3);
            this.groupBox2.Controls.Add(this.textBox_RC_6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.textBox_RC_4);
            this.groupBox2.Controls.Add(this.textBox_RC_5);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(12, 83);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(224, 309);
            this.groupBox2.TabIndex = 86;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "RC status";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.numericUpDown_motor_steer);
            this.groupBox3.Controls.Add(this.numericUpDown_motor_gas);
            this.groupBox3.Controls.Add(this.button_control_stop);
            this.groupBox3.Controls.Add(this.label23);
            this.groupBox3.Controls.Add(this.button_control_update);
            this.groupBox3.Controls.Add(this.label18);
            this.groupBox3.Location = new System.Drawing.Point(615, 83);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(237, 125);
            this.groupBox3.TabIndex = 87;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Motor control";
            // 
            // button_control_update
            // 
            this.button_control_update.Location = new System.Drawing.Point(153, 64);
            this.button_control_update.Name = "button_control_update";
            this.button_control_update.Size = new System.Drawing.Size(75, 23);
            this.button_control_update.TabIndex = 0;
            this.button_control_update.Text = "Update";
            this.button_control_update.UseVisualStyleBackColor = true;
            this.button_control_update.Click += new System.EventHandler(this.button_control_update_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(6, 44);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(29, 13);
            this.label18.TabIndex = 117;
            this.label18.Text = "Gas:";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(6, 70);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(35, 13);
            this.label23.TabIndex = 119;
            this.label23.Text = "Steer:";
            // 
            // button_control_stop
            // 
            this.button_control_stop.Location = new System.Drawing.Point(153, 91);
            this.button_control_stop.Name = "button_control_stop";
            this.button_control_stop.Size = new System.Drawing.Size(75, 23);
            this.button_control_stop.TabIndex = 120;
            this.button_control_stop.Text = "Stop";
            this.button_control_stop.UseVisualStyleBackColor = true;
            this.button_control_stop.Click += new System.EventHandler(this.button_control_stop_Click);
            // 
            // numericUpDown_motor_gas
            // 
            this.numericUpDown_motor_gas.Location = new System.Drawing.Point(52, 42);
            this.numericUpDown_motor_gas.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericUpDown_motor_gas.Name = "numericUpDown_motor_gas";
            this.numericUpDown_motor_gas.Size = new System.Drawing.Size(81, 20);
            this.numericUpDown_motor_gas.TabIndex = 88;
            // 
            // numericUpDown_motor_steer
            // 
            this.numericUpDown_motor_steer.Location = new System.Drawing.Point(52, 68);
            this.numericUpDown_motor_steer.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericUpDown_motor_steer.Name = "numericUpDown_motor_steer";
            this.numericUpDown_motor_steer.Size = new System.Drawing.Size(81, 20);
            this.numericUpDown_motor_steer.TabIndex = 121;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(862, 403);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textBox_fw_version);
            this.Controls.Add(this.label40);
            this.Controls.Add(this.textBox_hotswap_state);
            this.Controls.Add(this.label41);
            this.Name = "FormMain";
            this.Text = "Robotplatform Monitor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_motor_gas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_motor_steer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer timer_connect;
        private System.Windows.Forms.Timer timer_update;
        private System.Windows.Forms.TextBox textBox_fw_version;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.TextBox textBox_hotswap_state;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.TextBox textBox_RC_0;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_RC_1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_RC_2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_RC_3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_RC_4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_RC_5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_RC_6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox_RC_7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox_RC_8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox_RC_9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox textBox_vesc_fault_right;
        private System.Windows.Forms.TextBox textBox_vesc_duty_right;
        private System.Windows.Forms.TextBox textBox_vesc_rpm_right;
        private System.Windows.Forms.TextBox textBox_vesc_iq_right;
        private System.Windows.Forms.TextBox textBox_vesc_id_right;
        private System.Windows.Forms.TextBox textBox_vesc_icurrent_right;
        private System.Windows.Forms.TextBox textBox_vesc_mcurrent_right;
        private System.Windows.Forms.TextBox textBox_vesc_tfet_right;
        private System.Windows.Forms.TextBox textBox_vesc_vin_right;
        private System.Windows.Forms.TextBox textBox_vesc_fault_left;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBox_vesc_duty_left;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBox_vesc_rpm_left;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBox_vesc_iq_left;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textBox_vesc_id_left;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox textBox_vesc_icurrent_left;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox textBox_vesc_mcurrent_left;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox textBox_vesc_tfet_left;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox textBox_vesc_vin_left;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button_control_stop;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Button button_control_update;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.NumericUpDown numericUpDown_motor_steer;
        private System.Windows.Forms.NumericUpDown numericUpDown_motor_gas;
    }
}

