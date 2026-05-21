using Coursework.Models; 
 
namespace Coursework.Solvers; 
 
public class NewtonSolver : INonlinearSolver 
{ 
    public double[] Solve(SystemModel model, double[] initialGuess, double precision, int maxIterations, out int iterationsTaken, out int operationsCount, out string errorMessage) 
    { 
        int n = model.N; 
        double[] x = (double[])initialGuess.Clone(); 
        iterationsTaken = 0; 
        operationsCount = 0; // Ініціалізуємо лічильник операцій
        errorMessage = string.Empty; 
 
        for (int iter = 0; iter < maxIterations; iter++) 
        { 
            iterationsTaken++; 
            double[,] J = new double[n, n]; // Матриця Якобі 
            double[] F = new double[n];     // Вектор функцій 
 
            // 1. Обчислюємо вектор -F(x) та матрицю Якобі J(x) 
            for (int i = 0; i < n; i++) 
            { 
                operationsCount++; // Рахуємо зміну знаку (унарний мінус)
                F[i] = -model.CalculateFunction(i, x, ref operationsCount);  
                
                for (int j = 0; j < n; j++) 
                { 
                    J[i, j] = model.CalculateDerivative(i, j, x, ref operationsCount); 
                } 
            }
 
            // 2. Розв'язуємо СЛАР J * dx = -F методом Гаусса 
            int gaussOps = 0;
            double[]? dx = SolveGauss(J, F, ref gaussOps); 
            operationsCount += gaussOps; // Додаємо операції, що пішли на Гаусса

            if (dx == null) 
            { 
                errorMessage = "Особливий випадок: Матриця Якобі вироджена. Можливо, розв'язків немає, або точка є точкою екстремуму."; 
                return x; 
            } 
 
            // 3. Оновлюємо вектор наближень: x = x + dx 
            double maxDiff = 0; 
            for (int i = 0; i < n; i++) 
            { 
                operationsCount++;
                x[i] += dx[i]; 
                if (Math.Abs(dx[i]) > maxDiff) 
                    maxDiff = Math.Abs(dx[i]); // Шукаємо найбільшу зміну для перевірки точності 
            } 
 
            // 4. Перевірка умови зупинки (чи досягнута задана точність) 
            if (maxDiff < precision) 
                return x; 
        } 
 
        errorMessage = "Захист від зациклювання: Перевищено максимальну кількість ітерацій."; 
        return x; 
    } 
 
    // Метод Гаусса для розв'язання СЛАР (знаходження вектора приросту dx) 
    private double[]? SolveGauss(double[,] A, double[] B, ref int ops) 
    { 
        int n = B.Length; 
        double[,] mat = new double[n, n + 1]; 
 
        // Формуємо розширену матрицю 
        for (int i = 0; i < n; i++) 
        { 
            for (int j = 0; j < n; j++) 
            {
                ops++; // Підрахунок
                mat[i, j] = A[i, j]; 
            }
            mat[i, n] = B[i]; 
        } 
 
        // Прямий хід Гаусса
        for (int i = 0; i < n; i++) 
        { 
            ops++;
            double maxEl = Math.Abs(mat[i, i]); 
            int maxRow = i; 
            for (int k = i + 1; k < n; k++) 
            { 
                ops++;
                if (Math.Abs(mat[k, i]) > maxEl) 
                { 
                    maxEl = Math.Abs(mat[k, i]); 
                    maxRow = k; 
                } 
            } 
 
            if (maxEl < 1e-10) return null; 
 
            // Міняємо рядки місцями 
            for (int k = i; k < n + 1; k++) 
            { 
                ops++;
                (mat[maxRow, k], mat[i, k]) = (mat[i, k], mat[maxRow, k]); 
            } 
 
            // Обнуляємо елементи під головним 
            for (int k = i + 1; k < n; k++) 
            { 
                ops++;
                double c = -mat[k, i] / mat[i, i]; 
                for (int j = i; j < n + 1; j++) 
                { 
                    ops++;
                    if (i == j) mat[k, j] = 0; 
                    else mat[k, j] += c * mat[i, j]; 
                } 
            } 
        } 
 
        // Зворотний хід 
        double[] x = new double[n]; 
        for (int i = n - 1; i >= 0; i--) 
        { 
            ops++;
            x[i] = mat[i, n]; 
            for (int j = i + 1; j < n; j++) 
            {
                ops++;
                x[i] -= mat[i, j] * x[j]; 
            }
            x[i] /= mat[i, i]; 
        } 
 
        return x; 
    } 
}