defmodule Hoppscotch.TeamMember do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          role: atom(),
          user_uid: String.t(),
          team: Hoppscotch.Team.t() | nil
        }

  schema "TeamMember" do
    field(:role, Ecto.Enum, values: [:OWNER, :VIEWER, :EDITOR])
    field(:user_uid, :string, source: :userUid)
    field(:team_id, :string, source: :teamID)
    belongs_to(:team, Hoppscotch.Team, foreign_key: :team_id, define_field: false, type: :string)
  end
end