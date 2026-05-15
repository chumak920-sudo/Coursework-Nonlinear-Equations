// ReSharper disable LocalizableElement
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using Coursework.Models;
using Coursework.Solvers;

namespace Coursework.UI; 

public partial class MainForm : Form
{
    private SystemModel? _lastModel;
    private double[]? _lastResult;

    public MainForm()
    {
        InitializeComponent();
        
        cmbSystemType.SelectedIndexChanged += cmbSystemType_SelectedIndexChanged;
        nudDimension.ValueChanged += NudDimension_ValueChanged;

        cmbSystemType.Items.Clear();
        cmbSystemType.Items.AddRange(["Алгебраїчна", "Тригонометрична", "Показникова"]);
        
        cmbMethod.Items.Clear();
        cmbMethod.Items.AddRange(["Метод Ньютона", "Метод січних"]);

        cmbSystemType.SelectedIndex = -1; 
        cmbMethod.SelectedIndex = -1;     
        txtPrecision.Text = "";           
        lblSystemFormula.Text = "";       

        nudDimension.Minimum = 2;
        nudDimension.Maximum = 10;
        nudDimension.Value = 2;
        nudMaxIterations.Minimum = 10;
        nudMaxIterations.Maximum = 1000;
        nudMaxIterations.Value = 100;

        dgvCoefficients.AllowUserToAddRows = false;
        dgvInitialGuess.AllowUserToAddRows = false;

        NudDimension_ValueChanged(this, EventArgs.Empty);
    }

    private string GetVarName(int index, int totalDimension)
    {
        if (totalDimension <= 3)
        {
            if (index == 0) return "x";
            if (index == 1) return "y";
            if (index == 2) return "z";
        }
        return $"x{index + 1}";
    }

