using Coursework.Models; 
 
namespace Coursework.Solvers; 
 
public interface INonlinearSolver 
{ 
    // Повертає масив знайдених коренів (вектор x) 
    // Використовуємо out-параметри для повернення кількості ітерацій, операцій та повідомлень про помилки 
    double[] Solve(SystemModel model, double[] initialGuess, double precision, int maxIterations, out int iterationsTaken, out int operationsCount, out string errorMessage); 
}