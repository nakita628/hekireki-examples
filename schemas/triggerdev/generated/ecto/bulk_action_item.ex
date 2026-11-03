defmodule Trigger.BulkActionItem do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          friendly_id: String.t() | nil,
          group_id: String.t(),
          type: atom(),
          status: atom(),
          error: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          source_run: Trigger.TaskRun.t() | nil,
          destination_run: Trigger.TaskRun.t() | nil
        }

  schema "BulkActionItem" do
    field(:id, :string, primary_key: true)
    field(:friendly_id, :string, source: :friendlyId)
    field(:group_id, :string, source: :groupId)
    field(:type, Ecto.Enum, values: [:CANCEL, :REPLAY])
    field(:status, Ecto.Enum, values: [:PENDING, :COMPLETED, :FAILED], default: :PENDING)
    field(:error, :string)
    field(:source_run_id, :string, source: :sourceRunId)
    field(:destination_run_id, :string, source: :destinationRunId)
    belongs_to(:source_run, Trigger.TaskRun, foreign_key: :source_run_id, define_field: false, type: :string)
    belongs_to(:destination_run, Trigger.TaskRun, foreign_key: :destination_run_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end