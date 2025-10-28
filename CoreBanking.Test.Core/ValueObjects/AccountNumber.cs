namespace CoreBanking.Test.Core.ValueObjects
{
    public class AccountNumber
    {
        public string Value { get; }

        public AccountNumber(string value)
        {
      if (string.IsNullOrWhiteSpace(value) || value.Length != 10)
      {
        throw new ArgumentException("Account number must be 10 digits long.");
      }
            if(!value.All(char.IsDigit))
            {
                throw new ArgumentException("Account number must contain only digits.");
            }

            Value = value;
        }

    public static implicit operator string(AccountNumber number) => number.Value;

    public static explicit operator AccountNumber(string value) => new(value);

    public override string ToString() => Value;
    }
}