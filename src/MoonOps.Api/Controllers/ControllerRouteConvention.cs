using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace MoonOps.Api.Controllers;

public class ControllerRouteConvention : IControllerModelConvention
{
    public void Apply(ControllerModel controller)
    {
        Console.WriteLine($"Aplicando convenção para: {controller.ControllerType.Name}");
        Console.WriteLine($"Namespace: {controller.ControllerType.Namespace}");
        
        if (controller.ControllerType.Namespace == null) return;

        // Extrai o segmento após "Controllers"
        var namespaceParts = controller.ControllerType.Namespace
            .Split('.')
            .SkipWhile(x => x != "Controllers")
            .Skip(1) // Pula "Controllers"
            .FirstOrDefault()?.ToLower();

        Console.WriteLine($"Namespace extraído: {namespaceParts}");

        if (string.IsNullOrEmpty(namespaceParts)) 
        {
            Console.WriteLine("Namespace vazio, pulando...");
            return;
        }

        // Limpa seletores existentes e cria nova rota
        controller.Selectors.Clear();
        var template = $"api/{namespaceParts}/[controller]";
        Console.WriteLine($"Template criado: {template}");

        controller.Selectors.Add(new SelectorModel
        {
            AttributeRouteModel = new AttributeRouteModel
            {
                Template = template
            }
        });
        Console.WriteLine($"Rota configurada para {controller.ControllerType.Name}");
    }
}