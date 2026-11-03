defmodule Langfuse.TracingReadinessSettings do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          trace_idle_ms: integer() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: Langfuse.Project.t() | nil,
          created_by_user: Langfuse.User.t() | nil,
          updated_by_user: Langfuse.User.t() | nil
        }

  schema "tracing_readiness_settings" do
    field(:trace_idle_ms, :integer)
    belongs_to(:project, Langfuse.Project, foreign_key: :project_id, type: :string)
    belongs_to(:created_by_user, Langfuse.User, foreign_key: :created_by, type: :string)
    belongs_to(:updated_by_user, Langfuse.User, foreign_key: :updated_by, type: :string)
    timestamps(inserted_at_source: :created_at)
  end
end