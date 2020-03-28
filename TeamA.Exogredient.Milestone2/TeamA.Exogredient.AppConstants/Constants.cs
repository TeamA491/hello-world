﻿using System;
using System.Collections.Generic;

namespace TeamA.Exogredient.AppConstants
{
    /// <summary>
    /// The constants, reaonly values, and other literal values used throughout our system.
    /// </summary>
    public static class Constants
    {
        // ENVIRONMENT VARIABLES
        public static readonly string PrivateKey = Environment.GetEnvironmentVariable("PRIVATE_KEY", EnvironmentVariableTarget.User);
        public static readonly string PublicKey = Environment.GetEnvironmentVariable("PUBLIC_KEY", EnvironmentVariableTarget.User);
        public static readonly string TwilioAuthToken = Environment.GetEnvironmentVariable("TWILIO_AUTH_TOKEN", EnvironmentVariableTarget.User);
        public static readonly string SystemEmailPassword = Environment.GetEnvironmentVariable("SYSTEM_EMAIL_PASSWORD", EnvironmentVariableTarget.User);
        public static readonly string NOSQLConnection = Environment.GetEnvironmentVariable("NOSQL_CONNECTION", EnvironmentVariableTarget.User);
        public static readonly string SQLConnection = Environment.GetEnvironmentVariable("SQL_CONNECTION", EnvironmentVariableTarget.User);
        public static readonly string MapSQLConnection = Environment.GetEnvironmentVariable("MAPTABLE_CONNECTION", EnvironmentVariableTarget.User);
        public static readonly string FTPpassword = Environment.GetEnvironmentVariable("FTP_PASSWORD", EnvironmentVariableTarget.User);
        public static readonly string AuthzPrivateKey = Environment.GetEnvironmentVariable("AUTHORIZATION_PRIVATE_KEY", EnvironmentVariableTarget.User);
        public static readonly string AuthzPublicKey = Environment.GetEnvironmentVariable("AUTHORIZATION_PUBLIC_KEY", EnvironmentVariableTarget.User);
        public static readonly string ProjectStatus = Environment.GetEnvironmentVariable("PROJECT_STATUS", EnvironmentVariableTarget.User);

        // PROJECT STATUSES
        public static readonly string StatusDev = "DEVELOPMENT";
        public static readonly string StatusProd = "PRODUCTION";
        public static readonly string StatusTest = "TESTING";


        // STRING UTILITY HELPER DATA STRUCTURES
        public static readonly IDictionary<int, int> MonthDays = new Dictionary<int, int>()
        {
            { 1, 31 }, { 2, 28 }, { 3, 31 }, { 4, 30 }, { 5, 31 },
            { 6, 30 }, { 7, 31 }, { 8, 31 }, { 9, 30 }, { 10, 31 },
            { 11, 30 }, { 12, 31 }
        };

        // TODO: CONVERT THESE TO ARRAYS
        // No < or > to protect from SQL injections.
        public static readonly List<char> ANSNoAngleBrackets = new List<char>()
        {
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q',
            'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '1', '2', '3', '4', '5', '6', '7', '8', '9',
            '0', '~', '`', '@', '#', '$', '%', '^', '&', '!', '*', '(', ')', '_', '-', '+', '=', '{',
            '[', '}', ']', '|', '\\', '"', '\'', ':', ';', '?', '/', '.', ','
        };

        public static readonly List<string> ContextSpecificWords = new List<string>()
        {
            "exogredient"
        };

        public static readonly List<char> LettersLower = new List<char>()
        {
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q',
            'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'
        };

        public static readonly List<char> LettersUpper = new List<char>()
        {
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q',
            'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
        };

        public static readonly List<char> Numbers = new List<char>()
        {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
        };

        public static readonly IDictionary<char, int> LettersLowerToPositions = new Dictionary<char, int>()
        {
            {'a', 1}, {'b', 2}, {'c', 3}, {'d', 4}, {'e', 5}, {'f', 6}, {'g', 7}, {'h', 8},
            {'i', 9}, {'j', 10}, {'k', 11}, {'l', 12}, {'m', 13}, {'n', 14}, {'o', 15}, {'p', 16},
            {'q', 17}, {'r', 18}, {'s', 19}, {'t', 20}, {'u', 21}, {'v', 22}, {'w', 23}, {'x', 24},
            {'y', 25}, {'z', 26}
        };

