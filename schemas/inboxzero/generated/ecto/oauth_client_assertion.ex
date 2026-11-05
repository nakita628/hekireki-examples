defmodule InboxZero.OauthClientAssertion do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          expires_at: DateTime.t()
        }

  schema "OauthClientAssertion" do
    field(:id, :string, primary_key: true)
    field(:expires_at, InboxZero.PrismaDateTime, source: :expiresAt)
  end
end