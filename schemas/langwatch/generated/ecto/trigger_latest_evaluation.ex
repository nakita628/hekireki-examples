defmodule LangWatch.TriggerLatestEvaluation do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          project_id: String.t(),
          evaluated_at: DateTime.t(),
          verdict: String.t(),
          observed_value: float() | nil,
          threshold: float() | nil,
          operator: String.t() | nil,
          time_period_minutes: integer() | nil,
          skip_code: String.t() | nil,
          updated_at: DateTime.t(),
          trigger: LangWatch.Trigger.t() | nil
        }

  schema "TriggerLatestEvaluation" do
    field(:project_id, :string, source: :projectId)
    field(:evaluated_at, LangWatch.PrismaDateTime, source: :evaluatedAt)
    field(:verdict, :string)
    field(:observed_value, :float, source: :observedValue)
    field(:threshold, :float)
    field(:operator, :string)
    field(:time_period_minutes, :integer, source: :timePeriodMinutes)
    field(:skip_code, :string, source: :skipCode)
    field(:trigger_id, :string, source: :triggerId)
    belongs_to(:trigger, LangWatch.Trigger, foreign_key: :trigger_id, define_field: false, type: :string)
    timestamps(inserted_at: false, updated_at_source: :updatedAt)
  end
end