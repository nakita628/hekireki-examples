defmodule LangWatch.WorkflowVersion do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          version: String.t(),
          commit_message: String.t(),
          auto_saved: boolean(),
          dsl: map(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          author: LangWatch.User.t() | nil,
          project: LangWatch.Project.t() | nil,
          workflow: LangWatch.Workflow.t() | nil,
          parent: LangWatch.WorkflowVersion.t() | nil,
          children: [LangWatch.WorkflowVersion.t()],
          workflow_as_latest_version: [LangWatch.Workflow.t()],
          workflow_as_current_version: [LangWatch.Workflow.t()]
        }

  schema "WorkflowVersion" do
    field(:id, :string, primary_key: true)
    field(:version, :string)
    field(:commit_message, :string, source: :commitMessage)
    field(:auto_saved, :boolean, default: false, source: :autoSaved)
    field(:dsl, :map)
    field(:author_id, :string, source: :authorId)
    field(:project_id, :string, source: :projectId)
    field(:workflow_id, :string, source: :workflowId)
    field(:parent_id, :string, source: :parentId)
    belongs_to(:author, LangWatch.User, foreign_key: :author_id, define_field: false, type: :string)
    belongs_to(:project, LangWatch.Project, foreign_key: :project_id, define_field: false, type: :string)
    belongs_to(:workflow, LangWatch.Workflow, foreign_key: :workflow_id, define_field: false, type: :string)
    belongs_to(:parent, LangWatch.WorkflowVersion, foreign_key: :parent_id, define_field: false, type: :string)
    has_many(:children, LangWatch.WorkflowVersion, foreign_key: :parent_id, references: :id)
    has_many(:workflow_as_latest_version, LangWatch.Workflow, foreign_key: :latest_version_id, references: :id)
    has_many(:workflow_as_current_version, LangWatch.Workflow, foreign_key: :current_version_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end