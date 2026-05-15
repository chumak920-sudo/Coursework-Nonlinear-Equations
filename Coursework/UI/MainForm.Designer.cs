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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
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
        resources.ApplyResources(cmbSystemType, "cmbSystemType");
        cmbSystemType.Name = "cmbSystemType";
        // 
        // nudDimension
        // 
        resources.ApplyResources(nudDimension, "nudDimension");
        nudDimension.Name = "nudDimension";
        // 
        // dgvCoefficients
        // 
        resources.ApplyResources(dgvCoefficients, "dgvCoefficients");
        dgvCoefficients.Name = "dgvCoefficients";
        // 
        // dgvInitialGuess
        // 
        resources.ApplyResources(dgvInitialGuess, "dgvInitialGuess");
        dgvInitialGuess.Name = "dgvInitialGuess";
        // 
        // btnClear
        // 
        resources.ApplyResources(btnClear, "btnClear");
        btnClear.Name = "btnClear";
        btnClear.UseVisualStyleBackColor = true;
        btnClear.Click += BtnClear_Click;
        // 
        // cmbMethod
        // 
        cmbMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        cmbMethod.FormattingEnabled = true;
        resources.ApplyResources(cmbMethod, "cmbMethod");
        cmbMethod.Name = "cmbMethod";
        // 
        // txtPrecision
        // 
        resources.ApplyResources(txtPrecision, "txtPrecision");
        txtPrecision.Name = "txtPrecision";
        // 
        // label1
        // 
        resources.ApplyResources(label1, "label1");
        label1.Name = "label1";
        // 
        // nudMaxIterations
        // 
        resources.ApplyResources(nudMaxIterations, "nudMaxIterations");
        nudMaxIterations.Name = "nudMaxIterations";
        // 
        // btnSolve
        // 
        resources.ApplyResources(btnSolve, "btnSolve");
        btnSolve.Name = "btnSolve";
        btnSolve.UseVisualStyleBackColor = true;
        btnSolve.Click += BtnSolve_Click;
        // 
        // rtbOutput
        // 
        resources.ApplyResources(rtbOutput, "rtbOutput");
        rtbOutput.Name = "rtbOutput";
        // 
        // btnSaveToFile
        // 
        resources.ApplyResources(btnSaveToFile, "btnSaveToFile");
        btnSaveToFile.Name = "btnSaveToFile";
        btnSaveToFile.UseVisualStyleBackColor = true;
        btnSaveToFile.Click += BtnSaveToFile_Click;
        // 
        // label2
        // 
        resources.ApplyResources(label2, "label2");
        label2.Name = "label2";
        // 
        // label3
        // 
        resources.ApplyResources(label3, "label3");
        label3.Name = "label3";
        // 
        // label4
        // 
        resources.ApplyResources(label4, "label4");
        label4.Name = "label4";
        // 
        // label5
        // 
        resources.ApplyResources(label5, "label5");
        label5.Name = "label5";
        // 
        // lblSystemFormula
        // 
        resources.ApplyResources(lblSystemFormula, "lblSystemFormula");
        lblSystemFormula.Name = "lblSystemFormula";
        // 
        // label6
        // 
        resources.ApplyResources(label6, "label6");
        label6.Name = "label6";
        // 
        // btnShowGraph
        // 
        resources.ApplyResources(btnShowGraph, "btnShowGraph");
        btnShowGraph.Name = "btnShowGraph";
        btnShowGraph.UseVisualStyleBackColor = true;
        btnShowGraph.Click += btnShowGraph_Click;
        // 
        // btnLoadFromFile
        // 
        resources.ApplyResources(btnLoadFromFile, "btnLoadFromFile");
        btnLoadFromFile.Name = "btnLoadFromFile";
        btnLoadFromFile.UseVisualStyleBackColor = true;
        btnLoadFromFile.Click += btnLoadFromFile_Click;
        // 
        // MainForm
        // 
        resources.ApplyResources(this, "$this");
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
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
        ((System.ComponentModel.ISupportInitialize)nudDimension).EndInit();
        ((System.ComponentModel.ISupportInitialize)dgvCoefficients).EndInit();
        ((System.ComponentModel.ISupportInitialize)dgvInitialGuess).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudMaxIterations).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    private System.Windows.Forms.Button btnLoadFromFile;

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

    #endregion
}