defmodule LangWatch.Identifier do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          user_id: String.t(),
          provider: String.t(),
          value: String.t() | nil,
          domain: String.t() | nil,
          identifier_hash: String.t() | nil,
          account_id: String.t() | nil,
          provider_id: String.t() | nil,
          issuer: String.t() | nil,
          provider_account_id: String.t() | nil,
          state: String.t(),
          connection_id: String.t() | nil,
          verified_at: DateTime.t() | nil,
          attached_at: DateTime.t(),
          detached_at: DateTime.t() | nil,
          last_used_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "Identifier" do
    field(:id, :string, primary_key: true)
    field(:user_id, :string, source: :userId)
    field(:provider, :string)
    field(:value, :string)
    field(:domain, :string)
    field(:identifier_hash, :string, source: :identifierHash)
    field(:account_id, :string, source: :accountId)
    field(:provider_id, :string, source: :providerId)
    field(:issuer, :string)
    field(:provider_account_id, :string, source: :providerAccountId)
    field(:state, :string)
    field(:connection_id, :string, source: :connectionId)
    field(:verified_at, LangWatch.PrismaDateTime, source: :verifiedAt)
    field(:attached_at, LangWatch.PrismaDateTime, source: :attachedAt)
    field(:detached_at, LangWatch.PrismaDateTime, source: :detachedAt)
    field(:last_used_at, LangWatch.PrismaDateTime, source: :lastUsedAt)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end