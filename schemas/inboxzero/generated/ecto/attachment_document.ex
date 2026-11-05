defmodule InboxZero.AttachmentDocument do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          file_id: String.t(),
          name: String.t(),
          mime_type: String.t(),
          modified_at: DateTime.t() | nil,
          summary: String.t() | nil,
          content: String.t() | nil,
          metadata: map() | nil,
          indexed_at: DateTime.t() | nil,
          error: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          attachment_source: InboxZero.AttachmentSource.t() | nil
        }

  schema "AttachmentDocument" do
    field(:id, :string, primary_key: true)
    field(:file_id, :string, source: :fileId)
    field(:name, :string)
    field(:mime_type, :string, source: :mimeType)
    field(:modified_at, InboxZero.PrismaDateTime, source: :modifiedAt)
    field(:summary, :string)
    field(:content, :string)
    field(:metadata, :map)
    field(:indexed_at, InboxZero.PrismaDateTime, source: :indexedAt)
    field(:error, :string)
    field(:attachment_source_id, :string, source: :attachmentSourceId)
    belongs_to(:attachment_source, InboxZero.AttachmentSource, foreign_key: :attachment_source_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end