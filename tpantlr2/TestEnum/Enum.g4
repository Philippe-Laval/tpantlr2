grammar Enum;

@parser::members {
public static bool Java5;
}

prog:   (   stat 
        |   enumDecl
        )+
    ;

stat:   id '=' expr ';' {Console.WriteLine($id.text+"="+$expr.text);} ;

expr
    :   id
    |   INT
    ;

enumDecl
    :   {Java5}? 'enum' name=id '{' id (',' id)* '}'
        {Console.WriteLine("enum "+$name.text);}
    ;

id  :   ID
    |   {!Java5}? 'enum'
    ;
    
ID  :   [a-zA-Z]+ ;
INT :   [0-9]+ ;
WS  :   [ \t\r\n]+ -> skip ;
