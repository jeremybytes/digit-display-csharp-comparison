# digit-display-csharp
Naive hand-written digit recognition with display applications to show image, prediction, and errors.  

This is a C# version of a project originally written in .NET (F# & C#). Details of that project are available here: [https://github.com/jeremybytes/digit-display](https://github.com/jeremybytes/digit-display).  

This is primarily used to compare performance and other environment features with similar projects in Go (golang) and Rust (rust-lang).

**Functions**  
* Reading files from the file system
* Training simple nearest-neighbor digit recognizers
    * Manhattan distance
    * Euclidean distance
* Output (pretty bad) ASCII art
* Multi-threading
* Channels
* Chunking / threading
* Parsing command-line parameters

**Usage**
```
PS C:\...> .\digits.exe --help
digits 1.0.0
Copyright (C) 2022 digits

  -o, --offset     (Default: 1000) Offset in the data set (default: 1000)
  -c, --count      (Default: 100) Number of records to process (default: 100)
  --classifier     (Default: euclidean) Classifier to use (default: 'euclidean')
  -t, --threads    (Default: 6) Number of threads to use (default: 6)
  --help           Display this help screen.
  --version        Display version information.
```

**Notes**  
This project was originally the slowest of the 3 (see [https://github.com/jeremybytes/digit-display-language-comparison](https://github.com/jeremybytes/digit-display-language-comparison) for comparison).  

Based on community input, the following performance updates were made (details will be written up in articles soon):  

* List&lt;int&gt; changed to int[]
* String concatenation changed from string+= to using StringBuilder
* Abstract override method was moved out of a tight loop to a higher level. This results in duplicated code but a very large change in performance.

These resulted in significant performance improvements. The first 2 changes resulted in run times that are **30% faster** (from 20 seconds to 14 seconds for a specific run). All 3 changes together resulted in run times that are **75% faster** (from 20 seconds to 5 seconds).

**Branches**  
The original and updated code are available in separate branches for comparison purposes.  

* *original* - includes the orginal code
* *perftest1* - contains the array and string builder changes  
* *perftest2* - contains the abstract method changes

Specifics on these changes will be detailed soon.