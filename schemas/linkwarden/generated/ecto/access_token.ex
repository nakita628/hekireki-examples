defmodule Linkwarden.AccessToken do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: Linkwarden.PrismaDateTime, autogenerate: {Linkwarden.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          name: String.t(),
          token: String.t(),
          revoked: boolean(),
          is_session: boolean(),
          expires: DateTime.t(),
          last_used_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: Linkwarden.User.t() | nil
        }

  schema "AccessToken" do
    field(:name, :string)
    field(:token, :string)
    field(:revoked, :boolean, default: false)
    field(:is_session, :boolean, default: false, source: :isSession)
    field(:expires, Linkwarden.PrismaDateTime)
    field(:last_used_at, Linkwarden.PrismaDateTime, source: :lastUsedAt)
    field(:user_id, :id, source: :userId)
    belongs_to(:user, Linkwarden.User, foreign_key: :user_id, define_field: false)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end