defmodule Papermark.ViewerGroupMembership do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Papermark.PrismaDateTime, autogenerate: {Papermark.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          viewer: Papermark.Viewer.t() | nil,
          group: Papermark.ViewerGroup.t() | nil
        }

  schema "ViewerGroupMembership" do
    field(:id, :string, primary_key: true)
    field(:viewer_id, :string, source: :viewerId)
    field(:group_id, :string, source: :groupId)
    belongs_to(:viewer, Papermark.Viewer, foreign_key: :viewer_id, define_field: false, type: :string)
    belongs_to(:group, Papermark.ViewerGroup, foreign_key: :group_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end