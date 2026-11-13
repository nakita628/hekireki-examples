defmodule LiteLLM.LiteLLMDailyModelUsage do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LiteLLM.PrismaDateTime, autogenerate: {LiteLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          date: String.t(),
          model_group: String.t(),
          model: String.t(),
          custom_llm_provider: String.t(),
          task_type: String.t(),
          spend: float(),
          prompt_tokens: integer(),
          completion_tokens: integer(),
          request_count: integer(),
          successful_requests: integer(),
          failed_requests: integer(),
          total_response_time_ms: integer(),
          timed_requests: integer(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "LiteLLM_DailyModelUsage" do
    field(:date, :string, primary_key: true)
    field(:model_group, :string, primary_key: true)
    field(:model, :string, primary_key: true)
    field(:custom_llm_provider, :string, primary_key: true)
    field(:task_type, :string, primary_key: true)
    field(:spend, :float, default: 0.0)
    field(:prompt_tokens, :integer, default: 0)
    field(:completion_tokens, :integer, default: 0)
    field(:request_count, :integer, default: 0)
    field(:successful_requests, :integer, default: 0)
    field(:failed_requests, :integer, default: 0)
    field(:total_response_time_ms, :integer, default: 0)
    field(:timed_requests, :integer, default: 0)
    timestamps(inserted_at_source: :created_at)
  end
end