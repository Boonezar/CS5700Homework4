# CS5700Homework4

To compile: `csc /out:Program.exe AppLayer\GamePieces\*.cs AppLayer\IO\*.cs AppLayer\SolvingAlgorithms\*.cs ".\CS 5700 Homework 4\Program.cs"`
To run: `.\Program <inputFile> <outputFile>`
Sample run: `.\Program .\SamplePuzzles\Input\Puzzle-4x4-0001.txt`

# Assignment 5 Write-UP

So I picked to refactor assignment 4 for a few reasons:
- My original implementation used the Strategy Pattern instead of the Template Pattern
- All my other projects correctly implemented the main pattern
- I didn't finish implementing the Timer function
- I realized I had a couple of bugs while doing the code review with Shimin

Things that I changed in my refactor:
- Fixed my design so that it properly uses the Template Method
- Finished Timer implementation
- Fixed bugs to correct bugs that occurred when writing results to file / console
- Fixed a couple of the Unit Tests that weren't working correctly
- Cleaned up the code a little bit.
