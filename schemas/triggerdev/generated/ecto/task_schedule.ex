defmodule Trigger.TaskSchedule do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          type: atom(),
          friendly_id: String.t(),
          task_identifier: String.t(),
          deduplication_key: String.t(),
          user_provided_deduplication_key: boolean(),
          generator_expression: String.t(),
          generator_description: String.t(),
          generator_type: atom(),
          timezone: String.t(),
          window_duration_seconds: integer() | nil,
          window_percentage: integer() | nil,
          default_window_duration_seconds: integer() | nil,
          minimum_window_duration_seconds: integer() | nil,
          external_id: String.t() | nil,
          last_run_triggered_at: DateTime.t() | nil,
          active: boolean(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: Trigger.Project.t() | nil,
          instances: [Trigger.TaskScheduleInstance.t()]
        }

  schema "TaskSchedule" do
    field(:id, :string, primary_key: true)
    field(:type, Ecto.Enum, values: [:DECLARATIVE, :IMPERATIVE], default: :IMPERATIVE)
    field(:friendly_id, :string, source: :friendlyId)
    field(:task_identifier, :string, source: :taskIdentifier)
    field(:deduplication_key, :string, source: :deduplicationKey)
    field(:user_provided_deduplication_key, :boolean, default: false, source: :userProvidedDeduplicationKey)
    field(:generator_expression, :string, source: :generatorExpression)
    field(:generator_description, :string, default: "", source: :generatorDescription)
    field(:generator_type, Ecto.Enum, values: [:CRON], default: :CRON, source: :generatorType)
    field(:timezone, :string, default: "UTC")
    field(:window_duration_seconds, :integer, source: :windowDurationSeconds)
    field(:window_percentage, :integer, source: :windowPercentage)
    field(:default_window_duration_seconds, :integer, source: :defaultWindowDurationSeconds)
    field(:minimum_window_duration_seconds, :integer, source: :minimumWindowDurationSeconds)
    field(:external_id, :string, source: :externalId)
    field(:last_run_triggered_at, Trigger.PrismaDateTime, source: :lastRunTriggeredAt)
    field(:active, :boolean, default: true)
    field(:project_id, :string, source: :projectId)
    belongs_to(:project, Trigger.Project, foreign_key: :project_id, define_field: false, type: :string)
    has_many(:instances, Trigger.TaskScheduleInstance, foreign_key: :task_schedule_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end