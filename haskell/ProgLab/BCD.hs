import System.Environment (getArgs, getProgName)
--import Data.Typeable (typeOf)
--import qualified Data.ByteString as BS
import qualified Data.ByteString.Lazy as BSL
import Data.Binary 
--import GHC.Exts.Heap (GenClosure(bytes))
import Data.Bits

main :: IO ()
main = do
    prog_name <- getProgName
    args <- getArgs
    --print $ (show args) ++ " " ++ prog_name
    --print $ typeOf args

    case args of 

        [fname] -> do
            bytes <- BSL.readFile fname
            --print $ readBCD bytes

            foldl (\acc x -> acc >> print x) (return ()) $ readBCD bytes


        _ -> putStrLn $ "Usage: " ++ prog_name ++ " <binary file>"


readBCD :: BSL.ByteString -> [Word32]
--readBCD :: BSL.ByteString -> [Word8]
readBCD bytes
    | BSL.null bytes = [] 
    -- | otherwise = (BSL.unpack $ BSL.take 4 bytes) ++ (readBCD $ BSL.drop 4 bytes)
    | otherwise = [( decodeBCD $ BSL.unpack $ BSL.take 4 bytes)] ++ (readBCD $ BSL.drop 4 bytes)

decodeBCD :: [Word8] -> Word32
decodeBCD bytes = do


    --decodeByte $ convertedBytes!!1
    --decodeByte (convertedBytes !! 0) 1
    --convertedBytes !! 0
    --decodeByte convertedBytes 3 
    --(fromIntegral :: Word8 -> Word32) (bytes !! 3)
    (decodeByte (convertedBytes !! 3) 1) + (decodeByte (convertedBytes !! 2) 100) + (decodeByte (convertedBytes !! 1) 10000) + (decodeByte (convertedBytes !! 0) 1000000)    
    where
        convertedBytes = map (fromIntegral :: Word8 -> Word32) bytes
        --decodeByte byte power = byte !! 0
        decodeByte byte power = ((byte .&. 15) * power) + ( ((shiftR byte 4) .&. 15 ) * power * 10)


    