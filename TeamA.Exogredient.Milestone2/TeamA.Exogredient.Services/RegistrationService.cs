﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TeamA.Exogredient.DAL;

namespace TeamA.Exogredient.Services
{
    /// <summary>
    /// Class <c>RegistrationService</c> Provides functionality to allow for user registration.
    /// </summary>
    public class RegistrationService
    {
        private UserDAO _userDAO;
        private CorruptedPasswordsDAO _corruptedPasswordsDAO;

        // No < or > to protect from SQL injections.
        private readonly List<char> _alphaNumericSpecialCharacters = new List<char>()
        {
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q',
            'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '1', '2', '3', '4', '5', '6', '7', '8', '9',
            '0', '~', '`', '@', '#', '$', '%', '^', '&', '!', '*', '(', ')', '_', '-', '+', '=', '{',
            '[', '}', ']', '|', '\\', '"', '\'', ':', ';', '?', '/', '.', ','
        };

        private readonly List<char> _numericalCharacters = new List<char>()
        {
            '1', '2', '3', '4', '5', '6', '7', '8', '9', '0'
        };

        // =================================================
        // NIST CHECKING:
        // =================================================

        private readonly int _maxRepetitionOrSequence = 3;

        private readonly List<char> _lettersLower = new List<char>()
        {
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q',
            'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'
        };

        private readonly List<char> _lettersUpper = new List<char>()
        {
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q',
            'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
        };

        private readonly List<char> _numbers = new List<char>()
        {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
        };

        private readonly IDictionary<char, int> _lettersLowerToPositions = new Dictionary<char, int>()
        {
            {'a', 1}, {'b', 2}, {'c', 3}, {'d', 4}, {'e', 5}, {'f', 6}, {'g', 7}, {'h', 8},
            {'i', 9}, {'j', 10}, {'k', 11}, {'l', 12}, {'m', 13}, {'n', 14}, {'o', 15}, {'p', 16},
            {'q', 17}, {'r', 18}, {'s', 19}, {'t', 20}, {'u', 21}, {'v', 22}, {'w', 23}, {'x', 24},
            {'y', 25}, {'z', 26}
        };

        private readonly IDictionary<char, int> _lettersUpperToPositions = new Dictionary<char, int>()
        {
            {'A', 1}, {'B', 2}, {'C', 3}, {'D', 4}, {'E', 5}, {'F', 6}, {'G', 7}, {'H', 8},
            {'I', 9}, {'J', 10}, {'K', 11}, {'L', 12}, {'M', 13}, {'N', 14}, {'O', 15}, {'P', 16},
            {'Q', 17}, {'R', 18}, {'S', 19}, {'T', 20}, {'U', 21}, {'V', 22}, {'W', 23}, {'X', 24},
            {'Y', 25}, {'Z', 26}
        };

        private readonly IDictionary<int, char> _positionsToLettersLower = new Dictionary<int, char>()
        {
            {1, 'a'}, {2, 'b'}, {3, 'c'}, {4, 'd'}, {5, 'e'}, {6, 'f'}, {7, 'g'}, {8, 'h'},
            {9, 'i'}, {10, 'j'}, {11, 'k'}, {12, 'l'}, {13, 'm'}, {14, 'n'}, {15, 'o'}, {16, 'p'},
            {17, 'q'}, {18, 'r'}, {19, 's'}, {20, 't'}, {21, 'u'}, {22, 'v'}, {23, 'w'}, {24, 'x'},
            {25, 'y'}, {26, 'z'}
        };

        private readonly IDictionary<int, char> _positionsToLettersUpper = new Dictionary<int, char>()
        {
            {1, 'A'}, {2, 'B'}, {3, 'C'}, {4, 'D'}, {5, 'E'}, {6, 'F'}, {7, 'G'}, {8, 'H'},
            {9, 'I'}, {10, 'J'}, {11, 'K'}, {12, 'L'}, {13, 'M'}, {14, 'N'}, {15, 'O'}, {16, 'P'},
            {17, 'Q'}, {18, 'R'}, {19, 'S'}, {20, 'T'}, {21, 'U'}, {22, 'V'}, {23, 'W'}, {24, 'X'},
            {25, 'Y'}, {26, 'Z'}
        };

        /// <summary>
        /// Constructor initializes the UserDAO object to provide
        /// the interface with the usertable.
        /// </summary>
        public RegistrationService()
        {
            _userDAO = new UserDAO();
            _corruptedPasswordsDAO = new CorruptedPasswordsDAO();
        }

