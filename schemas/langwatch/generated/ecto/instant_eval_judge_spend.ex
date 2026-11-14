defmodule LangWatch.InstantEvalJudgeSpend do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          organization_id: String.t(),
          request_id: String.t(),
          spend_nano_usd: integer(),
          occurred_at: DateTime.t()
        }

  schema "InstantEvalJudgeSpend" do
    field(:organization_id, :string, primary_key: true, source: :organizationId)
    field(:request_id, :string, primary_key: true, source: :requestId)
    field(:spend_nano_usd, :integer, source: :spendNanoUsd)
    field(:occurred_at, LangWatch.PrismaDateTime, source: :occurredAt)
  end
end