﻿using System.Windows.Media;

namespace LitKit1.ControlsWPF
{
    static class SolutionBrushes
    {
        public static Brush Text_Primary = (Brush)new BrushConverter().ConvertFromString("#1F1F1F");
        public static Brush Text_Secondary = (Brush)new BrushConverter().ConvertFromString("#6A6866");
        public static Brush Text_Ancillary = (Brush)new BrushConverter().ConvertFromString("#9D9D9D");

        public static Brush RibbonBackground = (Brush)new BrushConverter().ConvertFromString("#f3f2f1");

        public static Brush Primary_DomGrey = (Brush)new BrushConverter().ConvertFromString("#f2f2f2");
        public static Brush Primary_DarkGrey = (Brush)new BrushConverter().ConvertFromString("#66696c");
        public static Brush Primary_MidGrey = (Brush)new BrushConverter().ConvertFromString("#a2a4a5");
        public static Brush Primary_LightGrey = (Brush)new BrushConverter().ConvertFromString("#c4c6c7");
        public static Brush Primary_DarkOrange = (Brush)new BrushConverter().ConvertFromString("#dd7c47");
        public static Brush Primary_MidOrange = (Brush)new BrushConverter().ConvertFromString("#df9870");
        public static Brush Primary_LightOrange = (Brush)new BrushConverter().ConvertFromString("#eac5a4");
        public static Brush Primary_Blue = (Brush)new BrushConverter().ConvertFromString("#84accc");
        public static Brush Primary_MidRed = (Brush)new BrushConverter().ConvertFromString("#d9ab9f");
        public static Brush Primary_LightRed = (Brush)new BrushConverter().ConvertFromString("#e8c9c3");

        public static Brush Exhibit = (Brush)new BrushConverter().ConvertFromString("#136F63");
        public static Brush LegalCite = (Brush)new BrushConverter().ConvertFromString("#619efa"); /*("#3F88C5");*/
        public static Brush RecordCite = (Brush)new BrushConverter().ConvertFromString("#D00000");
        public static Brush OtherCite = (Brush)new BrushConverter().ConvertFromString("#FFBA08");
        public static Brush Extra = (Brush)new BrushConverter().ConvertFromString("#032B43");


        public static Color ExhibitColor = new Color() { R = 19, G = 111, B = 99, A = 20 };
        public static Color LegalCiteColor = new Color() { R = 63, G = 136, B = 197, A = 20 };
        public static Color RecordCiteColor = new Color() { R = 208, G = 0, B = 0, A = 20 };
        public static Color OtherCiteColor = new Color() { R = 255, G = 186, B = 8, A = 20 };
        public static Color ExtraColor = new Color() { R = 3, G = 43, B = 67, A = 20 };


        public static Brush Icon_Orange = (Brush)new BrushConverter().ConvertFromString("#dd6d4f");
        public static Brush Icon_Blue = (Brush)new BrushConverter().ConvertFromString("#4d82b8");
        public static Brush Icon_Gray = (Brush)new BrushConverter().ConvertFromString("#808080");

        /*
         #dbdbd7 
        #070808
        #656a7c
        #373f5e
        #96969c
        #9fa9b1
        #9f9583
        #b8b1a6
        #64593f
        #b2b3ba
        #fadc95

        white
        #e9e9e0
        #eaebe6
        #efecdf
        #d1cfc1

        Green
        #466339
        #4e6d43
        #9cac97


         */
    }

}
