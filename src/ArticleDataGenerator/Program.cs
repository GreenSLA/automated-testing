using System.Xml.Serialization;
using ArticleDataGenerator;

// Пример использования
if (args.Length < 4 || args[0] != "g" || args[3] != "xml")
{
    Console.WriteLine("Использование: ArticleDataGenerator g <количество> <имя_файла> xml");
    Console.WriteLine("Пример: ArticleDataGenerator g 5 articles.xml xml");
    return 1;
}

if (!int.TryParse(args[1], out var count) || count <= 0)
{
    Console.WriteLine("Ошибка: количество должно быть положительным целым числом.");
    return 1;
}

var fileName = args[2];

var articles = GenerateArticles(count);
SaveToXml(articles, fileName);

Console.WriteLine($"Сгенерировано {count} статей и сохранено в {fileName}");
return 0;

// Генерация заголовков
static List<ArticleData> GenerateArticles(int count)
{
    var random = new Random();
    var topics = new[] { "Selenium", "NUnit", "Testing", "Automation", "CI/CD", "Docker", "Angular", "API", "Performance", "Security" };
    var adjectives = new[] { "Complete", "Quick", "Advanced", "Practical", "Ultimate", "Modern", "Effective", "Essential" };
    var tags = new[] { "selenium", "nunit", "testing", "automation", "csharp", "dotnet", "webdriver", "ci" };

    var articles = new List<ArticleData>();

    for (var i = 0; i < count; i++)
    {
        var topic = topics[random.Next(topics.Length)];
        var adj = adjectives[random.Next(adjectives.Length)];
        var stamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() + i;

        articles.Add(new ArticleData($"{adj} Guide to {topic} #{stamp}")
        {
            Description = $"A {adj.ToLower()} introduction to {topic} for automated testing.",
            Body = $"This article covers {topic} in depth. You will learn everything you need to get started with {topic} quickly and effectively.",
            Tags = tags[random.Next(tags.Length)]
        });
    }

    return articles;
}

// Сериализация в XML
static void SaveToXml(List<ArticleData> articles, string fileName)
{
    var serializer = new XmlSerializer(typeof(List<ArticleData>));
    using var writer = new StreamWriter(fileName, append: false, System.Text.Encoding.UTF8);
    serializer.Serialize(writer, articles);
}
