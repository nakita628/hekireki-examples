defmodule Chibisafe.ShortenedLinks do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: Chibisafe.PrismaDateTime, autogenerate: {Chibisafe.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          uuid: String.t(),
          identifier: String.t(),
          destination: String.t(),
          views: integer(),
          inserted_at: DateTime.t(),
          user: Chibisafe.Users.t() | nil
        }

  schema "shortenedLinks" do
    field(:uuid, :string)
    field(:identifier, :string)
    field(:destination, :string)
    field(:views, :integer, default: 0)
    field(:user_id, :id, source: :userId)
    belongs_to(:user, Chibisafe.Users, foreign_key: :user_id, define_field: false)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end