        /// <summary>
        /// Determines whether the user is within the project scope.
        /// </summary>
        /// <param name="answer">The user's selected scope answer.</param>
        /// <returns>Returns the value of bool that determines whether the 
        /// user is allowed to proceed.</returns>
        public bool CheckScope(bool answer)
        {
            return answer == true;
        }

        /// <summary>
        /// Check whether a given string meets the requirement for length.
        /// </summary>
        /// <param name="name">The string the user wants to check length of.</param>
        /// <param name="length">The length that the string must be equal to.</param>
        /// <param name="min">A optional parameter. If this is set then, name's length can be a 
        /// range from min to length (inclusive).</param>
        /// <returns>Returns value of bool to represent whether the name met the required constraints.</returns>
        public bool CheckLength(string name, int length, int min = -1)
        {
            if (min == -1)
            {
                return name.Length == length;
            }
            else
            {
                return name.Length >= min && name.Length <= length;
            }
        }

        /// <summary>
        /// Check whether a given string contains only Alphanumeric and Special characters.
        /// </summary>
        /// <param name="name">The string that we are checking.</param>
        /// <returns>Returns value of bool to represent whether all the characters
        /// in name meet the specification.</returns>
        public bool CheckIfANSCharacters(string name)
        {

            bool result = true;

            foreach (char c in name.ToLower())
            {
                result = result && _alphaNumericSpecialCharacters.Contains(c);
            }

            return result;
        }

        /// <summary>
        /// Check whether a given string contains only numerical characters.
        /// </summary>
        /// <param name="name">The string that we are checking.</param>
        /// <returns>Returns value of bool to represent whether all the characters
        /// in name meet the specification.</returns>
        public bool CheckIfNumericCharacters(string name)
        {
            bool result = true;

            foreach (char c in name)
            {
                result = result && _numericalCharacters.Contains(c);
            }

            return result;
        }

