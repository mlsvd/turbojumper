using TurboJumper.Models;

namespace TurboJumper.Decorators;

public class ProcessListIconDecorator: IProcessListDecorator
{
    public List<ProcessWrapper> Decorate(List<ProcessWrapper> processWrappers)
    {
        List<ProcessWrapper> result = new List<ProcessWrapper>();
        foreach (ProcessWrapper processWrapper in processWrappers)
        {
            Image? image = null;
            try
            {
                Icon? icon = Icon.ExtractAssociatedIcon(processWrapper.GetMainModuleFileName() ?? "");
                if (icon != null)
                {
                    image = icon.ToBitmap();
                }
                else
                {
                    throw new Exception("Icon cannot be retrieved");
                }
            }
            catch (Exception)
            {
                image = Image.FromFile("src\\Resources\\Images\\default_app_icon.png");
            }

            processWrapper.AppIcon = image;
            
            result.Add(processWrapper);
        }

        return result;
    }
}
