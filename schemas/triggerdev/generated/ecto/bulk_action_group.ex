defmodule Trigger.BulkActionGroup do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          friendly_id: String.t(),
          type: atom(),
          status: atom(),
          query_name: String.t() | nil,
          params: map() | nil,
          dedupe_key: String.t() | nil,
          cursor: map() | nil,
          success_count: integer(),
          failure_count: integer(),
          total_count: integer(),
          completion_notification: atom(),
          name: String.t() | nil,
          completed_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: Trigger.Project.t() | nil,
          environment: Trigger.RuntimeEnvironment.t() | nil,
          user: Trigger.User.t() | nil
        }

  schema "BulkActionGroup" do
    field(:id, :string, primary_key: true)
    field(:friendly_id, :string, source: :friendlyId)
    field(:type, Ecto.Enum, values: [:CANCEL, :REPLAY])
    field(:status, Ecto.Enum, values: [:PENDING, :COMPLETED, :ABORTED], default: :PENDING)
    field(:query_name, :string, source: :queryName)
    field(:params, :map)
    field(:dedupe_key, :string, source: :dedupeKey)
    field(:cursor, :map)
    field(:success_count, :integer, default: 0, source: :successCount)
    field(:failure_count, :integer, default: 0, source: :failureCount)
    field(:total_count, :integer, default: 0, source: :totalCount)
    field(:completion_notification, Ecto.Enum, values: [:NONE, :EMAIL], default: :NONE, source: :completionNotification)
    field(:name, :string)
    field(:completed_at, Trigger.PrismaDateTime, source: :completedAt)
    field(:project_id, :string, source: :projectId)
    field(:environment_id, :string, source: :environmentId)
    field(:user_id, :string, source: :userId)
    belongs_to(:project, Trigger.Project, foreign_key: :project_id, define_field: false, type: :string)
    belongs_to(:environment, Trigger.RuntimeEnvironment, foreign_key: :environment_id, define_field: false, type: :string)
    belongs_to(:user, Trigger.User, foreign_key: :user_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end