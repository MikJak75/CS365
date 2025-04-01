import Text.Printf (printf)

calcPercentage :: Double -> Double -> Double
calcPercentage salary taxes 
    |salary == 0 = 0 
    | otherwise = taxes/salary * 100

taxRates :: [(Double, Double)]
-- Write tax rates here
-- The left value is the income floor
-- The right value is the tax rate for that income floor
taxRates = [(0, 0.2), (1000, 0.25), (5000, 0.2871), (10000, 0.3129), (15000, 0.3815)]

amountInBracket :: Double -> (Double, Double) -> Double
amountInBracket salary (floor, rate) 
    | salary - floor <= 0 = 0
    | otherwise = salary - floor


calculateTaxes :: Double -> [(Double,Double)] -> Double
calculateTaxes salary [] = 0
calculateTaxes salary (h:rest) = 
    let amount = (amountInBracket salary h) 
        (_, rate) = h in
    amount * rate + (calculateTaxes (salary - amount) rest)
--calculateTaxes remainingSalary [(floor, rate)] = (amountInBracket remainingSalary (floor, rate)) * rate
-- Write calculateTaxes here
-- The first parameter is the dollar amount.
-- The second parameter is a list of tuples containing the tax rates.
--calculateTaxes dollars rates = 0.0


main :: IO ()
main = do
    input <- getLine
    --print $ ( read input) * 1.5
    --let salary = 3712.22
    let salary = read input
        taxes = calculateTaxes salary (reverse taxRates)
        percentage = calcPercentage salary taxes
    printf "$%.2f\n" $ taxes
    printf "%.0f%%\n" $ percentage

-- You will need to update main to format in accordance with the writeup.
