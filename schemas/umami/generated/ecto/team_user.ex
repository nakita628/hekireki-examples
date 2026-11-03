defmodule Umami.TeamUser do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Umami.PrismaDateTime, autogenerate: {Umami.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          role: String.t(),
          inserted_at: DateTime.t() | nil,
          updated_at: DateTime.t() | nil,
          team: Umami.Team.t() | nil,
          user: Umami.User.t() | nil
        }

  schema "team_user" do
    field(:id, :string, primary_key: true, source: :team_user_id)
    field(:role, :string)
    belongs_to(:team, Umami.Team, foreign_key: :team_id, type: :string)
    belongs_to(:user, Umami.User, foreign_key: :user_id, type: :string)
    timestamps(inserted_at_source: :created_at)
  end
end