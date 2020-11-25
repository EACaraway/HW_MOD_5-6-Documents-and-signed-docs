using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HW_MOD_5_6_1_Documents_and_Signed_Docs
{
    class Program
    {
        static void Main(string[] args)
        {
            Document doc1 = new Document();
            doc1.Title = "First Document";
            doc1.NumberOfPages = 5;
            doc1.Data = doc1.AddData("Some very important data.");
            doc1.Print();

            Console.WriteLine();

            SignedDocument signedDocument = new SignedDocument("SignedDoc1");
            signedDocument.NumberOfPages = 10;
            //signedDocument.Signature = "Evan Caraway";
            signedDocument.Data = signedDocument.AddData("Adding some data to my signed document.");
            signedDocument.Signature = "Evan Caraway";
            signedDocument.Print(); 
            
        }
    }

    // Create a class called Document with any properties/methods you think are appropriate. At a minimum include a property for the user to add text or data.
    public class Document
    {
        public string Title { get; set; }
        public int NumberOfPages { get; set; }
        public ArrayList Data { get; set; }

        public virtual ArrayList AddData(string data) //virtual allows this method to be inherited in another class
        {
            ArrayList listData = new ArrayList();

            listData.Add (data);
            return listData;
        }
        public void Print()
        {
            Console.WriteLine($"Title: {Title} \nNumber of Pages: {NumberOfPages}");
            Console.Write("Data: ");
            foreach (var item in Data)
            {
                Console.WriteLine(item);
            }
        }

        //public Document(string title) // why cant I have this constructor in both clases?
        //{
        //    Title = title;
        //}
    }


    // Create a second class called SignedDocument. This class will have a property called signature, which can only be written to once. The text/data of the signed document can only be modified if it is not yet signed.
    public class SignedDocument : Document
    {
        private readonly SetOnce<string> signature = new SetOnce<string>();

        public string Signature
        {
            get { return signature; }
            set { signature.Value = value; }
        }

        //public string Signature { get; init; }

        public override ArrayList AddData(string data)
        {
            ArrayList listData = new ArrayList();

            if (Signature == null)
            {
                listData.Add(data);
                return listData;
            }
            else
            {
                throw new ArgumentException("The text/data of the signed document can only be modified if it is not yet signed.");
            }

        }
        //constructor to initialize data
        public SignedDocument(string title)
        {
            Title = title;

        }
    }
    
    // half understand this code that I copied when I searched for how to only set once.
    public class SetOnce<T>
    {
        private bool set;
        private T value;

        public T Value
        {
            get { return value; }
            set
            {
                if (set) throw new AlreadySetException(value);
                set = true;
                this.value = value;
            }
        }

        public static implicit operator T(SetOnce<T> toConvert)
        {
            return toConvert.value;
        }
    }

    // bunch of code that was an auto fix from the red squigglies under AlreadySetException.
    [Serializable]
    internal class AlreadySetException : Exception
    {
        private object value;

        public AlreadySetException()
        {
        }

        public AlreadySetException(object value)
        {
            this.value = value;
        }

        public AlreadySetException(string message) : base(message)
        {
        }

        public AlreadySetException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AlreadySetException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
