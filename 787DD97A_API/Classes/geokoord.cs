using System.Net.Http.Headers;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
public class georooords
{
    private static readonly HttpClient client = new HttpClient();

    static double shirota = 0;
    static double dolgota = 0;
    public static double rasst = 0;
    static private async Task ProcessRepositories(string addres) //returned json file
    {
        string n = "";
        for (int i = 0; i < addres.Length; i++)
        {
            if (addres[i] == ' ')
            {
                n += '+';
            }
            else
                n += addres[i];

        }

        client.DefaultRequestHeaders.Accept.Clear();

        var stringTask = client.GetStringAsync("https://geocode-maps.yandex.ru/1.x/?apikey=d3cee173-e72b-4354-85e3-130296ab306b&format=json&geocode=" + n);

        var msg = await stringTask;
        int j = 0; //позиция начала цифр
        int counter = 0;
        string first = "";
        string second = "";
        for (int i = 0; i < msg.Length; i++)//ищем 
        {
            Console.WriteLine(msg[i]);
            if (msg[i] == 'p' && msg[i + 1] == 'o' && msg[i + 2] == 's' && msg[i + 3] != 't')
            {
                Console.WriteLine(msg[i]);
                j = i + 6;
                break;
            }
        }
        int buff = j + 9;
        for (; j < buff; j++)//парсим первое число
        {
            if (msg[j] == '.')
                first += ',';
            else
                first += msg[j];
        }
        j++;//пропускаем пробел
        int buff2 = j + 9;
        for (; j < buff2; j++)//парсим второе число
        {
            if (msg[j] == '.')
                second += ',';
            else
                second += msg[j];
        }
        shirota = Convert.ToDouble(first);
        dolgota = Convert.ToDouble(second);
    }


    public static async Task Calc_Distance(string FirstAddres,string SecondAddres)
    {
        await ProcessRepositories(FirstAddres);
        double localshirota = shirota;
        double localdolgota = dolgota;
        await ProcessRepositories(SecondAddres);
        rasst = Math.Sqrt(Math.Pow(localshirota - shirota, 2) + Math.Pow(localdolgota - dolgota, 2));
    }
}
