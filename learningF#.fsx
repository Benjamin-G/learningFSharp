// Let defines an (immutable) value
let myInt = 5
let myFloat = 3.14
let myString = "hello"

// Lists (Commas are never used as delimiters only semicolons)
let twoToFive = [2;3;4;5]

let oneToFive = 1 :: twoToFive // :: creates list with new 1st element

let zeroToFive = [0;1] @ twoToFive // @ concats two lists  


//Functions
let square x = x * x
square 2

let add x y = x + y
let addOne y = add 1 y
addOne 1 //curry like

//Multiline function indent no tab
let evens list =
 let isEven x = x%2 = 0
 List.filter isEven list

evens zeroToFive

// You can use parens to clarify precedence. In this example,
// do "map" first, with two args, then do "sum" on the result.
// Without the parens, "List.map" would be passed as an arg to List.sum
let sumOfSquaresTo100 =
 List.sum ( List.map square [1..100] )

// sum of squares using pipes
let sumOfSquaresTo100piped = 
 [1..100] |> List.map square |> List.sum

// sum of squares using pipe and Lambdas (anon functions)
let sumOfSquaresTo100withFun = 
 [1..100] |> List.map (fun x->x*x) |> List.sum

// Returns are implicit -- no "return" needed


// Pattern Matching
// Match..with.. is a "supercharged" case/switch statement
let simplePatternMatch x =
 //let x = "a"
 match x with 
 | "a" -> printfn "x is a"
 | "b" -> printfn "x is b"
 | _ -> printfn "x is something else"  // underscore matches anything

simplePatternMatch "a" 

// Some(..) and None are roughly Nullable wrappers
let validVal = Some(99)
let invalidVal = None

let optionPatternMatch input =
 match input with
 | Some i -> printfn "input is an int=%d" i
 | None -> printfn "input is missing"

 
optionPatternMatch validVal
optionPatternMatch invalidVal


//Complex data types
