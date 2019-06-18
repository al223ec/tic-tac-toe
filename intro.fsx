let a = 10;

let items = [1..5]
List.append items [6]
items

let prefix prefixStr baseStr =
    prefixStr + ", " + baseStr

prefix "Hello" "anton"

let names = ["David"; "Liv"; "Anton"]

let helloPrefix = prefix "hello"
let exclaim s =
    s + "!"

let bigHello = helloPrefix >> exclaim

names
|> Seq.map bigHello
|> Seq.sort