//-----------------------------------------------------------------------
// <copyright file="SwaggerToCSharpControllerCommand.cs" company="NSwag">
//     Copyright (c) Rico Suter. All rights reserved.
// </copyright>
// <license>https://github.com/RicoSuter/NSwag/blob/master/LICENSE.md</license>
// <author>Rico Suter, mail@rsuter.com</author>
//-----------------------------------------------------------------------

using NConsole;
using NSwag.CodeGeneration.CSharp;
using NSwag.CodeGeneration.CSharp.Models;

#pragma warning disable 1591

namespace NSwag.Commands.CodeGeneration
{
    [Command(Name = "openapi2cscontroller", Description = "Generates CSharp Web API controller code from a Swagger/OpenAPI specification.")]
    public class OpenApiToCSharpControllerCommand : OpenApiToCSharpCommandBase<CSharpControllerGeneratorSettings>
    {
        public OpenApiToCSharpControllerCommand() : base(new CSharpControllerGeneratorSettings())
        {
        }

        [Argument(Name = "ControllerBaseClass", Description = "The controller base class (empty for 'ApiController').", IsRequired = false)]
        public string ControllerBaseClass
        {
            get => Settings.ControllerBaseClass;
            set => Settings.ControllerBaseClass = value;
        }

        [Argument(Name = "ControllerStyle", Description = "The controller generation style (partial, abstract; default: partial).", IsRequired = false)]
        public CSharpControllerStyle ControllerStyle
        {
            get => Settings.ControllerStyle;
            set => Settings.ControllerStyle = value;
        }

        [Argument(Name = "ControllerTarget", Description = "controller target framework (default: AspNetCore).", IsRequired = false)]
        public CSharpControllerTarget ControllerTarget
        {
            get => Settings.ControllerTarget;
            set => Settings.ControllerTarget = value;
        }

        [Argument(Name = "UseCancellationToken", Description = "Add a cancellation token parameter (default: false).", IsRequired = false)]
        public bool UseCancellationToken
        {
            get => Settings.UseCancellationToken;
            set => Settings.UseCancellationToken = value;
        }

        [Argument(Name = "UseActionResultType", Description = "Use ASP.Net Core (2.1) ActionResult type as return type (default: false)", IsRequired = false)]
        public bool UseActionResultType
        {
            get => Settings.UseActionResultType;
            set => Settings.UseActionResultType = value;
        }

        [Argument(Name = "GenerateModelValidationAttributes", Description = "Add model validation attributes (default: false).", IsRequired = false)]
        public bool GenerateModelValidationAttributes
        {
            get => Settings.GenerateModelValidationAttributes;
            set => Settings.GenerateModelValidationAttributes = value;
        }

        [Argument(Name = "RouteNamingStrategy", Description = "The strategy for naming controller routes (none, operationid; default: none).", IsRequired = false)]
        public CSharpControllerRouteNamingStrategy RouteNamingStrategy
        {
            get => Settings.RouteNamingStrategy;
            set => Settings.RouteNamingStrategy = value;
        }

        [Argument(Name = "BasePath", Description = "The Base path on which the API is served, which is relative to the Host", IsRequired = false)]
        public string BasePath
        {
            get => Settings.BasePath;
            set => Settings.BasePath = value;
        }

        public override async Task<object> RunAsync(CommandLineProcessor processor, IConsoleHost host)
        {
            var code = await RunAsync();
            await TryWriteFileOutputAsync(host, () => code).ConfigureAwait(false);
            return code;
        }

        public async Task<string> RunAsync()
        {
            var document = await GetInputSwaggerDocument().ConfigureAwait(false);
            var clientGenerator = new CSharpControllerGenerator(document, Settings);
            return clientGenerator.GenerateFile();
        }
    }
}
