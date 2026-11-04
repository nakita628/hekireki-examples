defmodule AnythingLLM.ModelRouterRules do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: AnythingLLM.PrismaDateTime, autogenerate: {AnythingLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          enabled: boolean(),
          priority: integer(),
          type: String.t(),
          title: String.t(),
          description: String.t() | nil,
          condition_logic: String.t() | nil,
          conditions: String.t() | nil,
          route_provider: String.t(),
          route_model: String.t(),
          created_by: integer() | nil,
          last_updated_at: DateTime.t(),
          inserted_at: DateTime.t(),
          router: AnythingLLM.ModelRouters.t() | nil
        }

  schema "model_router_rules" do
    field(:enabled, :boolean, default: true)
    field(:priority, :integer)
    field(:type, :string, default: "calculated")
    field(:title, :string)
    field(:description, :string)
    field(:condition_logic, :string)
    field(:conditions, :string)
    field(:route_provider, :string)
    field(:route_model, :string)
    field(:created_by, :integer)
    field(:last_updated_at, AnythingLLM.PrismaDateTime, autogenerate: true, source: :lastUpdatedAt)
    belongs_to(:router, AnythingLLM.ModelRouters, foreign_key: :router_id)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end