defmodule Langfuse.PendingDeletion do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          object: String.t(),
          object_id: String.t(),
          is_deleted: boolean(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: Langfuse.Project.t() | nil
        }

  schema "pending_deletions" do
    field(:id, :string, primary_key: true)
    field(:object, :string)
    field(:object_id, :string)
    field(:is_deleted, :boolean, default: false)
    belongs_to(:project, Langfuse.Project, foreign_key: :project_id, type: :string)
    timestamps(inserted_at_source: :created_at)
  end
end