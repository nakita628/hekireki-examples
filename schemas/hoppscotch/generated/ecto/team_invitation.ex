defmodule Hoppscotch.TeamInvitation do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          creator_uid: String.t(),
          invitee_email: String.t(),
          invitee_role: atom(),
          team: Hoppscotch.Team.t() | nil
        }

  schema "TeamInvitation" do
    field(:id, :string, primary_key: true)
    field(:creator_uid, :string, source: :creatorUid)
    field(:invitee_email, :string, source: :inviteeEmail)
    field(:invitee_role, Ecto.Enum, values: [:OWNER, :VIEWER, :EDITOR], source: :inviteeRole)
    field(:team_id, :string, source: :teamID)
    belongs_to(:team, Hoppscotch.Team, foreign_key: :team_id, define_field: false, type: :string)
  end
end