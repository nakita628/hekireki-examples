defmodule Trigger.Prompt do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          friendly_id: String.t(),
          slug: String.t(),
          description: String.t() | nil,
          type: String.t(),
          file_path: String.t() | nil,
          export_name: String.t() | nil,
          variable_schema: map() | nil,
          default_model: String.t() | nil,
          default_config: map() | nil,
          tags: [String.t()],
          archived_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          organization: Trigger.Organization.t() | nil,
          project: Trigger.Project.t() | nil,
          runtime_environment: Trigger.RuntimeEnvironment.t() | nil,
          versions: [Trigger.PromptVersion.t()]
        }

  schema "prompts" do
    field(:id, :string, primary_key: true)
    field(:friendly_id, :string)
    field(:slug, :string)
    field(:description, :string)
    field(:type, :string, default: "text")
    field(:file_path, :string, source: :filePath)
    field(:export_name, :string, source: :exportName)
    field(:variable_schema, :map, source: :variableSchema)
    field(:default_model, :string, source: :defaultModel)
    field(:default_config, :map, source: :defaultConfig)
    field(:tags, {:array, :string})
    field(:archived_at, Trigger.PrismaDateTime, source: :archivedAt)
    field(:organization_id, :string, source: :organizationId)
    field(:project_id, :string, source: :projectId)
    field(:runtime_environment_id, :string, source: :runtimeEnvironmentId)
    belongs_to(:organization, Trigger.Organization, foreign_key: :organization_id, define_field: false, type: :string)
    belongs_to(:project, Trigger.Project, foreign_key: :project_id, define_field: false, type: :string)
    belongs_to(:runtime_environment, Trigger.RuntimeEnvironment, foreign_key: :runtime_environment_id, define_field: false, type: :string)
    has_many(:versions, Trigger.PromptVersion, foreign_key: :prompt_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end