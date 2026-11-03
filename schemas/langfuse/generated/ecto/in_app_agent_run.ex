defmodule Langfuse.InAppAgentRun do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          project_id: String.t(),
          id: String.t(),
          conversation_id: String.t(),
          model: String.t() | nil,
          mcp_api_key_id: String.t() | nil,
          error_code: String.t() | nil,
          error_message: String.t() | nil,
          finished_at: DateTime.t() | nil,
          status: String.t() | nil,
          request: map() | nil,
          claimed_at: DateTime.t() | nil,
          heartbeat_at: DateTime.t() | nil,
          cancel_requested_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: Langfuse.Project.t() | nil,
          triggered_by_user: Langfuse.User.t() | nil
        }

  schema "in_app_agent_runs" do
    field(:id, :string, primary_key: true)
    field(:conversation_id, :string)
    field(:model, :string)
    field(:mcp_api_key_id, :string)
    field(:error_code, :string)
    field(:error_message, :string)
    field(:finished_at, Langfuse.PrismaDateTime)
    field(:status, :string)
    field(:request, :map)
    field(:claimed_at, Langfuse.PrismaDateTime)
    field(:heartbeat_at, Langfuse.PrismaDateTime)
    field(:cancel_requested_at, Langfuse.PrismaDateTime)
    field(:project_id, :string, primary_key: true)
    belongs_to(:project, Langfuse.Project, foreign_key: :project_id, define_field: false, type: :string)
    belongs_to(:triggered_by_user, Langfuse.User, foreign_key: :triggered_by_user_id, type: :string)
    timestamps(inserted_at_source: :created_at)
  end
end