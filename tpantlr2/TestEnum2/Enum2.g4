grammar Enum2;

@lexer::members {
public static bool Java5 = false;
}

prog:   (   stat 
        |   enumDecl
        )+
    ;

stat:   ID '=' expr ';' {Console.WriteLine($ID.text+"="+$expr.text);} ;

expr:   ID
    |   INT
    ;

// No predicate needed here because 'enum' token undefined if !java5
enumDecl
    :   'enum' name=ID '{' ID (',' ID)* '}'
        {Console.WriteLine("enum "+$name.text);}
    ;

ENUM:   'enum' {Java5}? ; // must be before ID
ID  :   [a-zA-Z]+ ;


INT :   [0-9]+ ;
WS  :   [ \t\r\n]+ -> skip ;
