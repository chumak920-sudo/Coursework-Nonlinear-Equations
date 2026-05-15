using System;

namespace Coursework.Models;

public class SystemModel
{
    public int N { get; set; }
    public double[,] A { get; set; } // Матриця коефіцієнтів a_ij
    public double[] B { get; set; }  // Вектор вільних членів b_i
    public EquationType Type { get; set; }

    public SystemModel(int n, EquationType type)
    {
        N = n;
        Type = type;
        A = new double[N, N];
        B = new double[N];
    }

    // Значення i-того рівняння в точці x
    public double CalculateFunction(int i, double[] x)
    {
        double sum = 0;
        for (int j = 0; j < N; j++)
        {
            switch (Type)
            {
                case EquationType.Algebraic:
                    sum += A[i, j] * Math.Pow(x[j], 2);
                    break;
                case EquationType.Trigonometric:
                    sum += A[i, j] * Math.Sin(x[j]);
                    break;
                case EquationType.Exponential:
                    sum += A[i, j] * Math.Exp(x[j]);
                    break;
            }
        }
        return sum - B[i];
    }

    // Похідна i-того рівняння по j-тій змінній (для матриці Якобі)
    public double CalculateDerivative(int i, int j, double[] x)
    {
        switch (Type)
        {
            case EquationType.Algebraic:
                return 2 * A[i, j] * x[j];
            case EquationType.Trigonometric:
                return A[i, j] * Math.Cos(x[j]);
            case EquationType.Exponential:
                return A[i, j] * Math.Exp(x[j]);
            default:
                throw new InvalidOperationException("Невідомий тип рівняння");
        }
    }
}