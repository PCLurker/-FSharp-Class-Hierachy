module ClassHierachy

open System

//type Inner(?A: int, ?B: int, ?Note: String) as this = 
type Inner internal (A: int, B: int, Note: String, b) = 

    // Execute when object created, after initialization
    do 
        printfn "Inner created."

    //let mutable a: int = 
    //    match A with
    //    | Some i -> i
    //    | None -> 0
    //member this.A
    //    with get() = a
    //    and set(value) = a <- value

    
    
        //Use let or let mutable if you want to define a local value that is visible only within the type (essentially a private field or a private function). Inside a module at top-level, these are publicly accessible and evaluated once. let mutable at module level creates a single writable field with no backing value.
    
        //You can use val to create an auto-property, it is short for member val Foo = .. with get. From F# this is seen as a field, but it's internally implemented as a get-property with a backing field to prevent mutation.
    
        //You can use val mutable to define a public field, but I wouldn't recommend this unless you actually need a public field (e.g. some .NET library may require types with this structure).
    
        //Using member x.Foo = ... is the best way to expose (read-only) state from a type. Most F# types are immutable, so this is perhaps the most common public member. It is short for a get-only instance property.
    
        //Using member x.Foo with get() = .. and set(value) ... is useful when you need to create a get/set property with your own custom code in the gettor and settor. This is sometimes useful when you're creating a mutable object.
    
        //Using member val Foo = ... with get, set is basically the same thing as auto-implemented properties in C#. This is useful if you need a mutable property with a getter and setter that just reads/writes a mutable backing field.
    
        //Using static let on a type creates a static (class-level) read-only field, which internally creates a property with a backing field. Use static mutable let ... for a read/write static field without a backing field.
    
        //Using static val mutable private creates a static read/write auto-property with a backing field, it cannot be public.
    

    // Properties with auto implemented get/set method, initialized with the primary constructor
    member val A: int = A with get, set
    member val B: int = B with get, set
    member val Note: String = Note with get, set
    
    new(C: Inner) = Inner(C.A, C.B, C.Note, false)
    new(?A: int, ?B: int, ?Note: String) = Inner(match A with | Some i -> i | None -> 0
                                                        , match B with | Some i -> i | None -> 0
                                                        , match Note with | Some i -> i | None -> ""
                                                        , false)
    member this.Clone(): Inner = new Inner(this)

type BaseClass internal (I: Inner, A: int, B: int, b) =
    do
        printfn "BaseClass created."
    member val I: Inner = I with get, set
    member val A: int = A with get, set
    member val B: int = B with get, set
    new(C: BaseClass) = BaseClass(C.I.Clone(), C.A, C.B, false)
    new(?I: Inner, ?A: int, ?B: int) = BaseClass(match I with | Some i -> i | None -> Inner()
                                             , match A with | Some i -> i | None -> 0
                                             , match B with | Some i -> i | None -> 0
                                             , false)
    abstract Clone: unit -> BaseClass
    default this.Clone(): BaseClass = BaseClass(this)

type DerivedClass internal (I: Inner, A: int, B: int, I2: Inner, C: int, D: int, b) =
    inherit BaseClass(I, A, B, false)
    do
        printfn "DerivedClass created."
    member val I2: Inner = I2 with get, set
    member val C: int = C with get, set
    member val D: int = D with get, set
    new(C: DerivedClass) = DerivedClass(C.I.Clone(), C.A, C.B, C.I2.Clone(), C.C, C.D, false)
    new(Base: BaseClass, ?I2: Inner, ?C: int, ?D: int) = DerivedClass(Base.I.Clone(), Base.A, Base.B, match I2 with | Some i -> i | None -> Inner()
                                                                                , match C with | Some i -> i | None -> 0
                                                                                , match D with | Some i -> i | None -> 0
                                                                                , false)
    new(?I: Inner, ?A: int, ?B: int, ?I2: Inner, ?C: int, ?D: int) = DerivedClass(match I with | Some i -> i | None -> Inner()
                                                                                        , match A with | Some i -> i | None -> 0
                                                                                        , match B with | Some i -> i | None -> 0
                                                                                        , match I2 with | Some i -> i | None -> Inner()
                                                                                        , match C with | Some i -> i | None -> 0
                                                                                        , match D with | Some i -> i | None -> 0
                                                                                        , false)
    override this.Clone(): BaseClass = DerivedClass(this) :> BaseClass
    //member this.TestFun = fun x -> x + 1
    //member this.TestFun2(x) = x + 1