        public static readonly IDictionary<char, int> LettersUpperToPositions = new Dictionary<char, int>()
        {
            {'A', 1}, {'B', 2}, {'C', 3}, {'D', 4}, {'E', 5}, {'F', 6}, {'G', 7}, {'H', 8},
            {'I', 9}, {'J', 10}, {'K', 11}, {'L', 12}, {'M', 13}, {'N', 14}, {'O', 15}, {'P', 16},
            {'Q', 17}, {'R', 18}, {'S', 19}, {'T', 20}, {'U', 21}, {'V', 22}, {'W', 23}, {'X', 24},
            {'Y', 25}, {'Z', 26}
        };

        public static readonly IDictionary<int, char> PositionsToLettersLower = new Dictionary<int, char>()
        {
            {1, 'a'}, {2, 'b'}, {3, 'c'}, {4, 'd'}, {5, 'e'}, {6, 'f'}, {7, 'g'}, {8, 'h'},
            {9, 'i'}, {10, 'j'}, {11, 'k'}, {12, 'l'}, {13, 'm'}, {14, 'n'}, {15, 'o'}, {16, 'p'},
            {17, 'q'}, {18, 'r'}, {19, 's'}, {20, 't'}, {21, 'u'}, {22, 'v'}, {23, 'w'}, {24, 'x'},
            {25, 'y'}, {26, 'z'}
        };

        public static readonly IDictionary<int, char> PositionsToLettersUpper = new Dictionary<int, char>()
        {
            {1, 'A'}, {2, 'B'}, {3, 'C'}, {4, 'D'}, {5, 'E'}, {6, 'F'}, {7, 'G'}, {8, 'H'},
            {9, 'I'}, {10, 'J'}, {11, 'K'}, {12, 'L'}, {13, 'M'}, {14, 'N'}, {15, 'O'}, {16, 'P'},
            {17, 'Q'}, {18, 'R'}, {19, 'S'}, {20, 'T'}, {21, 'U'}, {22, 'V'}, {23, 'W'}, {24, 'X'},
            {25, 'Y'}, {26, 'Z'}
        };

        // AUTHORIZATION
        public const int TOKEN_EXPIRATION_MIN = 20;

        public const string MediaType = "typ";
        public const string MediaJWT = "JWT";
        public const string SigningAlgKey = "alg";
        public const string AuthzSigningAlgorithm = "RS512";
        public const string AuthzExpirationField = "exp";
        public const string AuthzPublicKeyField = "pk";

        public const string UserTypeKey = "userType";
        public const string IdKey = "id";

        public const string SHA1 = "SHA1";

        public const string SIGNING_ALGORITHM = "RS512";
        public const string HASHING_ALGORITHM = "SHA512";
        public const string EXPIRATION_FIELD = "exp";
        public const string PUBLIC_KEY_FIELD = "pk";

        public enum USER_TYPE
        {
            UNREGISTERED = 0,
            REGISTERED = 1,
            STORE_OWNER = 2,
            ADMIN = 3,
            SYS_ADMIN = 4,
        };

        public static readonly Dictionary<string, int> UserOperations = new Dictionary<string, int>
        {
            // Format:
            // <operation> : <user level allowed>
            // NOTE: Users with a higher user number are allowed to access
            // the operations that lower leveled users can
            { "login", 0 },
            { "register", 0 },
            { "search", 1 },
            { "upload", 1 },
            { "claimBusiness", 2 },
            { "createAd", 2 },
            { "createUser", 3 },
            { "deleteUser", 3 },
            { "createSysAdmin", 4 },
        };

        // ARCHIVING 
        public const string SevenZipPath = @"C:\Program Files\7-Zip\7z.exe";
        public const string ArchivePrefixArgument = "a -t7z ";
        public const string ArchivePostfixArgument = " -sdel";
        public const string SevenZipFileExtension = ".7z";
        public const string FTPUrl = @"ftp://52.250.120.151:21/";
        public const string FTPUsername = "Archiver";
        public const string NamingFormatString = "ddMMyy";

        // AUTHENTICATION / USER MANAGEMENT
        public const string GoogleSMTP = "smtp.gmail.com";
        public const int GoogleSMTPPort = 465;

        public const string TwilioCallChannel = "call";

        public const string EmailVerificationSubject = "Exogredient Account Verification";

        public const int EmailCodeLength = 6;

        // FLAT FILE LOGGING
        public static readonly List<string> CsvVulnerabilities = new List<string>()
        {
            "=", "@", "+", "-"
        };

        public const string CsvProtection = @"\t";

