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

        let bytes_as_int = u32::from_ne_bytes(
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
    println!("Length of vec {}", cv_vec.len());

    let mut f2 = File::create(&a[2]).expect("can't create file");
    for val in val_vec {
        //writeln!(f2, "{}", val).expect("error writing");
        writeln!(f2, "{}", val).unwrap();
    }


    //println!("Hello, world!");
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
            CompValue::Dpd(x) => 2
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

pub fn decode_bcd(x: u32) -> u32 {
    //x.to_be_bytes().iter().zip([1000000 ,10000, 100, 1].iter())
    x.to_be_bytes().iter().zip([1, 100, 10_000, 1_000_000].iter())
    .map(|(&bits, &multiplier)| ((bits & 0xf) as u32)*multiplier + ((bits >> 4) as u32)*multiplier*10)
    .fold(0, |acc, x| acc + x )
}