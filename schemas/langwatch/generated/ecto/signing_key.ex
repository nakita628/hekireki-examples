defmodule LangWatch.SigningKey do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          organization_id: String.t(),
          project_id: String.t(),
          name: String.t(),
          public_id: String.t(),
          secret_ciphertext: String.t(),
          created_by_user_id: String.t(),
          rotated_at: DateTime.t() | nil,
          verifies_until: DateTime.t() | nil,
          revoked_at: DateTime.t() | nil,
          last_used_at: DateTime.t() | nil,
          inserted_at: DateTime.t()
        }

  schema "SigningKey" do
    field(:id, :string, primary_key: true)
    field(:organization_id, :string, source: :organizationId)
    field(:project_id, :string, source: :projectId)
    field(:name, :string)
    field(:public_id, :string, source: :publicId)
    field(:secret_ciphertext, :string, source: :secretCiphertext)
    field(:created_by_user_id, :string, source: :createdByUserId)
    field(:rotated_at, LangWatch.PrismaDateTime, source: :rotatedAt)
    field(:verifies_until, LangWatch.PrismaDateTime, source: :verifiesUntil)
    field(:revoked_at, LangWatch.PrismaDateTime, source: :revokedAt)
    field(:last_used_at, LangWatch.PrismaDateTime, source: :lastUsedAt)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end