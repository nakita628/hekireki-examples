defmodule Hoppscotch.UserCollection do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Hoppscotch.PrismaDateTime, autogenerate: {Hoppscotch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          title: String.t(),
          type: atom(),
          order_index: integer(),
          data: map() | nil,
          created_on: DateTime.t(),
          updated_at: DateTime.t(),
          parent: Hoppscotch.UserCollection.t() | nil,
          user: Hoppscotch.User.t() | nil,
          children: [Hoppscotch.UserCollection.t()],
          requests: [Hoppscotch.UserRequest.t()]
        }

  schema "UserCollection" do
    field(:id, :string, primary_key: true)
    field(:title, :string)
    field(:type, Ecto.Enum, values: [:REST, :GQL])
    field(:order_index, :integer, source: :orderIndex)
    field(:data, :map)
    field(:created_on, Hoppscotch.PrismaDateTime, autogenerate: true, source: :createdOn)
    field(:parent_id, :string, source: :parentID)
    field(:user_uid, :string, source: :userUid)
    belongs_to(:parent, Hoppscotch.UserCollection, foreign_key: :parent_id, define_field: false, type: :string)
    belongs_to(:user, Hoppscotch.User, foreign_key: :user_uid, define_field: false, type: :string, references: :uid)
    has_many(:children, Hoppscotch.UserCollection, foreign_key: :parent_id, references: :id)
    has_many(:requests, Hoppscotch.UserRequest, foreign_key: :collection_id, references: :id)
    timestamps(inserted_at: false, updated_at_source: :updatedOn)
  end
end