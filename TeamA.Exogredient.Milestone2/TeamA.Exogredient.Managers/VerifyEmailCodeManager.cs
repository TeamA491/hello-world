﻿using System;
using System.Threading.Tasks;
using TeamA.Exogredient.AppConstants;
using TeamA.Exogredient.DataHelpers;
using TeamA.Exogredient.Services;

namespace TeamA.Exogredient.Managers
{
    public class VerifyEmailCodeManager
    {
        private readonly LoggingService _loggingService;
        private readonly UserManagementService _userManagementService;

        public VerifyEmailCodeManager(LoggingService loggingService, UserManagementService userManagementService)
        {
            _loggingService = loggingService;
            _userManagementService = userManagementService;
        }

        public async Task<Result<bool>> VerifyEmailCodeAsync(string username, string inputCode, string ipAddress,
                                                                    int currentNumExceptions)
        {
            try
            {
                bool emailVerificationSuccess = false;

                UserObject user = await _userManagementService.GetUserInfoAsync(username).ConfigureAwait(false);

                if (user.EmailCodeFailures >= Constants.MaxEmailCodeAttempts)
                {
                    await _loggingService.LogAsync(DateTime.UtcNow.ToString(Constants.LoggingFormatString),
                                                  Constants.VerifyEmailOperation, username, ipAddress,
                                                  Constants.MaxEmailTriesReachedLogMessage).ConfigureAwait(false);

                    return SystemUtilityService.CreateResult(Constants.MaxEmailTriesReachedUserMessage, emailVerificationSuccess, false, currentNumExceptions);
                }

                long maxValidTimeSeconds = TimeUtilityService.TimespanToSeconds(Constants.EmailCodeMaxValidTime);
                long currentUnix = TimeUtilityService.CurrentUnixTime();

                if (user.EmailCodeTimestamp + maxValidTimeSeconds < currentUnix)
                {
                    await _loggingService.LogAsync(DateTime.UtcNow.ToString(Constants.LoggingFormatString),
                                                  Constants.VerifyEmailOperation, username, ipAddress,
                                                  Constants.EmailCodeExpiredLogMessage).ConfigureAwait(false);

                    return SystemUtilityService.CreateResult(Constants.EmailCodeExpiredUserMessage, emailVerificationSuccess, false, currentNumExceptions);
                }

                if (user.EmailCode.Equals(inputCode))
                {
                    emailVerificationSuccess = true;
                    await _loggingService.LogAsync(DateTime.UtcNow.ToString(Constants.LoggingFormatString),
                                                  Constants.VerifyEmailOperation, username, ipAddress).ConfigureAwait(false);

                    return SystemUtilityService.CreateResult(Constants.VerifyEmailSuccessUserMessage, emailVerificationSuccess, false, currentNumExceptions);
                }
                else
                {
                    await _loggingService.LogAsync(DateTime.UtcNow.ToString(Constants.LoggingFormatString),
                                                  Constants.VerifyEmailOperation, username, ipAddress,
                                                  Constants.WrongEmailCodeMessage).ConfigureAwait(false);

                    await _userManagementService.IncrementEmailCodeFailuresAsync(username).ConfigureAwait(false);

                    return SystemUtilityService.CreateResult(Constants.WrongEmailCodeMessage, emailVerificationSuccess, false, currentNumExceptions);
                }
            }
            catch (Exception e)
            {
                await _loggingService.LogAsync(DateTime.UtcNow.ToString(Constants.LoggingFormatString),
                                              Constants.VerifyEmailOperation, username, ipAddress, e.Message).ConfigureAwait(false);

                if (currentNumExceptions + 1 >= Constants.MaximumOperationRetries)
                {
                    await SystemUtilityService.NotifySystemAdminAsync($"{Constants.VerifyEmailOperation} failed a maximum number of times for {username}.", Constants.SystemAdminEmailAddress).ConfigureAwait(false);
                }

                return SystemUtilityService.CreateResult(Constants.SystemErrorUserMessage, false, true, currentNumExceptions + 1);
            }
        }
    }
}