        /// <summary>
        /// Check whether the email is in a valid format (minimally: contains an @ with text on
        /// either side, and that text does not contain "..").
        /// </summary>
        /// <param name="email">The email we are checking</param>
        /// <returns>Returns a bool representing whether the email satisfies
        /// the specifications.</returns>
        public bool EmailFormatValidityCheck(string email)
        {
            string[] splitResult = email.Split('@');

            if (splitResult.Length == 2)
            {
                string first = splitResult[0];
                string second = splitResult[1];

                if (first.Length >= 1 && second.Length >= 1)
                {
                    if (first.Contains("..") || second.Contains(".."))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Breaks email address up into two parts, the local-part 
        /// and the domain.
        /// First we convert to lowercase.
        /// Then we check if the domain is "gmail.com".
        /// If it is, then the local-part is checked for "+[anything]", ".", and """", 
        /// and they are then removed.
        /// Finally all double quotes are removed
        /// </summary>
        /// <param name="email">The email we are checking.</param>
        /// <returns>Returns value of string to represent the 
        /// canonicalized email.</returns>
        public string CanonicalizingEmail(string email)
        {
            string[] splitResult = email.Split('@');
            string username = splitResult[0].ToLower();
            string domain = splitResult[1].ToLower();

            string transposedUsername = username;

            if (domain.Equals("gmail.com"))
            {
                // Remove dots.
                transposedUsername = transposedUsername.Replace(".", "");

                // Remove plus and everything after.
                int plusIndex = transposedUsername.IndexOf("+");

                if (plusIndex != -1)
                {
                    transposedUsername = transposedUsername.Remove(plusIndex);
                }
            }

            // Remove quotes.
            transposedUsername = transposedUsername.Replace("\"", "");

            return transposedUsername + "@" + domain;
        }

        /// <summary>
        /// Checks if a canonicalized email exists in the database.
        /// </summary>
        /// <param name="canonEmail">Email that already has been canonicalized.</param>
        /// <returns>Returns the value of bool to represent whether
        /// an canonicalized email is unique.</returns>
        public async Task<bool> CheckEmailUniquenessAsync(string canonEmail)
        {
            //return await _userDAO.CheckEmailUniquenessAsync(canonEmail);
            return false;
        }

        /// <summary>
        /// Checks if a phone number already exists in the database.
        /// </summary>
        /// <param name="phoneNumber">The phone number we are checking.</param>
        /// <returns>Returns the value of bool to represent whether
        /// a phone number is unique.</returns>
        public async Task<bool> CheckPhoneUniquenessAsync(string phoneNumber)
        {
            //return await _userDAO.CheckPhoneUniquenessAsync(phoneNumber);
            return false;
        }

        /// <summary>
        /// Checks if a username already exists in the database.
        /// </summary>
        /// <param name="username">The username we are checking.</param>
        /// <returns>Returns the value of bool to represent whether
        /// an username is unique.</returns>
        public async Task<bool> CheckUsernameUniquenessAsync(string username)
        {
            //return await _userDAO.CheckUsernameUniquenessAsync(username);
            return false;
        }

        public async Task<bool> CheckPasswordSecurityAsync(string plaintextPassword)
        {
            // Test if password contains context specific words.

            if (plaintextPassword.ToLower().Contains("exogredient"))
            {
                return false;
            }


            // Check if password contains an english word, upper or lowercase, among the top 9000 most popular
            // words that are over 3 characters in length. Done 2nd because fastest IO test.

            string lineInput = "";

            using (StreamReader reader = new StreamReader(@"..\..\..\..\words.txt"))
            {
                while ((lineInput = await reader.ReadLineAsync()) != null)
                {
                    if (plaintextPassword.Contains(lineInput))
                    {
                        return false;
                    }
                }
            }

            // Check if password has been corrupted in previous breaches. Done second to last because
            // it is the slowest IO check.

            List<string> passwordHashes = await _corruptedPasswordsDAO.ReadAsync();
            string passwordSha1 = SecurityService.HashWithSHA1(plaintextPassword);

            foreach (string hash in passwordHashes)
            {
                if (passwordSha1.Equals(hash))
                {
                    return false;
                }
            }

            // Repetition and sequence checking.
            int patternCount = 1;

            bool repetition = false;
            bool increasingSequence = false;
            bool decreasingSequence = false;

            char previousCharacter = '\b';

            bool first = true;

            foreach (char character in plaintextPassword)
            {
                if (first)
                {
                    first = false;
                    previousCharacter = character;
                }
                else
                {
                    // Continue flags, or stop them.
                    if (repetition || increasingSequence || decreasingSequence)
                    {
                        if (repetition)
                        {
                            if (character == previousCharacter)
                            {
                                patternCount++;
                            }
                            else
                            {
                                repetition = false;
                                patternCount = 1;
                            }
                        }
                        else if (increasingSequence)
                        {
                            int previousPosition = 0;
                            bool number = false;
                            bool upperLetter = false;
                            bool lowerLetter = false;

                            if (_lettersLower.Contains(previousCharacter))
                            {
                                lowerLetter = true;
                                previousPosition = _lettersLowerToPositions[previousCharacter];
                            }
                            else if (_lettersUpper.Contains(previousCharacter))
                            {
                                upperLetter = true;
                                previousPosition = _lettersUpperToPositions[previousCharacter];
                            }
                            else if (_numbers.Contains(previousCharacter))
                            {
                                number = true;
                                // Characters represented by sequential numbers in every utf-16
                                previousPosition = previousCharacter - '0';
                            }

                            int nextPosition = previousPosition + 1;

                            if (number)
                            {
                                if (nextPosition == 10)
                                {
                                    nextPosition = 1;
                                }

                                if (_numbers[nextPosition] == character)
                                {
                                    patternCount++;
                                }
                                else
                                {
                                    increasingSequence = false;
                                    patternCount = 1;
                                }
                            }
                            else if (upperLetter)
                            {
                                if (nextPosition == 27)
                                {
                                    nextPosition = 1;
                                }

                                if (_positionsToLettersUpper[nextPosition] == character)
                                {
                                    patternCount++;
                                }
                                else
                                {
                                    increasingSequence = false;
                                    patternCount = 1;
                                }
                            }
                            else if (lowerLetter)
                            {
                                if (nextPosition == 27)
                                {
                                    nextPosition = 1;
                                }

                                if (_positionsToLettersLower[nextPosition] == character)
                                {
                                    patternCount++;
                                }
                                else
                                {
                                    increasingSequence = false;
                                    patternCount = 1;
                                }
                            }
                        }
                        else if (decreasingSequence)
                        {
                            int previousPosition = 0;
                            bool number = false;
                            bool upperLetter = false;
                            bool lowerLetter = false;

                            if (_lettersLower.Contains(previousCharacter))
                            {
                                lowerLetter = true;
                                previousPosition = _lettersLowerToPositions[previousCharacter];
                            }
                            else if (_lettersUpper.Contains(previousCharacter))
                            {
                                upperLetter = true;
                                previousPosition = _lettersUpperToPositions[previousCharacter];
                            }
                            else if (_numbers.Contains(previousCharacter))
                            {
                                number = true;
                                // Characters represented by sequential numbers in every utf-16
                                previousPosition = previousCharacter - '0';
                            }

                            int nextPosition = previousPosition - 1;

                            if (number)
                            {
                                if (nextPosition == 0)
                                {
                                    nextPosition = 9;
                                }

                                if (_numbers[nextPosition] == character)
                                {
                                    patternCount++;
                                }
                                else
                                {
                                    decreasingSequence = false;
                                    patternCount = 1;
                                }
                            }
                            else if (upperLetter)
                            {
                                if (nextPosition == 0)
                                {
                                    nextPosition = 26;
                                }

                                if (_positionsToLettersUpper[nextPosition] == character)
                                {
                                    patternCount++;
                                }
                                else
                                {
                                    decreasingSequence = false;
                                    patternCount = 1;
                                }
                            }
                            else if (lowerLetter)
                            {
                                if (nextPosition == 0)
                                {
                                    nextPosition = 26;
                                }

                                if (_positionsToLettersLower[nextPosition] == character)
                                {
                                    patternCount++;
                                }
                                else
                                {
                                    decreasingSequence = false;
                                    patternCount = 1;
                                }
                            }
                        }
                    }
                    else
                    {
                        // Set flags for first instance of a pattern.
                        if (previousCharacter == character)
                        {
                            patternCount++;
                            repetition = true;
                        }
                        else
                        {
                            if (_lettersLower.Contains(character))
                            {
                                int previousPosition = 0;

                                if (_lettersLower.Contains(previousCharacter))
                                {
                                    previousPosition = _lettersLowerToPositions[previousCharacter];

                                    int nextPositionIncrease = previousPosition + 1;
                                    int nextPositionDecrease = previousPosition - 1;

                                    if (nextPositionIncrease == 27)
                                    {
                                        nextPositionIncrease = 1;
                                    }

                                    if (nextPositionDecrease == 0)
                                    {
                                        nextPositionDecrease = 26;
                                    }

                                    if (_positionsToLettersLower[nextPositionIncrease] == character)
                                    {
                                        patternCount++;
                                        increasingSequence = true;
                                    }

                                    if (_positionsToLettersLower[nextPositionDecrease] == character)
                                    {
                                        patternCount++;
                                        decreasingSequence = true;
                                    }
                                }
                            }
                            else if (_lettersUpper.Contains(character))
                            {
                                int previousPosition = 0;

                                if (_lettersUpper.Contains(previousCharacter))
                                {
                                    previousPosition = _lettersUpperToPositions[previousCharacter];

                                    int nextPositionIncrease = previousPosition + 1;
                                    int nextPositionDecrease = previousPosition - 1;

                                    if (nextPositionIncrease == 27)
                                    {
                                        nextPositionIncrease = 1;
                                    }

                                    if (nextPositionDecrease == 0)
                                    {
                                        nextPositionDecrease = 26;
                                    }

                                    if (_positionsToLettersUpper[nextPositionIncrease] == character)
                                    {
                                        patternCount++;
                                        increasingSequence = true;
                                    }

                                    if (_positionsToLettersUpper[nextPositionDecrease] == character)
                                    {
                                        patternCount++;
                                        decreasingSequence = true;
                                    }
                                }
                            }
                            else if (_numbers.Contains(character))
                            {
                                int previousPosition = 0;

                                if (_numbers.Contains(previousCharacter))
                                {
                                    previousPosition = previousCharacter - '0';

                                    int nextPositionIncrease = previousPosition + 1;
                                    int nextPositionDecrease = previousPosition - 1;

                                    if (nextPositionIncrease == 10)
                                    {
                                        nextPositionIncrease = 1;
                                    }

                                    if (nextPositionDecrease == 0)
                                    {
                                        nextPositionDecrease = 9;
                                    }

                                    if (_numbers[nextPositionIncrease] == character)
                                    {
                                        patternCount++;
                                        increasingSequence = true;
                                    }

                                    if (_numbers[nextPositionDecrease] == character)
                                    {
                                        patternCount++;
                                        decreasingSequence = true;
                                    }
                                }
                            }
                        }
                    }

                    // Constant check at end of each iteration to possibly return false from this function.
                    if (patternCount == _maxRepetitionOrSequence)
                    {
                        return false;
                    }

                    // If here, we go to the next iteration.
                    previousCharacter = character;
                }
            }

            return true;
        }

        bool GenerateTempUser(string firstName, string lastName, string phoneNumber,
            string username, string email, string digestPlusSalt)
        {
            //return async bool _UserDAO.GenerateTempUser(string username);
            return false;
        }

        bool DeleteTempUser(string username)
        {
            //return async bool _UserDAO.DeleteTempUser(string username)
            return false;
        }

        bool MakeTempUserPerm(string username)
        {
            //return async bool _UserDAO.MakeTempUserPerm(string username)
            return false;
        }
    }
}
