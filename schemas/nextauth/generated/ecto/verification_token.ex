defmodule NextAuth.VerificationToken do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          identifier: String.t(),
          token: String.t(),
          expires: DateTime.t()
        }

  schema "VerificationToken" do
    field(:identifier, :string, primary_key: true)
    field(:token, :string, primary_key: true)
    field(:expires, NextAuth.PrismaDateTime)
  end
end