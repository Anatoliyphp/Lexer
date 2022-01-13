using System;
using System.Collections.Generic;
using System.IO;

namespace CSharpLexer
{
    public class TokenDetalization
    {
        private TokenType _type;
        private string _token;
        private KeyValuePair<int, int> _tokenPosition;

        public TokenDetalization(TokenType type, string token, KeyValuePair<int, int> tokenPosition)
        {
            _token = token;
            _type = type;
            _tokenPosition = tokenPosition;
        }

        public void PrintTokenDetalization(StreamWriter outputStream)
        {
            if (
                _type != TokenType.SPACE
                && _type != TokenType.ONE_LINE_COMMENT
                && _type != TokenType.BLOCK_COMMENT_START
                && _type != TokenType.BLOCK_COMMENT_END)
            {
                outputStream.WriteLine($"{_type} '{_token}' [{_tokenPosition.Key},{_tokenPosition.Value}]");
            }
        }
        
        public TokenType GetTokenType()
        {
            return _type;
        }

        public KeyValuePair<int, int> GetTokenPosition()
        {
            return _tokenPosition;
        }
    }
}