using Application.Common.Interfaces;
using Application.Common.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationContext _context;
        public OrderService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<bool> ValidateDetailOptions(int productId, IEnumerable<(int optionId, int quantity)> options)
        {
            var choices = await _context.ProductChoices
                .Include(x => x.ChoiceOptions)
                .Where(x => x.ProductId == productId)
                .ToListAsync();

            if (!options.Any() && choices.Count == 0) return true;

            foreach (var choice in choices)
            {
                bool hasOptions = choice.ChoiceOptions != null && choice.ChoiceOptions.Count > 0;
                if (!hasOptions) continue;
                
                int quantity = 0;
                foreach(var choiceOption in choice.ChoiceOptions!)
                {
                    var option = options.FirstOrDefault(x => x.optionId == choiceOption.ChoiceOptionId);
                    if (option == default) continue;

                    quantity += option.quantity;
                }

                if (choice.Required && choice.Quantity != quantity) return false;

                if (choice.Quantity < quantity) return false;
            }

            return true;
        }

        public async Task<bool> ValidateDiscount(int productId, int discountId)
        {
            var today = DateTime.UtcNow;
            var isValid = await _context.ProductDiscounts
                .AnyAsync(x => x.ProductDiscountId == discountId && 
                    x.ProductId == productId && x.Enabled && 
                    x.Since < today && x.Until > today
                );

            return isValid;
        }
    }
}
