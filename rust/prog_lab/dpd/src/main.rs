use std::env::args;
use std::io::prelude::*;
use std::fs::File;
use std::cmp::Ord;
use std::cmp::Ordering;

fn main() {
    let a: Vec<String> = args().collect();
    if a.len() != 3 {
        println!("Usage: {} <input> <output>", &a[0]);
        return;
    }

    let mut f1 = File::open(&a[1]).expect("can't open file");

    let mut cv_vec: Vec<CompValue> = Vec::new();

    //let a = CompValue::Dpd(4);
    //val_vec.push(a);

    let mut buf: [u8; 5]= [0; 5];

    while let Ok(()) = f1.read_exact(&mut buf){
        let first_byte = buf[0];

        let bytes_as_int = u32::from_be_bytes(
            //(buf[1..]).try_into().unwrap()
            (buf[1..]).try_into().unwrap()
        );

        let e = match  first_byte {
            0 => CompValue::Bcd(bytes_as_int),
            1 => CompValue::Dpd(bytes_as_int),
            _ => {
                println!("Error: Not a valid first byte {}", first_byte);
                return;
            }
        };

        cv_vec.push(e);

        //println!("First bit: {}", &buf[0]);
    }

    cv_vec.sort();

    let val_vec =  cv_vec.iter().map(|x| x.decode()).collect::<Vec<u32>>();

    //println!("{:?}", cv_vec.iter()
    //.map(|x| x.decode()).collect::<Vec<u32>>());
    //println!("Length of vec {}", cv_vec.len());

    let mut f2 = File::create(&a[2]).expect("can't create file");
    for val in val_vec {
        writeln!(f2, "{}", val).expect("error writing");
        //writeln!(f2, "{}", val).unwrap();
    }



    //println!("here1");
    //let val = decode_dpd(0x0a395bcf);

    //println!("here");
    //println!("Decoded val is {}", val)
    //println!("Decode val:  {}", val_vec[0].decode());
}

#[derive(Debug)]
pub enum CompValue {
    Bcd(u32),
    Dpd(u32)
}

impl CompValue {
    pub fn decode(&self) -> u32 {
       match self {
            CompValue::Bcd(x) => decode_bcd(*x),
            //CompValue::Dpd(x) => 2
            CompValue::Dpd(x) => decode_dpd(*x)
       } 
    }
}

impl Ord for CompValue {
  fn cmp(&self, other: &Self) -> Ordering{
    //match (self, other) {
        //(CompValue::Bcd(x), CompValue::Bcd(y)) => decode_bcd(*x).cmp(&decode_bcd(*y)),
        //(CompValue::Dpd(x), CompValue::Dpd(y)) => x.cmp(y),
        //(CompValue::Bcd(_), CompValue::Dpd(_)) => Ordering::Equal,
        //(CompValue::Dpd(_), CompValue::Bcd(_)) => Ordering::Equal,
    //}
    self.decode().cmp(&other.decode())
  }
}

impl PartialOrd for CompValue {
    fn partial_cmp(&self, other: &Self) -> Option<Ordering> {
        Some(self.cmp(other))
    }
}

impl PartialEq for CompValue {
    fn eq(&self, other: &Self) -> bool {
       self.decode() == other.decode()
    }
}

impl Eq for CompValue {}

    //x.to_be_bytes().iter().zip([1000000 ,10000, 100, 1].iter())
pub fn decode_bcd(x: u32) -> u32 {
    x.to_be_bytes().iter().zip([1000000 ,10000, 100, 1].iter())
    //x.to_be_bytes().iter().zip([1, 100, 10_000, 1_000_000].iter())
    .map(|(&bits, &multiplier)| ((bits & 0xf) as u32)*multiplier + ((bits >> 4) as u32)*multiplier*10)
    .fold(0, |acc, x| acc + x )
}

pub fn decode_dpd(x: u32) -> u32{
    let mut nibbles: [u32; 3] = [0; 3];

    nibbles[0] = x & 0b11_1111_1111;
    nibbles[1] = (x >> 10) & 0b11_1111_1111;
    nibbles[2] = (x >> 20) & 0b11_1111_1111;

    //println!("{x:#b}", nibbles);
    
    for val in nibbles {
        //println!("{:#b}", val);
        decode_nibble(val);
    }

    
    
    let val = decode_nibble(nibbles[0]) + decode_nibble(nibbles[1]) * 1_000 + decode_nibble(nibbles[2]) * 1_000_000;
    return val
}

