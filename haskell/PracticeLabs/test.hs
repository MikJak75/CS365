import Data.Char (toLower, toUpper, isLower, isUpper)

convertChar :: Char -> Char
convertChar c 
    | isLower c = toUpper c
    | otherwise = toLower c

processWord :: String -> String
processWord w = map convertChar w

processLine :: [String] -> [String]
processLine l = map processWord l


processFile :: [[String]] -> [[String]]
processFile f = map processLine f

printLine :: String -> IO()
printLine s 
    | s == "" = pure ()
    | otherwise = print s

printAllLines :: [String] -> IO()
printAllLines [] = pure ()
printALLLines (h:rest) = printLine h >> (printALLLines rest)

main :: IO ()
main = do 
    l <- getLine
    file <- readFile l

    let convertedLines = (map unwords (processFile (map words (lines file)))) 
    --print convertedLines (map unwords (processFile (map words (lines file)))) in

    
    print convertedLines

    --putStrLn $ unwords $ processFile $ lines $ file
