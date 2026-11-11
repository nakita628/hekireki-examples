defmodule LiteLLM.LiteLLMDailyGatewayRequests do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LiteLLM.PrismaDateTime, autogenerate: {LiteLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          date: String.t(),
          category: String.t(),
          route: String.t(),
          successful_requests: integer(),
          failed_requests: integer(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "LiteLLM_DailyGatewayRequests" do
    field(:date, :string, primary_key: true)
    field(:category, :string, primary_key: true)
    field(:route, :string, primary_key: true)
    field(:successful_requests, :integer, default: 0)
    field(:failed_requests, :integer, default: 0)
    timestamps(inserted_at_source: :created_at)
  end
end