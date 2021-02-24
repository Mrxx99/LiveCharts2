﻿// The MIT License(MIT)

// Copyright(c) 2021 Alberto Rodriguez Orozco & LiveCharts Contributors

// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using LiveChartsCore.Drawing;
using LiveChartsCore.Motion;
using SkiaSharp;

namespace LiveChartsCore.SkiaSharpView.Drawing
{
    public class DoughnutGeometry : Geometry, IDoughnutGeometry<SkiaDrawingContext>
    {
        private readonly FloatMotionProperty wProperty;
        private readonly FloatMotionProperty hProperty;
        private readonly FloatMotionProperty startProperty;
        private readonly FloatMotionProperty sweepProperty;

        public DoughnutGeometry()
        {
            wProperty = RegisterMotionProperty(new FloatMotionProperty(nameof(Width)));
            hProperty = RegisterMotionProperty(new FloatMotionProperty(nameof(Height)));
            startProperty = RegisterMotionProperty(new FloatMotionProperty(nameof(StartAngle)));
            sweepProperty = RegisterMotionProperty(new FloatMotionProperty(nameof(SweepAngle)));
        }

        public float Width { get => wProperty.GetMovement(this); set => wProperty.SetMovement(value, this); }
        public float Height { get => hProperty.GetMovement(this); set => hProperty.SetMovement(value, this); }
        public float StartAngle { get => startProperty.GetMovement(this); set => startProperty.SetMovement(value, this); }
        public float SweepAngle { get => sweepProperty.GetMovement(this); set => sweepProperty.SetMovement(value, this); }

        public override SKSize Measure(SkiaDrawingContext context, SKPaint paint)
        {
            return new SKSize(Width, Height);
        }

        public override void OnDraw(SkiaDrawingContext context, SKPaint paint)
        {
            SKPath path = new SKPath();

            path.ArcTo(
                new SKRect { Left = X, Top = Y, Size = new SKSize { Width = Width, Height = Height } }, 
                StartAngle,
                SweepAngle,
                false);

            path.Close();

            context.Canvas.DrawPath(path, context.Paint);
        }
    }
}
