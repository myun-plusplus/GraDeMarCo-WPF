using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace GraDeMarCoWPF.Models
{
    [Serializable]
    public class Dot
    {
        public Point Location;
        public Color Color;
        public double Size;

        public Dot Clone()
        {
            return new Dot
            {
                Location = this.Location,
                Color = this.Color,
                Size = this.Size
            };
        }
    }

    [Serializable]
    public class DotData
    {
        public List<Dot> Dots;

        // false:draw, true:erase
        public List<Tuple<Dot, bool>> DoneList, UndoList;

        public DotData()
        {
            Dots = new List<Dot>();
            DoneList = new List<Tuple<Dot, bool>>();
            UndoList = new List<Tuple<Dot, bool>>();
        }
    }
}
