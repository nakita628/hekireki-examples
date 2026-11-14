defmodule Langfuse.SessionView do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          is_pinned_by_default_for_project: boolean(),
          id: String.t(),
          name: String.t(),
          filters: map(),
          view_mode: atom(),
          unmatched_traces: atom(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: Langfuse.Project.t() | nil,
          created_by_user: Langfuse.User.t() | nil
        }

  schema "session_views" do
    field(:is_pinned_by_default_for_project, :boolean, default: false)
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:filters, :map)
    field(:view_mode, Ecto.Enum, values: [:conversational, :list])
    field(:unmatched_traces, Ecto.Enum, values: [:show, :hide])
    belongs_to(:project, Langfuse.Project, foreign_key: :project_id, type: :string)
    belongs_to(:created_by_user, Langfuse.User, foreign_key: :created_by, type: :string)
    timestamps(inserted_at_source: :created_at)
  end
end