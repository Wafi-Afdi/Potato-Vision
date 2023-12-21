using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visual_Object
{

    public enum VisualTargetSelection
    {
        Apple,
        Paprika,
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
        private static int[] RedAppleBound = { 171, 74, 56, 255, 255, 255 };
        private static int[] GreenAppleBound = { 28, 30, 32, 36, 255, 255 };
        private static ColorTarget RedApple = new ColorTarget(VisualTargetSelection.Apple, RedAppleBound, "Red");
        private static ColorTarget GreenApple = new ColorTarget(VisualTargetSelection.Apple, GreenAppleBound, "Green");

        private static int[] RedPaprikaBound = { 0, 55, 120, 10, 255, 255 };
        private static int[] GreenPaprikaBound = { 38, 35, 0, 50, 255, 255 };
        private static int[] YellowPaprikaBound = { 17, 45, 230, 25, 230, 255 };
        private static ColorTarget RedPaprika = new ColorTarget(VisualTargetSelection.Paprika, RedPaprikaBound, "Red");
        private static ColorTarget GreenPaprika = new ColorTarget(VisualTargetSelection.Paprika, GreenPaprikaBound, "Green");
        private static ColorTarget YellowPaprika = new ColorTarget(VisualTargetSelection.Paprika, YellowPaprikaBound, "Yellow");

        private static List<ColorTarget> AppleTarget = new List<ColorTarget> { RedApple, GreenApple }; // Target List untuk Apple
        private static List<ColorTarget> PaprikaTarget = new List<ColorTarget> { RedPaprika, GreenPaprika, YellowPaprika }; // Target List untuk anggur

        private static List<List<ColorTarget>> CollectionFruit = new List<List<ColorTarget>>() { AppleTarget, PaprikaTarget}; // Koleksi Semua target disini
        public static IList<List<ColorTarget>> ReadCollectionFruit = CollectionFruit.AsReadOnly(); // Aksesible oleh luah

        // Data objek yang ditarget
        private VisualTargetSelection objectTarget;
        private int[] targetColorRange = new int[6];
        private List<int[]> rejectedRange = new List<int[]> { };

        // Buat yang terpilih
        private string terpilih = "";
        public ColorTarget? warnaDipilih { private set; get; }
        public List<ColorTarget>? targetDipilih { private set; get; }

        public void SetWarnaDipilih(string terpilih)
        {
            if(targetDipilih== null)
            {
                throw new Exception("targetDipilih is null, cannot continue process");
            }
            for (int i= 0 ; i < targetDipilih.Count(); i++)
            {
                if(targetDipilih.ElementAt(i).ColorName == terpilih)
                {
                    warnaDipilih = targetDipilih[i];
                    return;
                }
            }
            throw new Exception("Invalid Color");
        }

        public void SettVisualTargetSelection (VisualTargetSelection selection)
        {
            objectTarget = selection;  
            switch(selection)
            {
                case VisualTargetSelection.Apple:
                    targetDipilih = AppleTarget;
                    break;
                case VisualTargetSelection.Paprika:
                    targetDipilih = PaprikaTarget;
                    break;
            }
        }

        public VisualTargetSelection GetTargetVisualSelection ()
        {
            return objectTarget;
        }

        public int[] GetTargetColor ()
        {
            return targetColorRange;
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

            rejectedRange.Clear();
            targetColorRange = warnaDipilih.hsvBound;
            terpilih = warnaDipilih.ColorName;

            for (int i= 0; i < targetDipilih.Count; i++)
            {
                if (terpilih == targetDipilih.ElementAt(i).ColorName) continue;

                rejectedRange.Add(targetDipilih.ElementAt(i).hsvBound);
            }
        }

        public List<int[]> GetRejectedRange()
        {
            return this.rejectedRange;
        }

        public string GetWarnaTerpilih()
        {
            return this.terpilih;
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