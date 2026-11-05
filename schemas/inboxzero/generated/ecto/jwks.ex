defmodule InboxZero.Jwks do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          public_key: String.t(),
          private_key: String.t(),
          created_at: DateTime.t(),
          expires_at: DateTime.t() | nil,
          alg: String.t() | nil,
          crv: String.t() | nil
        }

  schema "Jwks" do
    field(:id, :string, primary_key: true)
    field(:public_key, :string, source: :publicKey)
    field(:private_key, :string, source: :privateKey)
    field(:created_at, InboxZero.PrismaDateTime, source: :createdAt)
    field(:expires_at, InboxZero.PrismaDateTime, source: :expiresAt)
    field(:alg, :string)
    field(:crv, :string)
  end
end