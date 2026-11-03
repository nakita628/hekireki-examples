defmodule Papermark.TagItem do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Papermark.PrismaDateTime, autogenerate: {Papermark.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          item_type: atom(),
          tagged_by: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          tag: Papermark.Tag.t() | nil,
          link: Papermark.Link.t() | nil,
          document: Papermark.Document.t() | nil,
          dataroom: Papermark.Dataroom.t() | nil
        }

  schema "TagItem" do
    field(:id, :string, primary_key: true)
    field(:item_type, Ecto.Enum, values: [:LINK_TAG, :DOCUMENT_TAG, :DATAROOM_TAG], source: :itemType)
    field(:tagged_by, :string, source: :taggedBy)
    field(:tag_id, :string, source: :tagId)
    field(:link_id, :string, source: :linkId)
    field(:document_id, :string, source: :documentId)
    field(:dataroom_id, :string, source: :dataroomId)
    belongs_to(:tag, Papermark.Tag, foreign_key: :tag_id, define_field: false, type: :string)
    belongs_to(:link, Papermark.Link, foreign_key: :link_id, define_field: false, type: :string)
    belongs_to(:document, Papermark.Document, foreign_key: :document_id, define_field: false, type: :string)
    belongs_to(:dataroom, Papermark.Dataroom, foreign_key: :dataroom_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end