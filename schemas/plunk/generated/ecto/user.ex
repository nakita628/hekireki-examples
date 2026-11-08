defmodule Plunk.User do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: Plunk.PrismaDateTime, autogenerate: {Plunk.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          email: String.t(),
          password: String.t() | nil,
          type: atom(),
          email_verified: boolean(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          memberships: [Plunk.Membership.t()]
        }

  schema "users" do
    field(:email, :string)
    field(:password, :string)
    field(:type, Ecto.Enum, values: [:PASSWORD, :GOOGLE_OAUTH, :GITHUB_OAUTH])
    field(:email_verified, :boolean, default: false, source: :emailVerified)
    has_many(:memberships, Plunk.Membership, foreign_key: :user_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end