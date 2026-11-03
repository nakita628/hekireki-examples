defmodule Formbricks.OauthClientAssertion do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          expires_at: DateTime.t()
        }

  schema "oauthClientAssertion" do
    field(:id, :string, primary_key: true)
    field(:expires_at, Formbricks.PrismaDateTime, source: :expiresAt)
  end
end