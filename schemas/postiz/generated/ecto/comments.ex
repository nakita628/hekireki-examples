defmodule Postiz.Comments do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: Postiz.PrismaDateTime, autogenerate: {Postiz.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          content: String.t(),
          display_name: String.t() | nil,
          anchor_start: integer() | nil,
          anchor_end: integer() | nil,
          anchor_quote: String.t() | nil,
          resolved_at: DateTime.t() | nil,
          deleted_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          organization: Postiz.Organization.t() | nil,
          post: Postiz.Post.t() | nil,
          user: Postiz.User.t() | nil,
          parent: Postiz.Comments.t() | nil,
          replies: [Postiz.Comments.t()]
        }

  schema "Comments" do
    field(:content, :string)
    field(:display_name, :string, source: :displayName)
    field(:anchor_start, :integer, source: :anchorStart)
    field(:anchor_end, :integer, source: :anchorEnd)
    field(:anchor_quote, :string, source: :anchorQuote)
    field(:resolved_at, Postiz.PrismaDateTime, source: :resolvedAt)
    field(:deleted_at, Postiz.PrismaDateTime, source: :deletedAt)
    field(:organization_id, :binary_id, source: :organizationId)
    field(:post_id, :string, source: :postId)
    field(:user_id, :binary_id, source: :userId)
    field(:parent_id, :binary_id, source: :parentId)
    belongs_to(:organization, Postiz.Organization, foreign_key: :organization_id, define_field: false)
    belongs_to(:post, Postiz.Post, foreign_key: :post_id, define_field: false, type: :string)
    belongs_to(:user, Postiz.User, foreign_key: :user_id, define_field: false)
    belongs_to(:parent, Postiz.Comments, foreign_key: :parent_id, define_field: false)
    has_many(:replies, Postiz.Comments, foreign_key: :parent_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end