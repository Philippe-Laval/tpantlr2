/** Grammar from tour chapter augmented with actions */
grammar Expr;

@header {
//package tools;
//import java.util.*;
}

@parser::members {
    /// <summary>
	/// "memory" for our calculator; variable/value pairs go here
	/// </summary>
    Dictionary<string, int> memory = new Dictionary<string, int>();

    protected int Eval(int left, int op, int right) {
        switch ( op ) {
            case MUL : return left * right;
            case DIV : return left / right;
            case ADD : return left + right;
            case SUB : return left - right;
        }
        return 0;
    }
}

stat:   e NEWLINE           {Console.WriteLine($e.v);}
    |   ID '=' e NEWLINE    {memory.Add($ID.text, $e.v);}
    |   NEWLINE                   
    ;

e returns [int v]
    : a=e op=('*'|'/') b=e  {$v = Eval($a.v, $op.type, $b.v);}
    | a=e op=('+'|'-') b=e  {$v = Eval($a.v, $op.type, $b.v);}  
    | INT                   {$v = $INT.int;}    
    | ID
      {
      String id = $ID.text;
      $v = memory.ContainsKey(id) ? memory[id] : 0;
      }
    | '(' e ')'             {$v = $e.v;}       
    ; 

MUL : '*' ;
DIV : '/' ;
ADD : '+' ;
SUB : '-' ;

ID  :   [a-zA-Z]+ ;      // match identifiers
INT :   [0-9]+ ;         // match integers
NEWLINE:'\r'? '\n' ;     // return newlines to parser (is end-statement signal)
WS  :   [ \t]+ -> skip ; // toss out whitespace