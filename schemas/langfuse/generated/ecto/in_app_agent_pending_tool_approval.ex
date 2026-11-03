defmodule Langfuse.InAppAgentPendingToolApproval do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          project_id: String.t(),
          conversation_id: String.t(),
          tool_call_id: String.t(),
          approval_fingerprint: String.t(),
          expires_at: DateTime.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: Langfuse.Project.t() | nil
        }

  schema "in_app_agent_pending_tool_approvals" do
    field(:conversation_id, :string, primary_key: true)
    field(:tool_call_id, :string, primary_key: true)
    field(:approval_fingerprint, :string)
    field(:expires_at, Langfuse.PrismaDateTime)
    field(:project_id, :string, primary_key: true)
    belongs_to(:project, Langfuse.Project, foreign_key: :project_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :created_at)
  end
end