# Most Recently Used List (MRU) Kata

We are developing an IDE. One of it's feature is the ability to show a list of the twenty most recently opened files.
Implements a components with the core behaviour.

### Details
MRU is an ordered (LIFO) list data structure that holds unique elements (es: string)

### Test List
test pattern Zero/One/Many/Error

1. [x] track one file: f1 -> [f1]
2. [x] no files tracked:  -> []
3. [x] track many files: f1, f2, f3 -> [f3, f2, f1]
   
4. [x] track duplicated files: f1, f2, f3, f2 -> [f2, f3, f1]
5. [x] track over capacity files: f1, ..., f22, f23 -> [f23, f22, ..., f2]
6. [x] track null/empty

7. [ ] il getter RecentlyTracked e' veramente necessario? posso direattente "accedere/iterare" su mru?
