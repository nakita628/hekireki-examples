defmodule AnythingLLM.ModelRouters do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: AnythingLLM.PrismaDateTime, autogenerate: {AnythingLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          name: String.t(),
          description: String.t() | nil,
          fallback_provider: String.t(),
          fallback_model: String.t(),
          cooldown_seconds: integer(),
          created_by: integer() | nil,
          last_updated_at: DateTime.t(),
          inserted_at: DateTime.t(),
          rules: [AnythingLLM.ModelRouterRules.t()]
        }

  schema "model_routers" do
    field(:name, :string)
    field(:description, :string)
    field(:fallback_provider, :string)
    field(:fallback_model, :string)
    field(:cooldown_seconds, :integer, default: 30)
    field(:created_by, :integer)
    field(:last_updated_at, AnythingLLM.PrismaDateTime, autogenerate: true, source: :lastUpdatedAt)
    has_many(:rules, AnythingLLM.ModelRouterRules, foreign_key: :router_id)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end