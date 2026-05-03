[![Version](https://img.shields.io/nuget/v/Mockurai?style=plastic)](https://www.nuget.org/packages/Mockurai/)
[![Nuget downloads](https://img.shields.io/nuget/dt/Mockurai?label=nuget%20downloads&logo=nuget&style=plastic)](https://www.nuget.org/packages/Mockurai/)  
# Mockurai

**A source-generator-based mocking library for .NET.**

Mockurai writes the mock for you. Decorate your test base with `[MockuraiGenerate]`, declare partial `IMock<T>` properties for the dependencies you need, and let the generator produce strongly-typed `Setup*` and `Verify*` methods at compile time — no reflection, no proxies, no runtime IL.

## 💎 Why Mockurai

- **Type-safe setup & verify.** Every mocked member gets a dedicated method (`SetupGet`, `SetupReturnWithParameter`, `VerifyInvoke`, …). Refactor a method signature and the compiler points you at every test that needs updating.
- **Optional setup parameters.** Omit parameters you don’t care about (don't use `It<T>.Any()`) and focus only on those that matter.
- **Verification in sequence.** Assert that calls happened in the expected order across one or many mocks with `VerifyInSequence`.
- **Auto-generated `VerifyNoOtherCalls`.** A single call asserts every recorded invocation across every mock has been verified — no manual bookkeeping.
- **Generics & ref-style parameters.** First-class support for generic interfaces, `in`, `out`, `ref`, and `ref readonly` parameters via dedicated matchers (`ItIn<T>`, `ItOut<T>`, `ItRef<T>`, `ItRefReadOnly<T>`).

## 🚀 Install

```sh
dotnet add package Mockurai
```

## ⚙️ How it works

Add `[MockuraiGenerate]` to a `partial` test base class and declare `IMock<T>` properties for each dependency. The generator fills in the implementations and produces the matching setup/verify extension methods.

```csharp
[MockuraiGenerate]
public abstract partial class CustomerServiceTestsBase
{
    protected partial IMock<ICustomerRepository> RepositoryMock { get; }
    protected partial IMock<IClock> ClockMock { get; }

    protected ICustomerService CreateFixture() =>
        new CustomerService(RepositoryMock.Object, ClockMock.Object);
}
```

## ✅ Setup, verify, and `VerifyNoOtherCalls`

```csharp
public sealed class GetCustomerShould : CustomerServiceTestsBase
{
    [Fact]
    public void ReturnCustomerFromRepository()
    {
        const string customerId = "C-001";
        var expected = new Customer(customerId, "Issei");

        RepositoryMock
            .SetupGetById(customerId)
            .Returns(expected);

        var actual = CreateFixture().GetCustomer(customerId);

        Assert.Equal(expected, actual);

        RepositoryMock.VerifyGetById(customerId, Times.Once);
        VerifyNoOtherCalls();
    }
}
```

`VerifyNoOtherCalls()` covers **every** mock declared on the base class — if any invocation slips through unverified, the test fails with a precise message pointing at it.

## 🔢 Verify in sequence

```csharp
[Fact]
public void SaveBeforeNotifying()
{
    var customer = new Customer("C-001", "Issei");

    CreateFixture().Register(customer);

    VerifyInSequence(static ctx =>
    {
        ctx.RepositoryMock.Save(It<Customer>.Equivalent(customer));
        ctx.ClockMock.UtcNow();
        ctx.RepositoryMock.MarkRegistered("C-001");
    });
    VerifyNoOtherCalls();
}
```

The sequence asserts both the **order** and the **arguments** across multiple mocks. Mismatches surface the full invocation log so you can see exactly where reality diverged from expectation.

## 🎯 Argument matchers

| Matcher | Use for |
| --- | --- |
| `It<T>` | by-value parameters |
| `ItIn<T>` | `in` parameters |
| `ItOut<T>` | `out` parameters |
| `ItRef<T>` | `ref` parameters |
| `ItRefReadOnly<T>` | `ref readonly` parameters |

Each one offers `.Value(...)`, `.Equivalent(...)`, `.Where(predicate)`, and `.Any()`.

## 📄 License

MIT
