defmodule Documenso.TeamProfile do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          enabled: boolean(),
          bio: String.t() | nil,
          team: Documenso.Team.t() | nil
        }

  schema "TeamProfile" do
    field(:id, :string, primary_key: true)
    field(:enabled, :boolean, default: false)
    field(:bio, :string)
    field(:team_id, :id, source: :teamId)
    belongs_to(:team, Documenso.Team, foreign_key: :team_id, define_field: false)
  end
end