using FluentValidation;
using MediatR;
using Zoo.Domain.Core.Exceptions;
using Zoo.Domain.Core.Result;

namespace Zoo.Application.Core.Behaviours;
public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest: notnull
{
    const string _verificationCode = "4001";
    const string _verificationMessage = "Parameters verification error!";
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }


    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);

        var failures = _validators
            .Select(v => v.Validate(context))
            .SelectMany(result => result.Errors)
            .Where(f => f != null)
            .ToList();


        if (failures.Count != 0)
        {
            throw new CustValidationException(failures.Select(x => new ValidationError(x.PropertyName, x.ErrorMessage)));
        }

        return await next();
    }
}
