defmodule TypeHero.ShortURL do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: TypeHero.PrismaDateTime, autogenerate: {TypeHero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          original_url: String.t(),
          short_url_slug: String.t(),
          expires_at: DateTime.t() | nil,
          clicks: integer(),
          inserted_at: DateTime.t(),
          user: TypeHero.User.t() | nil
        }

  schema "ShortURL" do
    field(:original_url, :string, source: :originalUrl)
    field(:short_url_slug, :string, source: :shortUrlSlug)
    field(:expires_at, TypeHero.PrismaDateTime, source: :expiresAt)
    field(:clicks, :integer, default: 0)
    field(:user_id, :binary_id, source: :userId)
    belongs_to(:user, TypeHero.User, foreign_key: :user_id, define_field: false, type: :binary_id)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end