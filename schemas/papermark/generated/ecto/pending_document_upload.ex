defmodule Papermark.PendingDocumentUpload do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Papermark.PrismaDateTime, autogenerate: {Papermark.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          upload_id: String.t(),
          team_id: String.t(),
          token_id: String.t(),
          key: String.t(),
          content_type: String.t(),
          storage_type: atom(),
          expires_at: DateTime.t(),
          consumed_at: DateTime.t() | nil,
          inserted_at: DateTime.t()
        }

  schema "PendingDocumentUpload" do
    field(:id, :string, primary_key: true)
    field(:upload_id, :string, source: :uploadId)
    field(:team_id, :string, source: :teamId)
    field(:token_id, :string, source: :tokenId)
    field(:key, :string)
    field(:content_type, :string, source: :contentType)
    field(:storage_type, Ecto.Enum, values: [:S3_PATH, :VERCEL_BLOB], default: :S3_PATH, source: :storageType)
    field(:expires_at, Papermark.PrismaDateTime, source: :expiresAt)
    field(:consumed_at, Papermark.PrismaDateTime, source: :consumedAt)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end