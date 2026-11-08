defmodule TypeHero.VerificationToken do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          identifier: String.t(),
          token: String.t(),
          expires: DateTime.t()
        }

  schema "VerificationToken" do
    field(:identifier, :string)
    field(:token, :string, primary_key: true)
    field(:expires, TypeHero.PrismaDateTime)
  end
end