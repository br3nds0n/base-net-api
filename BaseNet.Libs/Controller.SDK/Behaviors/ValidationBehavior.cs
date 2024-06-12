using MediatR;
using FluentValidation;

namespace BaseNet.Libs.Controller.SDK.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);

            var validations = await Task.WhenAll(
                _validators
                    .Select(
                        v => v.ValidateAsync(context, cancellationToken)
                    )
            );
            var failures = validations
                .SelectMany(result => result.Errors)
                .Where(failure => failure != null)
                .ToList();

            if (failures.Count != 0)
            {
                throw new Exception("Ocorreu um erro de validação");
            }

            return await next();
        }
    }
}