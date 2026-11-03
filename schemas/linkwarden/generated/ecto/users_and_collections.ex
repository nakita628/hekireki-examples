defmodule Linkwarden.UsersAndCollections do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Linkwarden.PrismaDateTime, autogenerate: {Linkwarden.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          user_id: term(),
          collection_id: term(),
          can_create: boolean(),
          can_update: boolean(),
          can_delete: boolean(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: Linkwarden.User.t() | nil,
          collection: Linkwarden.Collection.t() | nil
        }

  schema "UsersAndCollections" do
    field(:can_create, :boolean, source: :canCreate)
    field(:can_update, :boolean, source: :canUpdate)
    field(:can_delete, :boolean, source: :canDelete)
    field(:user_id, :id, primary_key: true, source: :userId)
    field(:collection_id, :id, primary_key: true, source: :collectionId)
    belongs_to(:user, Linkwarden.User, foreign_key: :user_id, define_field: false)
    belongs_to(:collection, Linkwarden.Collection, foreign_key: :collection_id, define_field: false)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end