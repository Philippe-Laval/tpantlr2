grammar PredCppStat;

@parser::header {
using System.Collections;
}

@parser::members {
HashSet<string> _types = new HashSet<string> { "T" };
bool Istype() { return _types.Contains(CurrentToken.Text); }
}

stat:   decl ';'  {Console.WriteLine("decl "+$decl.text);}
    |   expr ';'  {Console.WriteLine("expr "+$expr.text);}
    ;

decl:   ID ID                         // E.g., "Point p"
    |   {Istype()}? ID '(' ID ')'     // E.g., "Point (p)", same as ID ID
    ;

expr:   INT                           // integer literal
    |   ID                            // identifier
    |   {!Istype()}? ID '(' expr ')'  // function call
    ;

ID  :   [a-zA-Z]+ ;
INT :   [0-9]+ ;
WS  :   [ \t\n\r]+ -> skip ;