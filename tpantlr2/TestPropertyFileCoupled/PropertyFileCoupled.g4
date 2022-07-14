grammar PropertyFileCoupled;
@members {
public virtual void StartFile() { } // blank implementations
public virtual void FinishFile() { }
public virtual void DefineProperty(IToken name, IToken value) { 
	// Default implementation
	Console.WriteLine($"{name.Text}={value.Text}");
}
}
file : {StartFile();} prop+ {FinishFile();} ;
prop : ID '=' STRING '\n' {DefineProperty($ID, $STRING);} ;
ID : [a-z]+ ;
STRING : '"' .*? '"' ;

PREPROC     :   '#' .*? '\n' -> skip ;