using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visual_Object
{



    public abstract class TargetObject
    {
        private enum VisualTarget
        {
            Potato,
            Grape,
        }

        private VisualTarget objectTarget;
        private int[] TargetColor = new int[3];

        private void test()
        {
            System.Console.WriteLine("Test");//Console is not found in system
        }
    }


}