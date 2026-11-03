defmodule Langfuse.VerificationToken do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          identifier: String.t(),
          token: String.t(),
          expires: DateTime.t()
        }

  schema "verification_tokens" do
    field(:identifier, :string)
    field(:token, :string, primary_key: true)
    field(:expires, Langfuse.PrismaDateTime)
  end
end