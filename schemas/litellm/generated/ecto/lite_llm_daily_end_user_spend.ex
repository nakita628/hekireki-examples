defmodule LiteLLM.LiteLLMDailyEndUserSpend do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: LiteLLM.PrismaDateTime, autogenerate: {LiteLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          end_user_id: String.t() | nil,
          date: String.t(),
          api_key: String.t(),
          model: String.t() | nil,
          model_group: String.t() | nil,
          custom_llm_provider: String.t() | nil,
          mcp_namespaced_tool_name: String.t() | nil,
          endpoint: String.t() | nil,
          prompt_tokens: integer(),
          completion_tokens: integer(),
          cache_read_input_tokens: integer(),
          cache_creation_input_tokens: integer(),
          compression_saved_tokens: integer(),
          compression_savings_spend: float(),
          prompt_caching_savings_spend: float(),
          gateway_injected_caching_savings_spend: float(),
          autorouter_savings_spend: float(),
          guardrail_spend: float(),
          spend: float(),
          api_requests: integer(),
          successful_requests: integer(),
          failed_requests: integer(),
          total_response_time_ms: integer(),
          timed_requests: integer(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "LiteLLM_DailyEndUserSpend" do
    field(:end_user_id, :string)
    field(:date, :string)
    field(:api_key, :string)
    field(:model, :string)
    field(:model_group, :string)
    field(:custom_llm_provider, :string)
    field(:mcp_namespaced_tool_name, :string)
    field(:endpoint, :string)
    field(:prompt_tokens, :integer, default: 0)
    field(:completion_tokens, :integer, default: 0)
    field(:cache_read_input_tokens, :integer, default: 0)
    field(:cache_creation_input_tokens, :integer, default: 0)
    field(:compression_saved_tokens, :integer, default: 0)
    field(:compression_savings_spend, :float, default: 0.0)
    field(:prompt_caching_savings_spend, :float, default: 0.0)
    field(:gateway_injected_caching_savings_spend, :float, default: 0.0)
    field(:autorouter_savings_spend, :float, default: 0.0)
    field(:guardrail_spend, :float, default: 0.0)
    field(:spend, :float, default: 0.0)
    field(:api_requests, :integer, default: 0)
    field(:successful_requests, :integer, default: 0)
    field(:failed_requests, :integer, default: 0)
    field(:total_response_time_ms, :integer, default: 0)
    field(:timed_requests, :integer, default: 0)
    timestamps(inserted_at_source: :created_at)
  end
end