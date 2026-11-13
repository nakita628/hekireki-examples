defmodule LiteLLM.LiteLLMPerformanceAlertState do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LiteLLM.PrismaDateTime, autogenerate: {LiteLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          enabled: boolean(),
          alerts_used: integer() | nil,
          id: integer(),
          owner: String.t(),
          next_run_at: DateTime.t(),
          day: String.t() | nil,
          scope: String.t() | nil,
          resume_after: String.t(),
          wrapped: boolean(),
          checked: integer(),
          skipped: integer(),
          start_scope: String.t() | nil,
          start_after: String.t(),
          completed_at: DateTime.t() | nil,
          updated_at: DateTime.t()
        }

  schema "LiteLLM_PerformanceAlertState" do
    field(:enabled, :boolean, default: true)
    field(:alerts_used, :integer)
    field(:id, :integer, primary_key: true)
    field(:owner, :string)
    field(:next_run_at, LiteLLM.PrismaDateTime)
    field(:day, :string)
    field(:scope, :string)
    field(:resume_after, :string, default: "")
    field(:wrapped, :boolean, default: false)
    field(:checked, :integer, default: 0)
    field(:skipped, :integer, default: 0)
    field(:start_scope, :string)
    field(:start_after, :string, default: "")
    field(:completed_at, LiteLLM.PrismaDateTime)
    timestamps(inserted_at: false)
  end
end