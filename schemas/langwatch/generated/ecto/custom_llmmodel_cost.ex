defmodule LangWatch.CustomLLMModelCost do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          organization_id: String.t(),
          scope_type: atom(),
          scope_id: String.t(),
          project_id: String.t() | nil,
          model: String.t(),
          regex: String.t(),
          input_cost_per_token: float() | nil,
          output_cost_per_token: float() | nil,
          cache_read_cost_per_token: float() | nil,
          cache_creation_cost_per_token: float() | nil,
          cache_creation1h_cost_per_token: float() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "CustomLLMModelCost" do
    field(:id, :string, primary_key: true)
    field(:organization_id, :string, source: :organizationId)
    field(:scope_type, Ecto.Enum, values: [:ORGANIZATION, :TEAM, :PROJECT], default: :PROJECT, source: :scopeType)
    field(:scope_id, :string, source: :scopeId)
    field(:project_id, :string, source: :projectId)
    field(:model, :string)
    field(:regex, :string)
    field(:input_cost_per_token, :float, source: :inputCostPerToken)
    field(:output_cost_per_token, :float, source: :outputCostPerToken)
    field(:cache_read_cost_per_token, :float, source: :cacheReadCostPerToken)
    field(:cache_creation_cost_per_token, :float, source: :cacheCreationCostPerToken)
    field(:cache_creation1h_cost_per_token, :float, source: :cacheCreation1hCostPerToken)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end