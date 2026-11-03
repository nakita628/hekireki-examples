defmodule Postiz.Messages do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: Postiz.PrismaDateTime, autogenerate: {Postiz.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          from: atom(),
          content: String.t() | nil,
          special: String.t() | nil,
          deleted_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          group: Postiz.MessagesGroup.t() | nil,
          posts: [Postiz.Post.t()]
        }

  schema "Messages" do
    field(:from, Ecto.Enum, values: [:BUYER, :SELLER])
    field(:content, :string)
    field(:special, :string)
    field(:deleted_at, Postiz.PrismaDateTime, source: :deletedAt)
    field(:group_id, :binary_id, source: :groupId)
    belongs_to(:group, Postiz.MessagesGroup, foreign_key: :group_id, define_field: false)
    has_many(:posts, Postiz.Post, foreign_key: :last_message_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end