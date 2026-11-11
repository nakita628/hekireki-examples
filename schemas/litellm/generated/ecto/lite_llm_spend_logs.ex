defmodule LiteLLM.LiteLLMSpendLogs do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LiteLLM.PrismaDateTime, autogenerate: {LiteLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          request_id: String.t(),
          call_type: String.t(),
          api_key: String.t(),
          spend: float(),
          total_tokens: integer(),
          prompt_tokens: integer(),
          completion_tokens: integer(),
          start_time: DateTime.t(),
          end_time: DateTime.t(),
          request_duration_ms: integer() | nil,
          completion_start_time: DateTime.t() | nil,
          model: String.t(),
          model_id: String.t() | nil,
          model_group: String.t() | nil,
          custom_llm_provider: String.t() | nil,
          api_base: String.t() | nil,
          user: String.t() | nil,
          metadata: map() | nil,
          cache_hit: String.t() | nil,
          cache_key: String.t() | nil,
          request_tags: map() | nil,
          team_id: String.t() | nil,
          organization_id: String.t() | nil,
          end_user: String.t() | nil,
          requester_ip_address: String.t() | nil,
          messages: map() | nil,
          response: map() | nil,
          session_id: String.t() | nil,
          status: String.t() | nil,
          mcp_namespaced_tool_name: String.t() | nil,
          billing_agent_id: String.t() | nil,
          agent_id: String.t() | nil,
          proxy_server_request: map() | nil,
          litellm_call_id: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "LiteLLM_SpendLogs" do
    field(:request_id, :string, primary_key: true)
    field(:call_type, :string)
    field(:api_key, :string, default: "")
    field(:spend, :float, default: 0.0)
    field(:total_tokens, :integer, default: 0)
    field(:prompt_tokens, :integer, default: 0)
    field(:completion_tokens, :integer, default: 0)
    field(:start_time, LiteLLM.PrismaDateTime, source: :startTime)
    field(:end_time, LiteLLM.PrismaDateTime, source: :endTime)
    field(:request_duration_ms, :integer)
    field(:completion_start_time, LiteLLM.PrismaDateTime, source: :completionStartTime)
    field(:model, :string, default: "")
    field(:model_id, :string, default: "")
    field(:model_group, :string, default: "")
    field(:custom_llm_provider, :string, default: "")
    field(:api_base, :string, default: "")
    field(:user, :string, default: "")
    field(:metadata, :map, default: %{})
    field(:cache_hit, :string, default: "")
    field(:cache_key, :string, default: "")
    field(:request_tags, :map)
    field(:team_id, :string)
    field(:organization_id, :string)
    field(:end_user, :string)
    field(:requester_ip_address, :string)
    field(:messages, :map, default: %{})
    field(:response, :map, default: %{})
    field(:session_id, :string)
    field(:status, :string)
    field(:mcp_namespaced_tool_name, :string)
    field(:billing_agent_id, :string)
    field(:agent_id, :string)
    field(:proxy_server_request, :map, default: %{})
    field(:litellm_call_id, :string)
    timestamps(inserted_at_source: :created_at)
  end
end