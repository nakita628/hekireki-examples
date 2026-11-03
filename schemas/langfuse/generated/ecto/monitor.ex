defmodule Langfuse.Monitor do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          view: atom(),
          filters: map(),
          metric: map(),
          window_ms: integer(),
          cadence_ms: integer(),
          threshold_operator: atom(),
          alert_threshold: Decimal.t(),
          warning_threshold: Decimal.t() | nil,
          severity: atom(),
          severity_changed_at: DateTime.t() | nil,
          no_data: map(),
          renotify: map(),
          status: atom(),
          scheduler_batch_id: integer(),
          next_run_at: DateTime.t() | nil,
          last_published_at: DateTime.t() | nil,
          last_claimed_at: DateTime.t() | nil,
          last_completed_at: DateTime.t() | nil,
          name: String.t(),
          tags: [String.t()],
          trigger_ids: [String.t()],
          alerted_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          created_by_user: Langfuse.User.t() | nil,
          updated_by_user: Langfuse.User.t() | nil,
          project: Langfuse.Project.t() | nil
        }

  schema "monitors" do
    field(:id, :string, primary_key: true)
    field(:view, Ecto.Enum, values: [:OBSERVATIONS, :SCORES_NUMERIC, :SCORES_CATEGORICAL, :SCORES_BOOLEAN])
    field(:filters, :map)
    field(:metric, :map)
    field(:window_ms, :integer)
    field(:cadence_ms, :integer)
    field(:threshold_operator, Ecto.Enum, values: [:GT, :GTE, :LT, :LTE, :EQ, :NEQ])
    field(:alert_threshold, :decimal)
    field(:warning_threshold, :decimal)
    field(:severity, Ecto.Enum, values: [:PAUSED, :UNKNOWN, :NO_DATA, :OK, :WARNING, :ALERT], default: :UNKNOWN)
    field(:severity_changed_at, Langfuse.PrismaDateTime)
    field(:no_data, :map)
    field(:renotify, :map)
    field(:status, Ecto.Enum, values: [:PAUSED, :ACTIVE, :ERROR_BAD_QUERY], default: :ACTIVE)
    field(:scheduler_batch_id, :integer)
    field(:next_run_at, Langfuse.PrismaDateTime)
    field(:last_published_at, Langfuse.PrismaDateTime)
    field(:last_claimed_at, Langfuse.PrismaDateTime)
    field(:last_completed_at, Langfuse.PrismaDateTime)
    field(:name, :string)
    field(:tags, {:array, :string})
    field(:trigger_ids, {:array, :string})
    field(:alerted_at, Langfuse.PrismaDateTime)
    belongs_to(:created_by_user, Langfuse.User, foreign_key: :created_by, type: :string)
    belongs_to(:updated_by_user, Langfuse.User, foreign_key: :updated_by, type: :string)
    belongs_to(:project, Langfuse.Project, foreign_key: :project_id, type: :string)
    timestamps(inserted_at_source: :created_at)
  end
end