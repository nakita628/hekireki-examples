defmodule Papermark.LinkVisitorGroup do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Papermark.PrismaDateTime, autogenerate: {Papermark.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          inserted_at: DateTime.t(),
          link: Papermark.Link.t() | nil,
          visitor_group: Papermark.VisitorGroup.t() | nil
        }

  schema "LinkVisitorGroup" do
    field(:id, :string, primary_key: true)
    field(:link_id, :string, source: :linkId)
    field(:visitor_group_id, :string, source: :visitorGroupId)
    belongs_to(:link, Papermark.Link, foreign_key: :link_id, define_field: false, type: :string)
    belongs_to(:visitor_group, Papermark.VisitorGroup, foreign_key: :visitor_group_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end