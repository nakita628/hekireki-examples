defmodule InboxZero.AttachmentSource do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          type: atom(),
          source_id: String.t(),
          source_path: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          rule: InboxZero.Rule.t() | nil,
          drive_connection: InboxZero.DriveConnection.t() | nil,
          documents: [InboxZero.AttachmentDocument.t()]
        }

  schema "AttachmentSource" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:type, Ecto.Enum, values: [:FILE, :FOLDER])
    field(:source_id, :string, source: :sourceId)
    field(:source_path, :string, source: :sourcePath)
    field(:rule_id, :string, source: :ruleId)
    field(:drive_connection_id, :string, source: :driveConnectionId)
    belongs_to(:rule, InboxZero.Rule, foreign_key: :rule_id, define_field: false, type: :string)
    belongs_to(:drive_connection, InboxZero.DriveConnection, foreign_key: :drive_connection_id, define_field: false, type: :string)
    has_many(:documents, InboxZero.AttachmentDocument, foreign_key: :attachment_source_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end