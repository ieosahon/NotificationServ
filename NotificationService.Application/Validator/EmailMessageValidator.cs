global using FluentValidation;
global using NotificationService.Application.DTOs.Request;
global using NotificationService.Application.DTOs.Response;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.Filters;
using NotificationService.Application.Utility;

namespace NotificationService.Application.Validator;

public class EmailMessageValidator : AbstractValidator<EmailMessage>
{
    public EmailMessageValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.To)
            .NotNull().WithMessage("'To' cannot be null.")
            .NotEmpty().WithMessage("'To' must contain at least one recipient.")
            .Must(ValidEmailList).WithMessage(x => $"Invalid email address(es) in 'To': {string.Join(", ", GetInvalidEmails(x.To))}");

        RuleFor(x => x.Cc)
            .Null().When(x => x.Cc != null && !x.Cc.Any()).WithMessage("'Cc' cannot be empty if provided.")
            .Must(ValidEmailList).When(x => x.Cc != null && x.Cc.Any()).WithMessage(x => $"Invalid email address(es) in 'Cc': {string.Join(", ", GetInvalidEmails(x.Cc))}");

        RuleFor(x => x.Attachments)
            .Null().When(x => x.Attachments != null && !x.Attachments.Any()).WithMessage("'Attachments' cannot be empty if provided.");

        RuleFor(x => x.Bcc)
            .Null().When(x => x.Bcc != null && !x.Bcc.Any()).WithMessage("'Bcc' cannot be empty if provided.")
            .Must(ValidEmailList).When(x => x.Bcc != null && x.Bcc.Any()).WithMessage(x => $"Invalid email address(es) in 'Bcc': {string.Join(", ", GetInvalidEmails(x.Bcc))}");

        RuleFor(x => x.Subject)
            .NotEmpty().WithMessage("'Subject' cannot be empty.")
            .NotNull().WithMessage("'Subject' cannot be null.");

        RuleFor(x => x.Body)
            .NotEmpty().WithMessage("'Body' cannot be empty.")
            .NotNull().WithMessage("'Body' cannot be null.");
    }

    private bool ValidEmailList(IEnumerable<string> emailList)
    {
        return emailList == null || emailList.All(EmailValidator.IsValidEmail);
    }

    private IEnumerable<string> GetInvalidEmails(IEnumerable<string> emailList)
    {
        return emailList == null ? Enumerable.Empty<string>() : emailList.Where(email => !EmailValidator.IsValidEmail(email));
    }
}


public class EmailMessageValidatorAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ActionArguments.TryGetValue("request", out var requestObject) && requestObject is EmailMessage baseRequest)
        {
            var validator = new EmailMessageValidator();
            var validationResult = validator.Validate(baseRequest);

            if (!validationResult.IsValid)
            {
                var errorMessage = validationResult.Errors.FirstOrDefault()?.ErrorMessage;
                context.Result = new BadRequestObjectResult(new NotificationResponse
                {
                    Code = "400",
                    Message = errorMessage ?? "Validation failed"
                });
            }
        }

        base.OnActionExecuting(context);
    }
}