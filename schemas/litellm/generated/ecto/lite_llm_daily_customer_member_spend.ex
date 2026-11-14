defmodule LiteLLM.LiteLLMDailyCustomerMemberSpend do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LiteLLM.PrismaDateTime, autogenerate: {LiteLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          date: String.t(),
          customer_org_id: String.t(),
          customer_team_id: String.t(),
          customer_member_id: String.t(),
          model: String.t(),
          model_group: String.t(),
          custom_llm_provider: String.t(),
          mcp_namespaced_tool_name: String.t(),
          endpoint: String.t(),
          api_key: String.t(),
          spend: float(),
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
          api_requests: integer(),
          successful_requests: integer(),
          failed_requests: integer(),
          total_response_time_ms: integer(),
          timed_requests: integer(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "LiteLLM_DailyCustomerMemberSpend" do
    field(:id, :string, primary_key: true)
    field(:date, :string)
    field(:customer_org_id, :string)
    field(:customer_team_id, :string, default: "")
    field(:customer_member_id, :string, default: "")
    field(:model, :string, default: "")
    field(:model_group, :string, default: "")
    field(:custom_llm_provider, :string, default: "")
    field(:mcp_namespaced_tool_name, :string, default: "")
    field(:endpoint, :string, default: "")
    field(:api_key, :string, default: "")
    field(:spend, :float, default: 0.0)
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
    field(:api_requests, :integer, default: 0)
    field(:successful_requests, :integer, default: 0)
    field(:failed_requests, :integer, default: 0)
    field(:total_response_time_ms, :integer, default: 0)
    field(:timed_requests, :integer, default: 0)
    timestamps(inserted_at_source: :created_at)
  end
end