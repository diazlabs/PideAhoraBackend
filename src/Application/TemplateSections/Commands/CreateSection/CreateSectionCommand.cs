using Application.Common.Extensions;
using Ardalis.Result;
using FluentValidation;
using MediatR;

namespace Application.TemplateSections.Commands.CreateSection
{
    public class CreateSectionCommand : IRequest<Result<CreateSectionResponse>>
    {
            public Guid TenantId { get; set; }
            public Guid TemplateId { get; set; }
            public int SectionVariantId { get; set; }
            public int Order { get; set; }
            public bool Visible { get; set; }
            public Guid Creator { get; set; }
            public List<CreateSectionProduct> Products { get; set; } = [];
    }

    public class  CreateSectionProduct
    {
        public int ProductId { get; set; }
        public int Order { get; set; }
    }

    public class CreateSectionValidator : AbstractValidator<CreateSectionCommand>
    {
        public CreateSectionValidator()
        {
            RuleFor(x => x.SectionVariantId).GreaterThan(0).WithMessage("La variante no esta definida");
            RuleFor(x => x.TenantId).RequireGuid();
            RuleFor(x => x.TemplateId).RequireGuid();
            RuleFor(x => x.Creator).RequireGuid();

            RuleForEach(x => x.Products).SetValidator(new SectionProductValidator());
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
}