pub fn decode_nibble(nibble: u32) -> u32{
    let r = [9, 8, 7, 6, 5, 4, 3, 2, 1, 0];
    let bits = r.iter()
    .map(|&x| get_bit_at(nibble, x))
    .collect::<Vec<u32>>();

    let decoded_val:u32;

    if nibble == 0{
        return 0;
    }

    if bits[6] == 0{
        //row 1 logic
        let a = bits[0];
        let b = bits[1];
        let c = bits[2];
        let d = bits[3];
        let e = bits[4];
        let f = bits[5];
        let g = bits[7];
        let h = bits[8];
        let i = bits[9];

        let dig1 = (a << 2) + (b << 1) + c;
        let dig2 = (d << 2) + (e << 1) + f;
        let dig3 = (g << 2) + (h << 1) + i;

        decoded_val = dig1 * 100 + dig2 * 10 + dig3;

    } else if  bits[3] == 1 &&  bits[4] == 0 && bits[6] == 1 && bits[7] == 1 && bits[8] ==1{
        //row 7 logic
        let a = bits[0];
        let b = bits[1];
        let c = bits[2];
        let f = bits[5];
        let i = bits[9];

        let dig1 = (a << 2) + (b << 1) + c;
        let dig2 = 0b1000 + f;
        let dig3 = 0b1000 + i;

        decoded_val = dig1 * 100 + dig2 * 10 + dig3;
    } 
    //row 2 logic
    else if bits[6] == 1 && bits[7] == 0 && bits[8] == 0
    {
        let a = bits[0];
        let b = bits[1];
        let c = bits[2];
        let d = bits[3];
        let e = bits[4];
        let f = bits[5];
        let i = bits[9];

        let dig1 = (a << 2) + (b << 1) + c;
        let dig2 = (d << 2) + (e << 1) + f;
        let dig3 = 0b1000 + i;

        decoded_val = dig1 * 100 + dig2 * 10 + dig3;
    }

    //row 3 logic
    else if bits[6] == 1 && bits[7] == 0 && bits[8] == 1
    {
        let a = bits[0];
        let b = bits[1];
        let c = bits[2];
        let g = bits[3];
        let h = bits[4];
        let f = bits[5];
        let i = bits[9];

        let dig1 = (a << 2) + (b << 1) + c;
        let dig2 = 0b1000 + f;
        let dig3 = (g << 2) + (h << 1) + i;

        decoded_val = dig1 * 100 + dig2 * 10 + dig3;
    }
    
    // row 4 logic
    else if bits[6] == 1 && bits[7] == 1 && bits[8] == 0
    {
        let g = bits[0];
        let h = bits[1];
        let c = bits[2];
        let d = bits[3];
        let e = bits[4];
        let f = bits[5];
        let i = bits[9];

        let dig1 = 0b1000 + c;
        let dig2 = (d << 2) + (e << 1) + f;
        let dig3 = (g << 2) + (h << 1) + i;

        decoded_val = dig1 * 100 + dig2 * 10 + dig3;
    }


    //row 5 logic
    else if  bits[3] == 0 &&  bits[4] == 0 && bits[6] == 1 && bits[7] == 1 && bits[8] ==1
    {
        let g = bits[0];
        let h = bits[1];
        let c = bits[2];
        let f = bits[5];
        let i = bits[9];

        let dig1 = 0b1000 + c;
        let dig2 = 0b1000 + f;
        let dig3 = (g << 2) + (h << 1) + i;

        decoded_val = dig1 * 100 + dig2 * 10 + dig3;
    }


    //row 6 logic
    else if  bits[3] == 0 &&  bits[4] == 1 && bits[6] == 1 && bits[7] == 1 && bits[8] ==1
    {
        let d = bits[0];
        let e = bits[1];
        let c = bits[2];
        let f = bits[5];
        let i = bits[9];

        let dig1 = 0b1000 + c;
        let dig2 = (d << 2) + (e << 1) + f;
        let dig3 = 0b1000 + i;

        decoded_val = dig1 * 100 + dig2 * 10 + dig3;
    }

    //row 8 logic
    else if  bits[3] == 1 &&  bits[4] == 1 && bits[6] == 1 && bits[7] == 1 && bits[8] ==1
    {
        let c = bits[2];
        let f = bits[5];
        let i = bits[9];

        let dig1 = 0b1000 + c;
        let dig2 = 0b1000 + f;
        let dig3 = 0b1000 + i;

        decoded_val = dig1 * 100 + dig2 * 10 + dig3;
    }

    
    // base case should never reach here
    else {
        println!("not a valid decoding pattern");
        return 0;
    }

    return decoded_val;
}

// helper function for decode_bcd)
// looking back on it now probably a little unneccsary but probably
// no one reads this
// comment like and subscribe if you got this far into the code
pub fn get_bit_at(input: u32, n: u32) -> u32{
    (input >> n) & 1
}
