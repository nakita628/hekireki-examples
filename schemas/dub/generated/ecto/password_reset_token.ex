defmodule Dub.PasswordResetToken do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          identifier: String.t(),
          token: String.t(),
          expires: DateTime.t()
        }

  schema "PasswordResetToken" do
    field(:identifier, :string)
    field(:token, :string, primary_key: true)
    field(:expires, Dub.PrismaDateTime)
  end
end