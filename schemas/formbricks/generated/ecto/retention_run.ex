defmodule Formbricks.RetentionRun do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          entity: atom(),
          kind: atom(),
          started_at: DateTime.t(),
          finished_at: DateTime.t() | nil,
          notified_count: integer(),
          archived_count: integer(),
          deleted_count: integer(),
          skipped_count: integer(),
          has_changes: boolean(),
          scan_cursor: String.t() | nil,
          organization: Formbricks.Organization.t() | nil,
          items: [Formbricks.RetentionRunItem.t()]
        }

  schema "RetentionRun" do
    field(:id, :string, primary_key: true)
    field(:entity, Ecto.Enum, values: [:responses, :surveys, :members])
    field(:kind, Ecto.Enum, values: [:sweep, :purge], default: :sweep)
    field(:started_at, Formbricks.PrismaDateTime, autogenerate: true, source: :startedAt)
    field(:finished_at, Formbricks.PrismaDateTime, source: :finishedAt)
    field(:notified_count, :integer, default: 0, source: :notifiedCount)
    field(:archived_count, :integer, default: 0, source: :archivedCount)
    field(:deleted_count, :integer, default: 0, source: :deletedCount)
    field(:skipped_count, :integer, default: 0, source: :skippedCount)
    field(:has_changes, :boolean, default: false, source: :hasChanges)
    field(:scan_cursor, :string, source: :scanCursor)
    field(:organization_id, :string, source: :organizationId)
    belongs_to(:organization, Formbricks.Organization, foreign_key: :organization_id, define_field: false, type: :string)
    has_many(:items, Formbricks.RetentionRunItem, foreign_key: :run_id, references: :id)
  end
end