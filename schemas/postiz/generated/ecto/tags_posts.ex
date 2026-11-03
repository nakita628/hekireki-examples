defmodule Postiz.TagsPosts do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Postiz.PrismaDateTime, autogenerate: {Postiz.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          post_id: String.t(),
          tag_id: Ecto.UUID.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          post: Postiz.Post.t() | nil,
          tag: Postiz.Tags.t() | nil
        }

  schema "TagsPosts" do
    field(:post_id, :string, primary_key: true, source: :postId)
    field(:tag_id, :binary_id, primary_key: true, source: :tagId)
    belongs_to(:post, Postiz.Post, foreign_key: :post_id, define_field: false, type: :string)
    belongs_to(:tag, Postiz.Tags, foreign_key: :tag_id, define_field: false, type: :binary_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end