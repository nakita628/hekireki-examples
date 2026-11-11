defmodule LiteLLM.LiteLLMDailyGuardrailUsageUnits do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LiteLLM.PrismaDateTime, autogenerate: {LiteLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          guardrail_id: String.t(),
          date: String.t(),
          team_id: String.t(),
          api_key: String.t(),
          usage_unit: String.t(),
          units: integer(),
          cost: float() | nil,
          untracked_units: integer(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "LiteLLM_DailyGuardrailUsageUnits" do
    field(:guardrail_id, :string, primary_key: true)
    field(:date, :string, primary_key: true)
    field(:team_id, :string, primary_key: true)
    field(:api_key, :string, primary_key: true)
    field(:usage_unit, :string, primary_key: true)
    field(:units, :integer, default: 0)
    field(:cost, :float)
    field(:untracked_units, :integer, default: 0)
    timestamps(inserted_at_source: :created_at)
  end
end