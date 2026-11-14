defmodule LangWatch.PromptTagAssignment do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          project_id: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          config: LangWatch.LlmPromptConfig.t() | nil,
          version: LangWatch.LlmPromptConfigVersion.t() | nil,
          prompt_tag: LangWatch.PromptTag.t() | nil,
          created_by: LangWatch.User.t() | nil,
          updated_by: LangWatch.User.t() | nil
        }

  schema "PromptTagAssignment" do
    field(:id, :string, primary_key: true)
    field(:project_id, :string, source: :projectId)
    field(:config_id, :string, source: :configId)
    field(:version_id, :string, source: :versionId)
    field(:tag_id, :string, source: :tagId)
    field(:created_by_id, :string, source: :createdById)
    field(:updated_by_id, :string, source: :updatedById)
    belongs_to(:config, LangWatch.LlmPromptConfig, foreign_key: :config_id, define_field: false, type: :string)
    belongs_to(:version, LangWatch.LlmPromptConfigVersion, foreign_key: :version_id, define_field: false, type: :string)
    belongs_to(:prompt_tag, LangWatch.PromptTag, foreign_key: :tag_id, define_field: false, type: :string)
    belongs_to(:created_by, LangWatch.User, foreign_key: :created_by_id, define_field: false, type: :string)
    belongs_to(:updated_by, LangWatch.User, foreign_key: :updated_by_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end