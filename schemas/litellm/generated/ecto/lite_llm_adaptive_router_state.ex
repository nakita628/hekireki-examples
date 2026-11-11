defmodule LiteLLM.LiteLLMAdaptiveRouterState do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LiteLLM.PrismaDateTime, autogenerate: {LiteLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          router_name: String.t(),
          request_type: String.t(),
          model_name: String.t(),
          alpha: float(),
          beta: float(),
          total_samples: integer(),
          updated_at: DateTime.t()
        }

  schema "LiteLLM_AdaptiveRouterState" do
    field(:router_name, :string, primary_key: true)
    field(:request_type, :string, primary_key: true)
    field(:model_name, :string, primary_key: true)
    field(:alpha, :float)
    field(:beta, :float)
    field(:total_samples, :integer, default: 0)
    timestamps(inserted_at: false, updated_at_source: :last_updated_at)
  end
end