defmodule Documenso.ApiToken do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: Documenso.PrismaDateTime, autogenerate: {Documenso.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          name: String.t(),
          token: String.t(),
          algorithm: atom(),
          expires: DateTime.t() | nil,
          last_used_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          user: Documenso.User.t() | nil,
          team: Documenso.Team.t() | nil
        }

  schema "ApiToken" do
    field(:name, :string)
    field(:token, :string)
    field(:algorithm, Ecto.Enum, values: [:SHA512], default: :SHA512)
    field(:expires, Documenso.PrismaDateTime)
    field(:last_used_at, Documenso.PrismaDateTime, source: :lastUsedAt)
    field(:user_id, :id, source: :userId)
    field(:team_id, :id, source: :teamId)
    belongs_to(:user, Documenso.User, foreign_key: :user_id, define_field: false)
    belongs_to(:team, Documenso.Team, foreign_key: :team_id, define_field: false)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end