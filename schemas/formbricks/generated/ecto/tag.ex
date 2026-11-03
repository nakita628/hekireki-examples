defmodule Formbricks.Tag do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Formbricks.PrismaDateTime, autogenerate: {Formbricks.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          workspace: Formbricks.Workspace.t() | nil,
          responses: [Formbricks.TagsOnResponses.t()]
        }

  schema "Tag" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:workspace_id, :string, source: :workspaceId)
    belongs_to(:workspace, Formbricks.Workspace, foreign_key: :workspace_id, define_field: false, type: :string)
    has_many(:responses, Formbricks.TagsOnResponses, foreign_key: :tag_id)
    timestamps(inserted_at_source: :created_at)
  end
end