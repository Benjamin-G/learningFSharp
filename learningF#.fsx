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

//Tuple
let twoTuple = 1,2
let threeTuple = "a",2,true

//Record types w/ named fields
type Person = {First:string; Last:string}
let person1 = {First="john"; Last="doe"}

//Union types
type Temp =
| DegreesF of float
| DegreesC of float
let temp = DegreesF 98.6

type Employee =
 | Worker of Person
 | Manager of Employee list

let jdoe = {First="john"; Last="doe"}
let worker = Worker jdoe

// Printing 
// The printf/printfn functions are similar to the
// Console.Write/WriteLine functions in C#.
printfn "Printing an int %i, a float %f, a bool %b" 1 2.0 true
printfn "A string %s, and something generic %A" "hello" [1;2;3;4]

// all complex types have pretty printing built in
printfn "twoTuple=%A,\nPerson=%A,\nTemp=%A,\nEmployee=%A" 
         twoTuple person1 temp worker

// There are also sprintf/sprintfn functions for formatting data into a string, similar to String.Format.


// extracting boilerplate 
// The action function always has two parameters: a running total (or state) and the list element to act on (called “x” in the above examples).
let product n = 
    let initialValue = 1
    let action productSoFar x = productSoFar * x
    [1..n] |> List.fold action initialValue

product 10

let sumOfOdds n = 
    let initialValue = 0
    let action sumSoFar x = if x%2=0 then sumSoFar else sumSoFar+x 
    [1..n] |> List.fold action initialValue

sumOfOdds 10

let alternatingSum n = 
    let initialValue = (true,0)
    let action (isNeg,sumSoFar) x = if isNeg then (false,sumSoFar-x)
                                             else (true ,sumSoFar+x)
    [1..n] |> List.fold action initialValue |> snd
//snd second value in a tuple
alternatingSum 100

type NameAndSize= {Name:string;Size:int}
 
let maxNameAndSize list = 
    
    let innerMaxNameAndSize initialValue rest = 
        let action maxSoFar x = if maxSoFar.Size < x.Size then x else maxSoFar
        rest |> List.fold action initialValue 

    // handle empty lists
    match list with
    | [] -> None
    | first::rest -> 
        let max = innerMaxNameAndSize first rest
        Some max


//test

let list = [
    {Name="Alice"; Size=10}
    {Name="Bob"; Size=1}
    {Name="Carol"; Size=12}
    {Name="David"; Size=5}
    ]    
maxNameAndSize list
maxNameAndSize []

// use the built in function
list |> List.maxBy (fun item -> item.Size)
// throws error [] |> List.maxBy (fun item -> item.Size)
let maxNameAndSize2 list = 
    match list with
    | [] -> 
        None
    | _ -> 
        let max = list |> List.maxBy (fun item -> item.Size)
        Some max

maxNameAndSize2 list
maxNameAndSize2 []