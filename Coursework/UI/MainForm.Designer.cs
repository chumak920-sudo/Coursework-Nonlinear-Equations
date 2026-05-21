using System.ComponentModel;

namespace Coursework.UI;

partial class MainForm
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private IContainer components = null;

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
        cmbSystemType = new System.Windows.Forms.ComboBox();
        nudDimension = new System.Windows.Forms.NumericUpDown();
        dgvCoefficients = new System.Windows.Forms.DataGridView();
        dgvInitialGuess = new System.Windows.Forms.DataGridView();
        btnClear = new System.Windows.Forms.Button();
        cmbMethod = new System.Windows.Forms.ComboBox();
        txtPrecision = new System.Windows.Forms.TextBox();
        label1 = new System.Windows.Forms.Label();
        nudMaxIterations = new System.Windows.Forms.NumericUpDown();
        btnSolve = new System.Windows.Forms.Button();
        rtbOutput = new System.Windows.Forms.RichTextBox();
        btnSaveToFile = new System.Windows.Forms.Button();
        label2 = new System.Windows.Forms.Label();
        label3 = new System.Windows.Forms.Label();
        label4 = new System.Windows.Forms.Label();
        label5 = new System.Windows.Forms.Label();
        lblSystemFormula = new System.Windows.Forms.Label();
        label6 = new System.Windows.Forms.Label();
        btnShowGraph = new System.Windows.Forms.Button();
        btnLoadFromFile = new System.Windows.Forms.Button();
        btnExit = new System.Windows.Forms.Button();
        ((System.ComponentModel.ISupportInitialize)nudDimension).BeginInit();
        ((System.ComponentModel.ISupportInitialize)dgvCoefficients).BeginInit();
        ((System.ComponentModel.ISupportInitialize)dgvInitialGuess).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudMaxIterations).BeginInit();
        SuspendLayout();
        // 
        // cmbSystemType
        // 
        cmbSystemType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        cmbSystemType.FormattingEnabled = true;
        cmbSystemType.Location = new System.Drawing.Point(197, 12);
        cmbSystemType.Name = "cmbSystemType";
        cmbSystemType.Size = new System.Drawing.Size(175, 28);
        cmbSystemType.TabIndex = 0;
        // 
        // nudDimension
        // 
        nudDimension.Location = new System.Drawing.Point(305, 98);
        nudDimension.Name = "nudDimension";
        nudDimension.Size = new System.Drawing.Size(67, 27);
        nudDimension.TabIndex = 1;
        // 
        // dgvCoefficients
        // 
        dgvCoefficients.ColumnHeadersHeight = 29;
        dgvCoefficients.Location = new System.Drawing.Point(-2, 197);
        dgvCoefficients.Name = "dgvCoefficients";
        dgvCoefficients.RowHeadersWidth = 51;
        dgvCoefficients.Size = new System.Drawing.Size(887, 226);
        dgvCoefficients.TabIndex = 3;
        // 
        // dgvInitialGuess
        // 
        dgvInitialGuess.ColumnHeadersHeight = 29;
        dgvInitialGuess.Location = new System.Drawing.Point(-2, 429);
        dgvInitialGuess.Name = "dgvInitialGuess";
        dgvInitialGuess.RowHeadersWidth = 51;
        dgvInitialGuess.Size = new System.Drawing.Size(887, 87);
        dgvInitialGuess.TabIndex = 4;
        // 
        // btnClear
        // 
        btnClear.Font = new System.Drawing.Font("Palatino Linotype", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)204));
        btnClear.Location = new System.Drawing.Point(762, 157);
        btnClear.Name = "btnClear";
        btnClear.Size = new System.Drawing.Size(107, 34);
        btnClear.TabIndex = 5;
        btnClear.Text = "Спочатку";
        btnClear.UseVisualStyleBackColor = true;
        btnClear.Click += BtnClear_Click;
        // 
        // cmbMethod
        // 
        cmbMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        cmbMethod.FormattingEnabled = true;
        cmbMethod.Location = new System.Drawing.Point(212, 52);
        cmbMethod.Name = "cmbMethod";
        cmbMethod.Size = new System.Drawing.Size(160, 28);
        cmbMethod.TabIndex = 6;
        // 
        // txtPrecision
        // 
        txtPrecision.Location = new System.Drawing.Point(656, 80);
        txtPrecision.Name = "txtPrecision";
        txtPrecision.Size = new System.Drawing.Size(64, 27);
        txtPrecision.TabIndex = 7;
        // 
        // label1
        // 
        label1.Font = new System.Drawing.Font("Palatino Linotype", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)204));
        label1.Location = new System.Drawing.Point(427, 80);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(223, 27);
        label1.TabIndex = 8;
        label1.Text = "Точність (від 1e-10 до 1e-1):";
        // 
        // nudMaxIterations
        // 
        nudMaxIterations.Location = new System.Drawing.Point(305, 129);
        nudMaxIterations.Name = "nudMaxIterations";
        nudMaxIterations.Size = new System.Drawing.Size(67, 27);
        nudMaxIterations.TabIndex = 9;
        // 
        // btnSolve
        // 
        btnSolve.Font = new System.Drawing.Font("Palatino Linotype", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)204));
        btnSolve.Location = new System.Drawing.Point(468, 157);
        btnSolve.Name = "btnSolve";
        btnSolve.Size = new System.Drawing.Size(116, 34);
        btnSolve.TabIndex = 10;
        btnSolve.Text = "Розв\'язати";
        btnSolve.UseVisualStyleBackColor = true;
        btnSolve.Click += BtnSolve_Click;
        // 
        // rtbOutput
        // 
        rtbOutput.Location = new System.Drawing.Point(-2, 522);
        rtbOutput.Name = "rtbOutput";
        rtbOutput.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
        rtbOutput.Size = new System.Drawing.Size(887, 352);
        rtbOutput.TabIndex = 15;
        rtbOutput.Text = "";
        // 
        // btnSaveToFile
        // 
        btnSaveToFile.Font = new System.Drawing.Font("Palatino Linotype", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)204));
        btnSaveToFile.Location = new System.Drawing.Point(590, 157);
        btnSaveToFile.Name = "btnSaveToFile";
        btnSaveToFile.Size = new System.Drawing.Size(164, 34);
        btnSaveToFile.TabIndex = 17;
        btnSaveToFile.Text = "Зберегти у файл";
        btnSaveToFile.UseVisualStyleBackColor = true;
        btnSaveToFile.Click += BtnSaveToFile_Click;
        // 
        // label2
        // 
        label2.Font = new System.Drawing.Font("Palatino Linotype", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)204));
        label2.Location = new System.Drawing.Point(12, 12);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(179, 28);
        label2.TabIndex = 18;
        label2.Text = "Оберіть тип системи:";
        // 
        // label3
        // 
        label3.Font = new System.Drawing.Font("Palatino Linotype", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)204));
        label3.Location = new System.Drawing.Point(80, 52);
        label3.Name = "label3";
        label3.Size = new System.Drawing.Size(126, 28);
        label3.TabIndex = 19;
        label3.Text = "Оберіть метод:";
        // 
        // label4
        // 
        label4.Font = new System.Drawing.Font("Palatino Linotype", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)204));
        label4.Location = new System.Drawing.Point(148, 98);
        label4.Name = "label4";
        label4.Size = new System.Drawing.Size(151, 27);
        label4.TabIndex = 20;
        label4.Text = "Кількість рівнянь:";
        // 
        // label5
        // 
        label5.Font = new System.Drawing.Font("Palatino Linotype", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)204));
        label5.Location = new System.Drawing.Point(32, 129);
        label5.Name = "label5";
        label5.Size = new System.Drawing.Size(267, 27);
        label5.TabIndex = 21;
        label5.Text = "Максимальна кількість ітерацій:";
        // 
        // lblSystemFormula
        // 
        lblSystemFormula.AutoSize = true;
        lblSystemFormula.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)204));
        lblSystemFormula.Location = new System.Drawing.Point(393, 25);
        lblSystemFormula.Name = "lblSystemFormula";
        lblSystemFormula.Size = new System.Drawing.Size(0, 27);
        lblSystemFormula.TabIndex = 22;
        // 
        // label6
        // 
        label6.Font = new System.Drawing.Font("Palatino Linotype", 13.200001F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)204));
        label6.Location = new System.Drawing.Point(19, 159);
        label6.Name = "label6";
        label6.Size = new System.Drawing.Size(409, 35);
        label6.TabIndex = 23;
        label6.Text = "Введіть коефіцієнти до рівняння:";
        // 
        // btnShowGraph
        // 
        btnShowGraph.Font = new System.Drawing.Font("Palatino Linotype", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)204));
        btnShowGraph.Location = new System.Drawing.Point(468, 119);
        btnShowGraph.Name = "btnShowGraph";
        btnShowGraph.Size = new System.Drawing.Size(194, 32);
        btnShowGraph.TabIndex = 24;
        btnShowGraph.Text = "Показати на графіку";
        btnShowGraph.UseVisualStyleBackColor = true;
        btnShowGraph.Visible = false;
        btnShowGraph.Click += btnShowGraph_Click;
        // 
        // btnLoadFromFile
        // 
        btnLoadFromFile.Font = new System.Drawing.Font("Palatino Linotype", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)204));
        btnLoadFromFile.Location = new System.Drawing.Point(668, 119);
        btnLoadFromFile.Name = "btnLoadFromFile";
        btnLoadFromFile.Size = new System.Drawing.Size(201, 32);
        btnLoadFromFile.TabIndex = 25;
        btnLoadFromFile.Text = "Завантажити з файлу";
        btnLoadFromFile.UseVisualStyleBackColor = true;
        btnLoadFromFile.Click += btnLoadFromFile_Click;
        // 
        // btnExit
        // 
        btnExit.Font = new System.Drawing.Font("Palatino Linotype", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)204));
        btnExit.Location = new System.Drawing.Point(804, 71);
        btnExit.Name = "btnExit";
        btnExit.Size = new System.Drawing.Size(65, 40);
        btnExit.TabIndex = 26;
        btnExit.Text = "Вихід";
        btnExit.UseVisualStyleBackColor = true;
        btnExit.Click += btnExit_Click;
        // 
        // MainForm
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(881, 866);
        Controls.Add(btnExit);
        Controls.Add(btnLoadFromFile);
        Controls.Add(btnShowGraph);
        Controls.Add(label6);
        Controls.Add(lblSystemFormula);
        Controls.Add(label5);
        Controls.Add(label4);
        Controls.Add(label3);
        Controls.Add(label2);
        Controls.Add(btnSaveToFile);
        Controls.Add(rtbOutput);
        Controls.Add(btnSolve);
        Controls.Add(nudMaxIterations);
        Controls.Add(label1);
        Controls.Add(txtPrecision);
        Controls.Add(cmbMethod);
        Controls.Add(btnClear);
        Controls.Add(dgvInitialGuess);
        Controls.Add(dgvCoefficients);
        Controls.Add(nudDimension);
        Controls.Add(cmbSystemType);
        Text = "Калькулятор систем нелінійних рівнянь";
        ((System.ComponentModel.ISupportInitialize)nudDimension).EndInit();
        ((System.ComponentModel.ISupportInitialize)dgvCoefficients).EndInit();
        ((System.ComponentModel.ISupportInitialize)dgvInitialGuess).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudMaxIterations).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    private System.Windows.Forms.Button btnExit;

    private System.Windows.Forms.Button btnShowGraph;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label lblSystemFormula;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Button btnSaveToFile;
    private System.Windows.Forms.RichTextBox rtbOutput;
    private System.Windows.Forms.Button btnSolve;
    private System.Windows.Forms.NumericUpDown nudMaxIterations;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox txtPrecision;
    private System.Windows.Forms.ComboBox cmbMethod;
    private System.Windows.Forms.Button btnClear;
    private System.Windows.Forms.DataGridView dgvInitialGuess;
    private System.Windows.Forms.DataGridView dgvCoefficients;
    private System.Windows.Forms.NumericUpDown nudDimension;
    private System.Windows.Forms.ComboBox cmbSystemType;
    private System.Windows.Forms.Button btnLoadFromFile;

    #endregion
}