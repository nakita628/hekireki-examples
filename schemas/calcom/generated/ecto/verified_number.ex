defmodule Calcom.VerifiedNumber do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}

  @type t :: %__MODULE__{
          id: integer(),
          phone_number: String.t(),
          user: Calcom.User.t() | nil,
          team: Calcom.Team.t() | nil
        }

  schema "VerifiedNumber" do
    field(:phone_number, :string, source: :phoneNumber)
    field(:user_id, :id, source: :userId)
    field(:team_id, :id, source: :teamId)
    belongs_to(:user, Calcom.User, foreign_key: :user_id, define_field: false)
    belongs_to(:team, Calcom.Team, foreign_key: :team_id, define_field: false)
  end
end