    private void NudDimension_ValueChanged(object? sender, EventArgs e)
    {
        int n = (int)nudDimension.Value;

        dgvCoefficients.Columns.Clear();
        for (int j = 0; j < n; j++)
        {
            dgvCoefficients.Columns.Add($"a{j + 1}", $"a{j + 1}");
        }
        int bIndex = dgvCoefficients.Columns.Add("b", "b (Вільні члени)");
        dgvCoefficients.Columns[bIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        
        dgvCoefficients.Rows.Clear();
        dgvCoefficients.Rows.Add(n);
        
        for (int i = 0; i < n; i++)
        {
            dgvCoefficients.Rows[i].HeaderCell.Value = (i + 1).ToString();
        }
        dgvCoefficients.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;

        dgvInitialGuess.Columns.Clear();
        for (int j = 0; j < n; j++)
        {
            string vName = GetVarName(j, n);
            dgvInitialGuess.Columns.Add($"col_{j}", $"{vName} (старт)");
        }
        dgvInitialGuess.Rows.Clear();
        dgvInitialGuess.Rows.Add(1); 
    }

    private void BtnSolve_Click(object? sender, EventArgs e)
    {
        rtbOutput.Clear();

        // Перевірка вибору типу та методу
        if (cmbSystemType.SelectedIndex == -1 || cmbMethod.SelectedIndex == -1)
        {
            MessageBox.Show("Будь ласка, оберіть тип системи та метод розв'язання зі списків!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        int n = (int)nudDimension.Value;
        EquationType type = (EquationType)cmbSystemType.SelectedIndex;
        SystemModel model = new SystemModel(n, type);
        double[] x0 = new double[n];

        // Збір та перевірка даних з таблиць
        try
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    var cellA = dgvCoefficients.Rows[i].Cells[j].Value;
                    if (cellA == null || string.IsNullOrWhiteSpace(cellA.ToString())) throw new Exception();
                    model.A[i, j] = Convert.ToDouble(cellA.ToString()!.Replace('.', ','));
                }
                
                var cellB = dgvCoefficients.Rows[i].Cells[n].Value;
                if (cellB == null || string.IsNullOrWhiteSpace(cellB.ToString())) throw new Exception();
                model.B[i] = Convert.ToDouble(cellB.ToString()!.Replace('.', ','));
                
                var cellX0 = dgvInitialGuess.Rows[0].Cells[i].Value;
                if (cellX0 == null || string.IsNullOrWhiteSpace(cellX0.ToString())) throw new Exception();
                x0[i] = Convert.ToDouble(cellX0.ToString()!.Replace('.', ','));
            }
        }
        catch (Exception)
        {
            MessageBox.Show("Перевірте таблиці! Усі клітинки повинні містити числа.", "Помилка даних", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return; 
        }

        // Перевіряємо точність
        string precText = txtPrecision.Text.Replace('.', ',');
        if (!double.TryParse(precText, out double precision) || precision > 0.1 || precision < 1e-10)
        {
            MessageBox.Show("Некоректна точність! Введіть число в межах від 0,1 до 1E-10 (наприклад: 0,0001).", "Помилка введення", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        
        int maxIter = (int)nudMaxIterations.Value;
        INonlinearSolver solver = (cmbMethod.SelectedIndex == 0) ? new NewtonSolver() : new SecantSolver();

        var watch = System.Diagnostics.Stopwatch.StartNew(); 
        double[] result = solver.Solve(model, x0, precision, maxIter, out int iters, out string errorMsg);
        watch.Stop();

        // Виведення результатів
        if (!string.IsNullOrEmpty(errorMsg))
        {
            rtbOutput.SelectionColor = Color.Red;
            rtbOutput.AppendText($"ЗУПИНКА АЛГОРИТМУ:\n{errorMsg}\n");
            
            if (errorMsg.Contains("зациклювання"))
            {
                rtbOutput.AppendText("-> Ймовірні причини:\n   1) Ця система не має дійсних розв'язків.\n   2) Дуже невдале початкове наближення.\n\n");
            }
        }
        else
        {
            rtbOutput.SelectionColor = Color.Green;
            rtbOutput.AppendText("РОЗВ'ЯЗОК УСПІШНО ЗНАЙДЕНО!\n\n");
        }

        rtbOutput.SelectionColor = Color.Black;
        for (int i = 0; i < n; i++)
        {
            string vName = GetVarName(i, n);
            rtbOutput.AppendText($"{vName} = {result[i]:F6}\n");
        }

        rtbOutput.AppendText($"\nКількість ітерацій: {iters}\n");
        rtbOutput.AppendText($"Витрачений час: {watch.ElapsedMilliseconds} мс\n\n");

        if (string.IsNullOrEmpty(errorMsg))
        {
            rtbOutput.AppendText("--- Перевірка ---\n");
            for (int i = 0; i < n; i++)
            {
                double fVal = model.CalculateFunction(i, result);
                rtbOutput.AppendText($"Рівняння {i + 1}: f(x) = {fVal:E3}\n");
            }
        }
        
        _lastModel = model;
        _lastResult = result;
        btnShowGraph.Visible = n == 2;
    }

    private void BtnClear_Click(object? sender, EventArgs e)
    {
        rtbOutput.Clear();
        cmbSystemType.SelectedIndex = -1;
        cmbMethod.SelectedIndex = -1;
        txtPrecision.Text = "";
        lblSystemFormula.Text = ""; 
        NudDimension_ValueChanged(this, EventArgs.Empty); 
        btnShowGraph.Visible = false;
    }

    private void BtnSaveToFile_Click(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(rtbOutput.Text))
        {
            MessageBox.Show("Немає даних для збереження!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        using (SaveFileDialog sfd = new SaveFileDialog())
        {
            sfd.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            sfd.Title = "Зберегти результати";
            sfd.FileName = "Result.txt";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(sfd.FileName, rtbOutput.Text);
                MessageBox.Show("Збережено успішно!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }

    private void cmbSystemType_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (cmbSystemType.SelectedIndex == 0)
        {
            lblSystemFormula.Text = "Вигляд: a1*x² + a2*y² + ... + an*z² = b";
        }
        else if (cmbSystemType.SelectedIndex == 1)
        {
            lblSystemFormula.Text = "Вигляд: a1*sin(x) + a2*sin(y) + ... + an*sin(z) = b";
        }
        else if (cmbSystemType.SelectedIndex == 2)
        {
            lblSystemFormula.Text = "Вигляд: a1*exp(x) + a2*exp(y) + ... + an*exp(z) = b";
        }
        else
        {
            lblSystemFormula.Text = ""; 
        }
    }

    private void btnShowGraph_Click(object? sender, EventArgs e)
    {
        if (_lastModel != null && _lastResult != null && _lastResult.Length >= 2)
        {
            GraphForm gf = new GraphForm(_lastModel, _lastResult[0], _lastResult[1]);
            gf.Show();
        }
    }

    private void btnLoadFromFile_Click(object sender, EventArgs e)
    {
        using (OpenFileDialog ofd = new OpenFileDialog())
        {
            ofd.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            ofd.Title = "Завантажити коефіцієнти з файлу";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string[] lines = File.ReadAllLines(ofd.FileName);
                    
                    // Відфільтровуємо порожні рядки
                    List<string> validLines = new List<string>();
                    foreach (string line in lines)
                    {
                        if (!string.IsNullOrWhiteSpace(line))
                            validLines.Add(line.Trim());
                    }
                    
                    // N рівнянь + 1 рядок наближень = N + 1 рядків у файлі
                    int detectedN = validLines.Count - 1;

                    if (detectedN < 2 || detectedN > 10)
                    {
                        MessageBox.Show($"Не вдалося визначити розмірність (знайдено рядків: {validLines.Count}).\n" +
                                        "Файл повинен містити N рядків рівнянь та 1 рядок наближень.", 
                                        "Помилка формату", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Змінюємо інтерфейс під знайдений N
                    nudDimension.Value = detectedN; 

                    // Зчитуємо матрицю коефіцієнтів та вільні члени
                    for (int i = 0; i < detectedN; i++)
                    {
                        // Розбиваємо рядок на числа (роздільник - пробіл або табуляція)
                        string[] parts = validLines[i].Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                        
                        if (parts.Length < detectedN + 1)
                        {
                            MessageBox.Show($"Рядок {i + 1} у файлі не містить достатньо чисел (потрібно {detectedN + 1})!", 
                                            "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        for (int j = 0; j < detectedN; j++)
                        {
                            dgvCoefficients.Rows[i].Cells[j].Value = parts[j].Replace('.', ',');
                        }
                        // Останнє число - вільний член
                        dgvCoefficients.Rows[i].Cells[detectedN].Value = parts[detectedN].Replace('.', ',');
                    }

                    // Зчитуємо початкові наближення
                    string[] guessParts = validLines[detectedN].Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    if (guessParts.Length < detectedN)
                    {
                        MessageBox.Show("Останній рядок файлу не містить достатньо початкових наближень!", 
                                        "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    for (int i = 0; i < detectedN; i++)
                    {
                        dgvInitialGuess.Rows[0].Cells[i].Value = guessParts[i].Replace('.', ',');
                    }

                    MessageBox.Show($"Файл розпізнано! Коефіцієнти для системи розмірності N={detectedN} успішно завантажено.", 
                                    "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Сталася помилка при читанні файлу:\n{ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}