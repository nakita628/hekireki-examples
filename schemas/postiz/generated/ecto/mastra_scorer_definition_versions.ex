defmodule Postiz.MastraScorerDefinitionVersions do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          scorer_definition_id: String.t(),
          version_number: integer(),
          name: String.t(),
          description: String.t() | nil,
          type: String.t(),
          model: map() | nil,
          instructions: String.t() | nil,
          score_range: map() | nil,
          preset_config: map() | nil,
          default_sampling: map() | nil,
          changed_fields: map() | nil,
          change_message: String.t() | nil,
          created_at: DateTime.t(),
          created_at_z: DateTime.t() | nil
        }

  schema "mastra_scorer_definition_versions" do
    field(:id, :string, primary_key: true)
    field(:scorer_definition_id, :string, source: :scorerDefinitionId)
    field(:version_number, :integer, source: :versionNumber)
    field(:name, :string)
    field(:description, :string)
    field(:type, :string)
    field(:model, :map)
    field(:instructions, :string)
    field(:score_range, :map, source: :scoreRange)
    field(:preset_config, :map, source: :presetConfig)
    field(:default_sampling, :map, source: :defaultSampling)
    field(:changed_fields, :map, source: :changedFields)
    field(:change_message, :string, source: :changeMessage)
    field(:created_at, Postiz.PrismaDateTime, source: :createdAt)
    field(:created_at_z, Postiz.PrismaDateTime, autogenerate: true, source: :createdAtZ)
  end
end