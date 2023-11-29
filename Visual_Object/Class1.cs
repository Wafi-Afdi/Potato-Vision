using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visual_Object
{

    public enum VisualTargetSelection
    {
        Apple,
        Grape,
    }

    public class ColorTarget
    {
        public int[] hsvBound { get; private set; }

        public VisualTargetSelection target { private set; get; }
        public string ColorName { private set; get; }

        public ColorTarget(VisualTargetSelection target, int[] Bound, string name )
        {
            if (Bound.Length > 6)
            {
                throw new Exception("Maximum array size is 6 for Bound");
            }
            this.target = target;
            this.hsvBound = Bound;
            this.ColorName = name;
        }

    }

    public class TargetObject
    {
        // Taruh HSV Bound Disini
        // Statik variable untuk dipakai
        private static int[] RedAppleBound = { 1, 2, 3, 4, 5, 6 };
        private static int[] GreenAppleBound = { 1, 2, 3, 4, 5, 6 };
        private static ColorTarget RedApple = new ColorTarget(VisualTargetSelection.Apple, RedAppleBound, "Red Apple");
        private static ColorTarget GreenApple = new ColorTarget(VisualTargetSelection.Apple, GreenAppleBound, "Green Apple");

        private static List<ColorTarget> AppleTarget = new List<ColorTarget> { RedApple, GreenApple }; // Target List untuk Apple
        private static List<ColorTarget> GrapeTarget = new List<ColorTarget> { }; // Target List untuk anggur

        // Data objek yang ditarget
        private VisualTargetSelection? objectTarget;
        private int[] targetRange = new int[6];
        private List<int[]> rejectedRange = new List<int[]> { };

        // Buat yang terpilih
        private string terpilih = "";
        public ColorTarget? warnaDipilih { private set; get; }
        public List<ColorTarget>? targetDipilih { private set; get; }

        public void SettVisualTargetSelection (VisualTargetSelection? selection)
        {
            objectTarget = selection;  
            switch(selection)
            {
                case VisualTargetSelection.Apple:
                    targetDipilih = AppleTarget;
                    break;
                case VisualTargetSelection.Grape:
                    targetDipilih = GrapeTarget;
                    break;
                case null:
                    targetDipilih = AppleTarget;
                    break;
            }
        }

        public VisualTargetSelection? GetTargetVisualSelection ()
        {
            return objectTarget;
        }

        public int[] GetTargetColor ()
        {
            return targetRange;
        }

        public void SetTargetColor()
        {
            if(warnaDipilih == null)
            {
                throw new Exception("Null Class warnaDiplih");
            } 
            else if(targetDipilih == null)
            {
                throw new Exception("Null Class targetDipilih");
            }

            targetRange = warnaDipilih.hsvBound;
            terpilih = warnaDipilih.ColorName;

            for (int i= 0; i < targetDipilih.Count; i++)
            {
                if (terpilih == targetDipilih.ElementAt(i).ColorName) continue;

                rejectedRange.Add(targetDipilih.ElementAt(i).hsvBound);
            }
        }

    }

    public class SaveData
    {

        public int AcceptView;
        public int RejectView;

        public int total { get; private set; }

        public void Total()
        {
            total = this.AcceptView + this.RejectView;
        }


    }

}