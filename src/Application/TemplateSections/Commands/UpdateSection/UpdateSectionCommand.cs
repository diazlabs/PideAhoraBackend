using Application.Common.Extensions;
using Ardalis.Result;
using FluentValidation;
using MediatR;

namespace Application.TemplateSections.Commands.UpdateSection
{
    public class UpdateSectionCommand : IRequest<Result<UpdateSectionResponse>>
    {
        public Guid TenantId { get; set; }
        public Guid TenantTemplateId { get; set; }
        public int TemplateSectionId { get; set; }
        public bool Visible { get; set; }
        public string SectionName { get; set; } = default!;
        public string? SectionDescription { get; set; }
        public List<UpdateSectionProduct> Products { get; set; } = [];
        public List<UpdateSectionConfig> Configs { get; set; } = [];
    }

    public class UpdateSectionProduct
    {
        public int SectionProductId { get; set; }
        public int ProductId { get; set; }
        public int Order { get; set; }
    }

    public class UpdateSectionConfig
    {
        public int SectionConfigId { get; set; }
        public string ConfigValue { get; set; } = default!;
    }

    public class UpdateSectionCommandValidator : AbstractValidator<UpdateSectionCommand>
    {
        public UpdateSectionCommandValidator()
        {
            RuleFor(x => x.TemplateSectionId).GreaterThan(0).WithMessage("No es una sección válida");
            RuleFor(x => x.TenantId).RequireGuid();
            RuleFor(x => x.TenantTemplateId).RequireGuid();
            RuleFor(x => x.SectionName).ValidateRequiredProperty("el nombre de la sección");
            RuleFor(x => x.SectionDescription).ValidateRequiredProperty("la descripcion de la seccion");

            RuleForEach(x => x.Products).SetValidator(new UpdateSectionProductValidator());
            RuleForEach(x => x.Configs).SetValidator(new UpdateSectionConfigValidator());
        }
    }

    public class UpdateSectionProductValidator : AbstractValidator<UpdateSectionProduct>
    {
        public UpdateSectionProductValidator()
        {
            RuleFor(x => x.SectionProductId).GreaterThanOrEqualTo(0).WithMessage("El id de la sección no es válido");
            RuleFor(x => x.ProductId).GreaterThan(0).WithMessage("El id del producto no es válido");
            RuleFor(x => x.Order).GreaterThanOrEqualTo(0).WithMessage("El orden no es válido");
        }
    }


    public class UpdateSectionConfigValidator : AbstractValidator<UpdateSectionConfig>
    {
        public UpdateSectionConfigValidator()
        {
            RuleFor(x => x.SectionConfigId).GreaterThanOrEqualTo(0).WithMessage("El id de la configuracion no es válido");
            RuleFor(x => x.ConfigValue).ValidateRequiredProperty("el valor de la configuracion", max: 100);
        }
    }
}
