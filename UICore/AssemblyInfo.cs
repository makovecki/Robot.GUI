using System.Windows;
using System.Windows.Markup;

[assembly: XmlnsPrefix("http://makovecki.com/uicore", "coreUI")]
[assembly: XmlnsDefinition("http://makovecki.com/uicore", "UICore.Dialogs.Controls")]
[assembly: XmlnsDefinition("http://makovecki.com/uicore", "UICore.Buttons.Controls")]
[assembly: XmlnsDefinition("http://makovecki.com/uicore", "UICore.Controls.ProgressBar")]
[assembly: XmlnsDefinition("http://makovecki.com/uicore", "UICore.Controls.TextBlock")]
[assembly: XmlnsDefinition("http://makovecki.com/uicore", "UICore.Converters")]
[assembly: XmlnsDefinition("http://makovecki.com/uicore", "UICore.Presentation.xaml")]
[assembly: ThemeInfo(
    ResourceDictionaryLocation.None, //where theme specific resource dictionaries are located
                                     //(used if a resource is not found in the page,
                                     // or application resource dictionaries)
    ResourceDictionaryLocation.SourceAssembly //where the generic resource dictionary is located
                                              //(used if a resource is not found in the page,
                                              // app, or any theme specific resource dictionaries)
)]
