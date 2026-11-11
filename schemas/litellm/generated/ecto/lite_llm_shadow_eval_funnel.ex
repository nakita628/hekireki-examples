defmodule LiteLLM.LiteLLMShadowEvalFunnel do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          job_id: String.t(),
          not_sampled: integer(),
          unjudgeable: integer(),
          shed: integer(),
          withheld: integer()
        }

  schema "LiteLLM_ShadowEvalFunnel" do
    field(:job_id, :string, primary_key: true)
    field(:not_sampled, :integer, default: 0)
    field(:unjudgeable, :integer, default: 0)
    field(:shed, :integer, default: 0)
    field(:withheld, :integer, default: 0)
  end
end