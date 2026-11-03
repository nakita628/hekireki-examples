defmodule Linkwarden.RssSubscription do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: Linkwarden.PrismaDateTime, autogenerate: {Linkwarden.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          url: String.t(),
          name: String.t(),
          last_build_date: DateTime.t() | nil,
          owner_id: integer(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          collection: Linkwarden.Collection.t() | nil
        }

  schema "RssSubscription" do
    field(:url, :string)
    field(:name, :string)
    field(:last_build_date, Linkwarden.PrismaDateTime, source: :lastBuildDate)
    field(:owner_id, :integer, source: :ownerId)
    field(:collection_id, :id, source: :collectionId)
    belongs_to(:collection, Linkwarden.Collection, foreign_key: :collection_id, define_field: false)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end