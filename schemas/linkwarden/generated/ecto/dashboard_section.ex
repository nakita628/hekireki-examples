defmodule Linkwarden.DashboardSection do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: Linkwarden.PrismaDateTime, autogenerate: {Linkwarden.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          type: atom(),
          order: integer(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: Linkwarden.User.t() | nil,
          collection: Linkwarden.Collection.t() | nil
        }

  schema "DashboardSection" do
    field(:type, Ecto.Enum, values: [:STATS, :RECENT_LINKS, :PINNED_LINKS, :COLLECTION])
    field(:order, :integer)
    field(:user_id, :id, source: :userId)
    field(:collection_id, :id, source: :collectionId)
    belongs_to(:user, Linkwarden.User, foreign_key: :user_id, define_field: false)
    belongs_to(:collection, Linkwarden.Collection, foreign_key: :collection_id, define_field: false)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end