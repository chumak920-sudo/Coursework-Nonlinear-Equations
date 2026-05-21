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
    
    // Метод з лічильником операцій
    public double CalculateFunction(int i, double[] x, ref int ops) 
    { 
        double sum = 0; 
        for (int j = 0; j < N; j++) 
        { 
            ops++; // Перевірка умови = ітерація
            
            switch (Type) 
            { 
                case EquationType.Algebraic: 
                    ops += 3; // 1 додавання, 1 множення, 1 піднесення до степеня
                    sum += A[i, j] * Math.Pow(x[j], 2); 
                    break; 
                case EquationType.Trigonometric: 
                    ops += 3; // 1 додавання, 1 множення, 1 виклик Sin
                    sum += A[i, j] * Math.Sin(x[j]); 
                    break; 
                case EquationType.Exponential: 
                    ops += 3; // 1 додавання, 1 множення, 1 виклик Exp
                    sum += A[i, j] * Math.Exp(x[j]); 
                    break; 
            } 
        } 
        ops++; // Операція віднімання
        return sum - B[i]; 
    } 

    // Звичайний метод (щоб не зламати GraphForm та перевірку в MainForm)
    public double CalculateFunction(int i, double[] x) 
    { 
        int dummyOps = 0; // Заглушка, яка нікуди не йде
        return CalculateFunction(i, x, ref dummyOps); 
    }

    // Похідна з лічильником операцій
    public double CalculateDerivative(int i, int j, double[] x, ref int ops) 
    { 
        switch (Type) 
        { 
            case EquationType.Algebraic: 
                ops += 2; // 2 множення
                return 2 * A[i, j] * x[j]; 
            case EquationType.Trigonometric: 
                ops += 2; // 1 множення, 1 виклик Cos
                return A[i, j] * Math.Cos(x[j]); 
            case EquationType.Exponential: 
                ops += 2; // 1 множення, 1 виклик Exp
                return A[i, j] * Math.Exp(x[j]); 
            default: 
                throw new InvalidOperationException("Невідомий тип рівняння"); 
        } 
    } 
}