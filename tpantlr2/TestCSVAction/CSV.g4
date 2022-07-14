grammar CSV;

@header {
using System.Linq;
}

/** Derived from rule "file : hdr row+ ;" */
file
locals [int i=0]
     : hdr ( rows+=row[$hdr.text.Split(",")] {$i++;} )+
       {
       Console.WriteLine($i+" rows");
       foreach (RowContext r in $rows) {
           Console.WriteLine("row token interval: "+r.SourceInterval);
       }
       }
     ;

hdr : row[null] {Console.WriteLine($"header: '{$text.Trim()}'");} ;

/** Derived from rule "row : field (',' field)* '\r'? '\n' ;" */
row[string[] columns] returns [Dictionary<string,string> values]
locals [int col=0]
@init {
    $values = new Dictionary<string,string>();
}
@after {
    if ($values!=null && $values.Count()>0) {
        var enumerable = $values.Select(o => $"{o.Key}={o.Value}");
		string text = string.Join(", ", enumerable);
        Console.WriteLine($"values = {text}");
    }
}
// rule row cont'd...
    :   field
        {
        if ($columns!=null) {
            $values.Add($columns[$col++].Trim(), $field.text.Trim());
        }
        }
        (   ',' field
            {
            if ($columns!=null) {
                $values.Add($columns[$col++].Trim(), $field.text.Trim());
            }
            }
        )* '\r'? '\n'
    ;

field
    :   TEXT
    |   STRING
    |
    ;

TEXT : ~[,\n\r"]+ ;
STRING : '"' ('""'|~'"')* '"' ; // quote-quote is an escaped quote
