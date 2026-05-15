using System;
using Coursework.Models;

namespace Coursework.Solvers;

public class SecantSolver : INonlinearSolver
{
    public double[] Solve(SystemModel model, double[] initialGuess, double precision, int maxIterations, out int iterationsTaken, out string errorMessage)
    {
        int n = model.N;
        double[] x = (double[])initialGuess.Clone();
        iterationsTaken = 0;
        errorMessage = string.Empty;
        
        // Малий крок для побудови січної
        double h = 1e-5; 

        for (int iter = 0; iter < maxIterations; iter++)
        {
            iterationsTaken++;
            double[,] J = new double[n, n];
            double[] F = new double[n];

            // 1. Обчислюємо вектор -F(x)
            for (int i = 0; i < n; i++)
            {
                F[i] = -model.CalculateFunction(i, x); 
            }

            // 2. Будуємо матрицю Якобі чисельно (метод січних для систем)
            for (int j = 0; j < n; j++)
            {
                double[] xPlusH = (double[])x.Clone();
                xPlusH[j] += h; // Зсуваємо лише одну координату

                for (int i = 0; i < n; i++)
                {
                    double fPlusH = model.CalculateFunction(i, xPlusH);
                    double fNormal = -F[i]; // Повертаємо справжнє значення F[i] без мінуса
                    
                    // Формула січної: ( f(x+h) - f(x) ) / h
                    J[i, j] = (fPlusH - fNormal) / h;
                }
            }

            // 3. Розв'язуємо СЛАР J * dx = -F
            double[] dx = SolveGauss(J, F);
            if (dx == null)
            {
                errorMessage = "Особливий випадок: Матриця січних вироджена.";
                return x;
            }

            // 4. Оновлюємо x
            double maxDiff = 0;
            for (int i = 0; i < n; i++)
            {
                x[i] += dx[i];
                if (Math.Abs(dx[i]) > maxDiff) maxDiff = Math.Abs(dx[i]);
            }

            // 5. Перевірка зупинки
            if (maxDiff < precision) return x;
        }

        errorMessage = "Захист від зациклювання: Перевищено максимальну кількість ітерацій.";
        return x;
    }

    // Копіюємо метод Гауса з Ньютона (для ізольованості класів)
    private double[] SolveGauss(double[,] A, double[] B)
    {
        int n = B.Length;
        double[,] mat = new double[n, n + 1];

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++) mat[i, j] = A[i, j];
            mat[i, n] = B[i];
        }

        for (int i = 0; i < n; i++)
        {
            double maxEl = Math.Abs(mat[i, i]);
            int maxRow = i;
            for (int k = i + 1; k < n; k++)
            {
                if (Math.Abs(mat[k, i]) > maxEl)
                {
                    maxEl = Math.Abs(mat[k, i]);
                    maxRow = k;
                }
            }

            if (maxEl < 1e-10) return null;

            for (int k = i; k < n + 1; k++)
            {
                (mat[maxRow, k], mat[i, k]) = (mat[i, k], mat[maxRow, k]);
            }

            for (int k = i + 1; k < n; k++)
            {
                double c = -mat[k, i] / mat[i, i];
                for (int j = i; j < n + 1; j++)
                {
                    if (i == j) mat[k, j] = 0;
                    else mat[k, j] += c * mat[i, j];
                }
            }
        }

        double[] x = new double[n];
        for (int i = n - 1; i >= 0; i--)
        {
            x[i] = mat[i, n];
            for (int j = i + 1; j < n; j++) x[i] -= mat[i, j] * x[j];
            x[i] /= mat[i, i];
        }

        return x;
    }
}