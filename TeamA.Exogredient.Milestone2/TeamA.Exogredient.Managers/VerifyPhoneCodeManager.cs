﻿using System;
using System.Threading.Tasks;
using TeamA.Exogredient.AppConstants;
using TeamA.Exogredient.DataHelpers;
using TeamA.Exogredient.Services;

namespace TeamA.Exogredient.Managers
{
    public class VerifyPhoneCodeManager
    {

        private readonly UserManagementService _userManagementService;
        private readonly LoggingService _loggingService;
        private readonly AuthenticationService _authenticationService;
        private readonly VerificationService _verificationService;

        public VerifyPhoneCodeManager(UserManagementService userManagementService,LoggingService loggingService,
                                      AuthenticationService authenticationService,VerificationService verificationService)
        {
            _userManagementService = userManagementService;
            _loggingService = loggingService;
            _authenticationService = authenticationService;
            _verificationService = verificationService;

        }

        public async Task<Result<bool>> VerifyPhoneCodeAsync(string username, string inputCode, string ipAddress,
                                                                    string phoneNumber, bool duringRegistration,
                                                                    int currentNumExceptions)
        {
            try
            {
                bool phoneVerificationSuccess = false;

                UserObject user = await _userManagementService.GetUserInfoAsync(username).ConfigureAwait(false);

                if (user.PhoneCodeFailures >= Constants.MaxPhoneCodeAttempts)
                {
                    await _loggingService.LogAsync(DateTime.UtcNow.ToString(Constants.LoggingFormatString),
                                                  Constants.VerifyPhoneOperation, username, ipAddress,
                                                  Constants.MaxPhoneTriesReachedLogMessage).ConfigureAwait(false);

                    return SystemUtilityService.CreateResult(Constants.MaxPhoneTriesReachedUserMessage, phoneVerificationSuccess, false, currentNumExceptions);
                }

                string verificationStatus = await _verificationService.VerifyPhoneCodeAsync(phoneNumber, inputCode).ConfigureAwait(false);

                if (verificationStatus.Equals(Constants.TwilioAuthenticationApprovedString))
                {
                    phoneVerificationSuccess = true;
                    await _loggingService.LogAsync(DateTime.UtcNow.ToString(Constants.LoggingFormatString),
                                                  Constants.VerifyPhoneOperation, username, ipAddress).ConfigureAwait(false);

                    if (duringRegistration)
                    {
                        await _userManagementService.MakeTempPermAsync(username).ConfigureAwait(false);
                    }

                    return SystemUtilityService.CreateResult(Constants.VerifyPhoneSuccessUserMessage, phoneVerificationSuccess, false, currentNumExceptions);
                }
                else if (verificationStatus.Equals(Constants.TwilioAuthenticationPendingString))
                {
                    await _userManagementService.IncrementPhoneCodeFailuresAsync(username).ConfigureAwait(false);

                    await _loggingService.LogAsync(DateTime.UtcNow.ToString(Constants.LoggingFormatString),
                                                  Constants.VerifyPhoneOperation, username, ipAddress,
                                                  Constants.WrongPhoneCodeMessage).ConfigureAwait(false);

                    return SystemUtilityService.CreateResult(Constants.WrongPhoneCodeMessage, phoneVerificationSuccess, false, currentNumExceptions);
                }
                else
                {
                    // Failed
                    await _userManagementService.IncrementPhoneCodeFailuresAsync(username).ConfigureAwait(false);

                    await _loggingService.LogAsync(DateTime.UtcNow.ToString(Constants.LoggingFormatString),
                                                  Constants.VerifyPhoneOperation, username, ipAddress,
                                                  Constants.PhoneCodeExpiredLogMessage).ConfigureAwait(false);

                    return SystemUtilityService.CreateResult(Constants.PhoneCodeExpiredUserMessage, phoneVerificationSuccess, false, currentNumExceptions);
                }
            }
            catch (Exception e)
            {
                await _loggingService.LogAsync(DateTime.UtcNow.ToString(Constants.LoggingFormatString),
                                              Constants.VerifyPhoneOperation, username, ipAddress, e.Message).ConfigureAwait(false);

                if (currentNumExceptions + 1 >= Constants.MaximumOperationRetries)
                {
                    await SystemUtilityService.NotifySystemAdminAsync($"{Constants.VerifyPhoneOperation} failed a maximum number of times for {username}.", Constants.SystemAdminEmailAddress).ConfigureAwait(false);
                }

                return SystemUtilityService.CreateResult(Constants.SystemErrorUserMessage, false, true, currentNumExceptions + 1);
            }
        }
    }
}
