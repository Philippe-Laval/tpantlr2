using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Antlr4.StringTemplate;
using Antlr4.Runtime.Misc;

namespace TestCallGraph
{
    /// <summary>
    /// A graph model of the output. 
    /// Tracks call from one function to another by mapping function to list of called functions.
    /// E.g., f -> [g, h]
    /// 
    /// Can dump DOT two ways (StringBuilder and ST). 
    /// 
    /// Sample output:
    /// 
    /// digraph G
    /// {
    ///       ... setup ...
    ///       f -> g;
    ///    g -> f;
    ///    g -> h;
    ///    h -> h;
    /// }
    /// </summary>
    public class Graph
    {
        /// <summary>
        /// list of functions
        /// </summary>
        public SortedSet<string> Nodes { get; set;} = new SortedSet<string>();
        /// <summary>
        /// caller->callee
        /// </summary>
        public MultiMap<string, string> Edges { get; set; } = new MultiMap<string, string>();
        
        public void Edge(String source, String target)
        {
            Edges.Map(source, target);
        }
        
        public override string ToString()
        {
            return "edges: " + Edges.ToString() + ", functions: " + Nodes;
        }
        
        public string ToDOT()
        {
            StringBuilder buf = new StringBuilder();
            buf.Append("digraph G {\n");
            buf.Append("  ranksep=.25;\n");
            buf.Append("  edge [arrowsize=.5]\n");
            buf.Append("  node [shape=circle, fontname=\"ArialNarrow\",\n");
            buf.Append("        fontsize=12, fixedsize=true, height=.45];\n");
            buf.Append("  ");
            foreach (string node in Nodes)
            { 
                // print all nodes first
                buf.Append(node);
                buf.Append("; ");
            }
            buf.Append("\n");
            foreach (string src in Edges.Keys)
            {
                foreach (string trg in Edges[src])
                {
                    buf.Append("  ");
                    buf.Append(src);
                    buf.Append(" -> ");
                    buf.Append(trg);
                    buf.Append(";\n");
                }
            }
            buf.Append("}\n");
            return buf.ToString();
        }

        /** Fill StringTemplate:
             digraph G {
               rankdir=LR;
               <edgePairs:{edge| <edge.a> -> <edge.b>;}; separator="\n">
               <childless:{f | <f>;}; separator="\n">
             }

		    Just as an example. Much cleaner than buf.append method
         */
        //public ST toST()
        //{
        //    ST st = new ST(
        //        "digraph G {\n" +
        //        "  ranksep=.25; \n" +
        //        "  edge [arrowsize=.5]\n" +
        //        "  node [shape=circle, fontname=\"ArialNarrow\",\n" +
        //        "        fontsize=12, fixedsize=true, height=.45];\n" +
        //        "  <funcs:{f | <f>; }>\n" +
        //        "  <edgePairs:{edge| <edge.a> -> <edge.b>;}; separator=\"\\n\">\n" +
        //        "}\n"
        //    );
        //    st.add("edgePairs", edges.getPairs());
        //    st.add("funcs", nodes);
        //    return st;
        //}
    }
}
