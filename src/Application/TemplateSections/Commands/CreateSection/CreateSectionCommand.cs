using Application.Common.Extensions;
using Ardalis.Result;
using FluentValidation;
using MediatR;

namespace Application.TemplateSections.Commands.CreateSection
{
    public class CreateSectionCommand : IRequest<Result<CreateSectionResponse>>
    {
            public Guid TenantId { get; set; }
            public Guid TenantTemplateId { get; set; }
            public bool Visible { get; set; }
            public string SectionName { get; set; } = default!;
            public string SectionDescription { get; set; } = default!;
            public List<CreateSectionProduct> Products { get; set; } = [];
            public List<CreateSectionConfiguration> Configs { get; set; } = [];
    }

    public class  CreateSectionProduct
    {
        public int ProductId { get; set; }
        public int Order { get; set; }
    }

    public class CreateSectionConfiguration
    {
        public string ConfigName { get; set; } = default!;
        public string ConfigValue { get; set; } = default!;
    }

    public class CreateSectionValidator : AbstractValidator<CreateSectionCommand>
    {
        public CreateSectionValidator()
        {
            RuleFor(x => x.TenantId).RequireGuid();
            RuleFor(x => x.TenantTemplateId).RequireGuid();
            RuleFor(x => x.SectionName).ValidateRequiredProperty("el nombre de la seccion");
            RuleFor(x => x.SectionDescription).ValidateRequiredProperty("la descripcion de la seccion");


            RuleFor(x => x.Products).Must(
                x => x.GroupBy(x => x.Order)
                .Select(x => new { Order = x.Key, Products = x })
                .All(x => x.Products.Count() == 1)
            );
            RuleForEach(x => x.Products).SetValidator(new SectionProductValidator());
            RuleForEach(x => x.Configs).SetValidator(new SectionConfigValidator());
        }
    }

    public class SectionProductValidator : AbstractValidator<CreateSectionProduct>
    {
        public SectionProductValidator()
        {
            RuleFor(x => x.ProductId).GreaterThan(0).WithMessage("El id del producto no es válido");
            RuleFor(x => x.Order).GreaterThanOrEqualTo(0).WithMessage("El orden no es válido");
        }
    }

    public class SectionConfigValidator : AbstractValidator<CreateSectionConfiguration>
    {
        public SectionConfigValidator()
        {
            RuleFor(x => x.ConfigName).ValidateRequiredProperty("el nombre de la configuracion", max: 30);
            RuleFor(x => x.ConfigValue).ValidateRequiredProperty("el valor de la configuracion", max: 100);
        }
    }
}
