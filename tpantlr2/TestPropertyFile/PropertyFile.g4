grammar PropertyFile;
file : prop+ ;
prop : ID '=' STRING '\n' ;
ID   : [a-z]+ ;
STRING : '"' .*? '"' ;

PREPROC     :   '#' .*? '\n' -> skip ;