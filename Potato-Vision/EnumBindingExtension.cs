using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using Visual_Object;

namespace Potato_Vision
{
    public class EnumBindingExtension : MarkupExtension 
    {
        public Type EnumType { get; private set; }

        public EnumBindingExtension(Type enumType)
        {
            if (enumType is null || !enumType.IsEnum)
            {
                throw new Exception("Enum Type must not be a null type");
            } 

            EnumType = enumType;
        }


        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Enum.GetValues(EnumType);
        }
    }
}