        // SECURITY SERVICE
        public const int DefaultSaltLength = 8;
        public const int DefaultHashIterations = 10000;
        public const int DefaultHashCharacterLength = 64;
        public const int DefaultHashByteLength = 32;

        public const int ByteLength = 8;

        // USER MANAGEMENT
        public const string NotifySysAdminSubjectFormatString = "MM-dd-yyyy";

        // UTILITY SERVICE
        public const int HoursInADay = 24;
        public const int MonthsInAYear = 12;
        public const int MinutesInAnHour = 60;
        public const int SecondsInAMinute = 60;
        public const int SecondsInAnHour = 3600;
        public const int FebruaryMonthValue = 2;
        public const int LeapDayValue = 29;
        public const int LeapYearOccurrenceYears = 4;
        public const int LeapYearUnoccurenceYears = 100;
        public const int LeapYearReoccurenceYears = 400;

        public const int SecondsStartValue = 0;
        public const int MinuteStartValue = 0;
        public const int HourStartValue = 0;
        public const int DayStartValue = 1;
        public const int MonthStartValue = 1;

        public const string GmailHost = "gmail.com";

        public const int HexBaseValue = 16;

        public const string WordsTxtPath = @"..\..\..\..\words.txt";
        public const string CorruptedPasswordsPath = @"..\..\..\..\corrupted-passwords-small.txt";

        public const int MaxDigitValue = 9;
        public const int MinDigitValue = 1;

        public const int MaxAlphaValue = 26;
        public const int MinAlphaValue = 1;

        // BUSINESS RULES
        public const string LoggingFormatString = "HH:mm:ss:ff UTC yyyyMMdd";

        public const string NoError = "null";

        public const string RegistrationOperation = "Registration";
        public const string LogInOperation = "Log In";
        public const string VerifyEmailOperation = "Verify Email Code";
        public const string VerifyPhoneOperation = "Verify Phone Code";
        public const string SendPhoneCodeOperation = "Send Phone Code";
        public const string SendEmailCodeOperation = "Send Email Code";
        public const string UpdatePasswordOperation = "Update Password";
        public const string SingleUserCreateOperation = "Single User Create";
        public const string BulkUserCreateOperation = "Bulk User Create";
        public const string SingleUserDeleteOperation = "Single Delete Create";
        public const string BulkUserDeleteOperation = "Bulk User Delete";
        public const string UpdateSingleUserOperation = "Single User Update";
        public const string BulkUserUpdateOperation = "Bulk User Update";
        public const string MapTableReadFromOperation = "Map Table Read From";
        public const string MapTableModifiedOperation = "Map Table Modified";
        public const string UpdateSingleIPOperation = "Single IP Update";
        public const string DeleteSingleIPOperation = "Single IP Delete";
        public const string GetStoresByIngredientOperation = "Get Stores By Ingredient";
        public const string GetStoresByStoreOperation = "Get Stores By Store";
        public const string GetIngredientsOperation = "Get Ingredients";

        public const string CustomerUserType = "Customer";
        public const string AdminUserType = "Admin";
        public const string AnonymousUserType = "Unregistered Customer";
        public const string AnonymousUserIdentifier = "<Unregistered Customer>";
        public const string SystemIdentifier = "System";

        public const string LocalHost = "127.0.0.1";

        public const int NumOfResultsPerSearchPage = 20;
        public const int NumOfIngredientsPerStorePage = 20;
        public const int DisabledStatus = 1;
        public const int EnabledStatus = 0;

        public const long NoValueLong = 0;
        public const int NoValueInt = 0;
        public const string NoValueString = "";

        public const int MaximumOperationRetries = 3;

        public const int MaxRepetitionOrSequence = 3;

        public const int LoggingRetriesAmount = 3;
        public const int MaxLogInAttempts = 18;
        public const int MaxRegistrationAttempts = 3;
        public const int MaxEmailCodeAttempts = 3;
        public const int MaxPhoneCodeAttempts = 3;
        
        public static readonly TimeSpan LogInTriesResetTime = new TimeSpan(2, 0, 0);
        public static readonly TimeSpan RegistrationTriesResetTime = new TimeSpan(0, 15, 0);
        public static readonly TimeSpan MaxIPLockTime = new TimeSpan(0, 15, 0);
        public static readonly TimeSpan EmailCodeMaxValidTime = new TimeSpan(0, 15, 0);
        public static readonly TimeSpan MaxTempUserTime = new TimeSpan(1, 0, 0);

