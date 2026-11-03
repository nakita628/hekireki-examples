defmodule Langfuse.TraceSession do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          project_id: String.t(),
          id: String.t(),
          bookmarked: boolean(),
          public: boolean(),
          environment: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: Langfuse.Project.t() | nil
        }

  schema "trace_sessions" do
    field(:id, :string, primary_key: true)
    field(:bookmarked, :boolean, default: false)
    field(:public, :boolean, default: false)
    field(:environment, :string, default: "default")
    field(:project_id, :string, primary_key: true)
    belongs_to(:project, Langfuse.Project, foreign_key: :project_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :created_at)
  end
end