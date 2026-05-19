// ReSharper disable LocalizableElement
// ReSharper disable VirtualMemberCallInConstructor
using Coursework.Models;

namespace Coursework.UI;

public class GraphForm : Form
{
    private readonly SystemModel _model;
    private readonly double _rootX;
    private readonly double _rootY;
    private readonly bool _isSuccess;

    public GraphForm(SystemModel model, double rootX, double rootY, bool isSuccess)
    {
        _model = model;
        _rootX = rootX;
        _rootY = rootY;
        _isSuccess = isSuccess;

        Text = "Графік системи рівнянь";
        Size = new Size(600, 600);
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        Graphics g = e.Graphics;
        
        int w = ClientSize.Width;
        int h = ClientSize.Height;

        g.Clear(Color.White);

        // Показуємо графік в радіусі 5 одиниць навколо знайденого кореня
        double range = 5.0;
        double minX = _rootX - range;
        double maxX = _rootX + range;
        double minY = _rootY - range;
        double maxY = _rootY + range;

        double dx = (maxX - minX) / w;
        double dy = (maxY - minY) / h;

        // Малюємо осі координат
        using Pen axisPen = new Pen(Color.LightGray, 2);
        int xAxisY = (int)(h - (0 - minY) / dy); 
        int yAxisX = (int)((0 - minX) / dx);     

        if (xAxisY >= 0 && xAxisY <= h) g.DrawLine(axisPen, 0, xAxisY, w, xAxisY);
        if (yAxisX >= 0 && yAxisX <= w) g.DrawLine(axisPen, yAxisX, 0, yAxisX, h);

        // Малюємо неявні функції алгоритмом Zero-crossing
        using Bitmap bmp = new Bitmap(w, h);
        for (int px = 0; px < w - 1; px++)
        {
            for (int py = 0; py < h - 1; py++)
            {
                double x = minX + px * dx;
                double y = maxY - py * dy;

                // Перевіряємо перше рівняння (Червоний)
                double v1 = _model.CalculateFunction(0, [x, y]);
                double v2 = _model.CalculateFunction(0, [x + dx, y]);
                double v3 = _model.CalculateFunction(0, [x, y - dy]);
                if (v1 * v2 <= 0 || v1 * v3 <= 0) bmp.SetPixel(px, py, Color.Red);

                // Перевіряємо друге рівняння (Синій)
                double u1 = _model.CalculateFunction(1, [x, y]);
                double u2 = _model.CalculateFunction(1, [x + dx, y]);
                double u3 = _model.CalculateFunction(1, [x, y - dy]);
                if (u1 * u2 <= 0 || u1 * u3 <= 0) bmp.SetPixel(px, py, Color.Blue);
            }
        }
        g.DrawImage(bmp, 0, 0);
        
        // Змінюємо шрифт
        using Font legendFont = new Font("Arial", 12, FontStyle.Bold);

        // Ставимо зелену точку тільки якщо розв'язок знайдено
        if (_isSuccess)
        {
            int rootPx = (int)((_rootX - minX) / dx);
            int rootPy = (int)((maxY - _rootY) / dy);
            g.FillEllipse(Brushes.Green, rootPx - 5, rootPy - 5, 10, 10);
            g.DrawString($"Результат ({_rootX:F2}; {_rootY:F2})", legendFont, Brushes.Green, 10, 50);
        }
        else
        {
            g.DrawString("Розв'язок відсутній", legendFont, Brushes.Gray, 10, 50);
        }

        // Легенда
        g.DrawString("Рівняння 1", legendFont, Brushes.Red, 10, 10);
        g.DrawString("Рівняння 2", legendFont, Brushes.Blue, 10, 30);
    }
}