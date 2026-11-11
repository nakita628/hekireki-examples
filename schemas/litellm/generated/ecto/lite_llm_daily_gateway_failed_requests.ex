defmodule LiteLLM.LiteLLMDailyGatewayFailedRequests do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LiteLLM.PrismaDateTime, autogenerate: {LiteLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          date: String.t(),
          category: String.t(),
          route: String.t(),
          status_code: integer(),
          failed_requests: integer(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "LiteLLM_DailyGatewayFailedRequests" do
    field(:date, :string, primary_key: true)
    field(:category, :string, primary_key: true)
    field(:route, :string, primary_key: true)
    field(:status_code, :integer, primary_key: true)
    field(:failed_requests, :integer, default: 0)
    timestamps(inserted_at_source: :created_at)
  end
end