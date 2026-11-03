defmodule Dub.LinkTag do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Dub.PrismaDateTime, autogenerate: {Dub.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          link: Dub.Link.t() | nil,
          tag: Dub.Tag.t() | nil
        }

  schema "LinkTag" do
    field(:id, :string, primary_key: true)
    field(:link_id, :string, source: :linkId)
    field(:tag_id, :string, source: :tagId)
    belongs_to(:link, Dub.Link, foreign_key: :link_id, define_field: false, type: :string)
    belongs_to(:tag, Dub.Tag, foreign_key: :tag_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end