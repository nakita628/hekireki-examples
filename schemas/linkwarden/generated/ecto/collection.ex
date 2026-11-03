defmodule Linkwarden.Collection do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: Linkwarden.PrismaDateTime, autogenerate: {Linkwarden.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          name: String.t(),
          description: String.t(),
          icon: String.t() | nil,
          icon_weight: String.t() | nil,
          color: String.t(),
          is_public: boolean(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          parent: Linkwarden.Collection.t() | nil,
          owner: Linkwarden.User.t() | nil,
          created_by: Linkwarden.User.t() | nil,
          sub_collections: [Linkwarden.Collection.t()],
          members: [Linkwarden.UsersAndCollections.t()],
          links: [Linkwarden.Link.t()],
          rss_subscriptions: [Linkwarden.RssSubscription.t()],
          dashboard_section: [Linkwarden.DashboardSection.t()]
        }

  schema "Collection" do
    field(:name, :string)
    field(:description, :string, default: "")
    field(:icon, :string)
    field(:icon_weight, :string, source: :iconWeight)
    field(:color, :string, default: "#0ea5e9")
    field(:is_public, :boolean, default: false, source: :isPublic)
    field(:parent_id, :id, source: :parentId)
    field(:owner_id, :id, source: :ownerId)
    field(:created_by_id, :id, source: :createdById)
    belongs_to(:parent, Linkwarden.Collection, foreign_key: :parent_id, define_field: false)
    belongs_to(:owner, Linkwarden.User, foreign_key: :owner_id, define_field: false)
    belongs_to(:created_by, Linkwarden.User, foreign_key: :created_by_id, define_field: false)
    has_many(:sub_collections, Linkwarden.Collection, foreign_key: :parent_id)
    has_many(:members, Linkwarden.UsersAndCollections, foreign_key: :collection_id)
    has_many(:links, Linkwarden.Link, foreign_key: :collection_id)
    has_many(:rss_subscriptions, Linkwarden.RssSubscription, foreign_key: :collection_id)
    has_many(:dashboard_section, Linkwarden.DashboardSection, foreign_key: :collection_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end