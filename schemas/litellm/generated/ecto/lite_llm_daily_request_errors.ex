defmodule LiteLLM.LiteLLMDailyRequestErrors do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LiteLLM.PrismaDateTime, autogenerate: {LiteLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          date: String.t(),
          api_key: String.t(),
          team_id: String.t(),
          user_id: String.t(),
          model_group: String.t(),
          status_code: integer(),
          failed_requests: integer(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "LiteLLM_DailyRequestErrors" do
    field(:date, :string, primary_key: true)
    field(:api_key, :string, primary_key: true)
    field(:team_id, :string, primary_key: true, default: "")
    field(:user_id, :string, primary_key: true, default: "")
    field(:model_group, :string, primary_key: true, default: "")
    field(:status_code, :integer, primary_key: true)
    field(:failed_requests, :integer, default: 0)
    timestamps(inserted_at_source: :created_at)
  end
end