defmodule LangWatch.LlmPromptConfig do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          handle: String.t() | nil,
          name: String.t(),
          deleted_at: DateTime.t() | nil,
          organization_id: String.t(),
          scope: atom(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: LangWatch.Project.t() | nil,
          copied_from: LangWatch.LlmPromptConfig.t() | nil,
          copied_prompts: [LangWatch.LlmPromptConfig.t()],
          versions: [LangWatch.LlmPromptConfigVersion.t()],
          version_tags: [LangWatch.PromptTagAssignment.t()]
        }

  schema "LlmPromptConfig" do
    field(:id, :string, primary_key: true)
    field(:handle, :string)
    field(:name, :string)
    field(:deleted_at, LangWatch.PrismaDateTime, source: :deletedAt)
    field(:organization_id, :string, source: :organizationId)
    field(:scope, Ecto.Enum, values: [:ORGANIZATION, :PROJECT], default: :PROJECT)
    field(:project_id, :string, source: :projectId)
    field(:copied_from_prompt_id, :string, source: :copiedFromPromptId)
    belongs_to(:project, LangWatch.Project, foreign_key: :project_id, define_field: false, type: :string)
    belongs_to(:copied_from, LangWatch.LlmPromptConfig, foreign_key: :copied_from_prompt_id, define_field: false, type: :string)
    has_many(:copied_prompts, LangWatch.LlmPromptConfig, foreign_key: :copied_from_prompt_id, references: :id)
    has_many(:versions, LangWatch.LlmPromptConfigVersion, foreign_key: :config_id, references: :id)
    has_many(:version_tags, LangWatch.PromptTagAssignment, foreign_key: :config_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end