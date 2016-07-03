using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowProgram.DesignTime
{
    public class ThemeConfiguration
    {
        public Dictionary<Type, Theme> Directory;
        public Theme DefaultTheme;

        public ThemeConfiguration()
        {
            Directory = new Dictionary<Type, Theme>();
            DefaultTheme = new Theme();
        }
    }
}
