defmodule LangWatch.LlmPromptConfigVersion do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          version: integer(),
          commit_message: String.t() | nil,
          config_data: map(),
          schema_version: String.t(),
          runtime_parameters: map(),
          project_id: String.t(),
          inserted_at: DateTime.t(),
          author: LangWatch.User.t() | nil,
          config: LangWatch.LlmPromptConfig.t() | nil,
          version_tags: [LangWatch.PromptTagAssignment.t()]
        }

  schema "LlmPromptConfigVersion" do
    field(:id, :string, primary_key: true)
    field(:version, :integer)
    field(:commit_message, :string, source: :commitMessage)
    field(:config_data, :map, source: :config)
    field(:schema_version, :string, source: :schemaVersion)
    field(:runtime_parameters, :map, default: %{}, source: :runtimeParameters)
    field(:project_id, :string, source: :projectId)
    field(:author_id, :string, source: :authorId)
    field(:config_id, :string, source: :configId)
    belongs_to(:author, LangWatch.User, foreign_key: :author_id, define_field: false, type: :string)
    belongs_to(:config, LangWatch.LlmPromptConfig, foreign_key: :config_id, define_field: false, type: :string)
    has_many(:version_tags, LangWatch.PromptTagAssignment, foreign_key: :version_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end