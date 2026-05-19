// ReSharper disable LocalizableElement
using System.Text;
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
        
        dgvCoefficients.CellValueChanged += Dgv_CellValueChanged;
        dgvInitialGuess.CellValueChanged += Dgv_CellValueChanged;
        
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
        btnShowGraph.Visible = false;
        
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
        btnShowGraph.Visible = false;

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
        INonlinearSolver solver = cmbMethod.SelectedIndex == 0 ? new NewtonSolver() : new SecantSolver();

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

    private void BtnSaveToFile_Click(object sender, EventArgs e)
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
            sfd.FileName = "Звіт_Курсова.txt";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    StringBuilder report = new StringBuilder();
                    report.AppendLine("==================================================");
                    report.AppendLine("        ЗВІТ: РОЗВ'ЯЗАННЯ СИСТЕМИ РІВНЯНЬ         ");
                    report.AppendLine("==================================================");
                    report.AppendLine($"Дата та час: {DateTime.Now}");
                    report.AppendLine($"Тип системи: {cmbSystemType.Text}");
                    report.AppendLine($"Метод:       {cmbMethod.Text}");
                    report.AppendLine($"Розмірність: {nudDimension.Value}");
                    report.AppendLine($"Точність:    {txtPrecision.Text}");
                    report.AppendLine("--------------------------------------------------");
                    report.AppendLine("ВХІДНІ КОЕФІЦІЄНТИ (Матриця системи):");
                    
                    string headerRow = "";
                    for (int c = 0; c < dgvCoefficients.Columns.Count; c++)
                    {
                        headerRow += dgvCoefficients.Columns[c].HeaderText.PadRight(18);
                    }
                    report.AppendLine(headerRow);

                    for (int r = 0; r < dgvCoefficients.Rows.Count; r++)
                    {
                        string rowData = "";
                        for (int c = 0; c < dgvCoefficients.Columns.Count; c++)
                        {
                            string cellValue = dgvCoefficients.Rows[r].Cells[c].Value?.ToString() ?? "0";
                            rowData += cellValue.PadRight(18);
                        }
                        report.AppendLine(rowData);
                    }

                    report.AppendLine("--------------------------------------------------");
                    report.AppendLine("ПОЧАТКОВІ НАБЛИЖЕННЯ:");
                    string guessRow = "";
                    for (int c = 0; c < dgvInitialGuess.Columns.Count; c++)
                    {
                        string header = dgvInitialGuess.Columns[c].HeaderText;
                        string val = dgvInitialGuess.Rows[0].Cells[c].Value?.ToString() ?? "0";
                        guessRow += $"{header}: {val}    ";
                    }
                    report.AppendLine(guessRow);
                    report.AppendLine("==================================================\n");

                    report.Append(rtbOutput.Text);

                    File.WriteAllText(sfd.FileName, report.ToString());
                    MessageBox.Show("Звіт успішно збережено!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Сталася помилка при збереженні:\n{ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }

    private void cmbSystemType_SelectedIndexChanged(object? sender, EventArgs e)
    {
        btnShowGraph.Visible = false;
        
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

    private void btnShowGraph_Click(object sender, EventArgs e)
    {
        if (_lastModel != null && _lastResult != null && _lastResult.Length >= 2)
        {
            bool isSuccess = !rtbOutput.Text.Contains("ЗУПИНКА");
        
            double centerX = isSuccess ? _lastResult[0] : 0.0;
            double centerY = isSuccess ? _lastResult[1] : 0.0;
        
            GraphForm gf = new GraphForm(_lastModel, centerX, centerY, isSuccess);
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
                    btnShowGraph.Visible = false;
                    
                    string[] lines = File.ReadAllLines(ofd.FileName);
                    
                    List<string> validLines = new List<string>();
                    foreach (string line in lines)
                    {
                        if (!string.IsNullOrWhiteSpace(line))
                            validLines.Add(line.Trim());
                    }
                    
                    int detectedN = validLines.Count - 1;

                    if (detectedN < 2 || detectedN > 10)
                    {
                        MessageBox.Show($"Не вдалося визначити розмірність (знайдено рядків: {validLines.Count}).\n" +
                                        "Файл повинен містити N рядків рівнянь та 1 рядок наближень.", 
                                        "Помилка формату", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    nudDimension.Value = detectedN; 

                    for (int i = 0; i < detectedN; i++)
                    {
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
                        dgvCoefficients.Rows[i].Cells[detectedN].Value = parts[detectedN].Replace('.', ',');
                    }

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
    private void Dgv_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
    {
        // Якщо користувач щось змінив у таблиці — ховаємо кнопку графіка
        btnShowGraph.Visible = false;
    }
}