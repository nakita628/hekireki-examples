defmodule Linkwarden.Link do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: Linkwarden.PrismaDateTime, autogenerate: {Linkwarden.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          name: String.t(),
          type: String.t(),
          description: String.t(),
          icon: String.t() | nil,
          icon_weight: String.t() | nil,
          color: String.t() | nil,
          url: String.t() | nil,
          text_content: String.t() | nil,
          preview: String.t() | nil,
          image: String.t() | nil,
          pdf: String.t() | nil,
          readable: String.t() | nil,
          monolith: String.t() | nil,
          client_side: boolean(),
          ai_tagged: boolean(),
          meta_description: String.t() | nil,
          index_version: integer() | nil,
          last_preserved: DateTime.t() | nil,
          import_date: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          created_by: Linkwarden.User.t() | nil,
          collection: Linkwarden.Collection.t() | nil,
          highlight: [Linkwarden.Highlight.t()],
          pinned_by: [Linkwarden.User.t()],
          tags: [Linkwarden.Tag.t()]
        }

  schema "Link" do
    field(:name, :string, default: "")
    field(:type, :string, default: "url")
    field(:description, :string, default: "")
    field(:icon, :string)
    field(:icon_weight, :string, source: :iconWeight)
    field(:color, :string)
    field(:url, :string)
    field(:text_content, :string, source: :textContent)
    field(:preview, :string)
    field(:image, :string)
    field(:pdf, :string)
    field(:readable, :string)
    field(:monolith, :string)
    field(:client_side, :boolean, default: false, source: :clientSide)
    field(:ai_tagged, :boolean, default: false, source: :aiTagged)
    field(:meta_description, :string, source: :metaDescription)
    field(:index_version, :integer, source: :indexVersion)
    field(:last_preserved, Linkwarden.PrismaDateTime, source: :lastPreserved)
    field(:import_date, Linkwarden.PrismaDateTime, source: :importDate)
    field(:created_by_id, :id, source: :createdById)
    field(:collection_id, :id, source: :collectionId)
    belongs_to(:created_by, Linkwarden.User, foreign_key: :created_by_id, define_field: false)
    belongs_to(:collection, Linkwarden.Collection, foreign_key: :collection_id, define_field: false)
    has_many(:highlight, Linkwarden.Highlight, foreign_key: :link_id)
    many_to_many(:pinned_by, Linkwarden.User, join_through: "_PinnedLinks", join_keys: [A: :id, B: :id])
    many_to_many(:tags, Linkwarden.Tag, join_through: "_LinkToTag", join_keys: [A: :id, B: :id])
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end