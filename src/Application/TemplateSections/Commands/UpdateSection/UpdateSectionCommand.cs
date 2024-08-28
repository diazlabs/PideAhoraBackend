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
        public List<UpdateSectionProduct> Products { get; set; } = [];
    }

    public class UpdateSectionProduct
    {
        public int SectionProductId { get; set; }
        public int ProductId { get; set; }
        public int Order { get; set; }
    }

    public class UpdateSectionCommandValidator : AbstractValidator<UpdateSectionCommand>
    {
        public UpdateSectionCommandValidator()
        {
            RuleFor(x => x.TemplateSectionId).GreaterThan(0).WithMessage("No es una sección válida");
            RuleFor(x => x.TenantId).RequireGuid();
            RuleFor(x => x.TenantTemplateId).RequireGuid();

            RuleForEach(x => x.Products).SetValidator(new SectionProductValidator());
        }
    }

    public class SectionProductValidator : AbstractValidator<UpdateSectionProduct>
    {
        public SectionProductValidator()
        {
            RuleFor(x => x.SectionProductId).GreaterThanOrEqualTo(0).WithMessage("El id de la sección no es válido");
            RuleFor(x => x.ProductId).GreaterThan(0).WithMessage("El id del producto no es válido");
            RuleFor(x => x.Order).GreaterThanOrEqualTo(0).WithMessage("El orden no es válido");
        }
    }
}
