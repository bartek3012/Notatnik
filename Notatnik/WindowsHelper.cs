/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using System.Drawing;

namespace Notatnik_2
{
   public static class WindowsHelper
    {
        public static System.Drawing.Color Convert(this Color color)
        {
            return System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
        }

        public static Color Convert(this System.Drawing.Color color)
        {
            return new Color()
            {
                A = color.A,
                R = color.R,
                G = color.G,
                B = color.B
            };
        }

        public static bool ChooseFont(ref Font font)
        {
            using(System.Windows.Forms.FontDialog fontDialog = new System.Windows.Forms.FontDialog())
            {
                fontDialog.ShowColor = true;
                fontDialog.ShowEffects = true;
                fontDialog.Font = Font.ToSystemDrawingFont(font);
                fontDialog.Color.Convert();

                bool result = fontDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK;
                if 
            }
        }
    }
}
*/