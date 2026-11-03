defmodule Langfuse.InAppAgentEvent do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          project_id: String.t(),
          conversation_id: String.t(),
          run_id: String.t(),
          sequence_number: integer(),
          type: String.t(),
          event: map(),
          inserted_at: DateTime.t(),
          project: Langfuse.Project.t() | nil
        }

  schema "in_app_agent_events" do
    field(:conversation_id, :string, primary_key: true)
    field(:run_id, :string)
    field(:sequence_number, :integer, primary_key: true)
    field(:type, :string)
    field(:event, :map)
    field(:project_id, :string, primary_key: true)
    belongs_to(:project, Langfuse.Project, foreign_key: :project_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :created_at, updated_at: false)
  end
end