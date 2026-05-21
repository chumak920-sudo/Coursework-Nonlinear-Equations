using Coursework.Models; 
 
namespace Coursework.Solvers; 
 
public class SecantSolver : INonlinearSolver 
{ 
    public double[] Solve(SystemModel model, double[] initialGuess, double precision, int maxIterations, out int iterationsTaken, out int operationsCount, out string errorMessage) 
    { 
        int n = model.N; 
        double[] x = (double[])initialGuess.Clone(); 
        iterationsTaken = 0; // Лічильник ітерацій
        operationsCount = 0; // Лічильник операцій
        errorMessage = string.Empty; 
         
        double h = 1e-5;  
 
        for (int iter = 0; iter < maxIterations; iter++) 
        { 
            iterationsTaken++; 
            double[,] J = new double[n, n]; 
            double[] F = new double[n]; 
 
            // 1. Обчислюємо вектор -F(x) 
            for (int i = 0; i < n; i++) 
            { 
                operationsCount++; // Рахуємо зміну знаку (унарний мінус)
                F[i] = -model.CalculateFunction(i, x, ref operationsCount);  
            } 
 
            // 2. Будуємо матрицю Якобі чисельно (метод січних для систем) 
            for (int j = 0; j < n; j++) 
            { 
                double[] xPlusH = (double[])x.Clone(); 
                
                operationsCount++; // Додавання
                xPlusH[j] += h;  
 
                for (int i = 0; i < n; i++) 
                { 
                    // Прокидаємо лічильник сюди
                    double fPlusH = model.CalculateFunction(i, xPlusH, ref operationsCount); 
                    double fNormal = -F[i]; 
                     
                    operationsCount += 2; // 1 віднімання та 1 ділення
                    J[i, j] = (fPlusH - fNormal) / h; 
                } 
            }
 
            // 3. Розв'язуємо СЛАР J * dx = -F 
            int gaussOps = 0;
            double[]? dx = SolveGauss(J, F, ref gaussOps); 
            operationsCount += gaussOps;
 
            if (dx == null) 
            { 
                errorMessage = "Особливий випадок: Матриця січних вироджена."; 
                return x; 
            } 
 
            // 4. Оновлюємо x 
            double maxDiff = 0; 
            for (int i = 0; i < n; i++) 
            { 
                operationsCount++;
                x[i] += dx[i]; 
                if (Math.Abs(dx[i]) > maxDiff) maxDiff = Math.Abs(dx[i]); 
            } 
 
            // 5. Перевірка зупинки 
            if (maxDiff < precision) return x; 
        } 
 
        errorMessage = "Захист від зациклювання: Перевищено максимальну кількість ітерацій."; 
        return x; 
    } 
 
    private double[]? SolveGauss(double[,] A, double[] B, ref int ops) 
    { 
        int n = B.Length; 
        double[,] mat = new double[n, n + 1]; 
 
        for (int i = 0; i < n; i++) 
        { 
            for (int j = 0; j < n; j++) 
            {
                ops++;
                mat[i, j] = A[i, j]; 
            }
            mat[i, n] = B[i]; 
        } 
 
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
 
            for (int k = i; k < n + 1; k++) 
            { 
                ops++;
                (mat[maxRow, k], mat[i, k]) = (mat[i, k], mat[maxRow, k]); 
            } 
 
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