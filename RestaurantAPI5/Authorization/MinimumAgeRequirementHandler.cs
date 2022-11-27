using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RestaurantAPI5.Authorization
{
    public class MinimumAgeRequirementHandler : AuthorizationHandler<MinimumAgeRequirement>
    {
        public MinimumAgeRequirementHandler(ILogger<MinimumAgeRequirementHandler> logger)
        {
            Logger = logger;
        }

        public ILogger<MinimumAgeRequirementHandler> Logger { get; }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
        {
            var dateOfBirth =  DateTime.Parse(context.User.FindFirst(c => c.Type == "DateOfBirth").Value);

            var userEmail = context.User.FindFirst(c => c.Type == ClaimTypes.Name).Value;

            Logger.LogInformation($"User: {userEmail} with date of birth: [{dateOfBirth}]");

            if (dateOfBirth.AddYears(requirement.MinimumAge) <= DateTime.Today)
            {
                Logger.LogInformation("Authorization succeded");
                context.Succeed(requirement);
            }
            else
            {
                Logger.LogInformation("Authorization failed");

            }

            return Task.CompletedTask;
        }
    }
}
