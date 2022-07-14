grammar Keywords;
@lexer::header {    // place this header action only in lexer, not the parser
using System.Collections.Generic;
}

// explicitly define keyword token types to avoid implicit def warnings
tokens { BEGIN, END, IF, THEN, WHILE }

@lexer::members {   // place this class member only in lexer
Dictionary<string,int> keywords = new Dictionary<string,int>() {
    {"begin", KeywordsParser.BEGIN},
    {"end",   KeywordsParser.END},
    {"if",    KeywordsParser.IF},
    {"then",  KeywordsParser.THEN},
    {"while", KeywordsParser.WHILE}
};
}

stat:   BEGIN stat* END 
    |   IF expr THEN stat
    |   WHILE expr stat
    |   ID '=' expr ';'
	;

expr:   INT | CHAR ;

ID  :   [a-zA-Z]+
        {
        if ( keywords.ContainsKey(Text) ) {
            Type = keywords[Text]; // reset token type
        }
        }
    ;

/** Convert 3-char 'x' input sequence to string x */
CHAR:   '\'' . '\'' {Text = Text[1].ToString();} ;

INT :   [0-9]+ ;

WS  :   [ \t\n\r]+ -> skip ;
