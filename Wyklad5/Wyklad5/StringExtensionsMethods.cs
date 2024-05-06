namespace Lab3;

public static class StringExtensionsMethods
{
    public static string GetStudentGroupNr(this string groupNr) //this oznacza ze jesi metoda jest w klasie statycznej
        //oraz jeden z parametrow (moze byc max 1) ma slowo this przrd typem danych, to oznacza ze to jest 
        //metoda rozszerzen. Rozszerzamy string i definiujemy sobie metode.
        //Mechanizm rozszerzen pozwala nam 'dokleic' metode do dwoolnej klasy 
    {
        return groupNr.Substring(2, 3);
    }
}