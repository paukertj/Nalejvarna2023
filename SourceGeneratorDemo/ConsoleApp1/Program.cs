// See https://aka.ms/new-console-template for more information
using AutoMapper;

Console.WriteLine("Hello, World!");

void Test(IMapper mapper)
{
    var a = new From();
    mapper.Map<To>(a);
}

record From
{
    public int MyIntPropertyFrom { get; init; }
}

record To
{
    public int MyIntPropertyTo { get; init; }
}