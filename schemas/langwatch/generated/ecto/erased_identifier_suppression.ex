defmodule LangWatch.ErasedIdentifierSuppression do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          organization_id: String.t(),
          provider: String.t(),
          identifier_hash: String.t(),
          erased_at: DateTime.t()
        }

  schema "ErasedIdentifierSuppression" do
    field(:id, :string, primary_key: true)
    field(:organization_id, :string, source: :organizationId)
    field(:provider, :string)
    field(:identifier_hash, :string, source: :identifierHash)
    field(:erased_at, LangWatch.PrismaDateTime, source: :erasedAt)
  end
end