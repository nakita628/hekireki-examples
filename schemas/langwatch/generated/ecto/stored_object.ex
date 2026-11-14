defmodule LangWatch.StoredObject do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          tenant_id: String.t(),
          id: String.t(),
          status: String.t(),
          purpose: String.t(),
          owner_kind: String.t(),
          owner_id: String.t(),
          filename: String.t(),
          sha256: String.t(),
          size_bytes: integer(),
          media_type: String.t(),
          media_type_verified: boolean(),
          storage_provider: String.t() | nil,
          storage_destination_id: String.t() | nil,
          storage_provider_relative_id: String.t() | nil,
          generation: integer(),
          audiences: map(),
          expires_at: DateTime.t() | nil,
          available_at: DateTime.t() | nil,
          deleted_at: DateTime.t() | nil,
          source: String.t(),
          legacy_fingerprint: String.t() | nil,
          created_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "StoredObject" do
    field(:tenant_id, :string, primary_key: true, source: :tenantId)
    field(:id, :string, primary_key: true)
    field(:status, :string)
    field(:purpose, :string)
    field(:owner_kind, :string, source: :ownerKind)
    field(:owner_id, :string, source: :ownerId)
    field(:filename, :string)
    field(:sha256, :string)
    field(:size_bytes, :integer, source: :sizeBytes)
    field(:media_type, :string, source: :mediaType)
    field(:media_type_verified, :boolean, default: false, source: :mediaTypeVerified)
    field(:storage_provider, :string, source: :storageProvider)
    field(:storage_destination_id, :string, source: :storageDestinationId)
    field(:storage_provider_relative_id, :string, source: :storageProviderRelativeId)
    field(:generation, :integer, default: 0)
    field(:audiences, :map)
    field(:expires_at, LangWatch.PrismaDateTime, source: :expiresAt)
    field(:available_at, LangWatch.PrismaDateTime, source: :availableAt)
    field(:deleted_at, LangWatch.PrismaDateTime, source: :deletedAt)
    field(:source, :string)
    field(:legacy_fingerprint, :string, source: :legacyFingerprint)
    field(:created_at, LangWatch.PrismaDateTime, source: :createdAt)
    timestamps(inserted_at: false, updated_at_source: :updatedAt)
  end
end