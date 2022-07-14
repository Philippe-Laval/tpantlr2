grammar Simple;

prog:   classDef+ ; // match one or more class definitions

classDef
    :   'class' ID '{' member+ '}' // a class has one or more members
        {Console.WriteLine("class "+$ID.text);}
    ;

member
    :   'int' ID ';'                       // field definition
        {Console.WriteLine("var "+$ID.text);}
    |   'int' f=ID '(' ID ')' '{' stat '}' // method definition
        {Console.WriteLine("method: "+$f.text);}
    ;

stat:   expr ';'
        {Console.WriteLine("found expr: "+$expr.text);}
    |   ID '=' expr ';'
        {Console.WriteLine("found assign: "+$ID.text);}
    ;

expr:   INT 
    |   ID '(' INT ')'
    ;

INT :   [0-9]+ ;
ID  :   [a-zA-Z]+ ;
WS  :   [ \t\r\n]+ -> skip ;
