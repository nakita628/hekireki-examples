defmodule Hoppscotch.UserRequest do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Hoppscotch.PrismaDateTime, autogenerate: {Hoppscotch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          title: String.t(),
          request: map(),
          type: atom(),
          mock_examples: map() | nil,
          order_index: integer(),
          created_on: DateTime.t(),
          updated_at: DateTime.t(),
          user_collection: Hoppscotch.UserCollection.t() | nil,
          user: Hoppscotch.User.t() | nil
        }

  schema "UserRequest" do
    field(:id, :string, primary_key: true)
    field(:title, :string)
    field(:request, :map)
    field(:type, Ecto.Enum, values: [:REST, :GQL])
    field(:mock_examples, :map, source: :mockExamples)
    field(:order_index, :integer, source: :orderIndex)
    field(:created_on, Hoppscotch.PrismaDateTime, autogenerate: true, source: :createdOn)
    field(:collection_id, :string, source: :collectionID)
    field(:user_uid, :string, source: :userUid)
    belongs_to(:user_collection, Hoppscotch.UserCollection, foreign_key: :collection_id, define_field: false, type: :string)
    belongs_to(:user, Hoppscotch.User, foreign_key: :user_uid, define_field: false, type: :string, references: :uid)
    timestamps(inserted_at: false, updated_at_source: :updatedOn)
  end
end