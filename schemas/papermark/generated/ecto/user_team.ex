defmodule Papermark.UserTeam do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          user_id: String.t(),
          team_id: String.t(),
          role: atom(),
          status: String.t(),
          blocked_at: DateTime.t() | nil,
          notification_preferences: map() | nil,
          user: Papermark.User.t() | nil,
          team: Papermark.Team.t() | nil
        }

  schema "UserTeam" do
    field(:role, Ecto.Enum, values: [:ADMIN, :MANAGER, :MEMBER, :DATAROOM_MEMBER], default: :MEMBER)
    field(:status, :string, default: "ACTIVE")
    field(:blocked_at, Papermark.PrismaDateTime, source: :blockedAt)
    field(:notification_preferences, :map, source: :notificationPreferences)
    field(:user_id, :string, primary_key: true, source: :userId)
    field(:team_id, :string, primary_key: true, source: :teamId)
    belongs_to(:user, Papermark.User, foreign_key: :user_id, define_field: false, type: :string)
    belongs_to(:team, Papermark.Team, foreign_key: :team_id, define_field: false, type: :string)
  end
end