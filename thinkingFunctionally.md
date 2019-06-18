
# Mathematical functions

#### The impetus behind functional programming

* The set of values that can be used as input to the function is called the domain. In this case, it could be the set of real numbers, but to make life simpler for now, let’s restrict it to integers only.

* The set of possible output values from the function is called the range (technically, the image on the codomain). In this case, it is also the set of integers.

* The function is said to map the domain to the range.

​

## Key properties of mathematical functions

* A function always gives the same output value for a given input value.

* A function has no side effects.

* The input and output values are immutable.

* A function always has exactly one input and one output. (Solved by currying)

​

### The power of pure functions

* They are trivially parallelizable. I could take all the integers from 1 to 1000, say, and given 1000 different CPUs, I could get each CPU to execute the “add1” function for the corresponding integer at the same time, safe in the knowledge that there was no need for any interaction between them. No locks, mutexes, semaphores, etc., needed.

* I can use a function lazily, only evaluating it when I need the output. I can be sure that the answer will be the same whether I evaluate it now or later.

* I only ever need to evaluate a function once for a certain input, and I can then cache the result, because I know that the same input always gives the same output.

* If I have a number of pure functions, I can evaluate them in any order I like. Again, it can’t make any difference to the final result.

​

# Function Values and Simple Values

#### Binding not assignment

``` fs

let add1 x = x + 1

```

What does the “x” mean here? It means:

​

1. Accept some value from the input domain.

2. Use the name “x” to represent that value so that we can refer to it later.

​

This process of using a name to represent a value is called “binding”. The name “x” is “bound” to the input value.

``` fs

let add1 x = x + 1

add1 5

// replace "x" with "5"

// add1 5 = 5 + 1 = 6

// result is 6

```

Once bound, x cannot change either; once associated with a value, always associated with a value.

​

This concept is a critical part of thinking functionally: there are no “variables”, only values.

​

### Function values

The name “add1” itself is just a binding to “the function that adds one to its input”. The function itself is independent of the name it is bound to.

​

When you type ```fs let add1 x = x + 1``` you are telling the F# compiler “every time you see the name “add1”, replace it with the function that adds 1 to its input”. “add1” is called a function value.

``` fs

let add1 x = x + 1

let plus1 = add1

add1 5

plus1 5

```

You can always identify a function value because its signature has the standard form ```domain -> range```. Here is a generic function value signature:

``` fs

val functionName : domain -> range

```

### Simple values

Imagine an operation that always returned the integer 5 and didn’t have any input.

​

This would be a “constant” operation.

​

Written in f#

``` fs

let c = 5

```

When evaluated:

``` fs

val c : int = 5

```

We’ve just defined a constant, or in F# terms, a simple value.

``` fs

val aName: type = constant     // Note that there is no arrow

```

### Simple values vs. function values

There is very little difference between simple values and function values, functions are values that can be passed around as inputs to other functions

​

A function always has a domain and range and must be “applied” to an argument to get a result. A simple value does not need to be evaluated after being bound. Using the example above, if we wanted to define a “constant function” that returns five we would have to use:

``` fs

let c = fun()->5

// or

let c() = 5

```

The signature for these functions is:

``` fs

val c : unit -> int

```

instead of:

``` fs

val c : int = 5

```

### "Values" vs. "Objects"

A value, as we have seen above, is just a member of a domain. The domain of ints, the domain of strings, the domain of functions that map ints to strings, and so on. In principle, values are immutable. And values do not have any behavior attached them.

​

An object, in a standard definition, is an encapsulation of a data structure with its associated behavior (methods). In general, objects are expected to have state (that is, be mutable), and all operations that change the internal state must be provided by the object itself (via “dot” notation).

​

### Naming

Unlike C#, the naming convention for F# is that functions and values start with lowercase letters rather than uppercase (camelCase rather than PascalCase) unless designed for exposure to other .NET languages. Types and modules use uppercase however.

​

# How types work with functions

#### Understanding the type notation

First, we need to understand the type notation a bit more. We’ve seen that the arrow notation “->” is used to show the domain and range. So that a function signature always looks like:

``` fs

val functionName : domain -> range

```

ex:

