defmodule Dub.SentEmail do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Dub.PrismaDateTime, autogenerate: {Dub.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          type: String.t(),
          inserted_at: DateTime.t(),
          project: Dub.Project.t() | nil
        }

  schema "SentEmail" do
    field(:id, :string, primary_key: true)
    field(:type, :string)
    field(:project_id, :string, source: :projectId)
    belongs_to(:project, Dub.Project, foreign_key: :project_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end