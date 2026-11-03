defmodule Postiz.Errors do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: Postiz.PrismaDateTime, autogenerate: {Postiz.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          message: String.t(),
          platform: String.t(),
          body: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          organization: Postiz.Organization.t() | nil,
          post: Postiz.Post.t() | nil
        }

  schema "Errors" do
    field(:message, :string)
    field(:platform, :string)
    field(:body, :string, default: "{}")
    field(:organization_id, :binary_id, source: :organizationId)
    field(:post_id, :string, source: :postId)
    belongs_to(:organization, Postiz.Organization, foreign_key: :organization_id, define_field: false)
    belongs_to(:post, Postiz.Post, foreign_key: :post_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end