        public const string ANSNoAngle = "ANS-NoAngle";
        public const string Numeric = "NUM";

        public static readonly IDictionary<string, List<char>> CharSetsData = new Dictionary<string, List<char>>()
        {
            { ANSNoAngle, ANSNoAngleBrackets },
            { Numeric, Numbers }
        };

        public const int MaximumUserTypeLength = 11;
        public const int IPAddressLength = 15;

        public const int MaximumFirstNameCharacters = 200;
        public const int MinimumFirstNameCharacters = 1;
        public const string FirstNameCharacterType = ANSNoAngle;

        public const int MaximumLastNameCharacters = 200;
        public const int MinimumLastNameCharacters = 1;
        public const string LastNameCharacterType = ANSNoAngle;

        public const int MaximumEmailCharacters = 200;
        public const int MinimumEmailCharacters = 1;
        public const string EmailCharacterType = ANSNoAngle;

        public const int MaximumUsernameCharacters = 200;
        public const int MinimumUsernameCharacters = 1;
        public const string UsernameCharacterType = ANSNoAngle;

        public const int PhoneNumberCharacterLength = 10;
        public const string PhoneNumberCharacterType = Numeric;

        public const int MaximumPasswordCharacters = 2000;
        public const int MinimumPasswordCharacters = 12;
        public const string PasswordCharacterType = ANSNoAngle;

        public const string TwilioAuthenticationFailString = "fail";
        public const string TwilioAuthenticationApprovedString = "approved";
        public const string TwilioAuthenticationPendingString = "pending";

        public const string sortByIngredientNum = "ingredientNum";
        public const string sortByDistance = "distance";
        public const string searchByIngredient = "ingredient";
        public const string searchByStore = "store";

        // BUSINESS RULES -- MESSAGES
        public const string RegistrationSuccessUserMessage = "Registration Successful!";
        public const string LogInSuccessUserMessage = "Logged in successfully!";
        public const string VerifyEmailSuccessUserMessage = "Email verified! Please select the 'Call Me' option";
        public const string VerifyPhoneSuccessUserMessage = "Phone code verified!";
        public const string SendPhoneCodeSuccessUserMessage = "Phone code sent!";
        public const string SendEmailCodeSuccessUserMessage = "Email code sent!";
        public const string UpdatePasswordSuccessUserMessage = "Updated password!";

        public const string SystemErrorUserMessage = "A system error occurred. Please try again later. A team of highly trained monkeys is currently working on the situation.";

        public const string UsernameDNELogMessage = "Username does not exist";
        public const string InvalidLogInUserMessage = "Id or password was invalid";

        public const string UserDisableLogMessage = "User disabled";
        public const string UserDisableUserMessage = "Your account is disabled, please contact the system administrator.";

        public const string InvalidPasswordLogMessage = "Invalid password entered";

        public const string UsernameExistsLogMessage = "Username taken";
        public const string EmailExistsLogMessage = "Email taken";
        public const string PhoneNumberExistsLogMessage = "Phone number taken";
        public const string UniqueIdExistsRegistrationUserMessage = "Your email, username, or phone number was invalid... please try again";

        public const string IPLockedLogMessage = "IP locked";
        public const string IPLockedUserMessage = "You cannot currently register, please try again later";

        public const string InvalidScopeLogMessage = "User not in scope";
        public const string InvalidScopeUserMassage = "You must in California to register";

        public const string InvalidFirstNameLengthLogMessage = "First name length invalid";
        public static readonly string InvalidFirstNameLengthUserMessage = $"Fist name length invalid ({MaximumFirstNameCharacters} max)";
        public const string InvalidFirstNameCharactersLogMessage = "First name characters invalid";
        public const string InvalidFirstNameCharactersUserMessage = "First name characters invalid, < and > not allowed";

        public const string InvalidLastNameLengthLogMessage = "Last name length invalid";
        public static readonly string InvalidLastNameLengthUserMessage = $"Last name length invalid ({MaximumLastNameCharacters} max)";
        public const string InvalidLastNameCharactersLogMessage = "Last name characters invalid";
        public const string InvalidLastNameCharactersUserMessage = "Last name characters invalid, < and > not allowed";

        public const string InvalidEmailLengthLogMessage = "Email length invalid";
        public static readonly string InvalidEmailLengthUserMessage = $"Email length invalid ({MaximumEmailCharacters} max)";
        public const string InvalidEmailCharactersLogMessage = "Email characters invalid";
        public const string InvalidEmailCharactersUserMessage = "Email characters invalid, < and > not allowed";
        public const string InvalidEmailFormatMessage = "Email format invalid";

