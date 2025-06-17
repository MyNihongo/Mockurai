namespace MyNihongo.Mock.Sample;

public readonly struct StructReturn
{
	public readonly int Age;
	public readonly string Name;
	public readonly DateOnly DateOfBirth;

	public StructReturn(in int age, in string name, in DateOnly dateOfBirth)
	{
		Age = age;
		Name = name;
		DateOfBirth = dateOfBirth;
	}
}
