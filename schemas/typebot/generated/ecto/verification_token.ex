defmodule Typebot.VerificationToken do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          identifier: String.t(),
          token: String.t(),
          value: String.t() | nil,
          failed_attempts: integer(),
          expires: DateTime.t()
        }

  schema "VerificationToken" do
    field(:identifier, :string)
    field(:token, :string, primary_key: true)
    field(:value, :string)
    field(:failed_attempts, :integer, default: 0, source: :failedAttempts)
    field(:expires, Typebot.PrismaDateTime)
  end
end