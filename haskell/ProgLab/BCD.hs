import System.Environment (getArgs, getProgName)
import Data.Typeable (typeOf)

main :: IO ()
main = do
    prog_name <- getProgName
    args <- getArgs
    --print $ (show args) ++ " " ++ prog_name
    --print $ typeOf args

    case args of 

        [x] -> print x
        _ -> putStrLn $ "Usage: " ++ prog_name ++ " <binary file>"


--readBCD :: ByteString -> [Word32]
--readBCD = 

--decodeBCD :: [Word8] -> Word32
--decodBCD = 