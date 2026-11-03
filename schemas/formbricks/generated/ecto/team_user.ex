defmodule Formbricks.TeamUser do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Formbricks.PrismaDateTime, autogenerate: {Formbricks.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          team_id: String.t(),
          user_id: String.t(),
          role: atom(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          team: Formbricks.Team.t() | nil,
          user: Formbricks.User.t() | nil
        }

  schema "TeamUser" do
    field(:role, Ecto.Enum, values: [:admin, :contributor])
    field(:team_id, :string, primary_key: true, source: :teamId)
    field(:user_id, :string, primary_key: true, source: :userId)
    belongs_to(:team, Formbricks.Team, foreign_key: :team_id, define_field: false, type: :string)
    belongs_to(:user, Formbricks.User, foreign_key: :user_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :created_at)
  end
end