defmodule LiteLLM.LiteLLMDailyToolSpend do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LiteLLM.PrismaDateTime, autogenerate: {LiteLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          date: String.t(),
          tool_name: String.t(),
          spend: float(),
          total_tokens: integer(),
          request_count: integer(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "LiteLLM_DailyToolSpend" do
    field(:date, :string, primary_key: true)
    field(:tool_name, :string, primary_key: true)
    field(:spend, :float, default: 0.0)
    field(:total_tokens, :integer, default: 0)
    field(:request_count, :integer, default: 0)
    timestamps(inserted_at_source: :created_at)
  end
end