defmodule Trigger.OrganizationAccessToken do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          type: atom(),
          hashed_token: String.t(),
          expires_at: DateTime.t() | nil,
          revoked_at: DateTime.t() | nil,
          last_accessed_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          organization: Trigger.Organization.t() | nil
        }

  schema "OrganizationAccessToken" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:type, Ecto.Enum, values: [:USER, :SYSTEM], default: :USER)
    field(:hashed_token, :string, source: :hashedToken)
    field(:expires_at, Trigger.PrismaDateTime, source: :expiresAt)
    field(:revoked_at, Trigger.PrismaDateTime, source: :revokedAt)
    field(:last_accessed_at, Trigger.PrismaDateTime, source: :lastAccessedAt)
    field(:organization_id, :string, source: :organizationId)
    belongs_to(:organization, Trigger.Organization, foreign_key: :organization_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end