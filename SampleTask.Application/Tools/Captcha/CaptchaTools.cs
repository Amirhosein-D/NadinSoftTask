
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace SampleTask.Application.Tools.Captcha
{
    public interface ICaptchaTools
    {
        string GenerateRandomCode_SimplePhrase();

        byte[] GenerateCaptchaImage_SimplePhrase(string captchaCode);

        void AddNoise(Bitmap bitmap);


        string GenerateRandomCode_MathematicalPhrase();

        int CalculateMathExpression(string mathExpression);

        byte[] GenerateCaptchaImage_MathematicalPhrase(string captchaCode);

        void AddCreativeEffects(SKCanvas canvas);
    }

    public class CaptchaTools : ICaptchaTools
    {

       
        public string GenerateRandomCode_SimplePhrase()
        {
            Random random = new Random();

            int length = 6;

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public byte[] GenerateCaptchaImage_SimplePhrase(string captchaCode)
        {
            using (Bitmap bitmap = new Bitmap(120, 40))
            {

                using (Graphics graphics = Graphics.FromImage(bitmap))
                {

                    graphics.SmoothingMode = SmoothingMode.AntiAlias;

                    graphics.Clear(System.Drawing.Color.White);

                    Font font = new Font("Arial", 20, FontStyle.Bold);

                    Brush brush = new SolidBrush(System.Drawing.Color.Black);

                    graphics.DrawString(captchaCode, font, brush, 10, 10);

                    AddNoise(bitmap);

                    using (MemoryStream stream = new MemoryStream())
                    {
                        bitmap.Save(stream, ImageFormat.Jpeg);
                        return stream.ToArray();
                    }
                }
            }   
        }


        public void AddNoise(Bitmap bitmap)
        {
            Random random = new Random();

            int noiseLevel = 150;

            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    if (random.NextDouble() > 0.9)
                    {
                        int alpha = random.Next(0, noiseLevel);
                        int red = random.Next(0, noiseLevel);
                        int green = random.Next(0, noiseLevel);
                        int blue = random.Next(0, noiseLevel);

                        System.Drawing.Color noiseColor = System.Drawing.Color.FromArgb(alpha, red, green, blue);

                        bitmap.SetPixel(x, y, noiseColor);
                    }
                }
            }
        }





        public string GenerateRandomCode_MathematicalPhrase()
        {
            Random random = new Random();

            int operand1 = random.Next(1, 10);
            int operand2 = random.Next(1, 10);

            char[] operators = { '+', '×' };
            char selectedOperator = operators[random.Next(operators.Length)];

            string mathExpression = $"{operand1} {selectedOperator} {operand2}";

            return mathExpression;
        }
        
        public int CalculateMathExpression(string mathExpression)
        {
            string[] parts = mathExpression.Split(' ');

            if (parts.Length != 3)
            {
                return 0;
            }

            int operand1 = int.Parse(parts[0]);
            int operand2 = int.Parse(parts[2]);
            char selectedOperator = parts[1][0];

            switch (selectedOperator)
            {
                case '+':
                    return operand1 + operand2;
                case '×':
                    return operand1 * operand2;
                default:
                    return 0;
            }
        }

        public byte[] GenerateCaptchaImage_MathematicalPhrase(string captchaCode)
        {
            using (var surface = SKSurface.Create(new SKImageInfo(120, 40)))
            {
                var canvas = surface.Canvas;

                canvas.Clear(SKColors.White);

                AddCreativeEffects(canvas);

                using (var paint = new SKPaint())
                {
                    paint.TextSize = 30;
                    paint.IsAntialias = true;
                    paint.Color = SKColors.Black;
                    paint.FakeBoldText = true;

                    canvas.DrawText(captchaCode, 10, 30, paint);
                }

                using (var image = surface.Snapshot())
                using (var data = image.Encode(SKEncodedImageFormat.Jpeg, 80))
                using (var stream = new MemoryStream())
                {
                    data.SaveTo(stream);
                    return stream.ToArray();
                }
            }
        }

        public void AddCreativeEffects(SKCanvas canvas)
        {
            Random random = new Random();

            int linesCount = 5;

            for (int i = 0; i < linesCount; i++)
            {
                float x1 = random.Next(120);
                float y1 = random.Next(40);
                float x2 = random.Next(120);
                float y2 = random.Next(40);

                var paint = new SKPaint
                {
                    Color = new SKColor((byte)random.Next(256), (byte)random.Next(256), (byte)random.Next(256)),
                    StrokeWidth = 2
                };

                canvas.DrawLine(x1, y1, x2, y2, paint);
            }

            int rectanglesCount = 5;

            for (int i = 0; i < rectanglesCount; i++)
            {
                float left = random.Next(120);
                float top = random.Next(40);
                float right = random.Next((int)left, 120);
                float bottom = random.Next((int)top, 40);

                var paint = new SKPaint
                {
                    Color = new SKColor((byte)random.Next(256), (byte)random.Next(256), (byte)random.Next(256)),
                    Style = SKPaintStyle.Stroke,
                    StrokeWidth = 2
                };

                canvas.DrawRect(left, top, right - left, bottom - top, paint);
            }
        }


    }
}