        public const string InvalidUsernameLengthLogMessage = "Username length invalid";
        public static readonly string InvalidUsernameLengthUserMessage = $"Username length invalid ({MaximumUsernameCharacters} max)";
        public const string InvalidUsernameCharactersLogMessage = "Username characters invalid";
        public const string InvalidUsernameCharactersUserMessage = "Username characters invalid, < and > not allowed";

        public const string InvalidPhoneNumberLengthLogMessage = "Phone number length invalid";
        public static readonly string InvalidPhoneNumberLengthUserMessage = $"Phone number length invalid ({PhoneNumberCharacterLength} only)";
        public const string InvalidPhoneNumberCharactersLogMessage = "Phone number characters invalid";
        public const string InvalidPhoneNumberCharactersUserMessage = "Phone number characters invalid, < and > not allowed";

        public const string InvalidPasswordLengthLogMessage = "Password length invalid";
        public static readonly string InvalidPasswordLengthUserMessage = $"Password length invalid ({MaximumPasswordCharacters} max, {MinimumPasswordCharacters} min)";
        public const string InvalidPasswordCharactersLogMessage = "Password characters invalid";
        public const string InvalidPasswordCharactersUserMessage = "Password characters invalid, < and > not allowed";
        public const string PasswordContextSpecificMessage = "Password contains context specific words";
        public const string PasswordSequencesOrRepetitionsLogMessage = "Password contains sequences or repetitions";
        public const string PasswordSequencesOrRepetitionsUserMessage = "Password contains sequences (111) or repetitions (123).";
        public const string PasswordWordsLogMessage = "Password contains words";
        public const string PasswordWordsUserMessage = "Password contains words from the English language";
        public const string PasswordCorruptedLogMessage = "Password corrupted";
        public const string PasswordCorruptedUserMessage = "Your password has been corrupted";

        public const string MaxEmailTriesReachedLogMessage = "Max email tries reached";
        public static readonly string MaxEmailTriesReachedUserMessage = $"Maximum email code tries reached ({MaxEmailCodeAttempts} max)";

        public const string EmailCodeExpiredLogMessage = "Email code expired";
        public const string EmailCodeExpiredUserMessage = "Email code expired, select the option to re-send.";

        public const string WrongEmailCodeMessage = "Wrong email code input";

        public const string MaxPhoneTriesReachedLogMessage = "Max phone tries reached";
        public static readonly string MaxPhoneTriesReachedUserMessage = $"Maximum pone code tries reached ({MaxPhoneCodeAttempts} max)";

        public const string PhoneCodeExpiredLogMessage = "Phone code expired";
        public const string PhoneCodeExpiredUserMessage = "Phone code expired, select the option to re-send.";

        public const string WrongPhoneCodeMessage = "Wrong phone code input";

        public const string StoresFetchSuccessMessage = "The list of stores for search successfully fetched.";
        public const string StoresFetchUnsuccessMessage = "System Error. Failed to fetch the list of stores for search. Please try again.";
        public const string IngredientsFetchSuccessMessage = "The list of ingredients for the selected store successfully fetched";
        public const string IngredientsFetchUnsuccessMessage = "System Error. Failed to fetch the list of ingredients for the selected store. Please try again.";

        // EMAIL
        public const string SystemEmailAddress = "exogredient.system@gmail.com";
        public const string SystemAdminEmailAddress = "TEAMA.CS491@gmail.com";

        // TWILIO
        public const string TwilioAccountSID = "AC94d03adc3d2da651c16c82932c29b047";
        public const string TwilioPathServiceSID = "VAa9682f046b6f511b9aa1807d4e2949e5";

        // FLAT FILE
        public const string LogFolder = @"C:\Logs";
        public const string LogFileType = ".CSV";
        public const string TokenFile = "token.txt";

        // SQL SCHEMA
        public const string ExogredientSQLSchemaName = "exogredient";
        public const string MapSQLSchemaName = "mapping_table";

