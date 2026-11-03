defmodule Formbricks.OnlineUpgradeState do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Formbricks.PrismaDateTime, autogenerate: {Formbricks.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          release: String.t(),
          target_image: String.t(),
          phase: String.t(),
          completed_phases: [String.t()],
          survey_cursor: String.t() | nil,
          survey_count: integer(),
          baseline_completed_at: DateTime.t() | nil,
          forward_only_at: DateTime.t() | nil,
          queue_paused_at: DateTime.t() | nil,
          queue_pause_deadline_at: DateTime.t() | nil,
          queue_resumed_at: DateTime.t() | nil,
          activated_at: DateTime.t() | nil,
          updated_at: DateTime.t()
        }

  schema "OnlineUpgradeState" do
    field(:release, :string, primary_key: true)
    field(:target_image, :string, source: :targetImage)
    field(:phase, :string)
    field(:completed_phases, {:array, :string}, source: :completedPhases)
    field(:survey_cursor, :string, source: :surveyCursor)
    field(:survey_count, :integer, default: 0, source: :surveyCount)
    field(:baseline_completed_at, Formbricks.PrismaDateTime, source: :baselineCompletedAt)
    field(:forward_only_at, Formbricks.PrismaDateTime, source: :forwardOnlyAt)
    field(:queue_paused_at, Formbricks.PrismaDateTime, source: :queuePausedAt)
    field(:queue_pause_deadline_at, Formbricks.PrismaDateTime, source: :queuePauseDeadlineAt)
    field(:queue_resumed_at, Formbricks.PrismaDateTime, source: :queueResumedAt)
    field(:activated_at, Formbricks.PrismaDateTime, source: :activatedAt)
    timestamps(inserted_at: false, updated_at_source: :updatedAt)
  end
end