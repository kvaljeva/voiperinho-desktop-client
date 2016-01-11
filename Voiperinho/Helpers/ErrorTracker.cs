using System.Collections.Generic;
using System.Windows.Forms;

namespace Voiperinho.Helpers
{
    public class ErrorTracker
    {
        private static Dictionary<Control, ErrorProvider> cErrors = new Dictionary<Control, ErrorProvider>();
        private static ErrorProvider cErrorProvider;

        public static void SetError(Control control, ErrorProvider errorProvider)
        {
            if (!cErrors.ContainsKey(control))
            {
                cErrorProvider = new ErrorProvider();
                cErrorProvider = errorProvider;

                cErrors.Add(control, cErrorProvider);
            }
        }

        public static void DisplayError(string errorText = "")
        {
            foreach (KeyValuePair<Control, ErrorProvider> item in cErrors)
            {
                item.Value.SetIconPadding(item.Key, 5);
                item.Value.SetError(item.Key, errorText);
            }
        }

        public static bool RemoveError(Control control)
        {
            if (cErrors.ContainsKey(control))
            {
                ErrorProvider error = cErrors[control]; 
                cErrors.Remove(control);
                error.Clear();

                return true;
            }

            return false;
        }

        public static void ClearErrors()
        {
            cErrors.Clear();
        }
    }
}
