defmodule LiteLLM.LiteLLMShadowEvalAttempt do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LiteLLM.PrismaDateTime, autogenerate: {LiteLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          job_id: String.t(),
          request_id: String.t(),
          outcome: String.t(),
          router_name: String.t() | nil,
          tier: String.t() | nil,
          real_model: String.t() | nil,
          shadow_model: String.t() | nil,
          confidence: float() | nil,
          judge_cost: float(),
          shadow_cost: float(),
          real_cost: float() | nil,
          real_classifier_cost: float(),
          shadow_classifier_cost: float(),
          real_cache_hit: boolean(),
          error: String.t() | nil,
          inserted_at: DateTime.t()
        }

  schema "LiteLLM_ShadowEvalAttempt" do
    field(:id, :string, primary_key: true)
    field(:job_id, :string)
    field(:request_id, :string)
    field(:outcome, :string)
    field(:router_name, :string)
    field(:tier, :string)
    field(:real_model, :string)
    field(:shadow_model, :string)
    field(:confidence, :float)
    field(:judge_cost, :float, default: 0.0)
    field(:shadow_cost, :float, default: 0.0)
    field(:real_cost, :float)
    field(:real_classifier_cost, :float, default: 0.0)
    field(:shadow_classifier_cost, :float, default: 0.0)
    field(:real_cache_hit, :boolean, default: false)
    field(:error, :string)
    timestamps(inserted_at_source: :created_at, updated_at: false)
  end
end