using System;
using System.Collections.Generic;

namespace CSharpLexer
{
    public class TokenSсanner
    {
        private const int ID_MAX_LENGTH = 511;
        private const int INTEGER_MAX_LENGTH = 10;
        private const int DOUBLE_MAX_PRECISION = 17;
        public List<TokenDetalization> Scan(List<string> lines)
        {
            List<TokenDetalization> tokenDetalizations = new List<TokenDetalization>();
            
            if (lines.Count == 0)
            {
                tokenDetalizations.Add(new TokenDetalization(TokenType.END_FILE, "", new KeyValuePair<int, int>(1, 0)));
                return tokenDetalizations;
            }      

            int lineNumber = 1;

            for (int i = 0; i < lines.Count; i++)
            {
                string line = lines[i];
                int linePosition = 0;

                TokenType tokenType = TokenType.END_FILE;
                string token = String.Empty;

                for (int startIndex = 0; startIndex < line.Length; startIndex++)
                {
                    linePosition = startIndex;
                    switch (line[startIndex])
                    {
                        case '.':
                            tokenType = TokenType.POINT;
                            token = ".";
                            break;
                        case ',':
                            tokenType = TokenType.COMMA;
                            token = ",";
                            break;
                        case '>':
                            if (startIndex + 1 < line.Length)
                            {
                                if (line[startIndex + 1] == '=')
                                {
                                    tokenType = TokenType.MORE_OR_EQUAL;
                                    token = ">=";
                                    startIndex++; 
                                    break;
                                }
                            }
                            tokenType = TokenType.MORE;
                            token = ">";
                            break;
                        case '<':
                            if (startIndex + 1 < line.Length)
                            {
                                if (line[startIndex + 1] == '=')
                                {
                                    tokenType = TokenType.LESS_OR_EQUAL;
                                    token = "<=";
                                    startIndex++; 
                                    break;
                                }
                            }
                            tokenType = TokenType.LESS;
                            token = "<";
                            break;
                        case ';':
                            tokenType = TokenType.SEMICOLON;
                            token = ";";
                            break;
                        case ':':
                            tokenType = TokenType.COLON;
                            token = ":";
                            break;
                        case '(':
                            tokenType = TokenType.ROUND_BRACKET_OPEN;
                            token = "(";
                            break;
                        case ')':
                            tokenType = TokenType.ROUND_BRACKET_CLOSE;
                            token = ")";
                            break;
                        case '{':
                            tokenType = TokenType.FIGURE_BRACKET_OPEN;
                            token = "{";
                            break;
                        case '}':
                            tokenType = TokenType.FIGURE_BRACKET_CLOSE;
                            token = "}";
                            break;
                        case '+':
                            tokenType = TokenType.PLUS;
                            token = "+";
                            break;
                        case '-':
                            tokenType = TokenType.MINUS;
                            token = "-";
                            break;
                        case '*':
                            if (startIndex + 1 < line.Length)
                            {
                                if (line[startIndex + 1] == '/')
                                {
                                    tokenType = TokenType.BLOCK_COMMENT_END;
                                    token = "*/";
                                    startIndex++; 
                                    break;
                                }
                            }
                            tokenType = TokenType.MULTIPLICATION;
                            token = "*";
                            break;
                        case '/':
                            if (startIndex + 1 < line.Length)
                            {
                                if (line[startIndex + 1] == '/')
                                {
                                    tokenType = TokenType.ONE_LINE_COMMENT;
                                    token = "//";
                                    startIndex = line.Length;
                                    startIndex++;
                                }
                                else if (line[startIndex + 1] == '*')
                                {
                                    tokenType = TokenType.BLOCK_COMMENT_START;
                                    token = "/*";
                                    startIndex++;
                                }
                                else if (line[startIndex - 1] == '*')
                                {
                                    tokenType = TokenType.DIVISION;
                                    token = "/";
                                }
                            }
                            break;
                        case ' ':
                            tokenType = TokenType.SPACE;
                            token = " ";
                            break;
                        case '!':
                            if (startIndex + 1 < line.Length)
                            {
                                if (line[startIndex + 1] == '=')
                                {
                                    tokenType = TokenType.NOT_EQUAL;
                                    token = "!=";
                                    startIndex++; 
                                    break;
                                }
                            }
                            tokenType = TokenType.NOT;
                            token = "!";
                            break;
                        case '=':
                            if (startIndex + 1 < line.Length)
                            {
                                if (line[startIndex + 1] == '=')
                                {
                                    tokenType = TokenType.COMPARISON;
                                    token = "==";
                                    startIndex++;
                                    break;
                                }
                            }
                            tokenType = TokenType.ASSIGNMENT;
                            token = "=";
                            break;
                        case '"':
                            tokenType = TokenType.STRING_CONST;
                            token = "";
                            break;
                        default:
                            if (Char.IsLetter(line[startIndex]) || line[startIndex] == '_')
                            {
                                string id = String.Empty;
                                while (Char.IsLetterOrDigit(line[startIndex]) || line[startIndex] == '_')
                                {
                                    id += line[startIndex];
                                    startIndex++;

                                    if (startIndex >= line.Length)
                                    {
                                        break;
                                    }
                                }

                                token = id;

                                switch (id)
                                {
                                    case "true":
                                        tokenType = TokenType.BOOLTRUE;
                                        break;
                                    case "false":
                                        tokenType = TokenType.BOOLFALSE;
                                        break;
                                    case "class":
                                        tokenType = TokenType.CLASS;
                                        break;
                                    case "const":
                                        tokenType = TokenType.CONST;
                                        break;
                                    case "public":
                                        tokenType = TokenType.ACCESS_LEVEL_PUBLIC;
                                        break;
                                    case "private":
                                        tokenType = TokenType.ACCESS_LEVEL_PRIVATE;
                                        break;
                                    case "internal":
                                        tokenType = TokenType.ACCESS_LEVEL_INTERNAL;
                                        break;
                                    case "boolean":
                                        tokenType = TokenType.BOOL;
                                        break;
                                    case "string":
                                        tokenType = TokenType.STRING;
                                        break;
                                    case "char":
                                        tokenType = TokenType.CHAR;
                                        break;
                                    case "double":
                                        tokenType = TokenType.DOUBLE;
                                        break;
                                    case "float":
                                        tokenType = TokenType.FLOAT;
                                        break;
                                    case "while":
                                        tokenType = TokenType.WHILE;
                                        break;
                                    case "void":
                                        tokenType = TokenType.VOID;
                                        break;
                                    case "if":
                                        tokenType = TokenType.IF;
                                        break;
                                    case "else":
                                        tokenType = TokenType.ELSE;
                                        break;
                                    case "int":
                                        tokenType = TokenType.INTEGER;
                                        break;
                                    default:
                                        if (id.Length <= ID_MAX_LENGTH)
                                        {
                                            tokenType = TokenType.ID;
                                        }
                                        else
                                        {
                                            tokenType = TokenType.ERROR;
                                        }
                                        break;
                                }
                            }
                            else if (Char.IsDigit(line[startIndex]))
                            {

                                string num = string.Empty;

                                while (Char.IsDigit(line[startIndex]) || line[startIndex] == '.')
                                {
                                    num += line[startIndex];
                                    startIndex++;
                                    if (startIndex == line.Length)
                                    {
                                        break;
                                    }
                                }

                                if (num.Length == 1 && num == "0")
                                {
                                    tokenType = TokenType.INTEGER_NUM;
                                }
                                else if (NumIsDouble(num))
                                {
                                    tokenType = TokenType.DOUBLE_NUM;
                                }
                                else if (NumIsInt(num))
                                {
                                    tokenType = TokenType.INTEGER_NUM;
                                }
                                else
                                {
                                    tokenType = TokenType.ERROR;
                                }
                            }
                            else
                            {
                                tokenType = TokenType.ERROR;
                                token = line[startIndex].ToString();
                            }
                            break;
                    }

                    if (tokenType == TokenType.STRING_CONST)
                    {
                        int lineNimberStart = lineNumber;
                        startIndex++;
                        int stringConstStart = startIndex;

                        int stringConstEnd = -1;
                        while (stringConstEnd == -1)
                        {
                            stringConstEnd = line.IndexOf('\"', startIndex);
                            if (stringConstEnd == -1)
                            {
                                for (int j = startIndex; j < line.Length; j++)
                                {
                                    token += line[j];
                                }
                                if ((i+1) == lines.Count)
                                {
                                    tokenType = TokenType.ERROR;
                                    token = '\"'.ToString();
                                    break;
                                }
                                line = lines[i + 1];
                                startIndex = 0;
                                i++;
                                lineNumber++;
                            }
                            else
                            {
                                for (int j = startIndex; j < stringConstEnd; j++)
                                {
                                    token += line[j];
                                }
                                startIndex = stringConstEnd;
                            }
                        }

                        tokenDetalizations.Add(new TokenDetalization(tokenType, token, new KeyValuePair<int, int>(lineNimberStart, linePosition)));
                    }
                    else
                    {
                        tokenDetalizations.Add(new TokenDetalization(tokenType, token, new KeyValuePair<int, int>(lineNumber, linePosition)));
                        if (startIndex < line.Length)
                        {
                            if (line[startIndex] == '<')
                            {
                                tokenDetalizations.Add(new TokenDetalization(TokenType.TRIANGULAR_BRACKET_OPEN, "<", new KeyValuePair<int, int>(lineNumber, linePosition)));
                            }
                            if (line[startIndex] == '>')
                            {
                                tokenDetalizations.Add(new TokenDetalization(TokenType.TRIANGULAR_BRACKET_CLOSE, ">", new KeyValuePair<int, int>(lineNumber, startIndex)));
                            }
                            if (line[startIndex] == ',')
                            {
                                tokenDetalizations.Add(new TokenDetalization(TokenType.COMMA, ",", new KeyValuePair<int, int>(lineNumber, startIndex)));
                            }
                        }
                    }
                }
                lineNumber++;
            }
            
            //delete comments
            while (true)
            {
                int blockCommentStartIndex = tokenDetalizations.FindIndex(p => p.GetTokenType() == TokenType.BLOCK_COMMENT_START);
                int blockCommentEndIndex = tokenDetalizations.FindIndex(p => p.GetTokenType() == TokenType.BLOCK_COMMENT_END);
                if (blockCommentStartIndex == -1 && blockCommentEndIndex == -1)
                {
                    break;
                }
                if (blockCommentEndIndex == -1)
                {
                    TokenDetalization errorToken = new TokenDetalization(TokenType.ERROR, "", tokenDetalizations[blockCommentStartIndex].GetTokenPosition());
                    tokenDetalizations.RemoveRange(blockCommentStartIndex, tokenDetalizations.Count - 1 - blockCommentStartIndex);
                    tokenDetalizations.Add(errorToken);
                    break;
                }
                else
                {
                    tokenDetalizations.RemoveRange(blockCommentStartIndex, blockCommentEndIndex - blockCommentStartIndex);
                    break;
                }
            }
            
            return tokenDetalizations;
        }
        
        private static bool NumIsInt(string num)
        {
            if (num.Length > INTEGER_MAX_LENGTH || num[0] == '0')
            {
                return false;
            }

            foreach (char ch in num)
            {
                if (!Char.IsDigit(ch))
                {
                    return false;
                }
            }

            return true;
        }
        
        private static bool NumIsDouble(string num)
        {
            if (!num.Contains("."))
            {
                return false;
            }

            if (num.Length - num.IndexOf('.') > DOUBLE_MAX_PRECISION)
            {
                return false;
            }

            for (int i = 0; i< num.Length; i++)
            {
                if (i != num.IndexOf('.'))
                {
                    if (!Char.IsDigit(num[i]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}