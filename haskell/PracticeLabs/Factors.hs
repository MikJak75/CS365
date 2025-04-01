

import Text.Printf (printf)


factors :: Int -> [Int]
factors number = filter (\x -> (number `mod` x) == 0) [1 .. number]

printLine :: (Int, [Int]) -> IO()
printLine (index, fs) = printf "%5d: %s\n" index $ unwords $ map show fs


printAllLines :: [ (Int, [Int])] -> IO()
--printAllLines [] = pure ()
printAllLines [] = return ()
printAllLines (h:rest) = printLine h >> (printAllLines rest)

main = do
    line1 <- getLine
    line2 <- getLine

    let start = read line1
        end = read line2
        fields = zip [start .. end] $ map factors [start .. end]


    printAllLines fields
    --printOutput (start, factors 4)
    --map printOutput $ zip [start .. end] (map factors [start .. end])
    --print (map factors [start .. end])