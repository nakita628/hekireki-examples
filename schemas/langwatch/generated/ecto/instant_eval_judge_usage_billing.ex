defmodule LangWatch.InstantEvalJudgeUsageBilling do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          organization_id: String.t(),
          usage_billed: boolean(),
          occurred_at: DateTime.t(),
          from_catch_up: boolean()
        }

  schema "InstantEvalJudgeUsageBilling" do
    field(:organization_id, :string, primary_key: true, source: :organizationId)
    field(:usage_billed, :boolean, source: :usageBilled)
    field(:occurred_at, LangWatch.PrismaDateTime, source: :occurredAt)
    field(:from_catch_up, :boolean, source: :fromCatchUp)
  end
end