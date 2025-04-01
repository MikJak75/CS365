import System.Environment (getArgs, getProgName)
import qualified Data.ByteString.Lazy as BSL
import Data.Binary 
import Data.Bits

main :: IO ()
main = do
    prog_name <- getProgName
    args <- getArgs

    case args of 

        [fname] -> do
            bytes <- BSL.readFile fname

            foldl (\acc x -> acc >> print x) (return ()) $ readBCD bytes


        _ -> putStrLn $ "Usage: " ++ prog_name ++ " <binary file>"


readBCD :: BSL.ByteString -> [Word32]
readBCD bytes
    | BSL.null bytes = [] 
    | otherwise = [( decodeBCD $ BSL.unpack $ BSL.take 4 bytes)] ++ (readBCD $ BSL.drop 4 bytes)

decodeBCD :: [Word8] -> Word32
decodeBCD bytes = do

    (decodeByte (convertedBytes !! 3) 1) + (decodeByte (convertedBytes !! 2) 100) + (decodeByte (convertedBytes !! 1) 10000) + (decodeByte (convertedBytes !! 0) 1000000)    
    where
        convertedBytes = map (fromIntegral :: Word8 -> Word32) bytes
        decodeByte byte power = ((byte .&. 15) * power) + ( ((shiftR byte 4) .&. 15 ) * power * 10)


    
