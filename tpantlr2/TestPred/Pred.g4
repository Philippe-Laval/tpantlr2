grammar Pred;

assign
    : ID '=' v=INT {if ($v.int==0) NotifyErrorListeners("values must be > 0");} ';'
      {Console.WriteLine("assign "+$ID.text+" to ");}
    ;

INT :   [0-9]+ ;
ID  :   [a-zA-Z]+ ;
WS  :   [ \t\r\n]+ -> skip ;

