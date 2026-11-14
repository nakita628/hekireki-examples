defmodule LangWatch.ScenarioVersion do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          project_id: String.t(),
          version: integer(),
          author_id: String.t() | nil,
          author_label: String.t(),
          change_description: String.t() | nil,
          snapshot: map(),
          schema_version: integer(),
          inserted_at: DateTime.t(),
          scenario: LangWatch.Scenario.t() | nil
        }

  schema "ScenarioVersion" do
    field(:id, :string, primary_key: true)
    field(:project_id, :string, source: :projectId)
    field(:version, :integer)
    field(:author_id, :string, source: :authorId)
    field(:author_label, :string, source: :authorLabel)
    field(:change_description, :string, source: :changeDescription)
    field(:snapshot, :map)
    field(:schema_version, :integer, default: 1, source: :schemaVersion)
    field(:scenario_id, :string, source: :scenarioId)
    belongs_to(:scenario, LangWatch.Scenario, foreign_key: :scenario_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end