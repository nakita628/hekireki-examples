defmodule LangWatch.ScimToken do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          connection_id: String.t() | nil,
          hashed_token: String.t(),
          hash_scheme: String.t(),
          description: String.t() | nil,
          last_used_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          organization: LangWatch.Organization.t() | nil
        }

  schema "ScimToken" do
    field(:id, :string, primary_key: true)
    field(:connection_id, :string, source: :connectionId)
    field(:hashed_token, :string, source: :hashedToken)
    field(:hash_scheme, :string, default: "sha256", source: :hashScheme)
    field(:description, :string)
    field(:last_used_at, LangWatch.PrismaDateTime, source: :lastUsedAt)
    field(:organization_id, :string, source: :organizationId)
    belongs_to(:organization, LangWatch.Organization, foreign_key: :organization_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end