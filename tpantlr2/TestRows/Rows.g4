grammar Rows;

@parser::members { 
	/// <summary>
	/// add members to generated RowsParser
	/// </summary>
	int col;

	/// <summary>
	/// custom constructor
	/// </summary>
	/// <param name="input"></param>
	/// <param name="col"></param>
	public RowsParser(ITokenStream input, int col) : this(input)
    { 
        this.col = col;
    }
}

file: (row NL)+ ;

row
locals [int i=0]
    : (   STUFF
          {
          $i++;
          if ( $i == col ) Console.WriteLine($STUFF.text);
          }
      )+
    ;

TAB  :  '\t' -> skip ;   // match but don't pass to the parser
NL   :  '\r'? '\n' ;     // match and pass to the parser
STUFF:  ~[\t\r\n]+ ;     // match any chars except tab, newline
