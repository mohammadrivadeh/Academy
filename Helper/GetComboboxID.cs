using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Controls;

namespace AcademyEFCore_NET7.Helper
{
    public class GetComboboxID
    {
        public int GetSelectedId(RadComboBox comboBox)
        {
            if (comboBox.SelectedItem != null)
            {
                var selectedValue = comboBox.SelectedValue;
                if (selectedValue is int)
                {
                    return (int)selectedValue;
                }
            }
            return 0;
        }
    }
}