``` fs

let intToString x = sprintf "x is %i" x  // format int to string

let stringToInt x = System.Int32.Parse(x)

```

the signatures:

``` fs

val intToString : int -> string

val stringToInt : string -> int

```

This means:

* intToString has a domain of ```int``` which it maps onto the range ```string```.

* stringToInt has a domain of ```string``` which it maps onto the range ```int```.

​

### Primitive types

The possible primitive types are what you would expect: string, int, float, bool, char, byte, etc., plus many more derived from the .NET type system.

​

More examples of functions using primitive types:

``` fs

let intToFloat x = float x // "float" fn. converts ints to floats

let intToBool x = (x = 2)  // true if x equals 2

let stringToString x = x + " world"

```

Their signatures are:

``` fs

val intToFloat : int -> float

val intToBool : int -> bool

val stringToString : string -> string

```

​

### Type annotations

In the previous examples, the F# compiler correctly determined the types of the parameters and results. But this is not always the case. If you try the following code, you will get a compiler error:

​

``` fs

let stringLength x = x.Length

   => error FS0072: Lookup on object of indeterminate type

```

The compiler does not know what type “x” is, and therefore does not know if “Length” is a valid method.

By giving the F# compiler a “type annotation”

``` fs

let stringLength (x:string) = x.Length

```

The parens around the x:string param are important. If they are missing, the compiler thinks that the return value is a string! That is, an “open” colon is used to indicate the type of the return value, as you can see in the example below.

``` fs

let stringLength (x:string) :int = x.Length

```

We’re indicating that the x param is a string and the return value is an int.

​

### Function types as parameters (higher order functions)

Consider a function ```evalWith5ThenAdd2```, which takes a function as a parameter, then evaluates the function with the value 5, and adds 2 to the result:

``` fs

  let evalWith5ThenAdd2 fn = fn 5 + 2

```

the signature

``` fs

  let evalWith5ThenAdd2 : (int -> int) -> int

```

the domain is ```(int -> int)``` and the range is ```int```. It means that the input parameter is not a simple value, but a function, and what’s more is restricted only to functions that map ```ints``` to ```ints```. The output is not a function, just an ```int```.

ex:

``` fs

  let add1 x = x + 1      // define a function of type (int -> int)

  evalWith5ThenAdd2 add1  // test it

```

gives:

``` fs

  val add1 : int -> int

  val it : int = 8

```

“add1” is a function that maps ints to ints, as we can see from its signature. So it is a valid parameter for the ```evalWith5ThenAdd2``` function. And the result is 8.

​

#### Functions as output

A function value can also be the output of a function. For example, the following function will generate an “adder” function that adds using the input value.

``` fs

let adderGenerator numberToAdd = (+) numberToAdd

```

signature:

``` fs

val adderGenerator : int -> (int -> int)

```

### The “unit” type

##### https://fsharpforfunandprofit.com/posts/how-types-work-with-functions/#the-unit-type

# Monads

It takes about fifteen minutes to learn the essential mathematical properties of a monad. What's hard is relating it to what you know from imperative programming.

Monads are not data structures or a particular function, they're a design pattern for expressing sequential computation in a purely functional way with constraints

on side effects.

​

The best way to grok them is to... er, use them in a language that has them.

​

- for expressing sequential computation

  - Or for expressing non-deterministic computation (List monad).

  - Or for expressing optional computation (Maybe monad).

  - Or for expressing error handling (Error monad).

  - And so on.

​

​

A monad is an object with of and chain functions. chain is like map except it un-nests the resulting nested object.

​

Implementation chain is also known as flatmap and bind in other languages.

``` js

Array.prototype.chain = function (f) {

  return this.reduce((acc, it) => acc.concat(f(it)), [])

}

```

Usage

``` js

Array.of('cat,dog', 'fish,bird').chain((a) => a.split(',')) // ['cat', 'dog', 'fish', 'bird']

```

Contrast to map

``` js

Array.of('cat,dog', 'fish,bird').map((a) => a.split(',')) // [['cat', 'dog'], ['fish', 'bird']]

```

of is also known as return in other functional languages. chain is also known as flatmap and bind in other languages.

​

## Blogs resources

https://sergeytihon.com/