        // USER TABLE
        public const string UserDAOtableName = "user";
        public const string UserDAOusernameColumn = "username";                                  // VARCHAR(200)
        public const string UserDAOnameColumn = "name";                                          // VARCHAR(401)
        public const string UserDAOemailColumn = "email";                                        // VARCHAR(200)
        public const string UserDAOphoneNumberColumn = "phone_number";                           // VARCHAR(10)
        public const string UserDAOpasswordColumn = "password";                                  // VARCHAR(2000)
        public const string UserDAOdisabledColumn = "disabled";                                  // TINYINT
        public const string UserDAOuserTypeColumn = "user_type";                                 // VARCHAR(11) -- could be enum/set
        public const string UserDAOsaltColumn = "salt";                                          // VARCHAR(200)
        public const string UserDAOtempTimestampColumn = "temp_timestamp";                       // BIGINT -- unix
        public const string UserDAOemailCodeColumn = "email_code";                               // VARCHAR(6)
        public const string UserDAOemailCodeTimestampColumn = "email_code_timestamp";            // BIGINT -- unix
        public const string UserDAOloginFailuresColumn = "login_failures";                       // INT
        public const string UserDAOlastLoginFailTimestampColumn = "last_login_fail_timestamp";   // BIGINT -- unix
        public const string UserDAOemailCodeFailuresColumn = "email_code_failures";              // INT
        public const string UserDAOphoneCodeFailuresColumn = "phone_code_failures";              // INT

        // RECORD HELPER DATA STRUCTURES
        public static readonly IDictionary<string, bool> UserDAOIsColumnMasked = new Dictionary<string, bool>()
        {
            {UserDAOusernameColumn, false},
            {UserDAOnameColumn, true},
            {UserDAOemailColumn, true},
            {UserDAOphoneNumberColumn, true},
            {UserDAOpasswordColumn, false},
            {UserDAOdisabledColumn, false},
            {UserDAOuserTypeColumn, false},
            {UserDAOsaltColumn, false},
            {UserDAOtempTimestampColumn, false},
            {UserDAOemailCodeColumn, false},
            {UserDAOemailCodeTimestampColumn, false},
            {UserDAOloginFailuresColumn, false},
            {UserDAOlastLoginFailTimestampColumn, false},
            {UserDAOemailCodeFailuresColumn, false},
            {UserDAOphoneCodeFailuresColumn, false}
        };

        // IP ADDRESS TABLE
        public const string IPAddressDAOtableName = "ip_address";
        public const string IPAddressDAOIPColumn = "ip";                                         // VARCHAR(15)
        public const string IPAddressDAOtimestampLockedColumn = "timestamp_locked";              // BIGINT -- unix
        public const string IPAddressDAOregistrationFailuresColumn = "registration_failures";    // INT
        public const string IPAddressDAOlastRegFailTimestampColumn = "last_reg_fail_timestamp";  // BIGINT -- unix

        // RECORD HELPER DATA STRUCTURES
        public static readonly IDictionary<string, bool> IPAddressDAOIsColumnMasked = new Dictionary<string, bool>()
        {
            {IPAddressDAOIPColumn, true},
            {IPAddressDAOtimestampLockedColumn, false},
            {IPAddressDAOregistrationFailuresColumn, false},
            {IPAddressDAOlastRegFailTimestampColumn, false}
        };

        // MAP TABLE
        public const string MapDAOTableName = "map";
        public const string MapDAOHashColumn = "hash";
        public const string MapDAOActualColumn = "actual";
        public const string MapDAOoccurrencesColumn = "occurrences";

        // STORE TABLE
        public const string StoreDAOTableName = "store";
        public const string StoreDAOStoreIdColumn = "store_id";
        public const string StoreDAOOwnerColumn = "owner";
        public const string StoreDAOStoreNameColumn = "store_name";
        public const string StoreDAOLatitudeColumn = "latitude";
        public const string StoreDAOLongitudeColumn = "longitude";
        public const string StoreDAOStoreDescriptionColumn = "store_description";
        public const string StoreDAODistanceColumn = "distance";
        public const string StoreDAOIngredientNumColumn = "ingredient_num";
        public const string StoreDAOPlaceIdColumn = "place_id";
        public const string StoreDAOTotalResultsNum = "total_results_num";

        // TICKET TABLE
        public const int DefaultVarCharLength = 32;

        public const string TicketDAOTableName = "ticket";
        public const string TicketCategoryDAOTableName = "ticket_category";
        public const string TicketStatusDAOTableName = "ticket_status";
        public const string TicketFlagColorDAOTableName = "ticket_flag_color";

        public const string TicketDAOTicketIDColumn = "ticket_id";
        public const string TicketDAOSubmitTimestampColumn = "submit_timestamp";
        public const string TicketDAOCategoryColumn = "category";
        public const string TicketDAOStatusColumn = "status";
        public const string TicketDAOFlagColorColumn = "flag_color";
        public const string TicketDAODescriptionColumn = "description";
        public const string TicketDAOIsReadColumn = "is_read";

