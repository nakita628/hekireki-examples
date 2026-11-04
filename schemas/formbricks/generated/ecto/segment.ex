defmodule Formbricks.Segment do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Formbricks.PrismaDateTime, autogenerate: {Formbricks.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          title: String.t(),
          description: String.t() | nil,
          is_private: boolean(),
          filters: map(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          workspace: Formbricks.Workspace.t() | nil,
          surveys: [Formbricks.Survey.t()]
        }

  schema "Segment" do
    field(:id, :string, primary_key: true)
    field(:title, :string)
    field(:description, :string)
    field(:is_private, :boolean, default: true, source: :isPrivate)
    field(:filters, :map)
    field(:workspace_id, :string, source: :workspaceId)
    belongs_to(:workspace, Formbricks.Workspace, foreign_key: :workspace_id, define_field: false, type: :string)
    has_many(:surveys, Formbricks.Survey, foreign_key: :segment_id, references: :id)
    timestamps(inserted_at_source: :created_at)
  end
end