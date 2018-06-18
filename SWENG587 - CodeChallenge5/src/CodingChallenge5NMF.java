import java.io.*;
import java.nio.charset.StandardCharsets;
import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;

public class Main {

    // Written By Nathan Fox
    // For Coding Challenge 5 - SWENG 586
    // Penn State Grad School

    public static void main(String[] args) {
        //Initial Display
        System.out.println("Welcome to the Primes, Perfect Squares, and Perfect Number Tester");

        //Do the number testing
        NumberTester();

        //String Searching Function
        FileSearcher();

        //Ending output
        System.out.println("Exiting Program");
    }

    private static void NumberTester() {
        //Get file Name
        String filename = GetUserInput("Enter the name of the output:","[^\\\\|\\||\\?|\\/|\\:|\\<|\\*|\\>|\\\"|\\r]+", "Please enter a valid file name");
        if( filename == null)
            return;

        //Get a Starting Number
        Integer start = Integer.parseInt(GetUserInput("Enter a start number:", "[0-9]+", "Only numbers please!"));
        //Get a Stopping number
        Integer stop = Integer.parseInt(GetUserInput("Enter a stop number:", "[0-9]+", "Only numbers please!"));

        BufferedWriter out = null;
        try (Writer writer = new BufferedWriter(new OutputStreamWriter(new FileOutputStream(filename), StandardCharsets.UTF_8)))
        {
            //Runs this loop from starting number to stopping number
            for (; start <= stop; start++) {

                Number number = new Number(start);

                //Output the results
                String output = "The number " + start + " is: " + number.getTypeString();
                System.out.println(output);
                writer.write(output);
                ((BufferedWriter) writer).newLine();
            }
        } catch (FileNotFoundException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    private static void FileSearcher() {
        //Get file Name
        String filename = GetUserInput("Enter the name of the input file to search:","[^\\\\|\\||\\?|\\/|\\:|\\<|\\*|\\>|\\\"|\\r]+", "Please enter a valid file name");
        //Get search string
        String search = GetUserInput("Enter the string to search:", ".*", "Please enter a valid search string.");

        //open the file
        File file = new File(filename);
        try {
            Scanner fileScanner = new Scanner(file);

            while((fileScanner).hasNextLine())
            {
                String line = fileScanner.nextLine();
                if(line.toLowerCase().contains(search.toLowerCase()))
                    System.out.println(line);
            }
        } catch(FileNotFoundException e) {
            e.printStackTrace();
        }

        return;
    }

    //Outputs display, tries to parse the user input with regex, and if it doesn't displays error
    public static String GetUserInput(String display, String regex, String error)
    {
        //Initial display
        System.out.print(display);

        //Initialize a new scanner
        Scanner reader = new Scanner(System.in);
        //Get user input
        String Data = reader.nextLine();

        //parse if match return data
        if( Data.matches( regex))
            return Data;
        //else show error parameter
        else {
            System.out.println(error);
            return null;
        }
    }
    public static class Number {
        //Class Variables
        private Integer number;
        private NumberType type;
        private List<Integer> factors;
        private Integer sum;
        private Integer square;

        //Ctor
        public Number(Integer num) {
            number = num;
            square = 0;
            SetType();
        }

        //region Public Methods
        //Returns a string that is the type of the number
        public String getTypeString() {
            switch (type) {
                case Prime:
                    return "Prime";
                case Perfect:
                    return "Perfect";
                case PerfectSquare:
                    return "Perfect Square";
                case ImperfectAbundant:
                    return "Imperfect Abundant";
                case ImperfectDeficient:
                    return "Imperfect Deficient";
                case Unknown:
                    return "Unknown";
                default:
                    return "";
            }
        }

        //getter for number
        public Integer getNumber() {
            return number;
        }
        //setter for number


        //endregion

        //region Private Methods
        private void SetType() {
            //first set factors
            DetermineFactors();
            SetSum();

            if (factors.size() <= 1)
                type = NumberType.Prime;
            else if(square > 0 )
                type = NumberType.PerfectSquare;
            else if(sum == number)
                type = NumberType.Perfect;
            else if(sum < number )
                type = NumberType.ImperfectDeficient;
            else if( sum > number)
                type = NumberType.ImperfectAbundant;
            else
                type = NumberType.Unknown;
        }

        //Iterative method of find all the factors
        public void DetermineFactors()
        {
            factors = new ArrayList<Integer>();
            for (int i = 1; i < number; i++) {
                if(number % i == 0)
                    factors.add(i);
                if(i * i == number)
                    square = i;
            }
        }

        //Get the sum of the factors
        private void SetSum()
        {
            sum = 0;
            for (Integer x : factors) {
                sum += x;
            }
        }

        //endregion
    }

    public enum NumberType {
        Prime, Perfect, PerfectSquare, ImperfectAbundant, ImperfectDeficient, Unknown
    }
}