        public static readonly string[] TicketFlagColors = { "red", "blue", "green", "yellow", "orange" };
        public static readonly string[] TicketStatuses = { "unresolved", "resolved" };
        public static readonly string[] TicketCategories = { "error", "bug", "other" };

        //STORE DISTANCE HOW MANY DIGITS AFTER DECIMAL POINT
        public const int FractionalDigits = 2;

        // UPLOAD TABLE
        public const string UploadDAOTableName = "upload";
        public const string UploadDAOUploadIdColumn = "upload_id";
        public const string UploadDAOStoreIdColumn = "store_id";
        public const string UploadDAOIngredientNameColumn = "ingredient_name";
        public const string UploadDAOUploaderColumn = "uploader";
        public const string UploadDAOPostTimeDateColumn = "post_time_date";
        public const string UploadDAODescriptionColumn = "description";
        public const string UploadDAORatingColumn = "rating";
        public const string UploadDAOPhotoColumn = "photo";
        public const string UploadDAOPriceColumn = "price";
        public const string UploadDAOUpvoteColumn = "upvote";
        public const string UploadDAODownvoteColumn = "downvote";
        public const string UploadDAOInProgressColumn = "in_progress";
        public const string UploadDAOUploadNumColumn = "upload_num";

        // CORRUPTED PASSWORDS COLLECTION
        public const string CorruptedPassSchemaName = "corrupted_passwords";
        public const string CorruptedPassCollectionName = "passwords";
        public const string CorruptedPassPasswordField = "password";

        // LOGS COLLECTION
        public const string LogsSchemaName = "exogredient_logs";
        public const string LogsCollectionPrefix = "logs_";
        public const string LogsIdField = "_id";
        public const string LogsTimestampField = "timestamp";
        public const string LogsOperationField = "operation";
        public const string LogsIdentifierField = "identifier";
        public const string LogsIPAddressField = "ip";
        public const string LogsErrorTypeField = "errorType";

        // RECORD HELPER DATA STRUCTURES
        public static readonly IDictionary<string, bool> LogsCollectionIsColumnMasked = new Dictionary<string, bool>()
        {
            {LogsTimestampField, false},
            {LogsOperationField, false},
            {LogsIdentifierField, false},
            {LogsIPAddressField, true},
            {LogsErrorTypeField, false}
        };

        // EXCEPTION MESSAGES -- Authorization
        public const string UserTypeIdNotProvided = "UserType or ID was not provided.";
        public const string JWTthreeSegments = "JWT must have 3 segments separated by periods.";
        public const string IncorrectEncryption = "Incorrect encryption algorithm.";
        public const string PubKeyNotFound = "Public key not found in the JWT payload!";
        public const string JWTNotVerified = "JWT could not be verified!";
        public const string ExpirationNotSpecified = "Expiration time is not specified!";
        public const string ExpirationNotNumeric = "Expiration time is not a number!";
        public const string DictionaryMissingBrackets = "Dictionary doesn't have proper surrounding brackets.";
        public const string InvalidCommaColon = "Invalid comma and / or colon formatting.";
        public const string InvalidKeyValue = "Invalid key/value pair.";
        public const string KeyValueNoDoubleQuotes = "Key or value isn't surrounded by double quotes.";
        public const string KeyValueNotAlphaNum = "Key or value is not alpha-numeric (excluding white-space).";
        public const string MustBeAdmin = "adminName does not exists or is not an admin";

        // EXCEPTION MESSAGES -- Data Store Logging
        public const string TimestampFormatIncorrect = "Timestamp Format Incorrect";

        // EXCEPTION MESSAGES -- User Management
        public const string UsernameDNE = "The username doesn't exsit.";
        public const string UserLocked = "This user is locked!";

        // EXCEPTION MESSAGES -- IPAddressDAO
        public const string IPCreateInvalidArgument = "IPAddressDAO.CreateAsync record argument must be of type IPAddressRecord";
        public const string IPRecordNoNull = "All columns in IPRecord must be not null.";
        public const string IPUpdateInvalidArgument = "IPAddressDAO.UpdateAsync record argument must be of type IPAddressRecord";
        public const string IPDeleteDNE = "IPAddressDAO.DeleteByIdsAsync ip did not exist";
        public const string IPReadDNE = "IPAddressDAO.ReadByIdAsync ip did not exist";
        public const string IPUpdateDNE = "IPAddressDAO.UpdateAsync ip did not exist";

