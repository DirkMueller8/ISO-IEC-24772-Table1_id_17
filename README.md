## Good Coding Principles acc. to ISO/IEC 24772-1:2024

### Standard ### 
**ISO/IEC 24772-1**  
Programming languages — Avoiding vulnerabilities in programming languages —
Part 1: Language-independent catalogue of vulnerabilities  

### Good Coding Recommendation  
"*Beware of short-circuiting behaviour when expressions with side effects are used on the right side of a short-circuited Boolean expression, since a left-hand expression evaluating to false, dictates that the right-hand expression, including function calls with side effects, will not be evaluated.*", in Table 1, Number 17, on p. 29   

### Theory around the Example  
#### Short-circuit AND (&&)  
With `&&`, the runtime first evaluates the left operand `a` in an expression `a && b`.  

#### Short-circuit AND (&)  
With `&`, the runtime evaluates the left and right operands in an expression `a && b`.  

### Explanation of Problematic Code  
- Short-circuit operators `&&` and `||` do not evaluate their right-hand side when the left-hand side already determines the result. If the RHS contains functions with side effects (like `SideEffectIncrement`), those side effects will not occur  
- The non-short-circuit boolean operators `&` and `|` (when used with bool) evaluate both sides and therefore execute RHS side effects  
- If a side effect must always run, either call the function before the boolean expression (as shown in the "Safe alternative") or avoid putting side-effectful calls on the RHS of a short-circuited expression.  

#### Method Producing the Side Effect
```
static bool SideEffectIncrement(string name)
{
    Console.WriteLine($"Invoking RHS function: {name}");
    counter++;
    return true;
}
```  
where the variable counter is defined as static.  
#### Expected sample output  
- Example 1 (`&&`): no "Invoking RHS..." line; counter remains 0.  
- Example 2 (`&`): shows "Invoking RHS..." and counter becomes 1.  
- Example 3 (`||`): no RHS invocation.  
- Example 4 (`|`): RHS invoked and counter becomes 1.  

##### Safe alternative:  
Forced RHS invoked before combining.  