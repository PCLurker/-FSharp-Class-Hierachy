// Learn more about F# at http://fsharp.org

//open System
open ClassHierachy

let fib x =
    let rec loop x r1 r2 =
        match x with
        | 0 -> r2
        | 1 -> r1
        | _ -> loop (x - 1) (r1 + r2) r1
    loop x 1 0

let rec SumN (x:int) (r:int) : int =
    match x with 
    | 0 -> r
    | _ -> SumN (x-1) (r+x)

let rec SumR x =
    match x with
    | 0 -> 0
    | _ -> x + SumR (x-1)

let rec S(x, y, z)=
    match x, y, z with
    | 0, _, z -> z
    | _, 0, z -> z-1
    | _, _, _ -> S(x-1, y-1, z+x+y)

let rec ListD(x, l) =
    if l = [] then []
    elif l.Head = x then l.Tail
    else l.Head::ListD(x, l.Tail)

let rec T1(x, y) = if x = y then true else false

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"
    printfn "Fib %d"  (fib 40)
    //printfn "%d" (S(10))
    let I1 = Inner(B = 16)
    printfn "%d" I1.B
    let B1 = BaseClass(Inner(12, 16, "Inner 1"), 20, 24)
    let B2 = BaseClass(I1.Clone(), 90, 12)
    let D1 = DerivedClass(B2, C = 0, D = 10)
    0 // return an integer exit code