        // EXCEPTION MESSAGES -- LogDAO
        public const string LogCreateInvalidArgument = "LogDAO.CreateAsync record argument must be of type LogRecord";
        public const string LogFindInvalidArgument = "LogDAO.FindIdFieldAsync record argument must be of type LogRecord";
        public const string LogFindDNE = "LogDAO.FindIdFieldAsync record did not exist in the collection";
        public const string LogDeleteDNE = "LogDAO.DeleteAsync uniqueId did not exist in the collection";

        // EXCEPTION MESSAGES -- UserDAO
        public const string UserCreateInvalidArgument = "UserDAO.CreateAsync record argument must be of type UserRecord";
        public const string UserRecordNoNull = "All columns in UserRecord must be not null.";
        public const string UserUpdateInvalidArgument = "UserDAO.UpdateAsync record argument must be of type UserRecord";
        public const string UserDeleteDNE = "UserDAO.DeleteByIdsAsync username did not exist";
        public const string UserReadDNE = "UserDAO.ReadByIdAsync username did not exist";
        public const string UserUpdateDNE = "UserDAO.UpdateAsync username did not exist";

        // EXCEPTION MESSAGES -- MapDAO
        public const string MapCreateInvalidArgument = "MapDAO.CreateAsync record argument must be of type MapRecord";
        public const string MapDeleteDNE = "MapDAO.DeleteByIdsAsync hash did not exist";
        public const string MapReadDNE = "MapDAO.ReadByIdAsync hash did not exist";
        public const string MapUpdateDNE = "MapDAO.UpdateAsync hash did not exist";
        public const string MapUpdateInvalidArgument = "MapDAO.UpdateAsync record argument must be of type MapRecord";

        // EXCEPTION MESSAGES -- TicketDAO
        public const string TicketCreateInvalidArgument = "TicketDAO.CreateAsync record argument must be of type TicketRecord";
        public const string TicketRecordNoNull = "All columns in Ticket must not be null";
        public const string TicketDeleteDNE = "TicketDAO.DeleteByIdsAsync ticket id did not exist";
        public const string TicketReadDNE = "TicketDAO.ReadByIdAsync ticket id did not exist";
        public const string TicketUpdateDNE = "TicketDAO.UpdateAsync ticket id did not exist";
        public const string TicketUpdateInvalidArgument = "TicketDAO.UpdateAsync record argument must be of type TicketRecord";

        // EXCEPTION MESSAGES -- Archiving
        public const string SourceDirectoryDNE = "Archiving failed on because source directory did not exist";
        public const string FirstArgumentNotInt = "Archiving failed because first argument must be an integer";
        public const string InvalidArgs = "Arvhiving failed because needed to enter days, source Directory, and target Directory for archiving";
        public const string FileFetchingDirectoryDNE = "File fetching source Directory does not exist.";
        public const string DaysLessThanZero = "File fetching days cannot be less than 0.";
        public const string FileFetchingNoLogs = "File fetching no logs to archive in the source directory.";
        public const string FileFetchingLogsNotMoved = "File fetching files were not moved to the target directory";
        public const string CompressionInvalidArguments = "Invalid compression source Directory or 7zip file path.";
        public const string CompressionFailed = "Compression archive failed to create.";
        public const string FTPfileNotFound = "FTP archive File not found.";
        public const string FTPinvalidCredentials = "Invalid ftp credentials";

        // EXCEPTION MESSAGES -- Project Status
        public const string NotInDevelopment = "Failed on because project status is not in development";

        // EXCEPTION MESSAGES -- Masking
        public const string HashNotInTable = "The hash attempted to access for decrementation was not in the table";
        public const string DecrementDeleteUnmasked = "DecrementMappingForDeleteAsync object was unmasked";
        public const string DecrementUpdateUnmasked = "DecrementMappingForUpdateAsync object was unmasked";

        // EXCEPTION MESSAGES -- UserManagement
        public const string UpdateIPRecordMasked = "UpdateIPAsync record was masked";
        public const string DeleteIPDNE = "DeleteIPAsync ip address did not exist";
        public const string CreateUserRecordMasked = "CreateUserAsync record was masked";
        public const string CreateUsersRecordMasked = "CreateUsersAsync record was masked";
        public const string UpdateUserRecordMasked = "UpdateUserAsync record was masked";
        public const string BulkUpdateUsersRecordMasked = "UpdateUserAsync record was masked";
    }
}
