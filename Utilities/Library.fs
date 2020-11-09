namespace Utilities

module Sorts =
 
 let rec QuickSort xs =
  match xs with
  | [] -> []
  | x :: xs ->
      let smaller = QuickSort (xs |> List.filter(fun e -> e <= x))
      let larger  = QuickSort (xs |> List.filter(fun e -> e >  x))
      smaller @ [x] @ larger