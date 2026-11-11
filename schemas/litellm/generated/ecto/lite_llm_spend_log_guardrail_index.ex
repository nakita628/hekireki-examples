defmodule LiteLLM.LiteLLMSpendLogGuardrailIndex do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          request_id: String.t(),
          guardrail_id: String.t(),
          policy_id: String.t() | nil,
          start_time: DateTime.t()
        }

  schema "LiteLLM_SpendLogGuardrailIndex" do
    field(:request_id, :string, primary_key: true)
    field(:guardrail_id, :string, primary_key: true)
    field(:policy_id, :string)
    field(:start_time, LiteLLM.PrismaDateTime)
  end
end