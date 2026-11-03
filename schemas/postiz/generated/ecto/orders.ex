defmodule Postiz.Orders do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: Postiz.PrismaDateTime, autogenerate: {Postiz.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          status: atom(),
          capture_id: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          buyer: Postiz.User.t() | nil,
          message_group: Postiz.MessagesGroup.t() | nil,
          seller: Postiz.User.t() | nil,
          orders_items: [Postiz.OrderItems.t()],
          payout_problems: [Postiz.PayoutProblems.t()],
          posts: [Postiz.Post.t()]
        }

  schema "Orders" do
    field(:status, Ecto.Enum, values: [:PENDING, :ACCEPTED, :CANCELED, :COMPLETED])
    field(:capture_id, :string, source: :captureId)
    field(:buyer_id, :binary_id, source: :buyerId)
    field(:message_group_id, :binary_id, source: :messageGroupId)
    field(:seller_id, :binary_id, source: :sellerId)
    belongs_to(:buyer, Postiz.User, foreign_key: :buyer_id, define_field: false)
    belongs_to(:message_group, Postiz.MessagesGroup, foreign_key: :message_group_id, define_field: false)
    belongs_to(:seller, Postiz.User, foreign_key: :seller_id, define_field: false)
    has_many(:orders_items, Postiz.OrderItems, foreign_key: :order_id)
    has_many(:payout_problems, Postiz.PayoutProblems, foreign_key: :order_id)
    has_many(:posts, Postiz.Post, foreign_key: :submitted_for_order_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end