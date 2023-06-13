using System.Collections;
using System.Reflection;
using MudBlazor;

namespace IFOA.Blazor.Helpers;

public static class GraphicHelpers
{
    public static string GetMudIconByName(string? name,
        string defaultIcon,
        bool? includeFilled = false,
        bool? includeOutlined = false,
        bool? includeRounded = false,
        bool? includeSharp = false,
        bool? includeTwoTone = false)
    {
        if (string.IsNullOrEmpty(name))
            return Icons.Material.Filled.Work;
        
        //Get property value by property name
        var brand = new Icons.Custom.Brands();

        var constant = GetConstants<string>(typeof(Icons.Custom.Brands));
        
        if(includeFilled == true)
        {
            var constant1 = GetConstants<string>(typeof(Icons.Material.Filled));
            constant.AddRange(constant1);
        }
        
        if(includeOutlined == true)
        {
            var constant2 = GetConstants<string>(typeof(Icons.Material.Outlined));
            constant.AddRange(constant2);
        }
        
        if(includeRounded == true)
        {
            var constant3 = GetConstants<string>(typeof(Icons.Material.Rounded));
            constant.AddRange(constant3);
        }
        
        if(includeSharp == true)
        {
            var constant3 = GetConstants<string>(typeof(Icons.Material.Sharp));
            constant.AddRange(constant3);
        }
        
        if(includeTwoTone == true)
        {
            var constant3 = GetConstants<string>(typeof(Icons.Material.TwoTone));
            constant.AddRange(constant3);
        }
        

        var value =  constant?.FirstOrDefault(x => x.FieldInfo.Name == name)?.Value;

        return value ?? defaultIcon;
    }
    public static List<FieldInfoValue<T>> GetConstants<T>(Type type)
    {
        FieldInfo[] fieldInfos = type.GetFields(BindingFlags.Public |
                                                BindingFlags.Static | BindingFlags.FlattenHierarchy);

        var data =  fieldInfos.Where(fi => fi.IsLiteral && !fi.IsInitOnly)
            .Select(x=> new FieldInfoValue<T>()
            {
                FieldInfo = x,
                Value = (T?)x.GetValue(null)
            })
            .ToList();

        return data;

    }

    public class FieldInfoValue<T>
    {
        public FieldInfo FieldInfo { get; set; }
        public T? Value { get; set; }
    }
    
}