defmodule Dub.Tag do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Dub.PrismaDateTime, autogenerate: {Dub.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          color: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: Dub.Project.t() | nil,
          links: [Dub.LinkTag.t()]
        }

  schema "Tag" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:color, :string, default: "blue")
    field(:project_id, :string, source: :projectId)
    belongs_to(:project, Dub.Project, foreign_key: :project_id, define_field: false, type: :string)
    has_many(:links, Dub.LinkTag, foreign_key: :tag_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end