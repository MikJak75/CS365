--vowels :: [String]
--vowels =  ["a", "e", "i", "o", "u"]

--firstVowelIndex :: String -> Maybe(Int)
--firstVowelIndex word = findIndex (\x -> elem x vowels) word

--firstIsVowel :: String -> Bool
--firstIsVowel word =
    --let vowels = ["a", "e", "i", "o", "u"]
    --in elem (take 1 word) vowels

--convertCons :: String -> String
--convertCons word = do
    --first_vowel <- word
    --if first_vowel == Nothing then word ++ "yay"
    --else word ++ (read firstVowel) ++ "yay'"


--convertWord :: String -> String
--convertWord word = 
    --if firstIsVowel word then word ++ "yay"
    --else convertCons

--main = do
    --line <- getLine
    --putStrLn $ unwords $ map convertWord (words line)

import Data.List (findIndex)

vowels :: String
vowels = "aeiou"

firstVowelIndex :: String -> Maybe(Int)
firstVowelIndex word = findIndex (\x -> elem x vowels) word

convertWord :: String -> Maybe(Int) -> String
convertWord word Nothing = word ++ "yay"
convertWord word (Just index)
    | index == 0 = word ++ "yay"
    | otherwise = first ++ second ++ "ay" where
        second = take index word
        first = drop index word


main = do
    line <- getLine
    --print $ map firstVowelIndex (words line)
    putStrLn $ unwords $ map (\x -> convertWord x (firstVowelIndex x)) (words line)