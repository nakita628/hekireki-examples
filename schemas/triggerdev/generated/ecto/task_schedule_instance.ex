defmodule Trigger.TaskScheduleInstance do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          schedule_phase: integer() | nil,
          active: boolean(),
          last_scheduled_timestamp: DateTime.t() | nil,
          next_scheduled_timestamp: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          task_schedule: Trigger.TaskSchedule.t() | nil,
          environment: Trigger.RuntimeEnvironment.t() | nil,
          project: Trigger.Project.t() | nil
        }

  schema "TaskScheduleInstance" do
    field(:id, :string, primary_key: true)
    field(:schedule_phase, :integer, source: :schedulePhase)
    field(:active, :boolean, default: true)
    field(:last_scheduled_timestamp, Trigger.PrismaDateTime, source: :lastScheduledTimestamp)
    field(:next_scheduled_timestamp, Trigger.PrismaDateTime, source: :nextScheduledTimestamp)
    field(:task_schedule_id, :string, source: :taskScheduleId)
    field(:environment_id, :string, source: :environmentId)
    field(:project_id, :string, source: :projectId)
    belongs_to(:task_schedule, Trigger.TaskSchedule, foreign_key: :task_schedule_id, define_field: false, type: :string)
    belongs_to(:environment, Trigger.RuntimeEnvironment, foreign_key: :environment_id, define_field: false, type: :string)
    belongs_to(:project, Trigger.Project, foreign_key: :project_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end