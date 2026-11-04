defmodule Formbricks.WorkflowVersion do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          workflow_id: String.t(),
          workspace_id: String.t(),
          version: integer(),
          definition: map(),
          published_at: DateTime.t(),
          publisher: Formbricks.User.t() | nil,
          runs: [Formbricks.WorkflowRun.t()]
        }

  schema "WorkflowVersion" do
    field(:id, :string, primary_key: true)
    field(:workflow_id, :string, source: :workflowId)
    field(:workspace_id, :string, source: :workspaceId)
    field(:version, :integer)
    field(:definition, :map)
    field(:published_at, Formbricks.PrismaDateTime, autogenerate: true, source: :publishedAt)
    field(:published_by, :string, source: :publishedBy)
    belongs_to(:publisher, Formbricks.User, foreign_key: :published_by, define_field: false, type: :string)
    has_many(:runs, Formbricks.WorkflowRun, foreign_key: :workflow_version_id, references: :id)
  end
end